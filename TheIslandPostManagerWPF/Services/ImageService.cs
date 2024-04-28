using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using TheIslandPostManagerWPF.Models;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using Image = System.Drawing.Image;
using System.Drawing;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;
using System.Drawing.Drawing2D;
using Size = System.Drawing.Size;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui;
using System.Windows;
using Point = System.Drawing.Point;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Services;

public partial class ImageService : ObservableObject
{
    [ObservableProperty] ObservableCollection<ImageObj> images = new();

    [ObservableProperty] private ImageObj selectedImage;
    [ObservableProperty] private bool isBusy;
    public Action OnImageCountUpdate;
    private int currentIndex = 0;
    [ObservableProperty] private ObservableCollection<OrderInformation> currentTransaction = new();
    private string _outPutFolder;
    private string _outPutFolder4x6;
    private string _tempFolder;
    private string _watermark;
    private static string _orientationQuery = "System.Photo.Orientation";
    private ISnackbarService _snackbarService;
    private INavigationService _navigationService;
    private ApplicationSaveService _applicationSaveService;

    public ImageService(INavigationService navigationService, ApplicationSaveService applicationSaveService, ISnackbarService snackbarService)
    {
        _snackbarService = snackbarService;
        _navigationService = navigationService;
        _applicationSaveService = applicationSaveService;
        applicationSaveService.Load();
        CurrentTransaction = applicationSaveService.LoadRecords();
        _outPutFolder = applicationSaveService.ApplicationSaveData.OutputLocation;
        _outPutFolder4x6 = applicationSaveService.ApplicationSaveData.PrinterLocation;
        _watermark = applicationSaveService.ApplicationSaveData.WatermarkLocation;
        _tempFolder = applicationSaveService.GetTempFolder();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        //var files = Directory.GetFiles(applicationSaveService.ApplicationSaveData.InputLocation, "*.JPG", SearchOption.TopDirectoryOnly);

        //foreach (var file in files)
        //{
        //    ////Image newImage = new Image();
        //    //BitmapImage src = new BitmapImage();
        //    //src.BeginInit();
        //    //src.UriSource = new Uri(file,UriKind.Absolute);

        //    //src.DecodePixelWidth = 200;
        //    //src.CacheOption = BitmapCacheOption.OnLoad;
        //    //src.EndInit();

        //    ////newImage.Source = src;

        //    ////newImage.Stretch = System.Windows.Media.Stretch.Uniform;
        //    ////newImage.Height = 100;

        //    Images.Add(new ImageObj
        //    {
        //        ImageUrl = file,
        //        Image =  geFile(file),
        //        Name = Path.GetFileName(file),
        //        Width = 300,
        //        Height = 450,
        //    }); ;
        //}        
    }

    internal void AddImage(string file)
    {
        Images.Add(new ImageObj
        {
            ImageUrl = file,
            /*HDImage = LoadImageFile(file, true),*/
            Image = LoadImageFile(file),
            Name = Path.GetFileName(file),
            Width = 300,
            Height = 450,
            Index = Images.Count + 1,
        });
        OnImageCountUpdate?.Invoke();
    }

    internal void UpdateImagesIndex()
    {
        for (int i = 0; i < Images.Count; i++)
        {
            Images[i].Index = i+1;
        }
    }

    internal void ClearImages()
    {
        foreach (var item in Images)
        {
            item.HDImage = null;
            item.ImageUrl = null;
        }
        
        var files = Directory.GetFiles(_applicationSaveService.ApplicationSaveData.InputLocation, "*.JPG", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            File.Delete(file);
        }

        GC.Collect();

        OnImageCountUpdate?.Invoke();
    }

    private CancellationTokenSource tokenSource = null;

    internal async Task ChangeSelectedImage(bool getNextImage)
    {
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();

        if (SelectedImage is not null)
        {
            SelectedImage.HDImage = null;
            GC.Collect();
        }


        if (SelectedImage is not null)
        {
            var index = Images.IndexOf(SelectedImage);

            if (getNextImage)
            {
                if (index + 1 > Images.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                if (index - 1 == -1)
                {
                    index = Images.Count - 1;
                }
                else
                {
                    index--;
                }
            }

            currentIndex = index;

            SelectedImage = Images.ElementAt(index);
            SelectedImage.HDImage = SelectedImage.Image; /*LoadImageFile(SelectedImage.ImageUrl, true);*/
        }
    }


    private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            dispatcherTimer.Stop();
            var image = await LoadHDImage(SelectedImage.ImageUrl);
            SelectedImage.HDImage = image;
        });
    }

    internal async Task<BitmapImage> LoadHDImage(string file)
    {
        return await Task.Run(() =>
        {
            return LoadImageFile(file,true);
        });


    }

    internal void FinishOrder(OrderInformation order)
    {
        CurrentTransaction.Add(order);
        _applicationSaveService.SaveRecords(CurrentTransaction);
        _snackbarService.Show(
                "Exporting Finished",
                "All Images have been exported successfully",
                Wpf.Ui.Controls.ControlAppearance.Info,
                new SymbolIcon(SymbolRegular.Info24),
                TimeSpan.FromSeconds(5));
    }

    public BitmapImage LoadImageFile(String path,bool fullQuality = false)
    {
        //var bitmap = new BitmapImage();
        //var stream = File.OpenRead(path);
        //bitmap.BeginInit();
        //bitmap.CacheOption = BitmapCacheOption.OnLoad;
        //bitmap.StreamSource = stream;
        //bitmap.EndInit();
        //stream.Close();
        //stream.Dispose();

        //return bitmap;



        Rotation rotation = Rotation.Rotate0;

        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            BitmapFrame bitmapFrame = BitmapFrame.Create(fileStream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            BitmapMetadata bitmapMetadata = bitmapFrame.Metadata as BitmapMetadata;

            if ((bitmapMetadata != null) && (bitmapMetadata.ContainsQuery(_orientationQuery)))
            {
                object o = bitmapMetadata.GetQuery(_orientationQuery);

                if (o != null)
                {
                    switch ((ushort)o)
                    {
                        case 6:
                            {
                                rotation = Rotation.Rotate90;
                            }
                            break;
                        case 3:
                            {
                                rotation = Rotation.Rotate180;
                            }
                            break;
                        case 8:
                            {
                                rotation = Rotation.Rotate270;
                            }
                            break;
                    }
                }
            }

            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.UriSource = new Uri(path);
            _image.Rotation = rotation;

            if (!fullQuality)
            {
                _image.DecodePixelWidth = 200;
            }

            _image.StreamSource = fileStream;
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.EndInit();
            _image.Freeze();

            return _image;
        }
    }

    internal async Task<bool> DeleteImage(ImageObj image)
    {
        var uiMessageBox = new MessageBox
        {
            Title = "Delete Image?",
            Content = "Are you sure you would like to remove this images from this collection?",
            IsPrimaryButtonEnabled = true,
            PrimaryButtonText = "Yes",
            CloseButtonText = "No ",

        };

        var result = await uiMessageBox.ShowDialogAsync();

        if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            image.HDImage = null;
            image.Image = null;
            File.Delete(image.ImageUrl);
            Images.Remove(image);
            UpdateImagesIndex();
            GC.Collect();

            OnImageCountUpdate?.Invoke();
            return true;
        }

        return false;
    }

    internal void SelectAllImages()
    {
        foreach (ImageObj image in Images)
        {
            image.IsSelected = true;
        }
    }

    internal void DeSelectAllImages()
    {
        foreach (ImageObj image in Images)
        {
            image.IsSelected = false;
        }
    }

    internal void PrintAllImages()
    {
        foreach (ImageObj image in Images)
        {
            image.IsPrintable = true;
            image.IsSelected = true;

        }
    }

    internal async Task DeleteAllImages()
    {
        var uiMessageBox = new MessageBox
        {
            Title = "Delete Image?",
            Content = "Are you sure you would like to remove all images from this collection?",
            IsPrimaryButtonEnabled = true,
            PrimaryButtonText = "Yes",
            CloseButtonText = "No ",

        };

        var result = await uiMessageBox.ShowDialogAsync();

        if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            for (int i = Images.Count - 1; i >= 0; i--)
            {
                var image = Images[i];
                image.HDImage = null;
                image.Image = null;
                File.Delete(Images[i].ImageUrl);
                Images.Remove(Images[i]);
                UpdateImagesIndex();
            }
            //Images = null;
            GC.Collect();
        }


        OnImageCountUpdate?.Invoke();
    }

    internal Task CompleteOrder(string name, string phoneNumm, string email)
    {
        IsBusy = true;

        return Task.Run(() => 
        {
            var order = new OrderInformation();

            foreach (ImageObj image in Images)
            {
                if (image.IsSelected)
                {
                    order.ApprovedImages.Add(image.Name);
                    _applicationSaveService.AddApprovedImage();

                    if (image.IsPrintable)
                    {
                        order.ApprovedPrints.Add(image.Name, image.PrintAmount);
                        _applicationSaveService.AddApprovedPrints(image.PrintAmount);
                    }

                    if(!Directory.Exists(_tempFolder))
                    {
                        Directory.CreateDirectory(_tempFolder);
                    }

                    AddWaterMark(name,image.ImageUrl, async (outputFile, filename) =>
                    {
                        if (image.IsPrintable)
                        {
                            var files = new List<Tuple<string, string>>();


                            for (int i = 0; i < image.PrintAmount; i++)
                            {
                                var wOutExt = Path.GetFileNameWithoutExtension(filename);
                                var result = $"{wOutExt}_{i}.jpg";
                                files.Add(new Tuple<string, string>(outputFile, Path.Combine(_tempFolder, result)));
                            }

                            //File.Copy(outputFile,Path.Combine(_outPutFolder4x6,filename));
                            await Copy(files);
                        }
                    });
                }
            }

            order.DateTime = DateTime.Now;
            order.Name = name;
            order.Email = email;
            order.PhoneNumber = phoneNumm;
            order.CustomerID = Guid.NewGuid().ToString("D");

            Application.Current.Dispatcher.Invoke(new Action(async () => 
            {
                ClearImages();

                _ = _navigationService.Navigate(typeof(DashboardPage));

                FinishOrder(order);
            }));

            IsBusy = false;
        });
    }

    private string ReplaceInvalidChars(string filename)
    {
        return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
    }

    private async Task Copy(List<Tuple<string,string>> file)
    {
        await Copier.CopyFiles(file, (prog, fileName) =>
        {
            var fileNameWEx = Path.GetFileName(fileName);
            var result = Path.Combine(_outPutFolder4x6, fileNameWEx);
            File.Move(fileName, result,true);
        });
    }

    private void AddWaterMark(string personName,string originalImage,Action<string,string> callback)
    {
        var fileName = Path.GetFileNameWithoutExtension(originalImage);
        var ext = Path.GetExtension(originalImage);
        var newFileName = $"{fileName}_watermarked{ext}";
        var origLocation = Path.GetDirectoryName(originalImage);

        using (Image image = Image.FromFile(originalImage))
        {
            Rotate(image);

            if (_applicationSaveService.ApplicationSaveData.AddWaterMarkToImage)
            {
                using (Image watermarkImage = Image.FromFile(_watermark))
                {
                    var newWatermark = ResizeImage(watermarkImage, 
                        new Size(watermarkImage.Width * 2, watermarkImage.Height * 2));

                    using (Graphics imageGraphics = Graphics.FromImage(image))
                    using (TextureBrush watermarkBrush = new TextureBrush(newWatermark))
                    {
                        int x = 0;
                        int y = 0;

                        switch(_applicationSaveService.ApplicationSaveData.WatermarkPosition)
                        {
                            case 0:
                                x = _applicationSaveService.ApplicationSaveData.ImageWidth;
                                y = _applicationSaveService.ApplicationSaveData.ImageHeight;
                                break;
                            case 1:
                                x = (image.Width - newWatermark.Width - _applicationSaveService.ApplicationSaveData.ImageWidth);
                                y = _applicationSaveService.ApplicationSaveData.ImageHeight;
                                break;
                            case 2:
                                x = (image.Width - newWatermark.Width) / 2;
                                y = (image.Height - newWatermark.Height) / 2;
                                break;
                            case 3:
                                x = _applicationSaveService.ApplicationSaveData.ImageWidth;
                                y = (image.Height - newWatermark.Height - _applicationSaveService.ApplicationSaveData.ImageHeight);
                                break;
                            case 4:
                                x = (image.Width - newWatermark.Width - _applicationSaveService.ApplicationSaveData.ImageWidth);
                                y = (image.Height - newWatermark.Height - _applicationSaveService.ApplicationSaveData.ImageHeight);
                                break;
                        }

                        watermarkBrush.TranslateTransform(x, y);
                        imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(newWatermark.Width + 1, newWatermark.Height)));      
                    }
                }
            }

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, _applicationSaveService.ApplicationSaveData.ImageQuality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            var personalDir = Path.Combine(_outPutFolder, ReplaceInvalidChars(personName));

            if (!Directory.Exists(personalDir))
            {
                Directory.CreateDirectory(personalDir);
            }

            var newFileLocation = Path.Combine(personalDir, newFileName);
            image.Save(newFileLocation, jpegCodec, encoderParams);
            callback?.Invoke(newFileLocation, newFileName);                     
        }
    }

    private const int bytesPerPixel = 4;

    /// <summary>
    /// Change the opacity of an image
    /// </summary>
    /// <param name="originalImage">The original image</param>
    /// <param name="opacity">Opacity, where 1.0 is no opacity, 0.0 is full transparency</param>
    /// <returns>The changed image</returns>
    public static System.Drawing.Bitmap ChangeImageOpacity(System.Drawing.Bitmap bmp, double opacity)
    {
        // Specify a pixel format.
        PixelFormat pxf = PixelFormat.Format32bppArgb;

        // Lock the bitmap's bits.
        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        // This code is specific to a bitmap with 32 bits per pixels 
        // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
        int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
        byte[] argbValues = new byte[numBytes];

        // Copy the ARGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

        // Manipulate the bitmap, such as changing the
        // RGB values for all pixels in the the bitmap.
        for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
        {
            // argbValues is in format BGRA (Blue, Green, Red, Alpha)

            // If 100% transparent, skip pixel
            if (argbValues[counter + bytesPerPixel - 1] == 0)
                continue;

            int pos = 0;
            pos++; // B value
            pos++; // G value
            pos++; // R value

            argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
        }

        // Copy the ARGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

        // Unlock the bits.
        bmp.UnlockBits(bmpData);

        return bmp;
    }

    /// <summary> 
    /// Returns the image codec with the given mime type 
    /// </summary> 
    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats 
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec 
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];

        return null;
    }

    private void Rotate(Image img)
    {
        if (Array.IndexOf(img.PropertyIdList, 274) > -1)
        {
            var orientation = (int)img.GetPropertyItem(274).Value[0];
            switch (orientation)
            {
                case 1:
                    // No rotation required.
                    break;
                case 2:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    img.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    img.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    img.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            // This EXIF data is now invalid and should be removed.
            img.RemovePropertyItem(274);
        }
    }

    private static Image ResizeImage(Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)size.Width / (float)sourceWidth);
        nPercentH = ((float)size.Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);
        //b = ChangeImageOpacity(b, 0.1);

        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (Image)b;
    }

    internal void FinalizeOrder(OrderInformation order)
    {
        var curOrder = CurrentTransaction.FirstOrDefault(x => x.CustomerID.Equals(order.CustomerID));
        if(curOrder is not null)
        {
            curOrder.IsFinalized = true;
            curOrder.DownloadURL = order.DownloadURL;
        }
    }
}