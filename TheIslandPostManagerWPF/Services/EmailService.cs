using FluentEmail.Core;
using FluentEmail.Smtp;
using System.IO;
using System.Net.Mail;
using System.Text;
using TheIslandPostManagerWPF.Models;

namespace TheIslandPostManagerWPF.Services;
public class EmailService
{
    internal async Task<bool> SendEmail(OrderInformation order)
    {

        var fluentService = App.GetService<IFluentEmail>();

        //Email.DefaultSender = new SmtpSender(smtp);

        var email = await fluentService
            .To(order.Email, order.Name)
            .Subject($"The Island Post Photography Department - {order.Name}")
            .UsingTemplateFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "template.cshtml"), new
            {
                Link = order.DownloadURL
            })
            .SendAsync();

        if (email.Successful)
        {
            return true;
        }

        return false;
    }


    internal void SendToWhatsapp()
    {
    }
}
