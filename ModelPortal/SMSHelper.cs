using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelPortal
{
    class SMSHelper
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Senderid { get; set; }
        public string UserMobileNo { get; set; }
        public int Message { get; set; }

        private static void Send(SMSHelper smsConfiguration)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://www.smsidea.co.in/sendsms.aspx?mobile=" + smsConfiguration.MobileNo + "&pass=" + smsConfiguration.Password + "&senderid=" + smsConfiguration.Senderid + " &to=" + smsConfiguration.UserMobileNo + " &msg=" + smsConfiguration.Message + " ");
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                StreamReader respStreamReader = new StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}
