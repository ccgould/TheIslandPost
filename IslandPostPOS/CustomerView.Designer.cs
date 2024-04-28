namespace IslandPostPOS;

partial class CustomerView
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerView));
        label1 = new Label();
        customerID = new Label();
        label2 = new Label();
        nameTxtb = new TextBox();
        numberTxtb = new TextBox();
        label3 = new Label();
        emailTxtb = new TextBox();
        label4 = new Label();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        paymentTypeCmb = new ComboBox();
        totalPhotosNum = new NumericUpDown();
        totalVideosNum = new NumericUpDown();
        totalPaymentNum = new NumericUpDown();
        submitBtn = new Button();
        ((System.ComponentModel.ISupportInitialize)totalPhotosNum).BeginInit();
        ((System.ComponentModel.ISupportInitialize)totalVideosNum).BeginInit();
        ((System.ComponentModel.ISupportInitialize)totalPaymentNum).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        label1.Location = new Point(12, 22);
        label1.Name = "label1";
        label1.Size = new Size(101, 20);
        label1.TabIndex = 0;
        label1.Text = "Customer ID:";
        // 
        // customerID
        // 
        customerID.AutoSize = true;
        customerID.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        customerID.Location = new Point(119, 22);
        customerID.Name = "customerID";
        customerID.Size = new Size(45, 20);
        customerID.TabIndex = 1;
        customerID.Text = "0000";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 30F);
        label2.Location = new Point(63, 80);
        label2.Name = "label2";
        label2.Size = new Size(168, 67);
        label2.TabIndex = 2;
        label2.Text = "NAME";
        // 
        // nameTxtb
        // 
        nameTxtb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        nameTxtb.Font = new Font("Segoe UI", 30F);
        nameTxtb.Location = new Point(247, 73);
        nameTxtb.Name = "nameTxtb";
        nameTxtb.Size = new Size(521, 74);
        nameTxtb.TabIndex = 3;
        // 
        // numberTxtb
        // 
        numberTxtb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numberTxtb.Font = new Font("Segoe UI", 30F);
        numberTxtb.Location = new Point(247, 188);
        numberTxtb.Name = "numberTxtb";
        numberTxtb.Size = new Size(521, 74);
        numberTxtb.TabIndex = 5;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 30F);
        label3.Location = new Point(12, 188);
        label3.Name = "label3";
        label3.Size = new Size(229, 67);
        label3.TabIndex = 4;
        label3.Text = "NUMBER";
        // 
        // emailTxtb
        // 
        emailTxtb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        emailTxtb.Font = new Font("Segoe UI", 30F);
        emailTxtb.Location = new Point(247, 291);
        emailTxtb.Name = "emailTxtb";
        emailTxtb.Size = new Size(521, 74);
        emailTxtb.TabIndex = 7;
        emailTxtb.TextChanged += textBox3_TextChanged;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 30F);
        label4.Location = new Point(63, 291);
        label4.Name = "label4";
        label4.Size = new Size(168, 67);
        label4.TabIndex = 6;
        label4.Text = "EMAIL";
        // 
        // label5
        // 
        label5.Anchor = AnchorStyles.None;
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        label5.Location = new Point(204, 420);
        label5.Name = "label5";
        label5.Size = new Size(112, 20);
        label5.TabIndex = 8;
        label5.Text = "Payment Type:";
        // 
        // label6
        // 
        label6.Anchor = AnchorStyles.None;
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        label6.Location = new Point(215, 451);
        label6.Name = "label6";
        label6.Size = new Size(101, 20);
        label6.TabIndex = 9;
        label6.Text = "Total Photos:";
        // 
        // label7
        // 
        label7.Anchor = AnchorStyles.None;
        label7.AutoSize = true;
        label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        label7.Location = new Point(215, 482);
        label7.Name = "label7";
        label7.Size = new Size(99, 20);
        label7.TabIndex = 10;
        label7.Text = "Total Videos:";
        // 
        // label8
        // 
        label8.Anchor = AnchorStyles.None;
        label8.AutoSize = true;
        label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        label8.Location = new Point(204, 515);
        label8.Name = "label8";
        label8.Size = new Size(114, 20);
        label8.TabIndex = 11;
        label8.Text = "Total Payment:";
        // 
        // paymentTypeCmb
        // 
        paymentTypeCmb.Anchor = AnchorStyles.None;
        paymentTypeCmb.FormattingEnabled = true;
        paymentTypeCmb.Items.AddRange(new object[] { "Card", "Cash", "Both" });
        paymentTypeCmb.Location = new Point(336, 412);
        paymentTypeCmb.Name = "paymentTypeCmb";
        paymentTypeCmb.Size = new Size(151, 28);
        paymentTypeCmb.TabIndex = 12;
        // 
        // totalPhotosNum
        // 
        totalPhotosNum.Anchor = AnchorStyles.None;
        totalPhotosNum.Location = new Point(336, 451);
        totalPhotosNum.Name = "totalPhotosNum";
        totalPhotosNum.Size = new Size(150, 27);
        totalPhotosNum.TabIndex = 13;
        // 
        // totalVideosNum
        // 
        totalVideosNum.Anchor = AnchorStyles.None;
        totalVideosNum.Location = new Point(337, 484);
        totalVideosNum.Name = "totalVideosNum";
        totalVideosNum.Size = new Size(150, 27);
        totalVideosNum.TabIndex = 14;
        // 
        // totalPaymentNum
        // 
        totalPaymentNum.Anchor = AnchorStyles.None;
        totalPaymentNum.Location = new Point(336, 517);
        totalPaymentNum.Name = "totalPaymentNum";
        totalPaymentNum.Size = new Size(150, 27);
        totalPaymentNum.TabIndex = 15;
        // 
        // submitBtn
        // 
        submitBtn.Anchor = AnchorStyles.None;
        submitBtn.Location = new Point(337, 583);
        submitBtn.Name = "submitBtn";
        submitBtn.Size = new Size(94, 29);
        submitBtn.TabIndex = 16;
        submitBtn.Text = "SUBMIT";
        submitBtn.UseVisualStyleBackColor = true;
        submitBtn.Click += submitBtn_Click;
        // 
        // CustomerView
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 638);
        Controls.Add(submitBtn);
        Controls.Add(totalPaymentNum);
        Controls.Add(totalVideosNum);
        Controls.Add(totalPhotosNum);
        Controls.Add(paymentTypeCmb);
        Controls.Add(label8);
        Controls.Add(label7);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(emailTxtb);
        Controls.Add(label4);
        Controls.Add(numberTxtb);
        Controls.Add(label3);
        Controls.Add(nameTxtb);
        Controls.Add(label2);
        Controls.Add(customerID);
        Controls.Add(label1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "CustomerView";
        Text = "Customer Details";
        Load += CustomerView_Load;
        ((System.ComponentModel.ISupportInitialize)totalPhotosNum).EndInit();
        ((System.ComponentModel.ISupportInitialize)totalVideosNum).EndInit();
        ((System.ComponentModel.ISupportInitialize)totalPaymentNum).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label customerID;
    private Label label2;
    private TextBox nameTxtb;
    private TextBox numberTxtb;
    private Label label3;
    private TextBox emailTxtb;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label8;
    private ComboBox paymentTypeCmb;
    private NumericUpDown totalPhotosNum;
    private NumericUpDown totalVideosNum;
    private NumericUpDown totalPaymentNum;
    private Button submitBtn;
}