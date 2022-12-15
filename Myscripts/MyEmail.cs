using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class MyEmail
    {
        public static void SendEmail(string emailTo,string subject= "Test email",string body = "This is a test email from link-todo.")
        {
            // 指定发件人、收件人、邮件主题和内容
            string from = Settings.EmailFrom;
            string to = emailTo;

            // 创建一个 SmtpClient 对象
            SmtpClient smtpClient = new SmtpClient("smtp.qq.com", 587);

            // 指定发件人的用户名和密码
            smtpClient.Credentials = new NetworkCredential(Settings.EmailFrom, Settings.EmailPwd);

            // 启用安全连接
            smtpClient.EnableSsl = true;


            try
            {
                // 创建一个 MailMessage 对象
                MailMessage mailMessage = new MailMessage(from, to, subject, body);

                // 发送邮件
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
