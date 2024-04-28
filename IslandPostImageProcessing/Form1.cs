using System.Diagnostics;
using System.IO;
using System.Text;

namespace IslandPostImageProcessing;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();


    }

    private void button3_Click(object sender, EventArgs e)
    {

    }

    private void processingDirBrwsBtn_Click(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                processDirTxtBox.Text = fbd.SelectedPath;
                ProjectSettings.Default.InputDirectory = fbd.SelectedPath;
                ProjectSettings.Default.Save();
            }
        }
    }

    private void outputDirBrwsBtn_Click(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                outputDirTxtB.Text = fbd.SelectedPath;
                ProjectSettings.Default.OutputDirectory = fbd.SelectedPath;
                ProjectSettings.Default.Save();
            }
        }
    }

    private void startProcessingBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(processDirTxtBox.Text))
        {
            MessageBox.Show("Please select the processing directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(outputDirTxtB.Text))
        {
            MessageBox.Show("Please select the output directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        ProcessImages();

        //ImageProcessingPage imageProcessing = new ImageProcessingPage();
        //imageProcessing.ShowDialog();
        //this.Show();
    }

    private void ProcessImages()
    {
        try
        {
            string[] files = System.IO.Directory.GetFiles(ProjectSettings.Default.InputDirectory, "*.JPG");

            var processingDile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "images.bin");



            // Check if file already exists. If yes, delete it.
            if (File.Exists(processingDile))
            {
                File.Delete(processingDile);
            }

            // Create a new file
            using (FileStream fs = File.Create(processingDile))
            {
                foreach (string fileName in files)
                {
                    // Add some text to file
                    Byte[] title = new UTF8Encoding(true).GetBytes(fileName);
                    fs.Write(title, 0, title.Length);
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                    fs.Write(newline, 0, newline.Length);
                }
            }


            var batFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "run.bat");

            // Check if file already exists. If yes, delete it.
            if (File.Exists(batFile))
            {
                File.Delete(batFile);
            }



            // Create a new file
            using (FileStream fs = File.Create(batFile))
            {
                // Add some text to file
                Byte[] title = new UTF8Encoding(true).GetBytes($"\"{ProjectSettings.Default.InfranLocation}\" /filelist=\"{processingDile}\" /advancedbatch /convert=\"{Path.Combine(ProjectSettings.Default.OutputDirectory, "*.jpg")}\"");
                fs.Write(title, 0, title.Length);
            }

            SetButtonsInteraction(false);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            var process =  Process.Start(batFile);
            //process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;
            process.Exited += (sender, e) => 
            {
                MessageBox.Show("Process Complete");
                this.Invoke((MethodInvoker)delegate 
                { 
                    SetButtonsInteraction(true);
                    if(checkBox1.Checked)
                    {
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                    }
                });                
            };
        }
        catch
        {
        }
    }

    private void SetButtonsInteraction(bool value)
    {
        if(value)
        {
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }
        else
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
        }


        infranBrwsDirBtn.Enabled = value;
        outputDirBrwsBtn.Enabled = value;
        processingDirBrwsBtn.Enabled = value;
        startProcessingBtn.Enabled = value;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        processDirTxtBox.Text = ProjectSettings.Default.InputDirectory;
        outputDirTxtB.Text = ProjectSettings.Default.OutputDirectory;
    }

    private void infranBrwsDirBtn_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog
        {
            Title = "Locate InfranView",

            DefaultExt = "exe",
            Filter = "Execustable file (*.exe)|*.exe",
            RestoreDirectory = true,
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            ProjectSettings.Default.InfranLocation = openFileDialog1.FileName;
            infranViewTxtb.Text = openFileDialog1.FileName;
            ProjectSettings.Default.Save();
        }
    }
}
