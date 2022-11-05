using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelPortal
{
    public class MessageSending
    {
        public string SendOTPMessage(string Mobile, string OTP)
        {
            string message = HttpUtility.UrlEncode(OTP);
            string URL = System.Configuration.ConfigurationManager.AppSettings["MessageGatewayURL"].ToString();
            string Username = System.Configuration.ConfigurationManager.AppSettings["MessageUsername"].ToString();
            string Password = System.Configuration.ConfigurationManager.AppSettings["MessagePassword"].ToString();
            string Sender = System.Configuration.ConfigurationManager.AppSettings["MessageSenderId"].ToString();
            string sendSMSUri = string.Format(@"{0}?username={1}&password={2}&to={3}&text={4}&from={5}", URL, Username, Password, Mobile, message, Sender);
            string result = string.Empty;
            WebClient objWebClient = new WebClient();
            StreamReader sr = new StreamReader(objWebClient.OpenRead(sendSMSUri));
            result = sr.ReadToEnd();
            return result;
        }
    }
}
