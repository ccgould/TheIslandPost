using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheIslandPostManagerWPF.ViewModels;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for FinalizePageView.xaml
/// </summary>
public partial class FinalizePageView : Page
{
    private int _numValue = 0;

    

    public int NumValue
    {
        get { return _numValue; }
        set
        {
            _numValue = value;
            //txtNum.Text = value.ToString();
        }
    }

    public FinalizePageView(FinalizeOrderViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        //txtNum.Text = _numValue.ToString();
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        NumValue++;
    }

    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        NumValue--;
    }

    private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
    {
        //if (txtNum == null)
        //{
        //    return;
        //}

        //if (!int.TryParse(txtNum.Text, out _numValue))
        //    txtNum.Text = _numValue.ToString();
    }
}
