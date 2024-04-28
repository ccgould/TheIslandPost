namespace IslandPostPOS;

partial class SettingsPage
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new Label();
        textBox1 = new TextBox();
        button1 = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 18);
        label1.Name = "label1";
        label1.Size = new Size(66, 20);
        label1.TabIndex = 0;
        label1.Text = "Location";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(239, 3);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(125, 27);
        textBox1.TabIndex = 1;
        // 
        // button1
        // 
        button1.Location = new Point(371, 58);
        button1.Name = "button1";
        button1.Size = new Size(94, 29);
        button1.TabIndex = 2;
        button1.Text = "button1";
        button1.UseVisualStyleBackColor = true;
        // 
        // SettingsPage
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(button1);
        Controls.Add(textBox1);
        Controls.Add(label1);
        Name = "SettingsPage";
        Size = new Size(666, 466);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox textBox1;
    private Button button1;
}
