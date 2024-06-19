using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Services;
internal class FirebaseService
{
    private readonly IMessageService messageService;
    IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "ZSyCFwG7v1HxhsNWj5yFZxLYUbw0QkXvrSrkI4VP",
        BasePath = "https://islandpost-ef47f-default-rtdb.firebaseio.com/"
    };

    private IFirebaseClient client;

    public FirebaseService(IMessageService messageService)
    {
        this.messageService = messageService;


        client = new FirebaseClient(config);

        if (client is not null)
        {
            messageService.ShowSnackBarMessage("Connection","Server Connection Successful");
        }

    }

    public async Task Listening()
    {
        //var response = await client.



        //var response = await client.OnAsync("chat", (sender, args, context) => {
        //    System.Console.WriteLine(args.Data);
        //});
    }


    public async Task NotifyServer(OrderLog log)
    {
        if (client is null) return;

        SetResponse response = await client.SetTaskAsync("Test", new PendingOrder());
        PendingOrder result = response.ResultAs<PendingOrder>();
    }
}
