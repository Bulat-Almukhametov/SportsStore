using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "khattartmasi@rocketmail.com";
        public string MailFromAddress = "night_bulat@mail.ru";
        public bool UseSsl = true;
        public string Username = "Night_Bulat";
        public string Password = "eqEUhae";
        public string ServerName = "smtp.mail.ru";
        public int ServerPort = 25;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Users\w\Desktop\sports_store_emails\";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings = new EmailSettings();
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username,
                    emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Был сделан новый заказ")
                    .AppendLine("---")
                    .AppendLine("Товары");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price*line.Quantity;
                    body.AppendFormat("{0} x {1} (цена за все экземпляры данного товара: {2:c}", line.Quantity,
                        line.Product.Name, subtotal);
                    body.AppendLine();
                }

                body.AppendFormat("Сумма всего заказа: {0:c}", cart.ComputeTotalValue())
                    .AppendLine()
                    .AppendLine("---")
                    .AppendLine("Получатель:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? String.Empty)
                    .AppendLine(shippingInfo.Line3 ?? String.Empty)
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Завернуть как подарок: {0}", shippingInfo.GiftWrap ? "да" : "нет");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress, //From
                    emailSettings.MailToAddress, //To
                    "Получен новый заказ!", //Subject
                    body.ToString()); //Body

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }


        }
    }
}
