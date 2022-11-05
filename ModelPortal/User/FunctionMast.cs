using ModelPortal.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ModelPortal.User
{
    #region Properties

    #region FuctionMast
    /// <summary>
    ///   This class is used  for  get /post function master record form /to database  
    /// </summary>
    public class FunctionMast
    {
        public int FUNID
        {
            get { return _FUNID; }
            set { _FUNID = value; }
        }
        private int _FUNID = 0;

        public int SYSDOCID_FM
        {
            get { return _SYSDOCID_FM; }
            set { _SYSDOCID_FM = value; }
        }
        private int _SYSDOCID_FM = 0;

        [Required(ErrorMessage = "Please enter function code")]
        [Remote("IsExistFunction", "UserManagment", AdditionalFields = "FUNID,FUNCODE,DESCRIPTION", ErrorMessage = "Function code already exist")]
        [StringLength(6, ErrorMessage = "Function code should not be greater than 6 characters")]
        public string FUNCODE
        {
            get { return _FUNCODE; }
            set { _FUNCODE = value; }
        }
        private string _FUNCODE;


        [Required(ErrorMessage = "Please enter function description")]
        [Remote("IsExistFunction", "UserManagment", AdditionalFields = "FUNID,FUNCODE,DESCRIPTION", ErrorMessage = "Function description already exist")]
        [StringLength(60, ErrorMessage = "Function description should not be greater than 60 characters")]

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private string _DESCRIPTION;


        public string SYS_DOC_description
        {
            get { return _SYS_DOC_description; }
            set { _SYS_DOC_description = value; }
        }
        private string _SYS_DOC_description;

        public int SUBDOCID_FM
        {
            get { return _SUBDOCID_FM; }
            set { _SUBDOCID_FM = value; }
        }
        private int _SUBDOCID_FM = 0;

        public bool ISACTIVE
        {
            get { return _ISACTIVE; }
            set { _ISACTIVE = value; }
        }
        private bool _ISACTIVE;

        public List<FunMenuMap> Menulist
        {
            get { return _Menulist; }
            set { _Menulist = value; }
        }
        private List<FunMenuMap> _Menulist;

        public List<Actions> Actions
        {
            get { return _Actions; }
            set { _Actions = value; }
        }
        private List<Actions> _Actions;

        public List<SysDocRight> SUBDOCSList
        {
            get { return _SUBDOCSList; }
            set { _SUBDOCSList = value; }
        }
        private List<SysDocRight> _SUBDOCSList;

        public int HiddenfunID
        {
            get { return _HiddenfunID; }
            set { _HiddenfunID = value; }
        }
        private int _HiddenfunID;

        public int MODMENUID
        {
            get { return _MODMENUID; }
            set { _MODMENUID = value; }
        }
        private int _MODMENUID;

        public int MENUID
        {
            get { return _MENUID; }
            set { _MENUID = value; }
        }
        private int _MENUID;

        public string MENUCODE
        {
            get { return _MENUCODE; }
            set { _MENUCODE = value; }
        }
        private string _MENUCODE;

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        private string _ACTIVE;


        public string MENUDESC
        {
            get { return _MENUDESC; }
            set { _MENUDESC = value; }
        }
        private string _MENUDESC;

        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();

        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;
        /// <summary>
        /// method used for getting user function list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="FunctionID">function id</param>
        /// <returns>user function list</returns>
        public List<FunctionMast> GetFuctionList(int Flag, int FunctionID)
        {
            xdoc = objxml.GetFuctionList_CodeXML(Flag, FunctionID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<FunctionMast> model = dt.AsEnumerable().Select(row =>
                            new FunctionMast
                            {
                                FUNID = row.Field<int>("FUNID"),
                                FUNCODE = row.Field<string>("FUNCODE"),
                                DESCRIPTION = ("<label class='blue' style='cursor:pointer;' onclick=onFunCodeClick(" + row.Field<int>("FUNID") + ") > " + row.Field<string>("DESCRIPTION") + " </label> "),
                                ACTIVE = row.Field<bool>("ISACTIVE") == true ? "<i class='fa fa-check green'></i>" : "<i class='fa fa-trash red'></i>",
                                ISACTIVE = row.Field<bool>("ISACTIVE")
                            }).ToList();

            return model;

        }
        /// <summary>
        /// method used for view user function
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="FunID">function id</param>
        /// <param name="UserID">user id</param>
        /// <returns> view user function</returns>
        public FunctionMast ViewFunction(int Flag, int FunID, int UserID)
        {
            FunctionMast Obj = new FunctionMast();

            XElement GetFunXml = new XElement("GETTREE",
                new XElement("FUNID", FunID),
                new XElement("USERID", UserID)
                );
            xdoc = objxml.GetFunctionDetail_CodeXML(Flag, GetFunXml.ToString());

            string OutPutXML = objbui.GetXMLFormat(xdoc);
            List<FunMenuMap> EmptyMenuList = new List<FunMenuMap>();
            List<Actions> EmptyActionList = new List<Actions>();
            List<SysDocRight> emptySydocList = new List<SysDocRight>();



            Obj = (from a in XDocument.Parse(OutPutXML).Descendants("FUNCTIONALITY")
                   select new FunctionMast
                   {

                       FUNID = Convert.ToInt32(a.Element("FUNID").Value),
                       DESCRIPTION = Convert.ToString(a.Element("DESCRIPTION").Value),
                       FUNCODE = Convert.ToString(a.Element("FUNCODE").Value),
                       ISACTIVE = Convert.ToBoolean(Convert.ToByte(a.Element("ISACTIVE").Value)),
                       Menulist = a.Descendants("FUNCTIONMAPS").Any() ? (from m in a.Descendants("FUNCTIONMAPS")
                                                                         select new FunMenuMap
                                                                         {
                                                                             MENUID = Convert.ToInt32(m.Element("MODMENUID").Value),
                                                                             MENUDESC = m.Element("MENUNAME").Value,
                                                                             Actions_M = m.Descendants("ACTIONS").Any() ?
                                                                              (from act in m.Descendants("ACTIONS").Elements("TRANS")
                                                                               select new Actions
                                                                               {
                                                                                   DESCRIPTION = act.Element("ACTNM").Value
                                                                               }).ToList() : EmptyActionList,

                                                                             SYSDOCLIST = m.Descendants("SUBDOCS").Any() ?
                                                                              (from act in m.Descendants("SUBDOCS").Elements("TRANS")
                                                                               select new SysDocRight
                                                                               {
                                                                                   SYSDOCID = Convert.ToInt32(act.Element("SUBDOCID").Value),
                                                                                   description = act.Element("DESCRIPTION").Value
                                                                               }).ToList()
                                                                            : emptySydocList
                                                                         }).ToList()

                                  : EmptyMenuList,




                   }).FirstOrDefault();



            return Obj;
        }
    }

    #endregion

    #region FunctionMenuMap

    public class FunMenuMap
    {
        public int MENUID
        {
            get { return _MENUID; }
            set { _MENUID = value; }
        }
        private int _MENUID;

        public string MENUDESC
        {
            get { return _MENUDESC; }
            set { _MENUDESC = value; }
        }
        private string _MENUDESC;

        public int ACTIONID
        {
            get { return _ACTIONID; }
            set { _ACTIONID = value; }
        }
        private int _ACTIONID;

        public string ACTIONDESC
        {
            get { return _ACTIONDESC; }
            set { _ACTIONDESC = value; }
        }
        private string _ACTIONDESC;

        public int FUNMNUMAPID
        {
            get { return _FUNMNUMAPID; }
            set { _FUNMNUMAPID = value; }
        }
        private int _FUNMNUMAPID;

        public int FLAG
        {
            get { return _FLAG; }
            set { _FLAG = value; }
        }
        private int _FLAG;

        public List<Actions> Actions_M
        {
            get { return _Actions_M; }
            set { _Actions_M = value; }
        }
        private List<Actions> _Actions_M;


        public List<SysDocRight> SYSDOCLIST
        {
            get { return _SYSDOCLIST; }
            set { _SYSDOCLIST = value; }
        }
        private List<SysDocRight> _SYSDOCLIST;
    }

    #endregion

    #region SysDocRights

    public class SysDocRight
    {
        public int SYSDOCID
        {
            get { return _SYSDOCID; }
            set { _SYSDOCID = value; }
        }
        private int _SYSDOCID;


        public int SUBDOCID
        {
            get { return _SUBDOCID; }
            set { _SUBDOCID = value; }
        }
        private int _SUBDOCID;

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _description;

        public string Authorise
        {
            get { return _Authorise; }
            set { _Authorise = value; }
        }
        private string _Authorise;

        public string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }
        private string _Display;

        public string Edit
        {
            get { return _Edit; }
            set { _Edit = value; }
        }
        private string _Edit;


        public string ExportToExcel
        {
            get { return _ExportToExcel; }
            set { _ExportToExcel = value; }
        }
        private string _ExportToExcel;

        public string ExportToPDF
        {
            get { return _ExportToPDF; }
            set { _ExportToPDF = value; }
        }
        private string _ExportToPDF;

        public string New
        {
            get { return _New; }
            set { _New = value; }
        }
        private string _New;

        public string PrintPrev
        {
            get { return _PrintPrev; }
            set { _PrintPrev = value; }
        }
        private string _PrintPrev;

        public string Remove
        {
            get { return _Remove; }
            set { _Remove = value; }
        }
        private string _Remove;

        public int MODMENUID
        {
            get { return _MODMENUID; }
            set { _MODMENUID = value; }
        }
        private int _MODMENUID;
    }

    #endregion

    #region Actions

    public class Actions
    {
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

        public string ACTTYP
        {
            get { return _ACTTYP; }
            set { _ACTTYP = value; }
        }
        private string _ACTTYP;

        public string Authorise
        {
            get { return _Authorise; }
            set { _Authorise = value; }
        }
        private string _Authorise;

        public string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }
        private string _Display;

        public string Edit
        {
            get { return _Edit; }
            set { _Edit = value; }
        }
        private string _Edit;


        public string ExportToExcel
        {
            get { return _ExportToExcel; }
            set { _ExportToExcel = value; }
        }
        private string _ExportToExcel;

        public string ExportToPDF
        {
            get { return _ExportToPDF; }
            set { _ExportToPDF = value; }
        }
        private string _ExportToPDF;

        public string New
        {
            get { return _New; }
            set { _New = value; }
        }
        private string _New;

        public string PrintPrev
        {
            get { return _PrintPrev; }
            set { _PrintPrev = value; }
        }
        private string _PrintPrev;

        public string Remove
        {
            get { return _Remove; }
            set { _Remove = value; }
        }
        private string _Remove;

        public string Relese
        {
            get { return _Relese; }
            set { _Relese = value; }
        }
        private string _Relese;

        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        private string _Accept;

        public string Reject
        {
            get { return _Reject; }
            set { _Reject = value; }
        }
        private string _Reject;

        public string Hold
        {
            get { return _Hold; }
            set { _Hold = value; }
        }
        private string _Hold;

        public string ForceClose
        {
            get { return _ForceClose; }
            set { _ForceClose = value; }
        }
        private string _ForceClose;

        public string Amendment
        {
            get { return _Amendment; }
            set { _Amendment = value; }
        }
        private string _Amendment;

        public string DeliverySchedule
        {
            get { return _DeliverySchedule; }
            set { _DeliverySchedule = value; }
        }
        private string _DeliverySchedule;

        public string GeneratePO
        {
            get { return _GeneratePO; }
            set { _GeneratePO = value; }
        }
        private string _GeneratePO;

        public string Copy
        {
            get { return _Copy; }
            set { _Copy = value; }
        }
        private string _Copy;

        public string AuditTrail
        {
            get { return _AuditTrail; }
            set { _AuditTrail = value; }
        }
        private string _AuditTrail;

        public string ShowMRN
        {
            get { return _ShowMRN; }
            set { _ShowMRN = value; }
        }
        private string _ShowMRN;

        public string PassBill
        {
            get { return _PassBill; }
            set { _PassBill = value; }
        }
        private string _PassBill;

        public string UnpassBill
        {
            get { return _UnpassBill; }
            set { _UnpassBill = value; }
        }
        private string _UnpassBill;

        public string ShowPO
        {
            get { return _ShowPO; }
            set { _ShowPO = value; }
        }
        private string _ShowPO;

        public int FUNMNUMAPID
        {
            get { return _FUNMNUMAPID; }
            set { _FUNMNUMAPID = value; }
        }
        private int _FUNMNUMAPID;

        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private int _Flag;

        public List<SubActions> SubAction
        {
            get { return _SubAction; }
            set { _SubAction = value; }
        }
        private List<SubActions> _SubAction;

        public string ACTIONS
        {
            get { return _ACTIONS; }
            set { _ACTIONS = value; }
        }
        private string _ACTIONS;
    }

    #endregion

    #region SubAction

    public class SubActions
    {
        public int SubActionID
        {
            get { return _SubActionID; }
            set { _SubActionID = value; }
        }
        private int _SubActionID;

        public string SubAction
        {
            get { return _SubAction; }
            set { _SubAction = value; }
        }
        private string _SubAction;

        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private int _Flag;
    }

    #endregion

    #endregion

}
