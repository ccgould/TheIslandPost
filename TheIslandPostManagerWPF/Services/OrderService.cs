using CommunityToolkit.Mvvm.ComponentModel;
using FluentEmail.Core.Models;
using FluentEmail.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TheIslandPostManagerWPF.Models;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui.Controls;
using System.Windows;
using System.IO;

namespace TheIslandPostManagerWPF.Services;
public partial class OrderService : ObservableObject, IOrderService
{
    private readonly IMessageService messageService;
    private readonly ApplicationSaveService applicationSaveService;
    private readonly IFileServices fileServices;
    private readonly ImageService imageService;
    [ObservableProperty] ObservableCollection<OrderInformation> orders = new();
    [ObservableProperty] OrderInformation order = new();
    [ObservableProperty] bool isBusy;

    public OrderService(IMessageService messageService, ApplicationSaveService applicationSaveService,IFileServices fileServices,ImageService imageService)
    {
        this.messageService = messageService;
        this.applicationSaveService = applicationSaveService;
        this.fileServices = fileServices;
        this.imageService = imageService;
    }

    public void CreateNewOrder()
    {
        Order = new OrderInformation();
        Orders.Add(Order);
    }

    public void AddCurrentOrder()
    {
        Orders.Add(Order);
        messageService.ShowDebugMessage("Order Added", $"Order added to collection. Collection amount: {Orders.Count()}");
        Order = new();
    }

    public void DeleteOrder(OrderInformation order)
    {
        var result = messageService.ShowMessage("Delete Order", $"Are you sure you would like to delete order {order.Name}?");

        if(result.Result == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            Orders.Remove(order);
            messageService.ShowDebugMessage("Order Removed", $"Order removed from collection. Collection amount: {Orders.Count()}");
        }
    }

    public void AddApprovedPhoto(OrderImageSelectionData orderImageSelectionData)
    {
        if(Order is not null)
        {
            Order.ApprovedPrints.Add(orderImageSelectionData);
        }
    }

    public void RemoveApprovedPhoto(OrderImageSelectionData orderImageSelectionData)
    {
        if (Order is not null)
        {
            Order.ApprovedPrints.Remove(orderImageSelectionData);
        }
    }

    public Task FinalizeOrder(bool bypassDigitals)
    {
        IsBusy = true;

        var personalDir = fileServices.CreateOrderDirectory(Order.Name);

        return Task.Run(() =>
        {
            var order = new OrderInformation();

            order.IsFinalized = bypassDigitals;

            //foreach (ImageObj image in Images)
            //{
            //    if (image.IsSelected)
            //    {
            //        var savImg = new OrderImageSelectionData { Name = image.Name, Path = image.ImageUrl };
            //        order.ApprovedImages.Add(savImg);
            //        applicationSaveService.AddApprovedImage();

            //        if (image.IsPrintable)
            //        {
            //            var savImg2 = new OrderImageSelectionData { Name = image.Name, Path = image.ImageUrl, Amount = image.PrintAmount, ParentImage = savImg };
            //            order.ApprovedPrints.Add(savImg2);
            //            applicationSaveService.AddApprovedPrints(image.PrintAmount);
            //        }
            //    }
            //}

            string curEmail = Order.Email;
            List<Address> cc = new();

            if (curEmail.Contains(','))
            {
                var emails = curEmail.Split(',');

                if (emails.Length > 1)
                {
                    for (int i = 1; i < emails.Length; i++)
                    {
                        cc.Add(new Address(emails[i]));
                    }
                    curEmail = emails[0];
                    order.CC = cc;
                }
            }


            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                order.DateTime = DateTime.Now;
                order.Name = Order.Name;
                order.Email = curEmail;
                //order.PhoneNumber = phoneNumm;
                //order.CustomerID = _currentGUID;

                applicationSaveService.AddToOrderLogs(order);
            }));

            foreach (OrderImageSelectionData image in order.ApprovedImages)
            {
                imageService.AddWaterMark(personalDir, image);
            }

            Application.Current.Dispatcher.Invoke(new Action(async () =>
            {
                foreach (OrderImageSelectionData image in order.ApprovedPrints)
                {
                    image.CopyParentData();
                    var files = new List<Tuple<string, string>>();

                    for (int i = 0; i < image.Amount; i++)
                    {
                        var wOutExt = Path.GetFileNameWithoutExtension(image.Name);
                        var result = $"{wOutExt}_{i}.jpg";
                        files.Add(new Tuple<string, string>(image.Path, Path.Combine(fileServices.GetTempDir(), result)));
                    }

                    await fileServices.Copy(files);
                }

            }));



            IsBusy = false;
        });
    }

    internal void EndTransaction()
    {

        // ClearImages();

        //_ = _navigationService.Navigate(typeof(DashboardPage));

        //FinishOrder(order, personalDir, bypassDigitals);
    }


    internal void OpenExportPath(OrderInformation order, string exportPath, bool bypassDigitals)
    {
        messageService.ShowSnackBarMessage("Exporting Finished", "All Images have been exported successfully", ControlAppearance.Info, SymbolRegular.Info24);

        if (!bypassDigitals)
        {
            Process.Start("explorer.exe", exportPath);
        }
    }
}
