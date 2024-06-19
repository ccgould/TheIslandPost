using FluentFTP;
using TheIslandPostManagerWPF.Models;
using Wpf.Ui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Text;

namespace TheIslandPostManagerWPF.Services;
internal class FTPService : IFTPService
{
    private AsyncFtpClient _client;
    private readonly IMessageService messageService;

    // connStr = "server=10.236.1.106;port=3306;user=CreswellWLaptop;database=islandpostphotography;password=Islandpostnassau1";
    string connStr = "server=192.168.0.3;port=3306;uid=CreswellWLaptop;database=db_island_post;pwd=wWw##4125";

    private MySqlConnection conn;

    public FTPService(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    public async Task Connect()
    {
        await connectToMySql();

        //await InsertPendingCustomer("612e0a4a90134ffeaa060e9bb4e909c32", "Phil", DateTime.Now);
        //await Get("612e0a4a90134ffeaa060e9bb4e909c32");

        //try
        //{
        //    if (_client is not null) return;
        //    // create an FTP client and specify the host, username and password
        //    // (delete the credentials to use the "anonymous" account)
        //    _client = new AsyncFtpClient(applicationSaveService.ApplicationSaveData.FtpHost, applicationSaveService.ApplicationSaveData.FTPUserID, applicationSaveService.ApplicationSaveData.FTPPassword ?? string.Empty);

        //    // connect to the server and automatically detect working FTP settings
        //    await _client.AutoConnect();
        //}
        //catch (Exception ex)
        //{
        //    snackbarService.Show(
        //        "Error",
        //        "Failed to connect to FTP",
        //        ControlAppearance.Danger,
        //        new SymbolIcon(SymbolRegular.ThumbDislike24),
        //        TimeSpan.FromSeconds(5));
        //}
    }

    private async Task connectToMySql()
    {
        conn = new MySqlConnection(connStr);
        try
        {
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //var token = cancellationTokenSource.Token;
            messageService.ShowSnackBarMessage("MYSQL","Connecting to MySQL...", ControlAppearance.Info, SymbolRegular.Warning28);
            await conn.OpenAsync();
            messageService.ShowSnackBarMessage("MYSQL", "Connected to MySQL...");

            //while (token.IsCancellationRequested)
            //{

            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        //close connection if you want
        //conn.Close();
        Console.WriteLine("Done.");
    }

    public async Task InsertPendingCustomer(string guid,string name)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL", "Connecting to MySQL...");

            if(conn is null)
            {
                await Connect();
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = $"INSERT INTO pending_customers (customername,guid,datetime) VALUES ('{name}','{guid}', '{date}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            //await conn.CloseAsync();
            messageService.ShowSnackBarMessage("MYSQL","Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task InsertOrder(OrderInformation order)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL", "Connecting to MySQL...");

            if (conn is null)
            {
                await Connect();
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sb = new();



            for (int i = 0; i < order.CC.Count; i++)
            {
                if(i > 0)
                {
                    sb.Append(";");
                }

                FluentEmail.Core.Models.Address? item = order.CC[i];
                sb.AppendLine(item.EmailAddress);
            }

            string sql = $"INSERT INTO orders (customername,guid,email,cc,downloadurl,datetime) VALUES ('{order.Name}','{order.Email}', '{sb.ToString()}','{order.DownloadURL}','{date}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            foreach (var item in order.ApprovedImages)
            {
                await AssignApprovedImagesToCustomer(order.CustomerID, item);
            }

            foreach (var item in order.ApprovedPrints)
            {
                await AssignApprovedPrintsToCustomer(order.CustomerID, item);
            }

            //await conn.CloseAsync();
            messageService.ShowSnackBarMessage("MYSQL", "Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task AssignApprovedImagesToCustomer(string customerID, OrderImageSelectionData item)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL", "Connecting to MySQL...");

            if (conn is null)
            {
                await Connect();
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

             
            string sql = $"INSERT INTO approvedImages (guid,imagename,path,amount,datetime) VALUES ('{customerID}','{item.Name}', '{item.Root}','{item.Amount}','{date}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();


            //await conn.CloseAsync();
            messageService.ShowSnackBarMessage("MYSQL", "Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task AssignApprovedPrintsToCustomer(string customerID, OrderImageSelectionData item)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL", "Connecting to MySQL...");

            if (conn is null)
            {
                await Connect();
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            string sql = $"INSERT INTO approvedprints (guid,imagename,path,amount,datetime) VALUES ('{customerID}','{item.Name}', '{item.Root}','{item.Amount}','{date}')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();


            //await conn.CloseAsync();
            messageService.ShowSnackBarMessage("MYSQL", "Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public void AssignImagesToCustomer(string guid, ImageObj image)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL", "Connecting to MySQL...");

            string sql = $"INSERT INTO images (guid,imageurl,customername,height,width,printamount,isselected,ispending,isprintable,imageindex) VALUES ('{guid}','{CorrectPath(image.ImageUrl)}','{image.Name}','{image.Height}','{image.Width}','{image.PrintAmount}','{Convert.ToInt32(image.IsSelected)}','{Convert.ToInt32(image.IsPending)}','{Convert.ToInt32(image.IsPrintable)}','0')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            //await conn.CloseAsync();
            messageService.ShowSnackBarMessage("MYSQL", "Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private string CorrectPath(string imageUrl)
    {
        var first = imageUrl.Replace("\\\\", "//");
        return first.Replace("\\", @"/");

    }

    private async Task Get(string id)
    {
        try
        {
            messageService.ShowSnackBarMessage("MYSQL","Connecting to MySQL...");


            if (conn.IsDisposed)
            {
                await conn.OpenAsync();
            }


            string sql = $"SELECT * FROM images WHERE id='{id}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            }
            await rdr.CloseAsync();
        }
        catch (Exception ex)
        {
            messageService.ShowSnackBarMessage("MYSQL", ex.ToString(), ControlAppearance.Danger, SymbolRegular.ThumbDislike24);
        }
    }

    public async Task UploadFile(string source, string dest)
    {

        try
        {
            // upload a file and retry 3 times before giving up
            _client.Config.RetryAttempts = 3;

            var status = await _client.UploadFile(source, dest, FtpRemoteExists.Overwrite, false, FtpVerify.Retry);

            if (status == FtpStatus.Success)
            {
                messageService.ShowSnackBarMessage("Success", "Successfully to sent image through FTP");
            }

            if (status == FtpStatus.Failed)
            {
                messageService.ShowSnackBarMessage("Error", "Failed to send image through FTP",ControlAppearance.Danger, SymbolRegular.ThumbDislike24,5);
            }
        }
        catch (Exception ex)
        {
            messageService.ShowErrorMessage("Error",ex.Message,ex.StackTrace);
        }
    }

    public async Task Disconnect()
    {
        // disconnect! good bye!
        await _client.Disconnect();
        _client = null;
        messageService.ShowMessage("FTP Message","FTP Disconnected.");
    }

    public async Task<ObservableCollection<PendingOrder>> LoadPendings()
    {
        MySqlDataReader? rdr = null;

        ObservableCollection<PendingOrder> pendingOrders = new();


        try
        {
            if (conn is null)
            {
                await Connect();
            }

            string sql = $"SELECT * FROM pending_customers";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var pendingOrder = new PendingOrder();
                //Get the results from the column
                var guid = (string)rdr["guid"];
                var customerName = (string)rdr["customerName"];
                var dateTime = (DateTime)rdr["datetime"];

                pendingOrder.Name = customerName;
                pendingOrder.Date = dateTime;
                pendingOrder.Guid = guid;

                pendingOrders.Add(pendingOrder);
            }

            await rdr.CloseAsync();
            

            foreach (var pendingOrder in pendingOrders)
            {
                string sql2 = $"SELECT * FROM images WHERE guid='{pendingOrder.Guid}'";

                cmd.Connection = conn;
                cmd.CommandText = sql2;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pendingOrder.PendingImages.Add(new ImageObj
                    {
                        ImageUrl = (string)rdr["imageurl"],
                        Name = (string)rdr["customername"],
                        Height = (int)rdr["height"],
                        Width = (int)rdr["width"],
                        PrintAmount = (int)rdr["printamount"],
                        IsSelected = Convert.ToBoolean(rdr["isselected"]),
                        IsPending = Convert.ToBoolean(rdr["ispending"]),
                        IsPrintable = Convert.ToBoolean(rdr["isprintable"]),
                        Index = (int)rdr["imageindex"]
                    });
                }

                await rdr.CloseAsync();

            }

        }
        catch (Exception ex)
        {
            return new();
        }
        finally
        {
            rdr?.Close();
        }

        return pendingOrders;
    }
}