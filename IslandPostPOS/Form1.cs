using IslandPostPOS.Enumerators;
using IslandPostPOS.Model;
using Newtonsoft.Json;
using Syncfusion.WinForms.Controls.Accessibility;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGridConverter;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Transactions;
using Transaction = IslandPostPOS.Model.Transaction;


namespace IslandPostPOS;

public partial class Form1 : Form
{
    public ObservableCollection<StoreItem> _currentPurchases = new();
    public ObservableCollection<Transaction> _currentTransaction = new();

    private List<StoreItem> _storeItems = new()
    {
        new(){ID = 1, Name="$5 Photo", MediaType = MediaType.Photo, Cost=5},
        new(){ID = 2, Name="$15 Deal", MediaType= MediaType.Photo, Multiplyer = 4, Cost = 15},
        new(){ID = 3, Name="$20 Deal", MediaType = MediaType.Photo, Multiplyer = 4, Cost = 20},
        new(){ID = 4, Name="$$6 Video", MediaType = MediaType.Video, Cost = 6},
    };
    private DataTable _table;
    private DataTable _logTable;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        _table = new DataTable();
        _table.Columns.Add("Product Name", typeof(string));

        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
        buttonColumn.Name = "DeleteBtn";
        buttonColumn.HeaderText = "Remove";
        buttonColumn.Text = "Remove";
        buttonColumn.UseColumnTextForButtonValue = true;

        currentPurchaseDatagrid.DataSource = _table;

        currentPurchaseDatagrid.Columns.Add(buttonColumn);

        _logTable = new DataTable();
        _logTable.Columns.Add("Date", typeof(DateTime));
        _logTable.Columns.Add("Name", typeof(string));
        _logTable.Columns.Add("Email", typeof(string));
        _logTable.Columns.Add("Phone Number", typeof(string));
        _logTable.Columns.Add("Image Print Count", typeof(string));
        _logTable.Columns.Add("Video Count", typeof(string));
        _logTable.Columns.Add("Parment Type", typeof(string));

        purchaseHistoryDataGrid.DataSource = _currentTransaction;

        purchaseHistoryDataGrid.Columns.Add(new GridButtonColumn()
        {
            AllowDefaultButtonText = true,
            DefaultButtonText = "Delete",
            HeaderText = "Delete Entry"
        });

        this.purchaseHistoryDataGrid.CellButtonClick += sfDataGrid1_CellButtonClick;


    }

    private void sfDataGrid1_CellButtonClick(object sender, CellButtonClickEventArgs e)
    {
        var result = MessageBox.Show("Are your sure you would like to delete this record?", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

        if (result == DialogResult.Yes)
        {
            _currentTransaction.RemoveAt(e.RowIndex - 1);

            SaveRecords();
        }
    }

    private void SaveRecords()
    {
        string output = JsonConvert.SerializeObject(new DataSave
        {
            CurrentPurchases = _currentPurchases,
            CurrentTransaction = _currentTransaction
        }, Formatting.Indented);

        var fileName = $"IslandPostSave_{DateTime.Now.ToString("MM_dd_yyyy")}.json";
        var rootFolder = Path.GetDirectoryName(Application.ExecutablePath);
        var savesDirectory = Path.Combine(rootFolder, "Data", "Saves");
        var path = Path.Combine(savesDirectory, fileName);

        if (!Directory.Exists(savesDirectory))
        {
            Directory.CreateDirectory(savesDirectory);
        }

        File.WriteAllText(path, output);

    }

    private int GetVideoCount()
    {
        return _currentTransaction.Sum(x => x.VideoAmount);
    }

    private int GetPhotoCount()
    {
        return _currentTransaction.Sum(x => x.PictureAmount);
    }

    private int GetCashTransCount()
    {
        return _currentTransaction.Count(x => x.PaymentType == PaymentType.Cash);
    }

    private int GetCardTransCount()
    {
        return _currentTransaction.Count(x => x.PaymentType == PaymentType.Card);
    }

    private void addPhotoBtn_Click(object sender, EventArgs e)
    {
        _currentPurchases.Add(_storeItems[0]);
        _table.Rows.Add(_storeItems[0].Name);

    }

    private void addFourForThreeBtn_Click(object sender, EventArgs e)
    {
        _currentPurchases.Add(_storeItems[1]);
        _table.Rows.Add(_storeItems[1].Name);
    }

    private void twentyDealBtn_Click(object sender, EventArgs e)
    {
        _currentPurchases.Add(_storeItems[2]);
        _table.Rows.Add(_storeItems[2].Name);
    }

    private void addVideoBtn_Click(object sender, EventArgs e)
    {
        _currentPurchases.Add(_storeItems[3]);
        _table.Rows.Add(_storeItems[3].Name);
    }

    private void currentPurchaseDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 1)
        {
            if (_currentPurchases.Count > 0)
            {
                _currentPurchases.RemoveAt(e.RowIndex);
                _table.Rows.RemoveAt(e.RowIndex);
            }
        }
    }

    private bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success;
    }

    private void completeTransBtn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(phoneNumTxtb.Text) && !IsPhoneNumber(phoneNumTxtb.Text))
        {
            MessageBox.Show("Invaild Phone number.", "Invaild Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (!IsPhoneNumber(phoneNumTxtb.Text) && string.IsNullOrWhiteSpace(emailTxtb.Text))
        {
            MessageBox.Show("Please provide and email or phone number", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        var picAmount = 0;
        var transactions = _currentPurchases.Where(x => x.ID < 4);
        var vidAmount = _currentPurchases.Count(x => x.ID == 4);

        foreach (var trans in transactions)
        {
            if (trans.MediaType == MediaType.Video) continue;
            picAmount += trans.Multiplyer;
        }

        var transaction = new Transaction
        {
            VideoAmount = vidAmount,
            PictureAmount = picAmount,
            Name = nameTxtb.Text,
            PhoneNumber = phoneNumTxtb.Text,
            DateTime = DateTime.Now,
            Email = emailTxtb.Text,
            PaymentType = GetTransactionType(),
            CashTotal = $"${GetTotal()}",
            CashTotalInt = GetTotal()
        };

        var tip = new TipForm();

        if (tip.ShowDialog(this) == DialogResult.OK)
        {
            transaction.Tip = tip.GetTipAmount();
            CompleteTransaction(transaction);
        }
        else
        {
            CompleteTransaction(transaction);
        }
    }

    private PaymentType GetTransactionType()
    {
        if (cashRadBTN.Checked)
        {
            return PaymentType.Cash;
        }
        else if (cardRadBTN.Checked)
        {
            return PaymentType.Card;
        }

        return PaymentType.Both;
    }

    private void CompleteTransaction(Transaction transaction)
    {
        _currentTransaction.Add(transaction);
        UpdateStats();
        ClearCurrentDatagrid();
        SaveRecords();
    }

    private int GetTotal()
    {
        int total = 0;

        foreach (var purchase in _currentPurchases)
        {
            total += purchase.Cost;
        }

        return total;
    }

    private void UpdateStats()
    {
        totalCardTransLbl.Text = $"Total Card Tans: {GetCardTransCount()}";
        totalCashTransLbl.Text = $"Total Cash Trans: {GetCashTransCount()}";
        totalPhotosLbl.Text = $"Total Photos: {GetPhotoCount()}";
        totalVideosLbl.Text = $"Total Videos: {GetVideoCount()}";
        totalLbl.Text = $"Total: ${GetTransTotal()}";
    }

    private int GetTransTotal()
    {
        int amount = 0;
        foreach (var item in _currentTransaction)
        {
            amount += item.CashTotalInt;
        }

        return amount;
    }

    private void exportBtn_Click(object sender, EventArgs e)
    {
        try
        {
            var fileName = $"IslandPostSales_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Export Log Files";
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FileName = fileName;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var options = new ExcelExportingOptions();
                var excelEngine = purchaseHistoryDataGrid.ExportToExcel(purchaseHistoryDataGrid.View, options);
                var workBook = excelEngine.Excel.Workbooks[0];

                workBook.SaveAs(saveFileDialog1.FileName);

                MessageBox.Show($"Data exported to {saveFileDialog1.FileName}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearCurrentDatagrid()
    {
        _table.Rows.Clear();
        _currentPurchases.Clear();
        nameTxtb.Text = string.Empty;
        phoneNumTxtb.Text = string.Empty;
        emailTxtb.Text = string.Empty;
    }

    private void loadBtn_Click(object sender, EventArgs e)
    {
        var fileName = $"IslandPostSave_{DateTime.Now.ToString("MM_dd_yyyy")}.json";

        OpenFileDialog openFileDialog1 = new OpenFileDialog
        {
            InitialDirectory = @":C\",
            Title = "Browse Log Files",
            //FileName = fileName,
            CheckFileExists = true,
            CheckPathExists = true,

            DefaultExt = "xlsx",
            Filter = "JSON files (*.json)|*.json",
            RestoreDirectory = true,
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            if (_currentPurchases.Count > 0)
            {
                _currentPurchases.Clear();
            }

            if (_currentTransaction.Count > 0)
            {
                _currentTransaction.Clear();
            }

            var json = File.ReadAllText(openFileDialog1.FileName);
            var transactions = JsonConvert.DeserializeObject<DataSave>(json);

            if (transactions is not null)
            {
                foreach (var transaction in transactions.CurrentTransaction)
                {
                    _currentTransaction.Add(transaction);
                }
            }
            UpdateStats();

            this.Text = $"The Island Post Photography Dept. POS - {Path.GetFileName(openFileDialog1.FileName)}";
        }
    }

    private void totalCashTransLbl_Click(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void showStatsBtn_Click(object sender, EventArgs e)
    {
        statsLbl.Visible = !statsLbl.Visible;
        statsPanel.Visible = !statsPanel.Visible;
    }

    private void customerViewBtn_Click(object sender, EventArgs e)
    {
        CustomerView customerView = new CustomerView();
        customerView.Show();

        customerView.OnTransactionComplete += (trans) =>
        {
            _currentTransaction.Add(trans);
            UpdateStats();
            SaveRecords();
        };
    }
}
