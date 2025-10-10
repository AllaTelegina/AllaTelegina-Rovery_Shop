using System.Net;
using System.Net.Mail;

namespace Backend_asp.net.SendMessage
{
    public static class EmailSendService
    {
        public static async Task SendAsynk(string Toimail, string Subject,  string Body)
        {
            var MyEmail= "maxpesfix30331@gmail.com";
            var Password = "adxjctrvojdpzjnp";

            // Объект протокола письма;
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(MyEmail, Password)
            };

            // Объект тела письма; 
            var mesage=new MailMessage(MyEmail, Toimail)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml= true
            };
            await smtp.SendMailAsync(mesage);
        }
    }
}
