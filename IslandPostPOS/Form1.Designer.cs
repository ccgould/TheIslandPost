namespace IslandPostPOS;

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
        panel1 = new Panel();
        customerViewBtn = new Button();
        loadBtn = new Button();
        exportBtn = new Button();
        versionLbl = new Label();
        pictureBox1 = new PictureBox();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        statsLbl = new Label();
        currentPurchaseDatagrid = new DataGridView();
        addPhotoBtn = new Button();
        addFourForThreeBtn = new Button();
        twentyDealBtn = new Button();
        nameTxtb = new TextBox();
        statsPanel = new Panel();
        totalCashTransLbl = new Label();
        totalCardTransLbl = new Label();
        totalVideosLbl = new Label();
        totalPhotosLbl = new Label();
        cashRadBTN = new RadioButton();
        cardRadBTN = new RadioButton();
        groupBox1 = new GroupBox();
        cardCashRad = new RadioButton();
        backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        groupBox2 = new GroupBox();
        groupBox3 = new GroupBox();
        purchaseHistoryDataGrid = new Syncfusion.WinForms.DataGrid.SfDataGrid();
        completeTransBtn = new Button();
        emailTxtb = new TextBox();
        addVideoBtn = new Button();
        splitContainer1 = new SplitContainer();
        phoneNumTxtb = new TextBox();
        totalLbl = new Label();
        showStatsBtn = new Button();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)currentPurchaseDatagrid).BeginInit();
        statsPanel.SuspendLayout();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)purchaseHistoryDataGrid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        panel1.BackColor = SystemColors.ControlDarkDark;
        panel1.Controls.Add(customerViewBtn);
        panel1.Controls.Add(loadBtn);
        panel1.Controls.Add(exportBtn);
        panel1.Controls.Add(versionLbl);
        panel1.Controls.Add(pictureBox1);
        panel1.Location = new Point(-1, -5);
        panel1.Name = "panel1";
        panel1.Size = new Size(177, 643);
        panel1.TabIndex = 1;
        // 
        // customerViewBtn
        // 
        customerViewBtn.Location = new Point(27, 307);
        customerViewBtn.Name = "customerViewBtn";
        customerViewBtn.Size = new Size(122, 49);
        customerViewBtn.TabIndex = 4;
        customerViewBtn.Text = "Open Customer View";
        customerViewBtn.UseVisualStyleBackColor = true;
        customerViewBtn.Click += customerViewBtn_Click;
        // 
        // loadBtn
        // 
        loadBtn.Location = new Point(24, 180);
        loadBtn.Name = "loadBtn";
        loadBtn.Size = new Size(122, 29);
        loadBtn.TabIndex = 3;
        loadBtn.Text = "Load Data";
        loadBtn.UseVisualStyleBackColor = true;
        loadBtn.Click += loadBtn_Click;
        // 
        // exportBtn
        // 
        exportBtn.Location = new Point(24, 225);
        exportBtn.Name = "exportBtn";
        exportBtn.Size = new Size(122, 29);
        exportBtn.TabIndex = 2;
        exportBtn.Text = "Export Data";
        exportBtn.UseVisualStyleBackColor = true;
        exportBtn.Click += exportBtn_Click;
        // 
        // versionLbl
        // 
        versionLbl.Anchor = AnchorStyles.Bottom;
        versionLbl.AutoSize = true;
        versionLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        versionLbl.ForeColor = SystemColors.ControlLightLight;
        versionLbl.Location = new Point(24, 611);
        versionLbl.Name = "versionLbl";
        versionLbl.Size = new Size(109, 20);
        versionLbl.TabIndex = 1;
        versionLbl.Text = "Version : ####";
        // 
        // pictureBox1
        // 
        pictureBox1.Image = Properties.Resources.the_island_post_w;
        pictureBox1.Location = new Point(33, 17);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(100, 100);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(197, 19);
        label1.Name = "label1";
        label1.Size = new Size(52, 20);
        label1.TabIndex = 2;
        label1.Text = "Name:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(197, 54);
        label2.Name = "label2";
        label2.Size = new Size(111, 20);
        label2.TabIndex = 3;
        label2.Text = "Phone Number:";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(197, 92);
        label3.Name = "label3";
        label3.Size = new Size(49, 20);
        label3.TabIndex = 4;
        label3.Text = "Email:";
        // 
        // statsLbl
        // 
        statsLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        statsLbl.AutoSize = true;
        statsLbl.Location = new Point(697, 6);
        statsLbl.Name = "statsLbl";
        statsLbl.Size = new Size(44, 20);
        statsLbl.TabIndex = 5;
        statsLbl.Text = "Stats:";
        statsLbl.Visible = false;
        // 
        // currentPurchaseDatagrid
        // 
        currentPurchaseDatagrid.BackgroundColor = SystemColors.ButtonHighlight;
        currentPurchaseDatagrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        currentPurchaseDatagrid.Dock = DockStyle.Fill;
        currentPurchaseDatagrid.Location = new Point(3, 23);
        currentPurchaseDatagrid.Name = "currentPurchaseDatagrid";
        currentPurchaseDatagrid.RowHeadersWidth = 51;
        currentPurchaseDatagrid.Size = new Size(299, 358);
        currentPurchaseDatagrid.TabIndex = 6;
        currentPurchaseDatagrid.CellClick += currentPurchaseDatagrid_CellClick;
        // 
        // addPhotoBtn
        // 
        addPhotoBtn.Location = new Point(453, 150);
        addPhotoBtn.Name = "addPhotoBtn";
        addPhotoBtn.Size = new Size(100, 89);
        addPhotoBtn.TabIndex = 8;
        addPhotoBtn.Text = "Add Photo";
        addPhotoBtn.UseVisualStyleBackColor = true;
        addPhotoBtn.Click += addPhotoBtn_Click;
        // 
        // addFourForThreeBtn
        // 
        addFourForThreeBtn.Location = new Point(559, 150);
        addFourForThreeBtn.Name = "addFourForThreeBtn";
        addFourForThreeBtn.Size = new Size(100, 89);
        addFourForThreeBtn.TabIndex = 9;
        addFourForThreeBtn.Text = "Add 4 for 3";
        addFourForThreeBtn.UseVisualStyleBackColor = true;
        addFourForThreeBtn.Click += addFourForThreeBtn_Click;
        // 
        // twentyDealBtn
        // 
        twentyDealBtn.Location = new Point(665, 150);
        twentyDealBtn.Name = "twentyDealBtn";
        twentyDealBtn.Size = new Size(100, 89);
        twentyDealBtn.TabIndex = 10;
        twentyDealBtn.Text = "Add $20 Deal";
        twentyDealBtn.UseVisualStyleBackColor = true;
        twentyDealBtn.Click += twentyDealBtn_Click;
        // 
        // nameTxtb
        // 
        nameTxtb.Location = new Point(255, 16);
        nameTxtb.Name = "nameTxtb";
        nameTxtb.Size = new Size(362, 27);
        nameTxtb.TabIndex = 12;
        // 
        // statsPanel
        // 
        statsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        statsPanel.BackColor = SystemColors.ControlDarkDark;
        statsPanel.Controls.Add(totalCashTransLbl);
        statsPanel.Controls.Add(totalCardTransLbl);
        statsPanel.Controls.Add(totalVideosLbl);
        statsPanel.Controls.Add(totalPhotosLbl);
        statsPanel.Location = new Point(694, 29);
        statsPanel.Name = "statsPanel";
        statsPanel.Size = new Size(257, 113);
        statsPanel.TabIndex = 15;
        statsPanel.Visible = false;
        // 
        // totalCashTransLbl
        // 
        totalCashTransLbl.AutoSize = true;
        totalCashTransLbl.ForeColor = SystemColors.Control;
        totalCashTransLbl.Location = new Point(3, 68);
        totalCashTransLbl.Name = "totalCashTransLbl";
        totalCashTransLbl.Size = new Size(118, 20);
        totalCashTransLbl.TabIndex = 19;
        totalCashTransLbl.Text = "Total Cash Trans:";
        totalCashTransLbl.Click += totalCashTransLbl_Click;
        // 
        // totalCardTransLbl
        // 
        totalCardTransLbl.AutoSize = true;
        totalCardTransLbl.ForeColor = SystemColors.Control;
        totalCardTransLbl.Location = new Point(3, 48);
        totalCardTransLbl.Name = "totalCardTransLbl";
        totalCardTransLbl.Size = new Size(118, 20);
        totalCardTransLbl.TabIndex = 18;
        totalCardTransLbl.Text = "Total Card Trans:";
        // 
        // totalVideosLbl
        // 
        totalVideosLbl.AutoSize = true;
        totalVideosLbl.ForeColor = SystemColors.Control;
        totalVideosLbl.Location = new Point(3, 28);
        totalVideosLbl.Name = "totalVideosLbl";
        totalVideosLbl.Size = new Size(94, 20);
        totalVideosLbl.TabIndex = 17;
        totalVideosLbl.Text = "Total Videos:";
        // 
        // totalPhotosLbl
        // 
        totalPhotosLbl.AutoSize = true;
        totalPhotosLbl.ForeColor = SystemColors.Control;
        totalPhotosLbl.Location = new Point(3, 8);
        totalPhotosLbl.Name = "totalPhotosLbl";
        totalPhotosLbl.Size = new Size(94, 20);
        totalPhotosLbl.TabIndex = 16;
        totalPhotosLbl.Text = "Total Photos:";
        // 
        // cashRadBTN
        // 
        cashRadBTN.AutoSize = true;
        cashRadBTN.Checked = true;
        cashRadBTN.Location = new Point(6, 26);
        cashRadBTN.Name = "cashRadBTN";
        cashRadBTN.Size = new Size(61, 24);
        cashRadBTN.TabIndex = 16;
        cashRadBTN.TabStop = true;
        cashRadBTN.Text = "Cash";
        cashRadBTN.UseVisualStyleBackColor = true;
        // 
        // cardRadBTN
        // 
        cardRadBTN.AutoSize = true;
        cardRadBTN.Location = new Point(6, 63);
        cardRadBTN.Name = "cardRadBTN";
        cardRadBTN.Size = new Size(61, 24);
        cardRadBTN.TabIndex = 17;
        cardRadBTN.Text = "Card";
        cardRadBTN.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(cardCashRad);
        groupBox1.Controls.Add(cardRadBTN);
        groupBox1.Controls.Add(cashRadBTN);
        groupBox1.Location = new Point(197, 135);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(250, 104);
        groupBox1.TabIndex = 18;
        groupBox1.TabStop = false;
        groupBox1.Text = "Payment Method";
        // 
        // cardCashRad
        // 
        cardCashRad.AutoSize = true;
        cardCashRad.Location = new Point(131, 26);
        cardCashRad.Name = "cardCashRad";
        cardCashRad.Size = new Size(61, 24);
        cardCashRad.TabIndex = 18;
        cardCashRad.Text = "Both";
        cardCashRad.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox2.Controls.Add(currentPurchaseDatagrid);
        groupBox2.Location = new Point(6, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(305, 384);
        groupBox2.TabIndex = 19;
        groupBox2.TabStop = false;
        groupBox2.Text = "Current Purchases";
        // 
        // groupBox3
        // 
        groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupBox3.Controls.Add(purchaseHistoryDataGrid);
        groupBox3.Location = new Point(3, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(619, 390);
        groupBox3.TabIndex = 20;
        groupBox3.TabStop = false;
        groupBox3.Text = "Purchase Log:";
        // 
        // purchaseHistoryDataGrid
        // 
        purchaseHistoryDataGrid.AccessibleName = "Table";
        purchaseHistoryDataGrid.Dock = DockStyle.Fill;
        purchaseHistoryDataGrid.Location = new Point(3, 23);
        purchaseHistoryDataGrid.Name = "purchaseHistoryDataGrid";
        purchaseHistoryDataGrid.PreviewRowHeight = 35;
        purchaseHistoryDataGrid.Size = new Size(613, 364);
        purchaseHistoryDataGrid.Style.BorderColor = Color.FromArgb(100, 100, 100);
        purchaseHistoryDataGrid.Style.CheckBoxStyle.CheckedBackColor = Color.FromArgb(0, 120, 215);
        purchaseHistoryDataGrid.Style.CheckBoxStyle.CheckedBorderColor = Color.FromArgb(0, 120, 215);
        purchaseHistoryDataGrid.Style.CheckBoxStyle.IndeterminateBorderColor = Color.FromArgb(0, 120, 215);
        purchaseHistoryDataGrid.Style.HyperlinkStyle.DefaultLinkColor = Color.FromArgb(0, 120, 215);
        purchaseHistoryDataGrid.TabIndex = 27;
        purchaseHistoryDataGrid.Text = "sfDataGrid1";
        // 
        // completeTransBtn
        // 
        completeTransBtn.BackColor = Color.LimeGreen;
        completeTransBtn.ForeColor = SystemColors.ButtonHighlight;
        completeTransBtn.Location = new Point(877, 150);
        completeTransBtn.Name = "completeTransBtn";
        completeTransBtn.Size = new Size(100, 89);
        completeTransBtn.TabIndex = 21;
        completeTransBtn.Text = "Complete Transaction";
        completeTransBtn.UseVisualStyleBackColor = false;
        completeTransBtn.Click += completeTransBtn_Click;
        // 
        // emailTxtb
        // 
        emailTxtb.Location = new Point(252, 92);
        emailTxtb.Name = "emailTxtb";
        emailTxtb.Size = new Size(362, 27);
        emailTxtb.TabIndex = 24;
        // 
        // addVideoBtn
        // 
        addVideoBtn.Location = new Point(771, 150);
        addVideoBtn.Name = "addVideoBtn";
        addVideoBtn.Size = new Size(100, 89);
        addVideoBtn.TabIndex = 25;
        addVideoBtn.Text = "Add Video";
        addVideoBtn.UseVisualStyleBackColor = true;
        addVideoBtn.Click += addVideoBtn_Click;
        // 
        // splitContainer1
        // 
        splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        splitContainer1.Location = new Point(197, 245);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(groupBox2);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(groupBox3);
        splitContainer1.Size = new Size(943, 393);
        splitContainer1.SplitterDistance = 314;
        splitContainer1.TabIndex = 26;
        // 
        // phoneNumTxtb
        // 
        phoneNumTxtb.Location = new Point(314, 52);
        phoneNumTxtb.Name = "phoneNumTxtb";
        phoneNumTxtb.Size = new Size(300, 27);
        phoneNumTxtb.TabIndex = 27;
        // 
        // totalLbl
        // 
        totalLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        totalLbl.AutoSize = true;
        totalLbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        totalLbl.Location = new Point(975, 38);
        totalLbl.Name = "totalLbl";
        totalLbl.Size = new Size(159, 41);
        totalLbl.TabIndex = 28;
        totalLbl.Text = "TOTAL: $0";
        // 
        // showStatsBtn
        // 
        showStatsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        showStatsBtn.Location = new Point(986, 83);
        showStatsBtn.Name = "showStatsBtn";
        showStatsBtn.Size = new Size(148, 29);
        showStatsBtn.TabIndex = 29;
        showStatsBtn.Text = "Show Break Down";
        showStatsBtn.UseVisualStyleBackColor = true;
        showStatsBtn.Click += showStatsBtn_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1152, 638);
        Controls.Add(showStatsBtn);
        Controls.Add(totalLbl);
        Controls.Add(phoneNumTxtb);
        Controls.Add(splitContainer1);
        Controls.Add(addVideoBtn);
        Controls.Add(emailTxtb);
        Controls.Add(completeTransBtn);
        Controls.Add(twentyDealBtn);
        Controls.Add(addFourForThreeBtn);
        Controls.Add(addPhotoBtn);
        Controls.Add(groupBox1);
        Controls.Add(statsPanel);
        Controls.Add(nameTxtb);
        Controls.Add(statsLbl);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(panel1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "Form1";
        Text = "The Island Post Photography Dept. POS";
        Load += Form1_Load;
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)currentPurchaseDatagrid).EndInit();
        statsPanel.ResumeLayout(false);
        statsPanel.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)purchaseHistoryDataGrid).EndInit();
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Panel panel1;
    private PictureBox pictureBox1;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label statsLbl;
    private DataGridView currentPurchaseDatagrid;
    private Button addPhotoBtn;
    private Button addFourForThreeBtn;
    private Button twentyDealBtn;
    private TextBox nameTxtb;
    private Panel statsPanel;
    private Label totalVideosLbl;
    private Label totalPhotosLbl;
    private Label totalCashTransLbl;
    private RadioButton cashRadBTN;
    private RadioButton cardRadBTN;
    private GroupBox groupBox1;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Label versionLbl;
    private Button completeTransBtn;
    private TextBox emailTxtb;
    private Button addVideoBtn;
    private SplitContainer splitContainer1;
    private Button loadBtn;
    private Button exportBtn;
    private Syncfusion.WinForms.DataGrid.SfDataGrid purchaseHistoryDataGrid;
    private TextBox phoneNumTxtb;
    private RadioButton cardCashRad;
    private Label totalCardTransLbl;
    private Label totalLbl;
    private Button showStatsBtn;
    private Button customerViewBtn;
}
