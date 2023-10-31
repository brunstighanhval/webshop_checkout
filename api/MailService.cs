using MimeKit;

namespace api;

public class MailService
{
    public void SendEmail(double orderPrice, string orderCurrency)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("The Webshop Inc.", Environment.GetEnvironmentVariable("FromEmail")));
        message.To.Add(new MailboxAddress("Customer", Environment.GetEnvironmentVariable("ToEmail")));
        message.Subject = "Your order confirmation";

        message.Body = new TextPart("plain")
        {
            Text = @"Total order price: "+orderPrice + " " + orderCurrency
        };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(Environment.GetEnvironmentVariable("FromEmail"), Environment.GetEnvironmentVariable("CheckOut") );
            client.Send(message);
            client.Disconnect(true);
        }
    }
}