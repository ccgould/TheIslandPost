using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using TheIslandPostManagerWPF.Helpers;
using TheIslandPostManagerWPF.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheIslandPostManagerWPF.Services;
internal class FileServices : IFileServices
{
    private readonly ApplicationSaveService applicationSaveService;
    private readonly ISnackbarService snackbarService;
    private readonly IFTPService ftpService;
    private readonly IMessageService messageService;
    private  ImageService imageService;
    private FileSystemWatcher _watcher;
    private SynchronizationContext? _syncContext;
    private string _tempFolder;


    public Action OnPendingUpdate { get; set; }

    public FileServices(ApplicationSaveService applicationSaveService, ISnackbarService snackbarService, IFTPService ftpService,IMessageService messageService)
    {
        this.applicationSaveService = applicationSaveService;
        this.snackbarService = snackbarService;
        this.ftpService = ftpService;
        this.messageService = messageService;
    }

    public void Load()
    {
        //var pendingFile = Path.Combine(applicationSaveService.ApplicationSaveData.PendingLocation, "PendingOrders.json");

        //if (File.Exists(pendingFile))
        //{
        //    string path = pendingFile; //applicationSaveService.ApplicationSaveData.PendingLocation; // watch for parent directory
        //    //if (!Directory.Exists(path)) // verify it exists before start
        //    //    return;

        //    _watcher = new FileSystemWatcher(applicationSaveService.ApplicationSaveData.PendingLocation);

        //    _watcher.NotifyFilter = NotifyFilters.Attributes
        //                         | NotifyFilters.CreationTime
        //                         | NotifyFilters.DirectoryName
        //                         | NotifyFilters.FileName
        //                         | NotifyFilters.LastAccess
        //                         | NotifyFilters.LastWrite
        //                         | NotifyFilters.Security
        //                         | NotifyFilters.Size;

        //    _watcher.Changed += OnChanged;
        //    _watcher.Created += OnCreated;
        //    _watcher.Deleted += OnDeleted;
        //    //_watcher.Renamed += OnRenamed;
        //    //_watcher.Error += OnError;

        //    _watcher.NotifyFilter = NotifyFilters.LastWrite;

        //    _watcher.Filter = "*.json";
        //    //_watcher.IncludeSubdirectories = true;
        //    _watcher.EnableRaisingEvents = true;

        //    Console.WriteLine("Press enter to exit.");
        //    Console.ReadLine();

        //    // we assume this ctor is called from the UI thread!
        //    _syncContext = SynchronizationContext.Current;
        //}
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            OnPendingUpdate?.Invoke();
        });

        //_syncContext.Post(o =>
        //{
        //    messageService.ShowDebugMessage("Test", "Updated");
        //}, null);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            OnPendingUpdate?.Invoke();
        });

        //_syncContext.Post(o =>
        //{
        //    messageService.ShowDebugMessage("Test", "Deleted");

        //}, null);
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            OnPendingUpdate?.Invoke();
        });


        //_syncContext.Post(o =>
        //{
        //    messageService.ShowDebugMessage("Test", "Created");

        //}, null);


    }

    public async Task CreateBackup(List<string> files,bool moveToPrinter, Action<int,string> callback)
    {
        string backupDirectory = applicationSaveService.ApplicationSaveData.BackupLocation;

        if (!Directory.Exists(backupDirectory))
        {
            Directory.CreateDirectory(backupDirectory);
        }

        string currentDate = DateTime.Now.ToString("MMMM_dd_yyyy");

        string backupCurDateDir = Path.Combine(backupDirectory, currentDate, "Photos");

        if (!Directory.Exists(backupCurDateDir))
        {
            Directory.CreateDirectory(backupCurDateDir);
        }

        string backupRawDir = Path.Combine(backupDirectory, currentDate, backupCurDateDir, "Raw");

        if (!Directory.Exists(backupRawDir))
        {
            Directory.CreateDirectory(backupRawDir);
        }

        var processFiles = new List<Tuple<string, string>>();

        foreach (var file in files)
        {
            var fileExt = Path.GetExtension(file);
            var fileName = Path.GetFileName(file);
            if (fileExt.Contains("jpg", StringComparison.OrdinalIgnoreCase))
            {
                //CopyToPhotoFolder
                processFiles.Add(new Tuple<string, string>(file, Path.Combine(backupCurDateDir, fileName)));
            }
            else
            {
                //CopyToRawFolder
                processFiles.Add(new Tuple<string, string>(file, Path.Combine(backupRawDir, fileName)));

            }
        }

        await Copy(processFiles,moveToPrinter,callback);


        snackbarService.Show(
                        "Backup Status",
                        "Backup Complete.",
                        ControlAppearance.Info,
                        new SymbolIcon(SymbolRegular.ThumbLike24),
                        TimeSpan.FromSeconds(5));
    }

    public async Task Copy(List<Tuple<string, string>> file,bool moveToPrinter = true, Action<int, string> callback = null)
    {
        try
        {
            await Copier.CopyFiles(file, async (prog, fileName) =>
            {
                var fileNameWEx = Path.GetFileName(fileName);

                if (moveToPrinter)
                {
                    var result = Path.Combine(applicationSaveService.ApplicationSaveData.PrinterLocation, fileNameWEx);

                    if(!Path.Exists(applicationSaveService.ApplicationSaveData.PrinterLocation))
                    {
                        await messageService.ShowErrorMessage("Error",$"Failed to locate printer directory cant copy file: {fileNameWEx}");
                        return;
                    }

                    File.Move(fileName, result, true);

                    //if (applicationSaveService.ApplicationSaveData.ConnectToFTP)
                    //{
                    //    var result = Path.Combine("\\\\WORKSTATION\\4x6", fileNameWEx);
                    //    File.Move(fileName, result, true);
                    //    // File.Copy(source, Path.Combine("\\\\WORKSTATION\\4x6", Path.GetFileName(dest)), true);

                    //    //await ftpService.Connect();

                    //    //var result = $"/{applicationSaveService.ApplicationSaveData.FTPRootFolder}/{fileNameWEx}";

                    //    //await ftpService.UploadFile(fileName, result);
                    //}
                    //else
                    //{
                    //    var result = Path.Combine(applicationSaveService.ApplicationSaveData.PrinterLocation, fileNameWEx);
                    //    File.Move(fileName, result, true);
                    //}
                }
                callback?.Invoke(prog,fileName);
            });
        }
        catch (Exception ex)
        {

            snackbarService.Show(
                "Error",
                ex.Message,
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.ThumbDislike24),
                TimeSpan.FromSeconds(5));
        }
    }
   
    public async Task<bool> CreatePendingOrder(PendingOrder pendingOrder)
    {
        try
        {
            string pendingDir = applicationSaveService.ApplicationSaveData.PendingLocation;

            if (!Directory.Exists(pendingDir))
            {
                Directory.CreateDirectory(pendingDir);
            }
            //var name = string.IsNullOrWhiteSpace(pendingOrder.Name) ? directoryIndex : pendingOrder.Name;


            var path = Path.Combine(pendingDir, pendingOrder.Guid);
            var thumbnail = Path.Combine(path, "thumbnail.jpg");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Create thumbnail
            this.imageService = App.GetService<ImageService>();

            await imageService.CreateThumbnail(pendingOrder.PendingImages[0].ImageUrl, thumbnail);

            foreach (var image in pendingOrder.PendingImages)
            {
                if (File.Exists(image.ImageUrl))
                {
                    var newLocation = Path.Combine(path, image.Name);
                    File.Move(image.ImageUrl, newLocation, true);
                    image.ImageUrl = newLocation;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            await messageService.ShowErrorMessage("Error", ex.Message,ex.StackTrace);
            messageService.ShowSnackBarMessage("Error", "Failed to create pending image/s.", ControlAppearance.Danger,SymbolRegular.ThumbLikeDislike24);
            return false;
        }
    }

    public string CreateOrderDirectory(string name)
    {
        if (!Directory.Exists(_tempFolder))
        {
            Directory.CreateDirectory(_tempFolder);
        }

        var monthDir = Path.Combine(applicationSaveService.ApplicationSaveData.OutputLocation, DateTime.Now.ToString("MMM"));

        var dateDir = Path.Combine(applicationSaveService.ApplicationSaveData.OutputLocation, DateTime.Now.ToString("MMM_dd_yyyy"));

        var personalDir = Path.Combine(monthDir, dateDir, HelperMethods.ReplaceInvalidChars(name));


        if (!Directory.Exists(monthDir))
        {
            Directory.CreateDirectory(monthDir);
        }

        if (!Directory.Exists(personalDir))
        {
            Directory.CreateDirectory(personalDir);
        }

        return personalDir;
    }

    public string GetTempDir() => _tempFolder;
}
