using System.Collections.ObjectModel;
using System.Windows.Controls;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Services;
using Wpf.Ui.Controls;
using ListViewItem = Wpf.Ui.Controls.ListViewItem;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for FinalizeDialog.xaml
/// </summary>
public partial class FinalizeDialog : ContentDialog
{
    
    public FinalizeDialog(ContentPresenter contentPresenter, WorkerService workerService) : base(contentPresenter)
    {
        InitializeComponent();

        DataContext = this;
        WorkerService = workerService;
    }

    public WorkerService WorkerService { get; }

    protected override void OnButtonClick(ContentDialogButton button)
    {

        if (button == ContentDialogButton.Close)
        {
            base.OnButtonClick(button);
            return;
        };
             


            if (decimal.TryParse(valueTxtb.Text, out decimal result))
            {
            if (result == 0)
            {
                //Say message to why
                return;
            }

            decimal split = 0;

            if (listView.SelectedItems.Count > 0)
            {
                split = result / listView.SelectedItems.Count;

                foreach (Worker item in listView.SelectedItems)
                {
                    item.AppendEarnings(split);
                }

                base.OnButtonClick(button);
            }

        }

        TextBlock.Visibility = System.Windows.Visibility.Visible;
        //CheckBox.Focus();
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var items = e.AddedItems;
        
        
    }
}
