using ModelPortal;
using ModelPortal.BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vzah.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace Vzah.Controllers
{
    //[Authorize]
    public class UserMastController : Controller
    {
        USERMAST daccess = new USERMAST();
        LoginSessionDetails objSessLog = new LoginSessionDetails();
        MessageSending objMessageSending = new MessageSending();
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ADDUSERMAST(int REGISTRATION_ID = 0)
        {
            var userId = User.Identity.GetUserName();
            USERMAST obj = new USERMAST();
            obj.REGISTRATION_ID = REGISTRATION_ID;
            if (REGISTRATION_ID > 0)
            {
                obj = daccess.GETUSERLIST(1, 0, REGISTRATION_ID, 0, "").FirstOrDefault();
            }
            obj.REGISTRATION_ID = REGISTRATION_ID;
            return View(obj);
        }
        [ValidateInput(false)]
        public ActionResult CHECKIFDATAEXIST(string MOBILE_NUMBER, string USERNAME, int REGISTRATION_ID)
        {
            BIErrors err = new BIErrors();
            USERMAST obj = new USERMAST();
            obj.MOBILE_NUMBER = MOBILE_NUMBER;
            obj.REGISTRATION_ID = REGISTRATION_ID;
            obj.USERNAME = USERNAME;
            err = daccess.SAVE("C", obj, 0);
            if (err.errorID > 0)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
        [ValidateInput(false)]
        public ActionResult CheckUserNameExists(string USERNAME, int REGISTRATION_ID)
        {
            BIErrors err = new BIErrors();
            USERMAST obj = new USERMAST();
            obj.REGISTRATION_ID = REGISTRATION_ID;
            obj.USERNAME = USERNAME;
            err = daccess.SAVE("USERNAME", obj, 0);
            if (err.errorID > 0)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult VIEWUSERMAST(int REGISTRATION_ID = 0)
        {
            USERMAST obj = new USERMAST();
            obj.REGISTRATION_ID = REGISTRATION_ID;
            if (REGISTRATION_ID > 0)
            {
                obj = daccess.GETUSERLIST(1, 0, REGISTRATION_ID, 0, "").FirstOrDefault();
            }
            obj.REGISTRATION_ID = REGISTRATION_ID;
            return View(obj);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult DELETEUSERMAST(int REGISTRATION_ID = 0)
        {
            USERMAST obj = new USERMAST();
            obj.REGISTRATION_ID = REGISTRATION_ID;
            if (REGISTRATION_ID > 0)
            {
                obj = daccess.GETUSERLIST(1, 0, REGISTRATION_ID, 0, "").FirstOrDefault();
            }
            obj.REGISTRATION_ID = REGISTRATION_ID;
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult ADDUSERMAST(USERMAST obj, string submit = "")
        {
            BIErrors err = new BIErrors();
            try
            {
                if (submit == "Save")
                {
                    err = daccess.SAVE(obj.REGISTRATION_ID > 0 ? "E" : "I", obj, 0);
                }
                else
                {
                    err.errorID = 1; err.flag = "OTP";
                    err.errorMsg = "Processed for OTP generation and validation";
                }
            }
            catch (Exception ex)
            {
                err.errorID = 0;
                err.errorMsg = "Error ocuured during update," + ex.Message;
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }
        public string HashSh1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                // declare stringbuilder
                var sb = new StringBuilder(hashSh1.Length * 2);

                // computing hashSh1
                foreach (byte b in hashSh1)
                {
                    // "x2"
                    sb.Append(b.ToString("X2").ToLower());
                }

                return sb.ToString();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult LOGINUSERMAST(USERMAST obj, string returnUrl)
        {
            BIErrors err = new BIErrors();
            try
            {
                var username = AESEncryption.DecryptStringAES(obj.USERNAME);
                var password = AESEncryption.DecryptStringAES(obj.USER_PASSWORD);
                obj = daccess.GETLOGINDETAILS(1, 0, username, password, 0, "").FirstOrDefault();
                returnUrl = (returnUrl == "/Account/ADDUSERMAST" ? "" : returnUrl);
                if (obj != null)
                {
                    //--------Start of cookie declaration
                    var userencrypt = string.Join(",", obj.USERNAME, obj.REGISTRATION_ID, obj.USER_TYPE, obj.USER_TYPE, 0);
                    var cookieText = Encoding.UTF8.GetBytes(userencrypt);
                    var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, "ProtectCookie"));
                    string ss = HashSh1(obj.USER_PASSWORD);
                    //--- Create cookie object and pass name of the cookie and value to be stored.
                    HttpCookie cookieObject = new HttpCookie("Vzah_user", encryptedValue);
                    Response.Cookies.Add(cookieObject);
                    Response.Cookies["Vzah_user"].Expires = DateTime.Now.AddHours(10);
                    //--------End Start of cookie declaration
                    objSessLog.UserName = obj.USERNAME;
                    objSessLog.UserId = obj.REGISTRATION_ID;
                    //objSessLog.ProfileImage = results.ProfilePath;
                    objSessLog.Roles = obj.USER_TYPE;
                    objSessLog.USERTYPEStr = obj.USER_TYPE;
                    Session["SessionInformation"] = objSessLog;
                    err.errorID = obj.REGISTRATION_ID; err.flag = "LOGIN";
                    err.errorMsg = "Login Success";
                    objSessLog.RedirectURL = obj.USER_TYPE == "SuperAdmin" ? "/Admin/Dashboard/Index" : "/Transaction/Index";
                    err.REDIRECTURL = obj.USER_TYPE == "SuperAdmin" ? "/Admin/Dashboard/Index" : "/Transaction/Index";
                }
                else
                {
                    err.errorID = 0;
                    err.errorMsg = "Login Failed, Please check user name and password entered";
                }
            }
            catch (Exception ex)
            {
                err.errorID = 0;
                err.errorMsg = "Error ocuured during update," + ex.Message;
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult USERMASTDELETE(int REGISTRATION_ID)
        {
            USERMAST obj = new USERMAST();
            obj.REGISTRATION_ID = REGISTRATION_ID;
            BIErrors err = new BIErrors();
            try
            {
                err = daccess.SAVE("D", obj, 0);
            }
            catch (Exception ex)
            {
                err.errorID = 0;
                err.errorMsg = "Error ocuured during Delete.";
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public ActionResult GETUSERMAST(int FLAG = 1, int REGISTRATION_ID = 0, int PAGENO = 1)
        {
            USERMAST obj = new USERMAST();
            obj.FLAG = FLAG; obj.REGISTRATION_ID = REGISTRATION_ID; obj.PAGENO = PAGENO;
            return View(obj);
        }
        [ValidateInput(false)]
        public JsonResult GETUSERMASTLIST(int Page = 1, int Flag = 0, int REGISTRATION_ID = 0, string SEARCH = "")
        {
            var dbResult = daccess.GETUSERLIST(Page, Flag, REGISTRATION_ID, 0, SEARCH);
            return Json(new { aaData = dbResult }, JsonRequestBehavior.AllowGet);

        }
        [ValidateInput(false)]
        public JsonResult VIEWUSERMAST(int Page = 1, int Flag = 0, int REGISTRATION_ID = 0)
        {
            var dbResult = daccess.GETUSERLIST(Page, Flag, REGISTRATION_ID, 0, "");
            return Json(new { aaData = dbResult }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendOTP(string MOBILE_NUMBER, string USERNAME, string EMAIL_ADDRESS)
        {
            string OTP = daccess.OTPGENERATION();
            TempData["OTP"] = OTP;
            TempData.Keep("OTP");
            string content = "Vzah Verification code : " + OTP + " .OTP is valid for " + Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OTPExpirationMinutes"]) + " minutes";
            //objMessageSending.SendOTPMessage(MOBILE_NUMBER, content); 
            DateTime OTPSendTime = DateTime.Now;
            TempData["VzahOTPSendTime"] = OTPSendTime;
            TempData.Keep("VzahOTPSendTime");
            //daccess.SendMailOTPDetails(OTP, MOBILE_NUMBER, USERNAME, EMAIL_ADDRESS);
            return Json(OTP, JsonRequestBehavior.AllowGet);
        }
        #region Verify OTP

        public JsonResult VerifyOTP(string OTP)
        {
            string Data = string.Empty;
            string ActualOTP = string.Empty;

            if (TempData.Peek("OTP") != null)
            {
                ActualOTP = TempData.Peek("OTP").ToString();
            }

            if (TempData.Peek("VzahOTPSendTime") != null)
            {
                DateTime SendTime = (DateTime)TempData.Peek("VzahOTPSendTime");
                int OTPExpirationMinutes = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OTPExpirationMinutes"]);
                DateTime OTPExpTime = SendTime.AddMinutes(OTPExpirationMinutes);
                TimeSpan span = DateTime.Now.Subtract(SendTime);
                int SpanMinutes = span.Minutes;
                if (SpanMinutes > OTPExpirationMinutes)
                {
                    TempData["OTP"] = null;
                    TempData["VzahOTPSendTime"] = null;
                    Data = "OTPExpired";
                }
                else
                {
                    if (OTP == ActualOTP)
                    {
                        Data = "Verified";
                    }
                    else
                    {
                        Data = "Invalid";
                    }
                }
            }
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        #endregion Verify OTP
    }
}