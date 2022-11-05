using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ModelPortal
{
    public class EmailHelper
    {
        #region Send OTP Mail

        public string OTPAgentMailSend(string ToEmailId, string FormalSubject, string BodyContent)
        {

            System.Net.Mail.MailMessage msgVzah = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            string UserID = string.Empty;
            string Password = string.Empty;
            try
            {
                string To = ToEmailId;
                string From = System.Configuration.ConfigurationManager.AppSettings["FromEmailIDVzah"].ToString();
                msgVzah.From = new System.Net.Mail.MailAddress(From, "Vzah");
                UserID = System.Configuration.ConfigurationManager.AppSettings["EmailIDVzah"].ToString();
                Password = System.Configuration.ConfigurationManager.AppSettings["PasswordVzah"].ToString();
                msgVzah.To.Add(To);
                msgVzah.Subject = FormalSubject;
                msgVzah.Body = BodyContent;
                msgVzah.IsBodyHtml = true; 
                smtpClient.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"].ToString());
                smtpClient.Host = System.Configuration.ConfigurationManager.AppSettings["EmailHost"].ToString();
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(UserID, Password);
                smtpClient.Send(msgVzah);

                msgVzah.Dispose();
                smtpClient.Dispose();

                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                msgVzah.Dispose();
                smtpClient.Dispose();
            }
        }
        #endregion Send OTP Mail
    }
}
