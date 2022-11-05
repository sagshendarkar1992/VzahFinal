using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.User
{
    /// <summary>
    ///   This class is used  for  get /post user action record form /to database  
    /// </summary>
    public class UserActionModel
    {
        #region Properties

        public int Controlid { get; set; }
        public int id { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMGPATH { get; set; }
        public string ACTNM { get; set; }
        public string Controlname { get; set; }
        public string CLASS { get; set; }

        #endregion

        #region Methods

        XDocument xdoc;
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objKloudqBusiness = new DataAccessBusinessPortal.KloudqBusiness();
        UserMenuXml objCommonXML = new UserMenuXml();
        /// <summary>
        /// This method is used for getting user action record list
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="SysDocID"></param>
        /// <param name="id"></param>
        /// <param name="concateaction"></param>
        /// <param name="controlname"></param>
        /// <param name="ActGrp"></param>
        /// <param name="UserId"></param>
        /// <returns>User action record list</returns>
        public List<UserActionModel> getUserActionList(int moduleid, int SysDocID, int id, string concateaction, string controlname, int ActGrp, int UserId)
        {
            xdoc = objCommonXML.getUserActionXml(UserId, SysDocID, ActGrp);
            DataTable dt = objKloudqBusiness.GetKloudqBusinessList(xdoc);
            List<UserActionModel> res = null;

            res = (from s in dt.AsEnumerable()
                   select new UserActionModel
                   {
                       DESCRIPTION = s.Field<string>("DESCRIPTION").ToString(),
                       IMGPATH = s.Field<string>("IMGPATH").ToString(),
                       ACTNM = s.Field<string>("ACTNM").ToString() + "" + concateaction.Trim(),
                       Controlid = moduleid,
                       Controlname = controlname,
                       id = id,
                       CLASS = s.Field<string>("CLASS")
                   }).ToList();
            return res;
        }


        #region Check User Rights  Manually
        /// <summary>
        /// This method is used for Check User Rights  Manually
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SysDocID"></param>
        /// <param name="ActGrp"></param>
        /// <param name="Action"></param>
        /// <returns>true or false</returns>
        public static bool IsAllowed(int UserId, int SysDocID, int ActGrp, string Action)
        {
            try
            {
                DataAccessBusinessPortal.KloudqBusiness objKloudqBusiness = new DataAccessBusinessPortal.KloudqBusiness();
                UserMenuXml objCommonXML = new UserMenuXml();
                XDocument xdoc = objCommonXML.getUserActionXml(UserId, SysDocID, ActGrp);
                DataTable dt = objKloudqBusiness.GetKloudqBusinessList(xdoc);
                bool contains = dt.AsEnumerable()
                   .Any(row => Action.ToLower() == row.Field<string>("ACTNM").ToLower());

                return contains;
            }
            catch { return false; }
        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class TransactionAction
    {
        XDocument xdoc;
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objKloudqBusiness = new DataAccessBusinessPortal.KloudqBusiness();
        UserMenuXml objCommonXML = new UserMenuXml();

        #region Property

        public TransactionAction()
        {
        }
        public int SYSDOCID
        {
            get { return _SYSDOCID; }
            set { _SYSDOCID = value; }
        }
        private int _SYSDOCID;

        public int DOCID
        {
            get { return _DOCID; }
            set { _DOCID = value; }
        }
        private int _DOCID;

        public int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        private int _USERID;

        public int ACTGRP
        {
            get { return _ACTGRP; }
            set { _ACTGRP = value; }
        }
        private int _ACTGRP;

        public int MID
        {
            get { return _MID; }
            set { _MID = value; }
        }
        private int _MID;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id;

        public int ACTIONID
        {
            get { return _ACTIONID; }
            set { _ACTIONID = value; }
        }
        private int _ACTIONID;

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private string _DESCRIPTION;

        public string IMGPATH
        {
            get { return _IMGPATH; }
            set { _IMGPATH = value; }
        }
        private string _IMGPATH;

        public string ACTNM
        {
            get { return _ACTNM; }
            set { _ACTNM = value; }
        }
        private string _ACTNM;

        public string CONTROLNAME
        {
            get { return _CONTROLNAME; }
            set { _CONTROLNAME = value; }
        }
        private string _CONTROLNAME;

        public bool ISBASIC
        {
            get { return _ISBASIC; }
            set { _ISBASIC = value; }
        }
        private bool _ISBASIC;

        public string SCRIPT
        {
            get { return _SCRIPT; }
            set { _SCRIPT = value; }
        }
        private string _SCRIPT;

        public string CLASS
        {
            get { return _CLASS; }
            set { _CLASS = value; }
        }
        private string _CLASS;

        #endregion

        #region Method
        /// <summary>
        /// This method is used for getting user transaction action record list
        /// </summary>
        /// <param name="SYSDOCID"></param>
        /// <param name="DOCID"></param>
        /// <param name="MID"></param>
        /// <param name="ACTGRP"></param>
        /// <param name="TRANSSTATUS"></param>
        /// <param name="ISAUTHO"></param>
        /// <param name="UserId"></param>
        /// <returns>User transaction action record list</returns>
        public List<TransactionAction> ReturnTransActList(int SYSDOCID, int DOCID, int MID, int ACTGRP, int TRANSSTATUS, int ISAUTHO, int UserId)
        {
            xdoc = objCommonXML.GetTransactionActionList_CodeXml(SYSDOCID, DOCID, MID, ACTGRP, TRANSSTATUS, ISAUTHO, UserId);
            DataTable dt = objKloudqBusiness.GetKloudqBusinessList(xdoc);

            var Result = dt.AsEnumerable().Select(a =>
                new TransactionAction
                {
                    ACTIONID = a.Field<int>("ACTIONID") == null ? 0 : a.Field<int>("ACTIONID"),
                    SYSDOCID = a.Field<int>("SYSDOCID") == null ? 0 : a.Field<int>("SYSDOCID"),
                    DOCID = a.Field<int>("DOCID") == null ? 0 : a.Field<int>("DOCID"),
                    ACTGRP = a.Field<int>("ACTGRP") == null ? 0 : a.Field<int>("ACTGRP"),
                    MID = a.Field<int>("MID") == null ? 0 : a.Field<int>("MID"),
                    CONTROLNAME = a.Field<string>("CONTROLNAME") == null ? "" : a.Field<string>("CONTROLNAME"),
                    DESCRIPTION = a.Field<string>("DESCRIPTION") == null ? "" : a.Field<string>("DESCRIPTION"),
                    IMGPATH = a.Field<string>("IMGPATH") == null ? "" : a.Field<string>("IMGPATH"),
                    ACTNM = a.Field<string>("ACTNM") == null ? "" : a.Field<string>("ACTNM"),
                    ISBASIC = a.Field<bool>("ISBASIC") == null ? false : a.Field<bool>("ISBASIC"),
                    SCRIPT = a.Field<string>("SCRIPT") == null ? "" : a.Field<string>("SCRIPT"),
                    CLASS = a.Field<string>("class") == null ? "" : a.Field<string>("class")
                }).ToList();

            return Result;
        }

        #endregion
    }
    /// <summary>
    ///   This class is used  for  get /post user  Trans Validation record form /to database  
    /// </summary>
    public class TransValidation
    {
        XDocument xdoc;
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objKloudqBusiness = new DataAccessBusinessPortal.KloudqBusiness();
        UserMenuXml objCommonXML = new UserMenuXml();

        #region Property

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _Remark;


        public int DOCID
        {
            get { return _DOCID; }
            set { _DOCID = value; }
        }
        private int _DOCID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _ID;

        public string MSG
        {
            get { return _MSG; }
            set { _MSG = value; }
        }
        private string _MSG;

        public string CONTROLNAME
        {
            get { return _CONTROLNAME; }
            set { _CONTROLNAME = value; }
        }
        private string _CONTROLNAME;


        public int MID
        {
            get { return _MID; }
            set { _MID = value; }
        }
        private int _MID;

        public int TID
        {
            get { return _TID; }
            set { _TID = value; }
        }
        private int _TID;

        public string ACTION
        {
            get { return _ACTION; }
            set { _ACTION = value; }
        }
        private string _ACTION;

        public bool LINEWISE
        {
            get { return _LINEWISE; }
            set { _LINEWISE = value; }
        }
        private bool _LINEWISE;

        #endregion

        #region Method
        /// <summary>
        /// This method is used for get scheme  record in list  from data base
        /// </summary>
        /// <param name="DOCID"></param>
        /// <param name="MID"></param>
        /// <param name="ActionID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public object ValidateTrans(int DOCID, int MID, int ActionID, int UserID)
        {
            XDocument xdoc;
            TransValidation obj = new TransValidation();
            if (ActionID == 3)
            {
                //TransactionParameters objParam = (TransactionParameters)Session["XmlParameter"];
                //SysDataFlag objSysData = new SysDataFlag();
                //objSysData.FLAG = "D";
                //objSysData.ACT = "S";
                //objSysData.PRIMARYKEY = MID;
                //objSysData.SYSDOCID = objParam.SYSDOCID;
                //objSysData.DOCID = DOCID;
                //objSysData.VIEWID = objParam.VIEWID;
                //xdoc = objSalesOrderXml.AddEditXmlTrans(objSysData, objParam);              
                //xdoc = objCommonXML.TransValidation_CodeXml(DOCID, MID, 0, ActionID, 0, UserID);
                xdoc = objCommonXML.TransValidationNew_CodeXml(DOCID, MID, 0, UserID);
            }
            else
            {
                xdoc = objCommonXML.TransValidation_CodeXml(DOCID, MID, 0, ActionID, 0, UserID);
            }
            string outxml = objKloudqBusiness.InsertTransXml(xdoc);
            XDocument XdocMastData = new XDocument(); XdocMastData = XDocument.Parse(outxml);
            List<TransValidation> query = new List<TransValidation>();
            if (ActionID != 3)
            {
                query = (from a in XdocMastData.Descendants("SYSMSGS")
                         select new TransValidation
                         {
                             ID = Convert.ToInt32(a.Element("ID").Value),
                             MSG = Convert.ToString(a.Element("MSG").Value),
                             CONTROLNAME = a.Element("CONTROLNAME") != null ? Convert.ToString(a.Element("CONTROLNAME").Value) : "",
                             ACTION = a.Element("ACTIONNAME") != null ? Convert.ToString(a.Element("ACTIONNAME").Value) : "",
                         }).ToList();
            }
            else
            {
                query = (from a in XdocMastData.Descendants("SYSMSGS")
                         select new TransValidation
                         {
                             ID = Convert.ToInt32(a.Element("ID").Value),
                         }).ToList();

                if (query.FirstOrDefault().ID < 0)
                {
                    query = (from a in XdocMastData.Descendants("SYSMSGS")
                             select new TransValidation
                             {
                                 ID = Convert.ToInt32(a.Element("ID").Value),
                                 MSG = Convert.ToString(a.Element("ERRORMSGS").Value)
                             }).ToList();
                }
            }
            if (query.FirstOrDefault().ID > 0)
            {
                //SessionManagment objSessMgmt = new SessionManagment();
                //objSessMgmt.ClearAllSalesOrderSessions();
            }
            var getall = new
            {
                ID = query.Select(p => p.ID),
                Msg = query.Select(p => p.MSG),
                ControlNm = query.Select(p => p.CONTROLNAME),
                Action = query.Select(p => p.ACTION)
            };
            return getall;
        }
        /// <summary>
        /// This method is used for get user Trans Set Status Action  record in list  
        /// </summary>
        /// <param name="DOCID"></param>
        /// <param name="SYSDOCID"></param>
        /// <param name="MID"></param>
        /// <param name="ACTIONID"></param>
        /// <param name="UserID"></param>
        /// <returns>user Trans Set Status Action  record in list  </returns>

        public object TransSetStatusAction(int DOCID, int SYSDOCID, int MID, int ACTIONID, int UserID, string Reason = "")
        {
            XDocument xdoc;
            TransSetStatusAction obj = new TransSetStatusAction();
            xdoc = objCommonXML.TransSetStatusAction_CodeXml(DOCID, SYSDOCID, MID, ACTIONID, UserID, Reason);
            string outxml = objKloudqBusiness.InsertTransXml(xdoc);
            XDocument XdocMastData = new XDocument();
            XdocMastData = XDocument.Parse(outxml);
            List<TransSetStatusAction> query = new List<TransSetStatusAction>();
            query = (from a in XdocMastData.Descendants("TEMPID")
                     select new TransSetStatusAction
                     {
                         ID = Convert.ToInt32(a.Element("ID").Value),
                         DOCID = Convert.ToInt32(a.Element("DOCID").Value),
                         SYSDOCID = Convert.ToInt32(a.Element("SYSDOCID").Value),
                         ERRDESC = Convert.ToString(a.Element("ERRDESC").Value),
                     }).ToList();

            if (query.FirstOrDefault().ID > 0)
            {
                //SessionManagment objSessMgmt = new SessionManagment();
                //objSessMgmt.ClearAllSalesOrderSessions();
            }
            var getall = new
            {
                ID = query.Select(p => p.ID),
                Msg = query.Select(p => p.ERRDESC)
            };
            return getall;
        }
        #endregion
    }
    /// <summary>
    /// This class is used for access user Trans Set Status Action  record 
    /// </summary>
    public class TransSetStatusAction
    {
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _ID;

        public int DOCID
        {
            get { return _DOCID; }
            set { _DOCID = value; }
        }
        private int _DOCID;

        public int ACTIONID
        {
            get { return _ACTIONID; }
            set { _ACTIONID = value; }
        }
        private int _ACTIONID;

        public int SYSDOCID
        {
            get { return _SYSDOCID; }
            set { _SYSDOCID = value; }
        }
        private int _SYSDOCID;

        public string ERRDESC
        {
            get { return _ERRDESC; }
            set { _ERRDESC = value; }
        }
        private string _ERRDESC;
    }
    /// <summary>
    /// This class is used for access user Trans Set Status Action  record  xml
    /// </summary>
     
}
