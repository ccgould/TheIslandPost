namespace IslandPostPOS;

partial class TipForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new Label();
        label2 = new Label();
        numericUpDown1 = new NumericUpDown();
        okBtn = new Button();
        noBtn = new Button();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 31);
        label1.Name = "label1";
        label1.Size = new Size(153, 20);
        label1.TabIndex = 0;
        label1.Text = "Did you recieve a tip?";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 77);
        label2.Name = "label2";
        label2.Size = new Size(62, 20);
        label2.TabIndex = 1;
        label2.Text = "Amount";
        // 
        // numericUpDown1
        // 
        numericUpDown1.DecimalPlaces = 2;
        numericUpDown1.Location = new Point(80, 75);
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size(150, 27);
        numericUpDown1.TabIndex = 2;
        // 
        // okBtn
        // 
        okBtn.Location = new Point(12, 117);
        okBtn.Name = "okBtn";
        okBtn.Size = new Size(94, 29);
        okBtn.TabIndex = 3;
        okBtn.Text = "Ok";
        okBtn.UseVisualStyleBackColor = true;
        okBtn.Click += button1_Click;
        // 
        // noBtn
        // 
        noBtn.Location = new Point(136, 117);
        noBtn.Name = "noBtn";
        noBtn.Size = new Size(94, 29);
        noBtn.TabIndex = 4;
        noBtn.Text = "No";
        noBtn.UseVisualStyleBackColor = true;
        noBtn.Click += noBtn_Click;
        // 
        // TipForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(258, 188);
        Controls.Add(noBtn);
        Controls.Add(okBtn);
        Controls.Add(numericUpDown1);
        Controls.Add(label2);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "TipForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Tip";
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private NumericUpDown numericUpDown1;
    private Button okBtn;
    private Button noBtn;
}