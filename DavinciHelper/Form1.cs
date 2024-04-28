using System.ComponentModel;

namespace DavinciHelper;

public partial class Form1 : Form
{

    BackgroundWorker backgroundWorker = new BackgroundWorker();
    Queue<Tuple<string,string>> fileQueue = new Queue<Tuple<string,string>>();

    public Form1()
    {
        InitializeComponent();
        backgroundWorker.WorkerSupportsCancellation = true;
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        backgroundWorker.DoWork += BackgroundWorker_DoWork;
        backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
    }

    private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        //progressBar1.Value = 0;
        //progressBarLbl.Text = "0%";

        if (fileQueue.Count > 0)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }

    private void CopyFIle(string source, string des)
    {
        FileStream fsOut = new FileStream(des, FileMode.Create);
        FileStream fsIn = new FileStream(source, FileMode.Open);
        byte[] bt = new byte[1048756];
        int readByte;

        while((readByte = fsIn.Read(bt, 0 , bt.Length)) > 0)
        {
            fsOut.Write(bt,0, readByte);
            backgroundWorker.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length));
        }

        fsIn.Close();
        fsOut.Close();
    }

    private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        if (fileQueue.Count <= 0) return;
        var file = fileQueue.Dequeue();
        CopyFIle(file.Item1, file.Item2);
    }

    private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        progressBar1.Value = e.ProgressPercentage;
        progressBarLbl.Text = progressBar1.Value.ToString() + "%";
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void panel1_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Copy;
    }

    private void panel1_DragDrop(object sender, DragEventArgs e)
    {
        var photos = new List<string>();
        var videos = new List<string>();


        var data = e.Data.GetData(DataFormats.FileDrop);

        if (data is not null)
        {
            foreach (var file in data as string[])
            {
                var extension = Path.GetExtension(file);
                
                if (extension.Equals(".JPG", StringComparison.OrdinalIgnoreCase))
                {
                    photos.Add(file);
                }

                if (extension.Equals(".MOV", StringComparison.OrdinalIgnoreCase))
                {
                    videos.Add(file);
                }
            }


            if (photos.Count > 5)
            {
                for (int i = photos.Count - 1; i >= 5; i--)
                {
                    photos.Remove(photos[i]);
                }
            }

            if (videos.Count > 1)
            {
                for (int i = videos.Count - 1; i >= 1; i--)
                {
                    videos.Remove(videos[i]);
                }
            }



            CopyFiles(photos, videos);     
            
            backgroundWorker.RunWorkerAsync();
        }

    }

    private void CopyFiles(List<string> photos, List<string> videos)
    {
        try
        {
            var path = fileLocationTxtB.Text;
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Please select a location for the processed files.");
                return;
            }

            int fileIndex = 0;

            for (int i = 0; i < photos.Count; i++)
            {
                string photo = photos[i];

                if (File.Exists(photo))
                {
                    var fileName = $"{fileIndex++.ToString("D2")}.JPG";
                    var newFile = Path.Combine(fileLocationTxtB.Text, fileName);

                    fileQueue.Enqueue(new Tuple<string,string>(photo, newFile));

                    //File.Copy(photo, newFile);
                }
                else
                {
                    MessageBox.Show($"File {photo} doesn't exist and cannot be processed", "Error File doesnt exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            for (int i = 0; i < videos.Count; i++)
            {
                string video = videos[i];

                if (File.Exists(video))
                {
                    var fileName = "MainVideo.MOV";
                    var newFile = Path.Combine(fileLocationTxtB.Text, fileName);

                    fileQueue.Enqueue(new Tuple<string, string>(video, newFile));

                    //File.Copy(photo, newFile);
                }
                else
                {
                    MessageBox.Show($"File {video} doesn't exist and cannot be processed", "Error File doesnt exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        catch (Exception ex)
        {
            var message = $"{ex.Message} \n {ex.StackTrace}";
            MessageBox.Show(message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw;
        }
    }

    private void browseBtn_Click(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                fileLocationTxtB.Text = fbd.SelectedPath;
            }
        }
    }
}
