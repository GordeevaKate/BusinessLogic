using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BusinessLogic.BusinessLogic
{
  public  class Mail
    {
      static  public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("dggfddg6@gmail.com", "Отчет!");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("dggfddg6@gmail.com", "alu050600!");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
