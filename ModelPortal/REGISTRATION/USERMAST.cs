using DataAccessBusinessPortal;
using ModelPortal.BI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml.Linq;

namespace ModelPortal
{
    public class USERMAST
    {
        public string hdrandomseed { get; set; }
        public int REGISTRATION_ID { get; set; }
        public string USERNAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string PASSWORD_EXPIRY { get; set; }
        public string USER_TYPE { get; set; }
        public string MOBILEOTP { get; set; }
        public string EMAILOTP { get; set; }
        public string CREATEDDATETIME { get; set; }
        public string UPDATEDDATETIME { get; set; }
        public bool ISACTIVE { get; set; }
        public int PAGENO { get; set; }
        public int FLAG { get; set; }
        public int PAGESIZE { get; set; }
        public int TOTALROWS { get; set; }
        public int TOTALPAGES { get; set; }
        public int PAGECOUNT { get; set; }
        #region Methods
        VzahBusiness objbui = new VzahBusiness();
        EmailHelper mailSending = new EmailHelper();
        public List<USERMAST> GETUSERLIST(int Page, int flag,int REGISTRATION_ID, int UserId, string SEARCH)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Cloud9",
           new XElement("XsdName", ""),
           new XElement("ProcName", "USERMAST_g"),
           new XElement("pFLAG", flag),
           new XElement("pREGISTRATION_ID", REGISTRATION_ID),
           new XElement("pSEARCH", SEARCH), new XElement("pPAGENO", Page),
           new XElement("pUSERID", UserId)));
            USERMAST obj = new USERMAST();
            DataTable dt = objbui.GetCloud9BusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new USERMAST
                            {
                                REGISTRATION_ID = s.Field<int>("REGISTRATION_ID"),
                                USERNAME = s.Field<string>("USERNAME"),
                                USER_PASSWORD = s.Field<string>("USER_PASSWORD"),
                                MOBILE_NUMBER = s.Field<string>("MOBILE_NUMBER"),
                                EMAIL_ADDRESS = s.Field<string>("EMAIL_ADDRESS"),
                                EMAILOTP = s.Field<string>("EMAILOTP"),
                                MOBILEOTP = s.Field<string>("MOBILEOTP"),
                                USER_TYPE = s.Field<string>("USER_TYPE"),
                                PASSWORD_EXPIRY = s.Field<string>("PASSWORD_EXPIRY"),
                                CREATEDDATETIME = s.Field<string>("CREATEDDATETIME"),
                                UPDATEDDATETIME = s.Field<string>("UPDATEDDATETIME"),
                                PAGECOUNT = s.Field<int>("PAGECOUNT"),
                                PAGESIZE = s.Field<int>("PAGESIZE"),
                                TOTALPAGES = s.Field<int>("TOTALPAGES"),
                                TOTALROWS = s.Field<int>("TOTALROWS")
                            }).ToList();
            return dbResult;
        }
        public List<USERMAST> GetUserData(int Page, int flag,string UserName,string Password, int UserId, string SEARCH)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Cloud9",
           new XElement("XsdName", ""),
           new XElement("ProcName", "LoginDetails_g"),
           new XElement("pFLAG", flag),
           new XElement("pUserName", UserName), new XElement("pPassword", Password),
           new XElement("pSEARCH", SEARCH), new XElement("pPAGENO", Page),
           new XElement("pUSERID", UserId)));
            USERMAST obj = new USERMAST();
            DataTable dt = objbui.GetCloud9BusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new USERMAST
                            {
                                REGISTRATION_ID = s.Field<int>("REGISTRATION_ID"),
                                USERNAME = s.Field<string>("USERNAME"),
                                USER_PASSWORD = s.Field<string>("USER_PASSWORD"),
                                MOBILE_NUMBER = s.Field<string>("MOBILE_NUMBER"),
                                EMAIL_ADDRESS = s.Field<string>("EMAIL_ADDRESS"),
                                EMAILOTP = s.Field<string>("EMAILOTP"),
                                MOBILEOTP = s.Field<string>("MOBILEOTP"),
                                USER_TYPE = s.Field<string>("USER_TYPE"),
                                PASSWORD_EXPIRY = s.Field<string>("PASSWORD_EXPIRY"),
                                CREATEDDATETIME = s.Field<string>("CREATEDDATETIME"),
                                UPDATEDDATETIME = s.Field<string>("UPDATEDDATETIME"),
                                PAGECOUNT = s.Field<int>("PAGECOUNT"),
                                PAGESIZE = s.Field<int>("PAGESIZE"),
                                TOTALPAGES = s.Field<int>("TOTALPAGES"),
                                TOTALROWS = s.Field<int>("TOTALROWS")
                            }).ToList();
            return dbResult;
        }
        public List<USERMAST> GETLOGINDETAILS(int Page, int flag,string username, string password, int UserId, string SEARCH)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Vzah",
           new XElement("XsdName", ""),
           new XElement("ProcName", "LoginDetails_g"),
           new XElement("pFLAG", flag),
           new XElement("pUserName", username),
           new XElement("pPassword", password),
           new XElement("pSEARCH", SEARCH), new XElement("pPAGENO", Page),
           new XElement("pUSERID", UserId)));
            USERMAST obj = new USERMAST();
            DataTable dt = objbui.GetCloud9BusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new USERMAST
                            {
                                REGISTRATION_ID = s.Field<int>("USERID"),
                                USERNAME = s.Field<string>("UserName"),
                                USER_PASSWORD = s.Field<string>("UsrPassword"),
                                MOBILE_NUMBER = s.Field<string>("Mobile"),
                                EMAIL_ADDRESS = s.Field<string>("Email"), 
                                USER_TYPE = s.Field<string>("UserType"),
                                 
                            }).ToList();
            return dbResult;
        }

        public BIErrors SAVE(string Flag, USERMAST model, int UserId)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", ""),
            new XElement("Vzah",
            new XElement("XsdName", ""),
            new XElement("ProcName", "USERMAST_c"),
            new XElement("pREGISTRATION_ID", model.REGISTRATION_ID),
            new XElement("pUSERNAME", model.USERNAME),
            new XElement("pUSER_PASSWORD", model.USER_PASSWORD),
            new XElement("pMOBILE_NUMBER", model.MOBILE_NUMBER),
            new XElement("pEMAIL_ADDRESS", model.EMAIL_ADDRESS), 
            new XElement("pUSER_TYPE", "End User"),
            new XElement("pMOBILEOTP", model.MOBILEOTP),
            new XElement("pEMAILOTP", model.MOBILEOTP),
            new XElement("pFLAG", Flag),
            new XElement("pUSERID", UserId)));
            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, Flag);
            return err;
        }
        #endregion Methods

        #region OTP Generation

        public string OTPGENERATION()
        {
            string OTP = string.Empty;
            string numbers = "1234567890";
            string characters = numbers;

            for (int i = 0; i < 6; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (OTP.IndexOf(character) != -1);
                OTP += character;
            }
            return OTP;
        }
        #endregion OTP Generation

        #region Send OTP on mail to Regiter User

        public void SendMailOTPDetails(string OTP, string Mobile, string USERNAME, string ToEmail)
        {
         
            StreamReader FileReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/MailTemplates/OTP_Agent.html"));
            string BodyContent = FileReader.ReadToEnd();
            BodyContent = BodyContent.Replace("#OTP", OTP);
            BodyContent = BodyContent.Replace("#CustomerName", USERNAME);
            BodyContent = BodyContent.Replace("#Mobile", Mobile);
            FileReader.Close();
            FileReader.Dispose();
            string Subject = "Vzah OTP Details of " + USERNAME; 
            mailSending.OTPAgentMailSend(ToEmail, Subject, BodyContent);
        }

        #endregion Send OTP on mail to Regiter User
    }
}
