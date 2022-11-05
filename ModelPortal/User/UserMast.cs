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
    /// <summary>
    ///   This class is used  for  get /post user master record form /to database  
    /// </summary>
    public class UserMast
    {
        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties
        // [Required(ErrorMessage = "Please select organization")]
        public int? PSLID
        {
            get { return _PSLID; }
            set { _PSLID = value; }
        }
        private int? _PSLID;

        public int? SLID
        {
            get { return _SLID; }
            set { _SLID = value; }
        }
        private int? _SLID;

        public string PSLDESC
        {
            get { return _PSLDESC; }
            set { _PSLDESC = value; }
        }
        private string _PSLDESC;

        public string SLDESC
        {
            get { return _SLDESC; }
            set { _SLDESC = value; }
        }
        private string _SLDESC;
        public int User
        {
            get { return _User; }
            set { _User = value; }
        }
        private int _User;
        //[Required(ErrorMessage = "Please select store")]
        public int PSLID_Store
        {
            get { return _PSLID_Store; }
            set { _PSLID_Store = value; }
        }
        private int _PSLID_Store;

        public int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        private int _USERID;

        public string LOGINID
        {
            get { return _LOGINID; }
            set { _LOGINID = value; }
        }
        private string _LOGINID;

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string LOGINID1
        {
            get { return _LOGINID1; }
            set { _LOGINID1 = value; }
        }
        private string _LOGINID1;

        [Required(ErrorMessage = "Please enter Passsword")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Max 20 characters allowed.")]
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
        }
        private string _PASSWORD;

        [DataType(DataType.Password)]
        //[Compare("PASSWORD", ErrorMessage = "Please enter Correct Password")]
        [Display(Name = "Password")]
        public string CONFIRMPASWD { get; set; }

        public bool ISDOMAIN
        {
            get { return _ISDOMAIN; }
            set { _ISDOMAIN = value; }
        }
        private bool _ISDOMAIN;

        [Required(ErrorMessage = "Please Select User Type")]
        [Display(Name = "User Type")]
        public int UTYPEID
        {
            get { return _UTYPEID; }
            set { _UTYPEID = value; }
        }
        private int _UTYPEID;


        public string UTYPE { get; set; }

        public string UGRPDESC
        {
            get { return _UGRPDESC; }
            set { _UGRPDESC = value; }
        }
        private string _UGRPDESC;

        [Required(ErrorMessage = "Please Select Time Zone")]
        [Display(Name = "Time Zone")]
        public int TMZONEID
        {
            get { return _TMZONEID; }
            set { _TMZONEID = value; }
        }
        private int _TMZONEID;

        public string TMZONEDESC
        {
            get { return _TMZONEDESC; }
            set { _TMZONEDESC = value; }
        }
        private string _TMZONEDESC;

        [Display(Name = "Active")]
        public bool ISACTIVE
        {
            get { return _ISACTIVE; }
            set { _ISACTIVE = value; }
        }
        private bool _ISACTIVE;

        public bool ISAUTH
        {
            get { return _ISAUTH; }
            set { _ISAUTH = value; }
        }
        private bool _ISAUTH;

        public int? HasRole
        {
            get { return _HasRole; }
            set { _HasRole = value; }
        }
        private int? _HasRole;


        public int AuthValID
        {
            get { return _AuthValID; }
            set { _AuthValID = value; }
        }
        private int _AuthValID;

        public string AuthDESC
        {
            get { return _AuthDESC; }
            set { _AuthDESC = value; }
        }
        private string _AuthDESC;

        public string FName
        {
            get { return _FName; }
            set { _FName = value; }
        }
        private string _FName;
        public string MName
        {
            get { return _MName; }
            set { _MName = value; }
        }
        private string _MName;
        public string LName
        {
            get { return _LName; }
            set { _LName = value; }
        }
        private string _LName;

        public string FULLNAME
        {
            get { return _FULLNAME; }
            set { _FULLNAME = value; }
        }
        private string _FULLNAME;

        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        private DateTime? _DOB;
        public string ProfImg
        {
            get { return _ProfImg; }
            set { _ProfImg = value; }
        }
        private string _ProfImg;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Address;


        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile number is not valid.")]
        public string MobileNumber { get; set; }

        #endregion

        public string ACTIVE { get; set; }

        public string ViewProfile { get; set; }
        public int Noofrecords { get; set; }
        public int NoOfPages { get; set; }
        public int PageSize { get; set; }
        #region Methods
        /// <summary>
        /// Method used for getting list of user details 
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="Flag">flag</param>
        /// <param name="EUSERID">E user id</param>
        /// <returns>user details  list</returns>
        public List<UserMast> GetUserDetails(int USERID, int Flag, int EUSERID)
        {
            xdoc = objxml.GetUserMast_CodeXml(USERID, Flag, EUSERID);
            dt = objbui.GetKloudqBusinessList(xdoc);

            List<UserMast> model = dt.AsEnumerable().Select(row =>
                         new UserMast
                         {

                             USERID = row.Field<int?>("USERID") ?? 0,
                             Email = row.Field<string>("Email") ?? "",
                             LOGINID = (USERID == 0) ? ("<label class='blue' style='cursor:pointer;' onclick=onUserClick(" + row.Field<int>("USERID") + ") > " + row.Field<string>("USERNAME") + " </label> ") : row.Field<string>("USERNAME"),
                             PASSWORD = row.Field<string>("PASSWORD"),
                             CONFIRMPASWD = row.Field<string>("PASSWORD"),
                             ISDOMAIN = row.Field<bool>("ISDOMAIN"),
                             UTYPEID = row.Field<int?>("UTYPEID") ?? 0,
                             UTYPE = row.Field<int>("UTYPEID") == 6 ? ("<label class='blue' style='cursor:pointer;' data-toggle='modal' data-target='.bs-example-modal-lg' onclick=\"onSetTargetClick(" + row.Field<int>("USERID") + ",'" + row.Field<string>("LOGINID") + "')\" > Set Target </label> ") : "",
                             UGRPDESC = row.Field<string>("UGRPDESC"),
                             //added by abhi MobileNo
                             MobileNumber = row.Field<string>("MobileNo"),
                             DOB = row.Field<DateTime?>("DOB"),
                             AuthValID = row.Field<int?>("AUTHLVLID") ?? 0,
                             TMZONEID = row.Field<int?>("TMZONEID") ?? 0,
                             SLID = row.Field<int?>("SLID") ?? 0,
                             AuthDESC = row.Field<string>("AuthDESC"),
                             SLDESC = row.Field<string>("SLDESC"),
                             TMZONEDESC = row.Field<string>("TMZONEDESC"),
                             FName = row.Field<string>("FNAME"),
                             MName = row.Field<string>("MNAME"),
                             LName = row.Field<string>("LNAME"),
                             FULLNAME = row.Field<string>("FNAME") + " " + row.Field<string>("MNAME") + " " + row.Field<string>("LNAME"),
                             ISACTIVE = row.Field<bool?>("ISACTIVE") ?? false,
                             ACTIVE = row.Field<bool>("ISACTIVE") == true ? "<i class='icon-ok bigger-110 green'></i>" : "<i class='icon-remove bigger-110 red'></i>",
                             ViewProfile = "<a  class='btn btn-link'  href='/Account/accprofile/?UserId=" + row.Field<int?>("USERID") + " '> View Profile  </a>",
                             User = row.Field<int?>("PARRENTID") ?? 0
                             //  HasRole = row.Field<int?>("HasRole") ?? 0
                         }).ToList();


            return model;
        }
        /// <summary>
        /// Method used for getting user  details record list
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="Flag">flag</param>
        /// <param name="EUSERID">E user id</param>
        /// <param name="Desc">description</param>
        /// <param name="PNo"></param>
        /// <returns>user details list</returns>
        public List<UserMast> GetUserJsonDetails(int USERID, int Flag, int EUSERID, string Desc, int PNo)
        {
            xdoc = objxml.GetJsonUserMast_CodeXml(USERID, Flag, EUSERID, Desc, PNo);
            dt = objbui.GetKloudqBusinessList(xdoc);

            List<UserMast> model = dt.AsEnumerable().Select(row =>
                         new UserMast
                         {
                             USERID = row.Field<int?>("USERID") ?? 0,
                             Email = row.Field<string>("Email") ?? "",
                             LOGINID = (USERID == 0) ? ("<label class='blue' style='cursor:pointer;' onclick=onUserClick(" + row.Field<int>("USERID") + ") > " + row.Field<string>("USERNAME") + " </label> ") : row.Field<string>("USERNAME"),
                             PASSWORD = row.Field<string>("PASSWORD"),
                             CONFIRMPASWD = row.Field<string>("PASSWORD"),
                             ISDOMAIN = row.Field<bool>("ISDOMAIN"),
                             UTYPEID = row.Field<int?>("UTYPEID") ?? 0,
                             UTYPE = row.Field<int>("UTYPEID") == 6 ? ("<label class='blue' style='cursor:pointer;' data-toggle='modal' data-target='.bs-example-modal-lg' onclick=\"onSetTargetClick(" + row.Field<int>("USERID") + ",'" + row.Field<string>("LOGINID") + "')\" > Set Target </label> ") : "",
                             UGRPDESC = row.Field<string>("UGRPDESC"),
                             //added by abhi MobileNo
                             MobileNumber = row.Field<string>("MobileNo"),
                             DOB = row.Field<DateTime?>("DOB"),
                             AuthValID = row.Field<int?>("AUTHLVLID") ?? 0,
                             TMZONEID = row.Field<int?>("TMZONEID") ?? 0,
                             SLID = row.Field<int?>("SLID") ?? 0,
                             AuthDESC = row.Field<string>("AuthDESC"),
                             SLDESC = row.Field<string>("SLDESC"),
                             TMZONEDESC = row.Field<string>("TMZONEDESC"),
                             FName = row.Field<string>("FNAME"),
                             MName = row.Field<string>("MNAME"),
                             LName = row.Field<string>("LNAME"),
                             FULLNAME = row.Field<string>("FNAME") + " " + row.Field<string>("MNAME") + " " + row.Field<string>("LNAME"),
                             ISACTIVE = row.Field<bool?>("ISACTIVE") ?? false,
                             ACTIVE = row.Field<bool>("ISACTIVE") == true ? "<i class='fa fa-check green'></i>" : "<i class='fa fa-trash red'></i>",//use for showing boolean value in ui form
                             ViewProfile = "<a class='btn btn-link'  href='/Account/accprofile/?UserId=" + row.Field<int?>("USERID") + " '> View Profile  </a>",// property used for convert value in click event
                             Noofrecords = row.Field<int?>("NUMBEROFROWS") ?? 0,
                             NoOfPages = row.Field<int>("NUMBEROFROWS") / Convert.ToInt32(row.Field<int>("PAGESIZE")),
                             PageSize = row.Field<int?>("PAGESIZE") ?? 0
                         }).ToList();


            return model;
        }
        /// <summary>
        /// Method used for filling time zone list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="TimeZoneID">Time Zone id</param>
        /// <returns> time zone list</returns>
        public List<SelectListItem> FillTimeZoneList(int Flag, int TimeZoneID)
        {
            xdoc = objxml.GetTIMEZONE_CodeXml(Flag, 0);
            dt = objbui.GetKloudqBusinessList(xdoc);

            List<SelectListItem> lst = (from D in dt.AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Value = D.Field<int>("TMZONEID").ToString(),
                                            Text = D.Field<string>("TMZONECODE").ToString() + " (" + D.Field<string>("DIFFFROMGMT").ToString() + ")",
                                            Selected = D.Field<int>("TMZONEID") == TimeZoneID ? true : false
                                        }).ToList();

            return lst;
        }

        /// <summary>
        /// Method used for Filling authuntication
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="AUTHLVLID">authuntication level id</param>
        /// <returns>Filling authuntication list</returns>
        public List<SelectListItem> FillAuth(int USERID, int AUTHLVLID)
        {
            xdoc = objxml.GetAuthValue(0, -1, USERID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<SelectListItem> lst = (from D in dt.AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Value = D.Field<int>("AUTHLVLID").ToString(),
                                            Text = D.Field<string>("DESCRIPTION").ToString(),
                                            Selected = D.Field<int>("AUTHLVLID") == AUTHLVLID ? true : false
                                        }).ToList();

            return lst;
        }
        /// <summary>
        /// Method used for filling user type list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="USERID">user id</param>
        /// <param name="UTYPEID">user type id</param>
        /// <returns>filling user type list</returns>
        public List<SelectListItem> FillUserTypeList(int Flag, int USERID, int UTYPEID)
        {
            xdoc = objxml.GetUserTypesUserWise_CodeXml(Flag, USERID, 0);
            dt = objbui.GetKloudqBusinessList(xdoc);

            List<SelectListItem> lst = (from D in dt.AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Value = D.Field<int>("UTYPEID").ToString(),
                                            Text = D.Field<string>("UGRPDESC").ToString(),
                                            Selected = D.Field<int>("UTYPEID") == UTYPEID ? true : false
                                        }).ToList();

            return lst;
        }

        #endregion

    }
}
