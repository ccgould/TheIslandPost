using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for WorkerValidationDialog.xaml
/// </summary>
public partial class WorkerValidationDialog : ContentDialog
{
    private bool isEditing;
    public WorkerValidationDialog(ContentPresenter contentPresenter) : base(contentPresenter)
    {
        InitializeComponent();
        CheckBox.Focus();
    }

    protected override void OnButtonClick(ContentDialogButton button)
    {
        
        if (button == ContentDialogButton.Close)
        {
            base.OnButtonClick(button);
            return;
        };

        if(CheckBox.Password.ToString().Equals("405300"))
        {
            base.OnButtonClick(button);
            return;
        }


        TextBlock.Visibility = Visibility.Visible;
        CheckBox.Focus();
    }
}
