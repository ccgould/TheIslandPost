using Syncfusion.Maui.DataForm;
using TheIslandPostPhotoManager.ViewModels;

namespace TheIslandPostPhotoManager.Views;

public partial class OrderCompletionPage : ContentPage
{
	public OrderCompletionPage(OrderCompletionPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        this.dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
    }



private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
    {
        if (e.DataFormItem is DataFormTextItem textItem)
        {
            e.DataFormItem.EditorTextStyle = new DataFormTextStyle()
            {
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Italic
            };
        }
    }
}