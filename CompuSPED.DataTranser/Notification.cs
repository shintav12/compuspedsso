using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace CompuSPED.DataTranser
{
    public static class Notification
    {
        /// <summary>
        ///  Send Emails to the adresses in the App.config
        /// </summary>
        /// <param name="ex">The Exception</param>
        /// <param name="extraInfo">Extra info about the error added to email body</param>
        public static void SendEmailException(Exception ex, string extraInfo = "")
        {
            string strEmailToAddress = ConfigurationManager.AppSettings["ErrorEmailAddresses"].ToString().Trim();
            string strEmailSubject = ConfigurationManager.AppSettings["ErrorEmailSubject"].ToString().Trim();
            string strEmailBody = DateTime.Now.ToString() + ConfigurationManager.AppSettings["ErrorEmailBody"].ToString().Trim() + "<BR><BR>";
            string strEmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"].ToString().Trim();
            string strEmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString().Trim();
            string strEmailUser = ConfigurationManager.AppSettings["EmailUser"].ToString().Trim();
            string strEmailSMTP = ConfigurationManager.AppSettings["SMTPServer"].ToString().Trim();
            strEmailBody += extraInfo + "<br/><br/>";
            strEmailBody += "Message: " + ex.Message.ToString().Trim() + "<BR><BR>";
            strEmailBody += "Stack Trace: " + ex.StackTrace.ToString().Trim() + "<BR><BR>";
            if (strEmailToAddress.ToString().Trim().Length > 0 & strEmailSubject.ToString().Trim().Length > 0 && strEmailBody.ToString().Trim().Length > 0)
            {
                MailMessage msg = new MailMessage();
                foreach (string strToEmail in strEmailToAddress.Split(';'))  // put a semicolon between email addresses for which you want exception errors to go, in the .config.
                    msg.To.Add(strToEmail);
                msg.From = new MailAddress(strEmailFromAddress);
                msg.Subject = strEmailSubject;
                msg.Body = strEmailBody;
                msg.IsBodyHtml = true;
                bool emailNeedsSSL = false;
                if (!bool.TryParse(ConfigurationManager.AppSettings["EmailNeedsSSL"], out emailNeedsSSL))
                {
                    emailNeedsSSL = false;
                }
                SmtpClient smtp = new SmtpClient(strEmailSMTP);
                smtp.EnableSsl = emailNeedsSSL;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(strEmailUser, strEmailPassword);
                smtp.Credentials = SMTPUserInfo;
                smtp.Send(msg);
            }
        }

        /// <summary>
        ///  Logs an error into our Error File
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void LogError(string errorMessage)
        {
            string errorFile = ConfigurationManager.AppSettings["ErrorFileName"].ToString().Trim();

            File.AppendAllText(errorFile, "\n" + "SFTPFileTransfer Program" + "\n");
            File.AppendAllText(errorFile, "\n" + DateTime.Now.ToString() + "\n");
            File.AppendAllText(errorFile, errorMessage + "\n\n");
        }
    }
}
