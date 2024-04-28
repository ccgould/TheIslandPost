using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandPostPOS;
public partial class TipForm : Form
{
    public TipForm()
    {
        InitializeComponent();
    }

    internal decimal GetTipAmount()
    {
        return numericUpDown1.Value;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        Close();
    }

    private void noBtn_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.No;
        Close();
    }
}
