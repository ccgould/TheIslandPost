namespace IslandPostImageProcessing;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        pictureBox1 = new PictureBox();
        label1 = new Label();
        processingDirBrwsBtn = new Button();
        outputDirBrwsBtn = new Button();
        label2 = new Label();
        startProcessingBtn = new Button();
        processDirTxtBox = new TextBox();
        outputDirTxtB = new TextBox();
        infranViewTxtb = new TextBox();
        infranBrwsDirBtn = new Button();
        label3 = new Label();
        checkBox1 = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Anchor = AnchorStyles.None;
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(187, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(100, 100);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left;
        label1.AutoSize = true;
        label1.Location = new Point(14, 140);
        label1.Name = "label1";
        label1.Size = new Size(191, 20);
        label1.TabIndex = 2;
        label1.Text = "Select Processing Directory:";
        // 
        // processingDirBrwsBtn
        // 
        processingDirBrwsBtn.Anchor = AnchorStyles.Right;
        processingDirBrwsBtn.Location = new Point(368, 165);
        processingDirBrwsBtn.Name = "processingDirBrwsBtn";
        processingDirBrwsBtn.Size = new Size(94, 29);
        processingDirBrwsBtn.TabIndex = 3;
        processingDirBrwsBtn.Text = "Browse";
        processingDirBrwsBtn.UseVisualStyleBackColor = true;
        processingDirBrwsBtn.Click += processingDirBrwsBtn_Click;
        // 
        // outputDirBrwsBtn
        // 
        outputDirBrwsBtn.Anchor = AnchorStyles.Right;
        outputDirBrwsBtn.Location = new Point(368, 235);
        outputDirBrwsBtn.Name = "outputDirBrwsBtn";
        outputDirBrwsBtn.Size = new Size(94, 29);
        outputDirBrwsBtn.TabIndex = 6;
        outputDirBrwsBtn.Text = "Browse";
        outputDirBrwsBtn.UseVisualStyleBackColor = true;
        outputDirBrwsBtn.Click += outputDirBrwsBtn_Click;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Left;
        label2.AutoSize = true;
        label2.Location = new Point(13, 210);
        label2.Name = "label2";
        label2.Size = new Size(167, 20);
        label2.TabIndex = 5;
        label2.Text = "Select Output Directory:";
        // 
        // startProcessingBtn
        // 
        startProcessingBtn.Anchor = AnchorStyles.Bottom;
        startProcessingBtn.Location = new Point(137, 408);
        startProcessingBtn.Name = "startProcessingBtn";
        startProcessingBtn.Size = new Size(187, 29);
        startProcessingBtn.TabIndex = 7;
        startProcessingBtn.Text = "Start Processing";
        startProcessingBtn.UseVisualStyleBackColor = true;
        startProcessingBtn.Click += startProcessingBtn_Click;
        // 
        // processDirTxtBox
        // 
        processDirTxtBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        processDirTxtBox.Location = new Point(14, 163);
        processDirTxtBox.Name = "processDirTxtBox";
        processDirTxtBox.ReadOnly = true;
        processDirTxtBox.Size = new Size(348, 27);
        processDirTxtBox.TabIndex = 8;
        // 
        // outputDirTxtB
        // 
        outputDirTxtB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        outputDirTxtB.Location = new Point(14, 235);
        outputDirTxtB.Name = "outputDirTxtB";
        outputDirTxtB.ReadOnly = true;
        outputDirTxtB.Size = new Size(348, 27);
        outputDirTxtB.TabIndex = 9;
        // 
        // infranViewTxtb
        // 
        infranViewTxtb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        infranViewTxtb.Location = new Point(14, 310);
        infranViewTxtb.Name = "infranViewTxtb";
        infranViewTxtb.ReadOnly = true;
        infranViewTxtb.Size = new Size(348, 27);
        infranViewTxtb.TabIndex = 12;
        // 
        // infranBrwsDirBtn
        // 
        infranBrwsDirBtn.Anchor = AnchorStyles.Right;
        infranBrwsDirBtn.Location = new Point(368, 310);
        infranBrwsDirBtn.Name = "infranBrwsDirBtn";
        infranBrwsDirBtn.Size = new Size(94, 29);
        infranBrwsDirBtn.TabIndex = 11;
        infranBrwsDirBtn.Text = "Browse";
        infranBrwsDirBtn.UseVisualStyleBackColor = true;
        infranBrwsDirBtn.Click += infranBrwsDirBtn_Click;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Left;
        label3.AutoSize = true;
        label3.Location = new Point(13, 285);
        label3.Name = "label3";
        label3.Size = new Size(159, 20);
        label3.TabIndex = 10;
        label3.Text = "Select Infran Directory:";
        // 
        // checkBox1
        // 
        checkBox1.Anchor = AnchorStyles.Left;
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point(14, 355);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(219, 24);
        checkBox1.TabIndex = 13;
        checkBox1.Text = "Delete Files After Processing";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(483, 449);
        Controls.Add(checkBox1);
        Controls.Add(infranViewTxtb);
        Controls.Add(infranBrwsDirBtn);
        Controls.Add(label3);
        Controls.Add(outputDirTxtB);
        Controls.Add(processDirTxtBox);
        Controls.Add(startProcessingBtn);
        Controls.Add(outputDirBrwsBtn);
        Controls.Add(label2);
        Controls.Add(processingDirBrwsBtn);
        Controls.Add(label1);
        Controls.Add(pictureBox1);
        MinimumSize = new Size(501, 496);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private ComboBox comboBox1;
    private Label label1;
    private Button processingDirBrwsBtn;
    private Button outputDirBrwsBtn;
    private Label label2;
    private ComboBox comboBox2;
    private Button startProcessingBtn;
    private TextBox processDirTxtBox;
    private TextBox outputDirTxtB;
    private TextBox infranViewTxtb;
    private Button infranBrwsDirBtn;
    private Label label3;
    private CheckBox checkBox1;
}
