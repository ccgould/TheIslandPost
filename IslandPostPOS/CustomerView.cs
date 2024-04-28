using IslandPostPOS.Enumerators;
using IslandPostPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandPostPOS;
public partial class CustomerView : Form
{
    public Transaction Transaction { get; set; }
    public Action<Transaction> OnTransactionComplete { get; set; }

    public CustomerView()
    {
        InitializeComponent();
    }

    private void CustomerView_Load(object sender, EventArgs e)
    {
       UpdateCustomerID();
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {

    }

    private void submitBtn_Click(object sender, EventArgs e)
    {

        if(paymentTypeCmb.SelectedIndex <= -1)
        {
            MessageBox.Show("Please Select PaymentType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if(totalPhotosNum.Value == 0 && totalVideosNum.Value == 0)
        {
            MessageBox.Show("Please Set Total Videos or Total Photos Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (totalPaymentNum.Value == 0)
        {
            MessageBox.Show("Please Set Total Payemnt Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if(string.IsNullOrWhiteSpace(nameTxtb.Text))
        {
            MessageBox.Show("Please Set Name Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (!String.IsNullOrWhiteSpace(numberTxtb.Text) && !IsPhoneNumber(numberTxtb.Text))
        {
            MessageBox.Show("Please check phone number.  Number is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (string.IsNullOrWhiteSpace(numberTxtb.Text) && string.IsNullOrWhiteSpace(emailTxtb.Text))
        {
            MessageBox.Show("Please Set  Number or Email Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        var transaction = new Transaction
        {
            VideoAmount = Convert.ToInt32(totalVideosNum.Value),
            PictureAmount = Convert.ToInt32(totalPhotosNum.Value),
            Name = nameTxtb.Text,
            PhoneNumber = numberTxtb.Text,
            DateTime = DateTime.Now,
            Email = emailTxtb.Text,
            PaymentType = GetTransactionType(),
            CashTotal = $"${totalPaymentNum.Value}",
            CashTotalInt = Convert.ToInt32(totalPaymentNum.Value),
            CustomerID = Settings1.Default.CustomerNumber.ToString("D4")
    };

        Transaction = transaction;
        this.OnTransactionComplete.Invoke(transaction);
        ClearFields();

        Settings1.Default.CustomerNumber += 1;
        Settings1.Default.Save();
        UpdateCustomerID();
    }

    private void ClearFields()
    {
        nameTxtb.Text = default;
        numberTxtb.Text = default;
        emailTxtb.Text = default;
        totalPaymentNum.Value = default;
        totalPhotosNum.Value = default;
        totalVideosNum.Value = default;
    }

    private PaymentType GetTransactionType()
    {
        return (PaymentType)paymentTypeCmb.SelectedIndex;
    }

    private bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success;
    }

    private void UpdateCustomerID()
    {
        customerID.Text = Settings1.Default.CustomerNumber.ToString("D4");
    }
}
