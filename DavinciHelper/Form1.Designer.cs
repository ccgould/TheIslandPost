namespace DavinciHelper;

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
        browseBtn = new Button();
        label1 = new Label();
        fileLocationTxtB = new TextBox();
        panel1 = new Panel();
        label2 = new Label();
        progressBar1 = new ProgressBar();
        progressBarLbl = new Label();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // browseBtn
        // 
        browseBtn.Location = new Point(337, 12);
        browseBtn.Name = "browseBtn";
        browseBtn.Size = new Size(94, 29);
        browseBtn.TabIndex = 0;
        browseBtn.Text = "Browse ...";
        browseBtn.UseVisualStyleBackColor = true;
        browseBtn.Click += browseBtn_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 11);
        label1.Name = "label1";
        label1.Size = new Size(69, 20);
        label1.TabIndex = 1;
        label1.Text = "Location:";
        // 
        // fileLocationTxtB
        // 
        fileLocationTxtB.Location = new Point(87, 11);
        fileLocationTxtB.Name = "fileLocationTxtB";
        fileLocationTxtB.ReadOnly = true;
        fileLocationTxtB.Size = new Size(244, 27);
        fileLocationTxtB.TabIndex = 2;
        // 
        // panel1
        // 
        panel1.AllowDrop = true;
        panel1.Controls.Add(label2);
        panel1.Location = new Point(20, 62);
        panel1.Name = "panel1";
        panel1.Size = new Size(414, 335);
        panel1.TabIndex = 3;
        panel1.DragDrop += panel1_DragDrop;
        panel1.DragEnter += panel1_DragEnter;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label2.Location = new Point(67, 157);
        label2.Name = "label2";
        label2.Size = new Size(266, 60);
        label2.TabIndex = 0;
        label2.Text = "Drag n Drop";
        label2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(31, 403);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(354, 29);
        progressBar1.TabIndex = 4;
        // 
        // progressBarLbl
        // 
        progressBarLbl.AutoSize = true;
        progressBarLbl.Location = new Point(391, 408);
        progressBarLbl.Name = "progressBarLbl";
        progressBarLbl.Size = new Size(45, 20);
        progressBarLbl.TabIndex = 5;
        progressBarLbl.Text = "100%";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(446, 450);
        Controls.Add(progressBarLbl);
        Controls.Add(progressBar1);
        Controls.Add(panel1);
        Controls.Add(fileLocationTxtB);
        Controls.Add(label1);
        Controls.Add(browseBtn);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button browseBtn;
    private Label label1;
    private TextBox fileLocationTxtB;
    private Panel panel1;
    private Label label2;
    private ProgressBar progressBar1;
    private Label progressBarLbl;
}
