using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace ShoeStoreClothing.Helpers
{
    public class SendGmail
    {
        public static async Task SendAsync(string name, string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Shoe Store", "vantaii12082003@gmail.com"));
            message.To.Add(new MailboxAddress(name, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("vantaii12082003@gmail.com", "lgws vrot kuem nzzi");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
