using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mail;
using System.IO;
using System.Web.UI;
using System.Net.Mail;
using System.Configuration;

namespace careforever.App_Code
{
    public class SendEmail
    {
        public SendEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool Send(string ToEmail, string Subject, string Mail_Body)
    {
        try
        {
            string F_Email = ConfigurationSettings.AppSettings["F_Email"].ToString();
            string F_Password = ConfigurationSettings.AppSettings["F_Password"].ToString();
            System.Net.Mail.MailMessage Output_mail = new System.Net.Mail.MailMessage();
            Output_mail.From = new MailAddress(F_Email, "BluLife", System.Text.Encoding.UTF8);
            Output_mail.To.Add(ToEmail);
            Output_mail.Subject = Subject;
            Output_mail.Body = Mail_Body;
            Output_mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(F_Email, F_Password);
            smtp.EnableSsl = false;
            smtp.Send(Output_mail);
            return true;
        }
        catch (Exception Ex)
        {
            return false;
        }
    }

    public bool SendAttachment(string sw, string Emailids, string CCMailid, string from, string subject)
    {
        if (!string.IsNullOrEmpty(Emailids))
        {
            string toemailids = Emailids;
            //FileInfo fi = new FileInfo("SalarySlip.html");
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(toemailids);

                if (CCMailid != "")
                {
                    mail.CC.Add(CCMailid);
                }
                mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings.Get("uname"), "Saboo Hyundai", System.Text.Encoding.UTF8);
                mail.From = new MailAddress(ConfigurationSettings.AppSettings["F_Email"].ToString(), "Saboo Hyundai", System.Text.Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = sw.ToString();
                /* Attachment  to mail id */
                var temp = Path.GetTempFileName();
                var tempFile = temp.Replace(Path.GetExtension(temp), ".html");
                using (System.IO.StreamWriter sw1 = new System.IO.StreamWriter(tempFile))
                {
                    sw1.Write(mail.Body);
                }
                Attachment attachFile = new System.Net.Mail.Attachment(tempFile);
                attachFile.Name = "SalarySlip.html";
                mail.Attachments.Add(attachFile);
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = System.Net.Mail.MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = "smtpauth.net4india.com";
                client.EnableSsl = false;
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["F_Email"].ToString(), ConfigurationSettings.AppSettings["F_Password"].ToString());
                client.Send(mail);
                File.Delete(temp);
                if (CCMailid != "")
                {
                    mail.CC.Add(CCMailid);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {

            }
        }
        else
        {
        }
        return true;
    }

    public bool SendMail(string sw, string Emailids, string CCMailid, string from, string subject)
    {
        if (!string.IsNullOrEmpty(Emailids))
        {
            string toemailids = Emailids;
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(toemailids);
                if (CCMailid != "")
                {
                    mail.CC.Add(CCMailid);
                }
                mail.From = new MailAddress(ConfigurationSettings.AppSettings["F_Email"].ToString(), "BLULIFE", System.Text.Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = sw.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = System.Net.Mail.MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["F_Email"].ToString(), ConfigurationSettings.AppSettings["F_Password"].ToString());
                client.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {

        }
        return true;
    }

    public static void Send_SMTP(string ToEmail, string Subject, string Mail_Body)
    {
        string F_Email = ConfigurationSettings.AppSettings["F_Email"].ToString();
        string F_Password = ConfigurationSettings.AppSettings["F_Password"].ToString();
        using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(F_Email, ToEmail))
        {
            mm.Subject = Subject;
            mm.Body = Mail_Body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            credentials.UserName = F_Email;
            credentials.Password = F_Password;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credentials;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }

    public static void Send_SMTP1(string ToEmail, string Subject, string Mail_Body, string CCMailID)
    {
        string F_Email = ConfigurationSettings.AppSettings["F_Email"].ToString();
        string F_Password = ConfigurationSettings.AppSettings["F_Password"].ToString();

        System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
        mm.From = new MailAddress(F_Email);
        mm.To.Add(ToEmail);
        mm.CC.Add(CCMailID);
        mm.Subject = Subject;
        mm.Body = Mail_Body;
        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
        credentials.UserName = F_Email;
        credentials.Password = F_Password;
        smtp.UseDefaultCredentials = true;
        smtp.Credentials = credentials;
        smtp.Port = 587;
        smtp.Send(mm);
    }

    

    private string EmailBody(string UserID, string RoleType, string string1, string string2, string Trace)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("ErrorMail.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserID}", UserID);
        body = body.Replace("{RoleType}", RoleType);
        body = body.Replace("{string1}", string1);
        body = body.Replace("{string2}", string2);
        body = body.Replace("{Trace}", Trace);
        return body;
    }

    


    #region ForgotPwd

    private string forgotpwdEmailBody(string Name, string Emailid, string pwd)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("Forgot.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Name}", Name);
        body = body.Replace("{Emailid}", Emailid);
        body = body.Replace("{pwd}", pwd);

        return body;
    }

    private string VerifyEmailBody(string Name, string Emailid, string pwd, string link)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("verify_email.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Name}", Name);
        body = body.Replace("{Emailid}", Emailid);
        body = body.Replace("{pwd}", pwd);
        body = body.Replace("{link}", link);

        return body;
    }

    private string ThanksEmailBody(string Name)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("Thanks_email.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Name}", Name);
        //body = body.Replace("{Emailid}", Emailid);
        return body;
    }

    public void SendFormateMails(string Name, string Emailid, string Pwd, string Subject, string EmailType, string link)
    {
        string dyntable = string.Empty;
        string MsgType = string.Empty;
        if (EmailType == "forgotpwd")
        {
            dyntable = this.forgotpwdEmailBody(Name, Emailid, Pwd);
            MsgType = "iNetworkhub";
        }
        else if (EmailType == "verifyemail")
        {
            dyntable = this.VerifyEmailBody(Name, Emailid, Pwd, link);

            MsgType = "iNetworkhub";
        }
        else if (EmailType == "thanksemail")
        {
            dyntable = this.ThanksEmailBody(Name);
            MsgType = "iNetworkhub";
        }

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(Emailid);


        mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings.Get("gmailuname"), MsgType);
        mail.Subject = Subject;
        //mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = dyntable.ToString();
        //mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        //mail.Priority = System.Net.Mail.MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Port = 25;

        client.Host = "mail.inetworkhub.com";
        client.EnableSsl = false;
        client.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("gmailuname"),
            System.Configuration.ConfigurationSettings.AppSettings.Get("gmailpwd"));
        //client.UseDefaultCredentials = false;
        client.Send(mail);

    }

   

    public void SendMailICS(string from, string to, string subject, string body, Attachment attachment)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to, subject, body);
        mail.Attachments.Add(attachment);
        SmtpClient smtp = new SmtpClient();
        smtp.Port = 25;
        smtp.Host = "mail.inetworkhub.com";
        smtp.Credentials = new System.Net.NetworkCredential("info@inetworkhub.com", "Inet@123");
        smtp.Send(mail);
        mail.Dispose();

        //            SendEmail emailsend = new SendEmail();

        //emailsend.IcsFile();
    }

    #endregion

    
    }
}