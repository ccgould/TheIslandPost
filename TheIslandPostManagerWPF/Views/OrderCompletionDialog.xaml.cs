using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Views;
/// <summary>
/// Interaction logic for OrderCompletionDialog.xaml
/// </summary>
public partial class OrderCompletionDialog : ContentDialog
{
    public OrderCompletionDialog(ContentPresenter contentPresenter) : base(contentPresenter)
    {
        InitializeComponent();
    }

    protected override void OnButtonClick(ContentDialogButton button)
    {
        base.OnButtonClick(button);
    }
}
