using ModelPortal.BI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ModelPortal.Account
{
    /// <summary>
    ///   This class is used  for  get /post user profile record form /to database  
    /// </summary>
    public class UserProfile
    {
        XDocument xdoc;

        /// <summary>
        /// this is used for access xml data from UserManagementXml  class and its finctionlality
        /// </summary>
        UserManagementXml objxml = new UserManagementXml();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties


        public int? SLID
        {
            get { return _SLID; }
            set { _SLID = value; }
        }
        private int? _SLID;

        public int CommID { get; set; }

        public int FMembID { get; set; }


        public string SLDESC
        {
            get { return _SLDESC; }
            set { _SLDESC = value; }
        }
        private string _SLDESC;

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

        public string IMGDESC
        {
            get { return _IMGDESC; }
            set { _IMGDESC = value; }
        }
        private string _IMGDESC;


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


        public string NAME { get; set; }

        public string MobileNo { get; set; }
        public int OTPNo { get; set; }
        public string Usrsetflag { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        #endregion

        #region Security

        #region PROPERTIES
        //  QID QUESTION ANSID ANSWER USERID USERNAME ISACTIVE CREATEDON
        public int SQId { get; set; }//question id
        public int SAnsId { get; set; }//answer id
        public int SUserId { get; set; }
        public string SQuestion { get; set; }//question
        public string SAnswer { get; set; }//answer
        public string SUserName { get; set; }
        public Boolean SIsActive { get; set; }
        public DateTime SCREATEDON { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Method used for filling sequerity question dropdown list
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Qid">question id</param>
        /// <returns>get security All Question</returns>
        public List<UserProfile> GetSecirityAllQ(int flag, int Qid)
        {


            UserProfile obj = new UserProfile();

            xdoc = objxml.GetAllSqcurityQ(flag, Qid);



            DataTable dt = objbui.GetKloudqBusinessList(xdoc);

            var dbResult = (from s in dt.AsEnumerable()
                            select new UserProfile
                            {
                                // 	QID QUESTION            
                                SQId = s.Field<int>("QID"),
                                SQuestion = s.Field<string>("QUESTION")
                            }).ToList();
            return dbResult;
        }
        /// <summary>
        /// Method used for getting list of sequerity question
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Ansid">answer id</param>
        /// <param name="Userid">user id</param>
        /// <returns>getting list of sequerity question</returns>
        public UserProfile GetUSecirityAllQ(int flag, int Ansid, int Userid)
        {

            UserProfile obj = new UserProfile();

            xdoc = objxml.GetUSecurityQ(flag, Ansid, Userid);



            DataTable dt = objbui.GetKloudqBusinessList(xdoc);

            var dbResult = (from s in dt.AsEnumerable()
                            select new UserProfile
                            {
                                // QID QUESTION ANSID ANSWER USERID USERNAME ISACTIVE CREATEDON

                                SQId = s.Field<int>("QID"),
                                SQuestion = s.Field<string>("QUESTION"),
                                SAnsId = s.Field<int>("ANSID"),
                                SAnswer = s.Field<string>("ANSWER"),
                                SUserId = s.Field<int>("USERID"),
                                SUserName = s.Field<string>("USERNAME"),
                                SIsActive = s.Field<Boolean>("ISACTIVE"),
                                SCREATEDON = s.Field<DateTime>("CREATEDON"),
                            }).FirstOrDefault();
            return dbResult;
        }
        /// <summary>
        /// Method used for insert security question
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="AnsId">answer id</param>
        /// <param name="Qid">question id</param>
        /// <param name="Ans">answer</param>
        /// <param name="userid">user id</param>
        /// <return> return BI error message </returns>
        public BIErrors SaveSecurityQ(string Flag, int AnsId, int Qid, string Ans, int userid)
        {

            //code is only for Insert & not for Edit As per requirement
            if (Flag == "I") //flag I used for insert the record
            {
                xdoc = objxml.SetSECURITYQ(Flag, AnsId, Qid, Ans, userid);
            }
            //  xdoc = objxml.ANNOUNCEMENTS_c_CodeXml(Flag, model.AId, model.ATYPID, userid, model.Subject, model.Body, Convert.ToDateTime(model.StartDate), Convert.ToDateTime(model.EndDate), model.ForGroup, model.ForAgents, model.ForCustomer, model.SmsNotify, model.EmailNotify, model.SendBySms);



            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, Flag);
            return err;

        }

        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// Method used for getting user profile details
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <param name="FamMeembId">family member id</param>
        /// <param name="DESC">description</param>
        /// <param name="PUserid"></param>
        /// <returns>getting user profile details </returns>
        public UserProfile GetProfileDetails(int UserId, int SLID, int FamMeembId, string DESC, int PUserid)
        {

            xdoc = objxml.GetUserProfileDetails(UserId, SLID, FamMeembId, DESC, PUserid);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<UserProfile> model = dt.AsEnumerable().Select(row =>
                        new UserProfile
                        {

                            USERID = row.Field<int?>("USERID") ?? 0,
                            Email = row.Field<string>("Email") ?? "",
                            LOGINID = row.Field<string>("LOGINID"),
                            UTYPEID = row.Field<int?>("UTYPEID") ?? 0,
                            UGRPDESC = row.Field<string>("UGRPDESC"),
                            TMZONEID = row.Field<int?>("TMZONEID") ?? 0,
                            SLID = row.Field<int?>("SLID") ?? 0,
                            SLDESC = row.Field<string>("SLDESC"),
                            TMZONEDESC = row.Field<string>("TMZONEDESC"),
                            IMGDESC = row.Field<string>("IMGDESC"),
                            NAME = row.Field<string>("NAME"),
                            MobileNo = row.Field<string>("MobileNo"),
                            Longitude = row.Field<string>("Longitude"),
                            Latitude = row.Field<string>("Latitude")
                            //                     public string Longitude { get; set; }
                            //public string Latitude { get; set; }
                        }).ToList();

            return model.FirstOrDefault();
        }
        /// <summary>
        /// Method used for update user setting
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Value">value</param>
        /// <param name="Uid">user id</param>
        /// <param name="CODE">code</param>
        /// <return> return BI error message </returns>
        public BIErrors UpdateUsrsetting(int flag, string Value, int Uid, string CODE)
        {


            xdoc = objxml.USERSETTINGS_c_CodeXml(flag, Value, Uid, CODE);
            dt = objbui.GetKloudqBusinessList(xdoc);


            BIErrors err = new BIErrors();

            XmlDocument xmldoc = new XmlDocument();
            //xmldoc.LoadXml((dt.Rows[0].ItemArray[0]).ToString());
            err.ReadBIErrors((dt.Rows[0].ItemArray[0]).ToString());

            //  BIErrors err = new BIErrors(Statuchk, "E");
            return err;
        }

        #endregion

        #region ResetPassword

        #region Properties
        //    EXEC PORTALRESETPW_C @pFlag = '1', -- char(1)
        //@pQID = 0, -- int
        //@pANSWER = '', -- varchar(255)
        //@pEmail = '' -- varchar(255)
        public int RPFlag { get; set; }
        public int RPQid { get; set; }
        public string RPAns { get; set; }
        public string RPEmail { get; set; }
        #endregion

        #region Mehods
        /// <summary>
        /// Method used for reset user password
        /// </summary>
        /// <param name="qid">question id</param>
        /// <param name="ans">answer</param>
        /// <param name="email">email id</param>
        /// <param name="flag">flag</param>
        /// <return> return BI error message </returns>
        public BIErrors ResetPass(int qid, string ans, string email, string flag)
        {
            //code is only for Insert & not for Edit As per requirement
            xdoc = objxml.ResetPw(flag, qid, ans, email);
            //  xdoc = objxml.ANNOUNCEMENTS_c_CodeXml(Flag, model.AId, model.ATYPID, userid, model.Subject, model.Body, Convert.ToDateTime(model.StartDate), Convert.ToDateTime(model.EndDate), model.ForGroup, model.ForAgents, model.ForCustomer, model.SmsNotify, model.EmailNotify, model.SendBySms);
            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, "I");
            return err;
        }

        #endregion

        #endregion

        #region ChangePassword

        #region Properties
        //ChangePass
        //    EXEC PortalPWChange_c @pUSERID = 40, -- int
        //@pOLDPASWD = 'admin1', -- varchar(120)
        //@pNEWPASWD = 'admin1', -- varchar(20)
        //@pType = 'change' -- varchar(20)

        public string ChOldPassW { get; set; }
        public string ChNewPassW { get; set; }
        public string ChType { get; set; }
        public string ChUserId { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Method used for change user password
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="oldpassw">old password</param>
        /// <param name="newpass">new password</param>
        /// <param name="type">user type</param>
        /// <return> return BI error message </returns>
        public BIErrors ChangePass(int userid, string oldpassw, string newpass, string type)
        {

            //code is only for Insert & not for Edit As per requirement

            xdoc = objxml.ChangePass(userid, oldpassw, newpass, type);

            //  xdoc = objxml.ANNOUNCEMENTS_c_CodeXml(Flag, model.AId, model.ATYPID, userid, model.Subject, model.Body, Convert.ToDateTime(model.StartDate), Convert.ToDateTime(model.EndDate), model.ForGroup, model.ForAgents, model.ForCustomer, model.SmsNotify, model.EmailNotify, model.SendBySms);



            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, "E");
            return err;

        }
        #endregion

        #endregion
    }
    /// <summary>
    /// this class is used for get user details record form database
    /// </summary>
    public class UserDetails
    {
        XDocument xdoc;

        UserManagementXml objxml = new UserManagementXml();

        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties
        public int SLID { get; set; }
        public int COMMID { get; set; }
        public int FAMID { get; set; }
        public int USERID { get; set; }
        public string IMGDESC { get; set; }
        public string SLEDGER { get; set; }
        public string USERNAME { get; set; }
        public string CONTACTPERSON { get; set; }
        public string FMEMBER { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// method used for getting user profile details
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <param name="COMMID">communcation id</param>
        /// <param name="FMEBID">family member id</param>
        /// <returns></returns>
        public UserDetails GetUserProfileDetails(int USERID, int SLID, int COMMID, int FMEBID)
        {
            xdoc = objxml.GetPortalProfileImgDetails(SLID, USERID, COMMID, FMEBID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<UserDetails> model = dt.AsEnumerable().Select(row =>
                        new UserDetails
                        {
                            SLID = row.Field<int?>("SLID") ?? 0,
                            SLEDGER = row.Field<string>("SLEDGER"),
                            USERID = row.Field<int?>("USERID") ?? 0,
                            USERNAME = row.Field<string>("USERNAME"),
                            COMMID = row.Field<int?>("COMMID") ?? 0,
                            CONTACTPERSON = row.Field<string>("CONTACTPERSON"),
                            FAMID = row.Field<int?>("FMembID") ?? 0,
                            FMEMBER = row.Field<string>("FMEMBER"),
                            IMGDESC = row.Field<string>("PROFILEIMG")
                        }).ToList();

            return model.FirstOrDefault();
        }

        #endregion
    }

    /// <summary>
    /// this class is used for employee monthly performance details
    /// </summary>
    public class EmpAnalysis
    {
        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties

        public int EarnedPoints { get; set; }
        public int NextReachablePoints { get; set; }
        public int MonthWisePoints { get; set; }
        public int FastResolCount { get; set; }
        public int OnTimeResolCount { get; set; }
        public int OverdueCount { get; set; }
        public int FirstCallResolCount { get; set; }

        public string Level { get; set; }
        public string NextReachableLevel { get; set; }


        #endregion

        #region Methods
        /// <summary>
        /// method used for getting employee performance 
        /// </summary>
        /// <param name="Userid">user id</param>
        /// <returns>getting employee performance  record</returns>
        public EmpAnalysis GetEmpAnalysis(int Userid)
        {
            xdoc = objxml.GetEmpAnalysis(Userid);
            dt = objbui.GetKloudqBusinessList(xdoc);
            foreach (DataRow dr in dt.Rows)
            {
                this.EarnedPoints = dr.Field<int?>("EarnedPoints") ?? 0;
                this.FastResolCount = dr.Field<int?>("FastResolCount") ?? 0;
                this.FirstCallResolCount = dr.Field<int?>("FirstCallResolCount") ?? 0;
                this.Level = dr.Field<string>("Level");
                this.MonthWisePoints = dr.Field<int?>("MonthWisePoints") ?? 0;
                this.NextReachableLevel = dr.Field<string>("NextReachableLevel");
                this.NextReachablePoints = dr.Field<int?>("NextReachablePoints") ?? 0;
                this.OnTimeResolCount = dr.Field<int?>("OnTimeResolCount") ?? 0;
                this.OverdueCount = dr.Field<int?>("OverdueCount") ?? 0;
            }


            return this;
        }

        #endregion
    }

    /// <summary>
    /// this class is used for getting Employee details
    /// </summary>
    public class PortalEmpDetails
    {
        XDocument xdoc;
        /// <summary>
        /// 
        /// </summary>
        UserManagementXml objxml = new UserManagementXml();
        /// <summary>
        /// 
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties

        public int EMPID { get; set; }
        public int DESIGID { get; set; }
        public int DEPTID { get; set; }
        public int USERID { get; set; }

        public string EMPCODE { get; set; }
        public string FULLNAME { get; set; }
        public string GENDER { get; set; }
        public string EMPTYPE { get; set; }
        public string DESIGNATION { get; set; }
        public string DEPARTMENT { get; set; }
        public string PANNO { get; set; }
        public string BLOODGROUP { get; set; }
        public string DRVLICNO { get; set; }
        public string PASSPORTNO { get; set; }

        public DateTime? TRAININGDATE { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public DateTime? JOINDATE { get; set; }
        public DateTime? PROBATIONDATE { get; set; }
        public DateTime? CONFIRMDATE { get; set; }

        public DateTime? RETIREMENTDATE { get; set; }
        public DateTime? ANNIVERSARY { get; set; }
        public DateTime? LEFTDATE { get; set; }
        public DateTime? INCREVDATE { get; set; }

        public string CONTACTNO { get; set; }
        public string ADDRESINFO { get; set; }
        public byte? WEEKOFF1 { get; set; }
        public byte? WEEKOFF2 { get; set; }

        public bool ISACTIVE { get; set; }


        #endregion

        #region Methods
        /// <summary>
        /// Method used for getting list of employee details
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <returns>employee details record</returns>
        public PortalEmpDetails GetPortalEmpDetals(int USERID)
        {
            xdoc = objxml.GetPortalEmpDetails(USERID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            foreach (DataRow dr in dt.Rows)
            {

                this.EMPID = dr.Field<int>("EMPID");
                this.DEPTID = dr.Field<int>("EMPID");
                this.DESIGID = dr.Field<int>("EMPID");
                this.USERID = dr.Field<int>("EMPID");

                this.EMPCODE = dr.Field<string>("EMPCODE");
                this.ADDRESINFO = dr.Field<string>("ADDRESINFO");
                this.CONTACTNO = dr.Field<string>("CONTACTNO");
                this.DEPARTMENT = dr.Field<string>("DEPARTMENT");
                this.DESIGNATION = dr.Field<string>("DESIGNATION");
                this.EMPTYPE = dr.Field<string>("EMPTYPE");
                this.FULLNAME = dr.Field<string>("FULLNAME");
                this.GENDER = dr.Field<string>("GENDER");
                this.PANNO = dr.Field<string>("PANNO");
                this.BLOODGROUP = dr.Field<string>("BLOODGROUP");
                this.DRVLICNO = dr.Field<string>("DRVLICNO");
                this.PASSPORTNO = dr.Field<string>("PASSPORTNO");
                this.WEEKOFF1 = dr.Field<byte?>("WEEKOFF1");
                this.WEEKOFF2 = dr.Field<byte?>("WEEKOFF2");

                this.BIRTHDATE = dr.Field<DateTime>("BIRTHDATE");
                this.CONFIRMDATE = dr.Field<DateTime?>("BIRTHDATE");
                this.ANNIVERSARY = dr.Field<DateTime?>("ANNIVERSARY");
                this.INCREVDATE = dr.Field<DateTime?>("INCREVDATE");
                this.JOINDATE = dr.Field<DateTime?>("JOINDATE");
                this.LEFTDATE = dr.Field<DateTime?>("LEFTDATE");
                this.PROBATIONDATE = dr.Field<DateTime?>("PROBATIONDATE");
                this.RETIREMENTDATE = dr.Field<DateTime?>("RETIREMENTDATE");
                this.TRAININGDATE = dr.Field<DateTime?>("TRAININGDATE");

            }


            return this;


        }

        #endregion
    }

    /// <summary>
    /// This class is used for get Customer details 
    /// </summary>
    public class PortalCustomer
    {
        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties

        public int SLID { get; set; }
        public int AGE { get; set; }
        public int DESIGID { get; set; }
        public int ProfessionID { get; set; }
        public int UNITID { get; set; }
        public int TOWERID { get; set; }
        public int SectorID { get; set; }

        public string CUSTOMER { get; set; }
        public string DEPARTMENT { get; set; }
        public string PHONENUM { get; set; }
        public string MOBILENUM { get; set; }
        public string EMAILID { get; set; }
        public string PROFESSION { get; set; }
        public string Nationality { get; set; }
        public string ProfilePhoto { get; set; }
        public string PAN { get; set; }
        public string UNIT { get; set; }
        public string TOWER { get; set; }
        public string SECTOR { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime? AnniversaryDt { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Method used for getting customer details
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <returns>customer details record</returns>
        public PortalCustomer GetCustomerDetails(int USERID, int SLID)
        {
            xdoc = objxml.GetPortalCustomerDetails(USERID, SLID);
            dt = objbui.GetKloudqBusinessList(xdoc);

            PortalCustomer model = dt.AsEnumerable().Select(dr =>
                new PortalCustomer
                {
                    SLID = dr.Field<int?>("SLID") ?? 0,
                    AGE = dr.Field<int?>("AGE") ?? 0,
                    AnniversaryDt = dr.Field<DateTime?>("AnniversaryDt"),
                    CUSTOMER = dr.Field<string>("CUSTOMER"),
                    DEPARTMENT = dr.Field<string>("DEPARTMENT"),
                    MOBILENUM = dr.Field<string>("MOBILENUM"),
                    DESIGID = dr.Field<int?>("DESIGID") ?? 0,
                    DOB = dr.Field<DateTime?>("DOB"),
                    EMAILID = dr.Field<string>("EMAILID"),
                    Nationality = dr.Field<string>("Nationality"),
                    PAN = dr.Field<string>("PAN"),
                    PHONENUM = dr.Field<string>("PHONENUM"),
                    PROFESSION = dr.Field<string>("PROFESSION"),
                    ProfessionID = dr.Field<int?>("ProfessionID") ?? 0,
                    ProfilePhoto = dr.Field<string>("ProfilePhoto"),
                    SECTOR = dr.Field<string>("SECTOR"),
                    TOWER = dr.Field<string>("TOWER"),
                    UNIT = dr.Field<string>("UNIT"),
                }).FirstOrDefault();

            return model;
        }

        #endregion
    }

    /// <summary>
    /// This class is used for get family member details
    /// </summary>
    public class FamilyMember
    {
        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        #region Properties

        public int SLID { get; set; }
        public int Age { get; set; }
        public int FmlyID { get; set; }
        public string Sledger { get; set; }
        public string FmlyMemberName { get; set; }
        public string Relation { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAILID { get; set; }
        public string SMARTCARDNO { get; set; }
        public string PhotoImage { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime? AnniversaryDt { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Method used for getting family member list
        /// </summary>
        /// <param name="FMembIDm">family member id</param>
        /// <param name="UserId">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <returns>Get family member record list </returns>
        public List<FamilyMember> GetFamilyMemberList(int FMembIDm, int UserId, int SLID)
        {

            xdoc = objxml.GetFamilyDetails(FMembIDm, UserId, SLID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<FamilyMember> lst = dt.AsEnumerable().Select(row =>
                new FamilyMember
                {
                    Age = row.Field<int?>("Age") ?? 0,
                    AnniversaryDt = row.Field<DateTime?>("AnniversaryDt"),
                    DOB = row.Field<DateTime?>("DOB"),
                    EMAILID = row.Field<string>("EMAILID"),
                    FmlyID = row.Field<int?>("FmlyID") ?? 0,
                    FmlyMemberName = row.Field<string>("FmlyMemberName"),
                    MOBILE = row.Field<string>("MOBILE"),
                    PHONE = row.Field<string>("PHONE"),
                    PhotoImage = row.Field<string>("PhotoImage"),
                    Relation = row.Field<string>("Relation"),
                    Sledger = row.Field<string>("Sledger"),
                    SLID = row.Field<int?>("SLID") ?? 0,
                    SMARTCARDNO = row.Field<string>("SMARTCARDNO")

                }).ToList();


            return lst;
        }

        #endregion
    }
    //
    /// <summary>
    /// This class is used for user profile management
    /// </summary>
    public class UserManagementXml
    {
        /// <summary>
        /// Method generate xml for update user setting
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Value">value</param>
        /// <param name="USERID">user id</param>
        /// <param name="CODE">code</param>
        /// <returns>return data in  X Document</returns>
        public XDocument USERSETTINGS_c_CodeXml(int flag, string Value, int USERID, string CODE)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERSETTINGS_c"),
                new XElement("pXMLFILE", getxmlvalue(flag, Value, CODE).ToString()),
                new XElement("pUSERID", USERID)

                 ));
            return CreateXml;
        }
        /// <summary>
        /// Method used for getting xml value
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Value">value</param>
        /// <param name="CODE">code</param>
        /// <returns>return data in  X Document</returns>
        public XElement getxmlvalue(int flag, string Value, string CODE)
        {
            var Reg =
               new XElement("USERSETTING",
                     new XElement("FLAG", flag),
                     new XElement("VALUE", Value),
                      new XElement("CODE", CODE)
                     );


            return Reg;
        }

        #region Functions XML
        /// <summary>
        /// Method used for generating xml for getting functionality list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="FunctionID">function id</param>
        /// <returns>return data in  X Document</returns>
        public XDocument GetFuctionList_CodeXML(int Flag, int FunctionID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "FUNCTIONALITY_g"),
                new XElement("pFlag", Flag),
                new XElement("pFUNID", FunctionID)
                )
                );
            return CreateXml;
        }
        /// <summary>
        /// Method used for creating xml for getting functionality tree details
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="XML">xml</param>
        /// <returns>return data in  X Document</returns>
        public XDocument GetFunctionDetail_CodeXML(int Flag, string XML)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "GETFUNTREE_g"),
                new XElement("pFlag", Flag),
                new XElement("pXML", XML)
                )
                );
            return CreateXml;
        }
        /// <summary>
        /// Method used for generating xml for filling mod menu map dropdown list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="SDESC">description</param>
        /// <param name="MODMENUID">mod menu id</param>
        /// <param name="UserID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument ModMenuMaphelp_CodeXml(int Flag, string SDESC, int MODMENUID, int UserID)
        {
            XDocument CreateXml = new XDocument(
                 new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "MODMENUMAP_h"),
                new XElement("pFlag", Flag),
                new XElement("pSDESC", SDESC),
                new XElement("pMODMENUID", MODMENUID),
                 new XElement("pUSERID", UserID)
                ));
            return CreateXml;
        }

        /// <summary>
        /// Method used for generating xml for Add edit functionality
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="FUNID">function id</param>
        /// <param name="FUNCODE">functuion code</param>
        /// <param name="DESCRIPTION"></param>
        /// <param name="ISACTIVE"></param>
        /// <param name="UserID"></param>
        /// <returns>Return data in  X Document</returns>
        public XDocument AddEditFunction_CodeXML(string Flag, int FUNID, string FUNCODE, string DESCRIPTION, bool ISACTIVE, int UserID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "FUNCTIONALITY_c"),
                new XElement("pFlag", Flag),
                new XElement("pFUNID", FUNID),
                new XElement("pFUNCODE", FUNCODE),
                new XElement("pDESCRIPTION", DESCRIPTION),
                new XElement("pISACTIVE", ISACTIVE),
                 new XElement("pUSERID", UserID)
                ));
            return CreateXml;
        }
        /// <summary>
        /// Method used for generating xml for getting functionality menu map
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="functionID">function id</param>
        /// <param name="menuID">menu id</param>
        /// <param name="FUNMENUMAPID">function menu map id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument FUNACTMENUMAP_G_CodeXml(int Flag, int functionID, int menuID, int FUNMENUMAPID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "FUNACTMENUMAP_g"),
                new XElement("pFlag", Flag),
                new XElement("pFUNID", functionID),
                new XElement("pMODMENUID", menuID),
                 new XElement("pFUNMNUMAPID", FUNMENUMAPID)
                ));
            return CreateXml;
        }

        /// <summary>
        /// Xml used for getting menu actions
        /// </summary>
        /// <param name="MODMENUID">mod menu id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetActions(int MODMENUID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "MENUACTIONS_g"),
                new XElement("pMODMENUID", MODMENUID)
                ));
            return CreateXml;
        }
        /// <summary>
        /// Method used for filling sub document dropdown list
        /// </summary>
        /// <param name="SYSDOCID">sys document id</param>
        /// <param name="UserID">user id</param>
        /// <param name="DESC">description</param>
        /// <param name="SUBDOCID">sub document id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetSubDocumnet_CodeXml(int SYSDOCID, int UserID, string DESC, int SUBDOCID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "SYSSUBDOC_h"),
                new XElement("pSYSDOCID", SYSDOCID),
                new XElement("pUSERID", UserID),
                new XElement("pDESC", DESC),
                new XElement("pSUBDOCID", SUBDOCID)

                ));
            return CreateXml;
        }
        /// <summary>
        /// Xml for create function act menu map
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="MASTID">master id</param>
        /// <param name="XmlTFile">xml file</param>
        /// <param name="UserID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument FUNACTMENUMAP_C_CodeXml(string Flag, int MASTID, string XmlTFile, int UserID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcId", "1144"),
                new XElement("pFlag", Flag),
                new XElement("pMASTID", MASTID),
                new XElement("XmlTFile", XmlTFile),
                 new XElement("pUSERID", UserID)
                ));
            return CreateXml;
        }

        /// <summary>
        /// Method used for inserting function activity menu map record
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="MASTID">master id</param>
        /// <param name="XmlTFile">xml file</param>
        /// <param name="UserID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument FUNACTDOCMENUMAP_CodeXml(string Flag, int MASTID, string XmlTFile, int UserID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "FUNACTDOCMENUMAP_c"),
                new XElement("pFlag", Flag),
                new XElement("pMASTID", MASTID),
                 new XElement("XmlTFile", XmlTFile),
                 new XElement("pUSERID", UserID)
                 ));
            return CreateXml;
        }

        /// <summary>
        /// Method used for generating xml for Add edit menu map activity
        /// </summary>
        /// <param name="funID">function id</param>
        /// <param name="FunctionMenuid">function menu id</param>
        /// <param name="Flag">flag</param>
        /// <param name="MenuID">menu id</param>
        /// <param name="ActChecked">activity checked</param>
        /// <returns>Return data in  X Document</returns>
        public XElement AddEditMenuAct_XML(int funID, int FunctionMenuid, int Flag, int MenuID, int[] ActChecked)
        {

            XElement xle = new XElement("FUNACTMENUMAP",
                      new XElement("FUNID", funID),
                      new XElement("MENU",
                          new XElement("MODMENUID", MenuID),
                          new XElement("ACTIONS",
                            from s in ActChecked
                            select
                    new XElement("TRANS",
           new XElement("FUNMNUMAPID", FunctionMenuid),
           new XElement("ACTIONID", s),
           new XElement("ISDELETE", Flag))
                          ))
                                  );
            return xle;
        }
        /// <summary>
        /// Method for generating xml for add edit menu document
        /// </summary>
        /// <param name="funID">functionality id</param>
        /// <param name="FunctionMenuid">function menu id</param>
        /// <param name="Flag">flag</param>
        /// <param name="MenuID">menu id</param>
        /// <param name="ActChecked">activity checked</param>
        /// <param name="DocChecked">document checked</param>
        /// <returns>Return data in  X Document</returns>
        public XElement AddEditMenuDOC_XML(int funID, int FunctionMenuid, int Flag, int MenuID, int[] ActChecked, int[] DocChecked)
        {


            XElement xle = new XElement("FUNACTMENUMAP",
                   new XElement("FUNID", funID),
                   new XElement("MENU",
                       new XElement("MODMENUID", MenuID),
                       new XElement("ACTIONS",
                         from s in ActChecked
                         select
                       new XElement("TRANS",
                        new XElement("FUNMNUMAPID", FunctionMenuid),
                        new XElement("ACTIONID", s),
                        new XElement("FLAG", Flag))
                                   ),
                       new XElement("SUBDOCS",
                              from s in DocChecked
                              select
                              new XElement("TRANS",
                              new XElement("FUNMNUDOCMAPID", FunctionMenuid),
                              new XElement("SUBDOCID", s),
                              new XElement("ISDELETE", Flag))
                                   ))
                               );
            return xle;


        }


        #endregion

        #region
        //1240
        //    EXEC PORTALRESETPW_C @pFlag = '1', -- char(1)
        //@pQID = 0, -- int
        //@pANSWER = '', -- varchar(255)
        //@pEmail = '' -- varchar(255)
        /// <summary>
        /// Method used for generating xml for reset user password
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="Qid">question id</param>
        /// <param name="Ans">answer</param>
        /// <param name="Email">email id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument ResetPw(string Flag, int Qid, string Ans, string Email)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "PORTALRESETPW_C"),
                new XElement("pFlag", Flag),
                new XElement("pQID", Qid),
                new XElement("pANSWER", Ans),
                new XElement("pEmail", Email)
                ));
            return CreateXml;
        }


        #endregion

        #region SqcurityQ
        //EXEC dbo.SECURITYQUIZ_g @pFlag = 1, -- smallint
        //@pQID = 0 -- int
        /// <summary>
        /// Xml for getting user security question
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="Qid">question id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetAllSqcurityQ(int Flag, int Qid)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "SECURITYQUIZ_g"),
                new XElement("pFlag", Flag),
                new XElement("pQID", Qid)
                ));
            return CreateXml;
        }

        //    EXEC dbo.SECURITYQUIZVAL_g @pFlag = 1, -- smallint
        //@pANSID = 0, -- int
        //@pUSERID = 0 -- int
        /// <summary>
        /// Xml used for getting answer of user security question
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="AnsId">answer id</param>
        /// <param name="UseriD">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetUSecurityQ(int Flag, int AnsId, int UseriD)//xml for getting security question with answer
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "SECURITYQUIZVAL_g"),
                new XElement("pFlag", Flag),
                new XElement("pANSID", AnsId),
                new XElement("pUSERID", UseriD)
                ));
            return CreateXml;
        }

        //    EXEC dbo.SECURITYQUIZVAL_c @pFlag = '', -- char(1)
        //@pANSID = 0, -- int
        //@pQID = 0, -- int
        //@pANSWER = '', -- varchar(255)
        //@pUSERID = 0 -- int
        /// <summary>
        /// Xml used for insert user security question
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="AnsId">answer id</param>
        /// <param name="Qid">question id</param>
        /// <param name="Ans">answer</param>
        /// <param name="UserId">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument SetSECURITYQ(string Flag, int AnsId, int Qid, string Ans, int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "SECURITYQUIZVAL_c"),
                new XElement("pFlag", Flag),
                new XElement("pANSID", AnsId),
                new XElement("pQID", Qid),
                new XElement("pANSWER", Ans),
                new XElement("pUSERID", UserId)
                ));
            return CreateXml;
        }
        #endregion

        #region ChangePass
        //1239
        //    EXEC PortalPWChange_c @pUSERID = 40, -- int
        //@pOLDPASWD = 'admin1', -- varchar(120)
        //@pNEWPASWD = 'admin1', -- varchar(20)
        //@pType = 'change' -- varchar(20)

        /// <summary>
        /// xml used for user registration
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="SLCODE">sales ledger code</param>
        /// <param name="EMAILID">Email id</param>
        /// <param name="MOBILENO">mobile number</param>
        /// <param name="FLAG">flag</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument Registration(int UserID, string SLCODE, string EMAILID, int MOBILENO, int FLAG)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcId", "1242"),
                new XElement("pUSERID", UserID),
                new XElement("pXMLFILE", RegisterUserData(SLCODE, EMAILID, MOBILENO, FLAG).ToString())

                ));
            return CreateXml;
        }
        /// <summary>
        /// method used for check if user is alredy exist
        /// </summary>
        /// <param name="Username">user name</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument CheckUser(string Username)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcId", "1244"),
                new XElement("pUSERNAME", Username)

                ));
            return CreateXml;
        }

        /// <summary>
        /// xml used for register user data
        /// </summary>
        /// <param name="SLCODE">sales ledger code</param>
        /// <param name="EMAILID">email id</param>
        /// <param name="MOBILENO">mobile number</param>
        /// <param name="FLAG">flag</param>
        /// <returns>Return data in  X Document</returns>
        public XElement RegisterUserData(string SLCODE, string EMAILID, int MOBILENO, int FLAG)
        {
            XElement Reg;
            if (FLAG == -1)
            {
                Reg = new XElement("ACCOUNTDTLS",
                    new XElement("CUSTOMERDATA",
                              new XAttribute("FLAG", FLAG),
                              new XAttribute("FMEMBID", SLCODE)
                              ));
            }
            else
            {
                Reg = new XElement("ACCOUNTDTLS",
                new XElement("CUSTOMERDATA",
                        new XAttribute("CUSTOMERNO", SLCODE),
                        new XAttribute("EMAILID", EMAILID),
                        new XAttribute("MOBILENO", MOBILENO),
                        new XAttribute("FLAG", FLAG)
                        ));

            }
            return Reg;
        }

        //Account Activation

        /// <summary>
        /// method used for user activation
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="UserID">user id</param>
        /// <param name="SLCODE">sales ledger code</param>
        /// <param name="USERNAME">user name</param>
        /// <param name="PASSWORD">password</param>
        /// <param name="TIMEZONEID">time zone id</param>
        /// <param name="QID">question id</param>
        /// <param name="Answer">answer</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument Activation(string flag, int UserID, string SLCODE, string USERNAME, string PASSWORD, int TIMEZONEID, int QID, string Answer)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcId", "1243"),
                new XElement("pUSERID", UserID),
                new XElement("pXMLFILE", AccountActivation(flag, SLCODE, USERNAME, PASSWORD, TIMEZONEID, QID, Answer).ToString())

                ));
            return CreateXml;
        }
        /// <summary>
        /// method used for generating xml for account activation
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="SLCODE">sales ledger code</param>
        /// <param name="USERNAME">user name</param>
        /// <param name="PASSWORD">password</param>
        /// <param name="TIMEZONEID">time zone id</param>
        /// <param name="QID">question id</param>
        /// <param name="Answer">answer</param>
        /// <returns>Return data in  X Document</returns>
        public XElement AccountActivation(string flag, string SLCODE, string USERNAME, string PASSWORD, int TIMEZONEID, int QID, string Answer)
        {
            XElement AcountAct = new XElement("CUSTOMERDTLS",
                new XElement("ACTIVATIONDATA",
                     new XAttribute("FLAG", flag),
                     new XAttribute("CODE", SLCODE),
                           new XAttribute("USERNAME", USERNAME),
                          new XAttribute("PASSWORD", PASSWORD),
                          new XAttribute("TIMEZONEID", TIMEZONEID),
                           new XAttribute("QID", QID),
                          new XAttribute("ANSWER", Answer)
                          ));
            return AcountAct;
        }
        /// <summary>
        /// method used for generating xml for change user password
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="oldpass">old password</param>
        /// <param name="newpass">new password</param>
        /// <param name="type">user type</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument ChangePass(int UserId, string oldpass, string newpass, string type)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "PortalPWChange_c"),
                new XElement("pUSERID", UserId),
                new XElement("pOLDPASWD", oldpass),
                new XElement("pNEWPASWD", newpass),
                new XElement("pType", type)
                ));
            return CreateXml;
        }

        #endregion

        #region Role XML
        /// <summary>
        /// method used for generating xml for getting user role master list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="ROLEID">role id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetRoleMastList_CodeXml(int Flag, int ROLEID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "ROLEMAST_g"),
                new XElement("pFlag", Flag),
                new XElement("pROLEID", ROLEID)
                ));
            return CreateXml;
        }
        /// <summary>
        /// xml for getting user role functionality map 
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="ROLEID">role id</param>
        /// <param name="ROLEFUNID">role function id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetRoleFunctionMap_CodeXml(short Flag, int ROLEID, int ROLEFUNID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "ROLEFUNACTMAP_g"),
                new XElement("pFlag", Flag),
                new XElement("pROLEID", ROLEID),
                  new XElement("pROLEFUNID", ROLEFUNID)

                ));
            return CreateXml;
        }
        /// <summary>
        /// method used for getting function tree list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="FUNID">function id</param>
        /// <param name="MENUID">menu id</param>
        /// <param name="FUNMNUMAPID">function menu map id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetFunctionTreeList_CodeXml(int Flag, int FUNID, int MENUID, int FUNMNUMAPID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "FUNTREE_g"),
                new XElement("pFlag", Flag),
                new XElement("pFUNID", FUNID),
                new XElement("pMENUID", MENUID),
                new XElement("pFUNMNUMAPID", FUNMNUMAPID)
                ));
            return CreateXml;
        }
        /// <summary>
        /// method used for add role function map
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="MASTID">master id</param>
        /// <param name="XmlTFile">xml file</param>
        /// <param name="UserID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument AddRoleFunMap(string Flag, int MASTID, string XmlTFile, int UserID)
        {
            XDocument CreateXml = new XDocument(
               new XDeclaration("1.0", "utf-8", ""),
               new XElement("Kloudq",
               new XElement("XsdName", ""),
               new XElement("ProcName", "ROLEFUNACTMAP_c"),
               new XElement("pFlag", Flag),
               new XElement("pMASTID", MASTID),
               new XElement("XmlTFile", XmlTFile),
                new XElement("pUSERID", UserID)
               ));
            return CreateXml;
        }

        /// <summary>
        /// Xml used for inserting record in role master
        /// </summary>
        /// <param name="OperationFlag">operation flag</param>
        /// <param name="ROLEID">role id</param>
        /// <param name="DESCRIPTION">description</param>
        /// <param name="LONGTEXT">long text</param>
        /// <param name="ISDOMAIN">is domain</param>
        /// <param name="ISACTIVE">is active</param>
        /// <param name="USERID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument RoleMast_CodeXml(string OperationFlag, int ROLEID, string DESCRIPTION, string LONGTEXT, bool ISDOMAIN, bool ISACTIVE, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "ROLEMAST_c"),
                new XElement("pFlag", OperationFlag),
                new XElement("pROLEID", ROLEID),
                new XElement("pLONGTEXT", LONGTEXT),
                new XElement("pDESCRIPTION", DESCRIPTION),
                new XElement("pISDOMAIN", ISDOMAIN),
                new XElement("pISACTIVE", ISACTIVE),
                 new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        public XElement RoleFunMapTrans(int[] CheckedRecords, int ROLEFUNID, int ROLEID, bool ISACTIVE, int FLAG)
        {
            XElement xe = new XElement("ROLEFUNACTMAP",

                            from s in CheckedRecords
                            select
                           new XElement("TRANS",
                           new XElement("ROLEFUNID", ROLEFUNID),
                           new XElement("ROLEID", ROLEID),
                           new XElement("FUNID", s),
                           new XElement("ISACTIVE", ISACTIVE),
                           new XElement("FLAG", FLAG)
                           )

                           );
            return xe;
        }

        #endregion

        #region Name:-User

        /// <summary>
        /// Xml for getting user master
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="Flag">flag</param>
        /// <param name="EUSERID">E user id</param>
        /// <param name="Desc">description</param>
        /// <param name="PageNo">page number</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetUserMast_CodeXml(int UserID, int Flag, int EUSERID, string Desc = "", int PageNo = 1)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERMAST_g"),
                new XElement("pEUSERID", EUSERID),
                 new XElement("pDESC", Desc),
              new XElement("pPAGENO", PageNo),
                new XElement("pUSERID", UserID),
                new XElement("pFlag", Flag)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml for getting json of user master
        /// </summary>
        /// <param name="UserID">user id</param>
        /// <param name="Flag">flag</param>
        /// <param name="EUSERID">E user id</param>
        /// <param name="Desc">description</param>
        /// <param name="PageNo">page number</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetJsonUserMast_CodeXml(int UserID, int Flag, int EUSERID, string Desc, int PageNo)
        {
            //        EXEC dbo.USERMAST_g @pEUSERID = 2, -- int
            //@pFlag = -1, -- smallint
            //@pUSERID = 0, -- int
            //@pDESC = '', -- varchar(30)
            //@pPAGENO = 1 -- int

            XDocument CreateXml = new XDocument(
              new XDeclaration("1.0", "utf-8", ""),
              new XElement("Kloudq",
              new XElement("XsdName", ""),
              new XElement("ProcName", "USERMAST_g"),
              new XElement("pEUSERID", EUSERID),
              new XElement("pUSERID", UserID),
              new XElement("pDESC", Desc),
              new XElement("pPAGENO", PageNo),
              new XElement("pFlag", Flag)
               ));
            return CreateXml;
            //XDocument CreateXml = new XDocument(
            //    new XDeclaration("1.0", "utf-8", ""),
            //    new XElement("Kloudq",
            //    new XElement("XsdName", ""),
            //    new XElement("ProcId", "1159"),
            //    new XElement("pEUSERID", EUSERID),
            //    new XElement("pUSERID", UserID),
            //    new XElement("pDESC", Desc),
            //    new XElement("pPAGENO", PageNo),
            //    new XElement("pFlag", Flag)
            //     ));
            //return CreateXml;
        }
        /// <summary>
        /// Method used for getting user role map 
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="USERROLEID">user role id</param>
        /// <param name="SUSERID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetUserRoleMapTrans_CodeXml(int Flag, int USERROLEID, int SUSERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERROLEMAP_g"),
                new XElement("pFlag", Flag),
                new XElement("pUSERROLEID", USERROLEID),
                new XElement("pSUSERID", SUSERID)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml used for getting time zone
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="TMZONEID">time zone id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetTIMEZONE_CodeXml(int Flag, int TMZONEID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "TIMEZONE_g"),
                new XElement("pFlag", Flag),
                new XElement("pTMZONEID", TMZONEID)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Method used for filling user type dropdown list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="USERID">user id</param>
        /// <param name="UTYPEID">user type id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetUserTypesUserWise_CodeXml(int Flag, int USERID, int UTYPEID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERTYPE_h"),
                new XElement("pUSERID", USERID),
                new XElement("pFlag", Flag),
                new XElement("pUTYPEID", UTYPEID)
                 ));
            return CreateXml;
        }

        /// <summary>
        /// Xml for create user
        /// </summary>
        /// <param name="OperationFlag"></param>
        /// <param name="USERID"></param>
        /// <param name="LOGINID"></param>
        /// <param name="PASSWORD"></param>
        /// <param name="ISDOMAIN"></param>
        /// <param name="UTYPEID"></param>
        /// <param name="TMZONEID"></param>
        /// <param name="ISACTIVE"></param>
        /// <param name="SLID"></param>
        /// <param name="Email"></param>
        /// <param name="ESUERID"></param>
        /// <param name="MobileNumber"></param>
        /// <param name="AuthvalId">authuntication value id</param>
        /// <param name="FName">first name</param>
        /// <param name="MName">middle name</param>
        /// <param name="LName">last name</param>
        /// <param name="DOB">date of birth</param>
        /// <param name="ProfImg">profile image</param>
        /// <param name="Address"></param>
        /// <param name="pUser"></param>
        /// <returns>Return data in  X Document</returns>
        public XDocument UserMast_CodeXml(string OperationFlag, int USERID, string LOGINID, string PASSWORD, bool ISDOMAIN, int UTYPEID, int TMZONEID, bool ISACTIVE, int? SLID, string Email, int ESUERID, string MobileNumber, int AuthvalId, string FName, string MName, string LName, DateTime? DOB, string ProfImg, string Address, int pUser = 0)
        { //slid removed  bcz removed from sp Sudhir


            //        EXEC dbo.USERMAST_c @pFlag = 'F', -- char(1)
            //@pUSERID = 0, -- int
            //@pLOGINID = '', -- varchar(120)
            //@pPASSWORD = '', -- varchar(20)
            //@pISDOMAIN = true, -- bit
            //@pUTYPEID = 0, -- int
            //@pTMZONEID = 0, -- int
            //@pISACTIVE = true, -- bit
            //@pMobileNo = '', -- varchar(13)
            //@pAUTHVALID = 0, -- int
            //@pEmail = N'', -- nvarchar(600)
            //@pFNAME = '', -- varchar(20)
            //@pMNAME = '', -- varchar(20)
            //@pLNAME = '', -- varchar(20)
            //@pDOB = '2015-01-08 05:28:59', -- date
            //@pADDRESS = '', -- varchar(100)
            //@pPROFILEIMG = '', -- varchar(max)
            //@pEUSERID = 0 -- int


            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERMAST_c"),
                new XElement("pFlag", OperationFlag),
                new XElement("pUSERID", USERID),
                new XElement("pLOGINID", LOGINID),
                new XElement("pPASSWORD", PASSWORD),
                new XElement("pISDOMAIN", ISDOMAIN),
                new XElement("pUTYPEID", UTYPEID),
                new XElement("pTMZONEID", TMZONEID),
                new XElement("pISACTIVE", ISACTIVE),
                new XElement("pEmail", Email),
                new XElement("pFNAME", FName),
                new XElement("pMNAME", MName),
                new XElement("pLNAME", LName),
                new XElement("pDOB", DOB),
                  new XElement("pADDRESS", Address),
                   new XElement("pPROFILEIMG", ProfImg),
                new XElement("pEUSERID", ESUERID),
                new XElement("pAUTHVALID", AuthvalId),
                new XElement("pMobileNo", MobileNumber),
                 new XElement("pPARRENTID", pUser)

                )
                );
            return CreateXml;
        }

        /// <summary>
        /// Xml used for getting Authuntication value
        /// </summary>
        /// <param name="AUTHLVLID">auth value id</param>
        /// <param name="PARENTID">parrent id</param>
        /// <param name="USERID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetAuthValue(int AUTHLVLID, int PARENTID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "ORGCHART_h"),
                new XElement("pXML", getauthval(AUTHLVLID, PARENTID, USERID).ToString())

                 ));
            return CreateXml;
        }

        public XElement getauthval(int AUTHLVLID, int PARENTID, int USERID)
        {
            var Reg =
               new XElement("ORGCHART",
                     new XElement("AUTHLVLID", AUTHLVLID),
                     new XElement("PARENTID", PARENTID),
                     new XElement("USERID", USERID)

                     );


            return Reg;
        }
        /// <summary>
        /// Xml for Add edit user role map
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="USERROLEID">user role id</param>
        /// <param name="XmlFile">xml file</param>
        /// <param name="USERID"></param>
        /// <returns>Return data in  X Document</returns>
        public XDocument AddEditRoleToUser(string Flag, int USERROLEID, string XmlFile, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERROLEMAP_c"),
                new XElement("pUSERROLEID", USERROLEID),
                new XElement("pFlag", Flag),
                new XElement("XmlFile", XmlFile),
                new XElement("pUSERID", USERID)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml used for user map trans
        /// </summary>
        /// <param name="USERID">user id</param>
        /// <param name="USERROLEID">user role id</param>
        /// <param name="ROLES">roles</param>
        /// <param name="ISACTIVE">is active</param>
        /// <returns>Return data in  X Document</returns>
        public XElement USERROLEMAPTRANS(int USERID, int USERROLEID, int[] ROLES, bool ISACTIVE)
        {
            XElement xe = new XElement(
                new XElement("USERROLEMAP",
                   new XElement("USER",
                       new XElement("USERID", USERID)
                       ),
                      from s in ROLES
                      select
                new XElement("TRANS",
                    new XElement("USERROLEID", USERROLEID),
                    new XElement("ROLEID", s),
                    new XElement("ISACTIVE", ISACTIVE))));

            return xe;
        }


        /// <summary>
        /// Xml used for getting employee pro chart list
        /// </summary>
        /// <param name="EmpProdChrtID">employee prod chart id</param>
        /// <param name="UserID">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument getEmpProdChartList(int? EmpProdChrtID, int UserID)
        {
            XDocument xdoc;
            xdoc = new XDocument(
               new XDeclaration("1.0", "utf-8", ""),
               new XElement("Kloudq",
               new XElement("XsdName", ""),
               new XElement("ProcName", "EmpProdChart_g"),
               new XElement("pEmpProdChrtID", EmpProdChrtID),
               new XElement("pUserID", UserID)
            ));

            return xdoc;
        }


        /// <summary>
        /// Xml used for Add edit employee prod chart
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="UserId">user id</param>
        /// <param name="XML">xml</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument AddEditDeleteEmpTarget(string flag, int UserId, string XML)
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "EmpProdChart_c"),
                new XElement("pFlag", flag),
                new XElement("pXML", XML.ToString()),
                new XElement("pUserId", UserId)
                ));
            return xdoc;
        }

        /// <summary>
        /// Xml used for employee target
        /// </summary>
        /// <param name="EmpProdChrtID">employee prod chart id</param>
        /// <param name="UserID">user id</param>
        /// <param name="TFromdate">from date</param>
        /// <param name="TTodate">to date</param>
        /// <param name="OrderLevel">order level</param>
        /// <param name="OrderCount">order count</param>
        /// <param name="TargetID">target id</param>
        /// <returns>Return data in  X Document</returns>
        public XElement EmpTarget_CodeXML(int EmpProdChrtID, int UserID, string TFromdate, string TTodate, int OrderLevel, long OrderCount, long TargetID)
        {
            XElement xle = new XElement("EmpProductivityChart",
                        new XElement("EmpProdChrtID", EmpProdChrtID),
                        new XElement("UserID", UserID),
                        new XElement("TFromdate", TFromdate),
                        new XElement("TTodate", TTodate),
                        new XElement("OrderLevel", OrderLevel),
                        new XElement("OrderCount", OrderCount),
                        new XElement("TargetID", TargetID)
                        );
            return xle;
        }

        #endregion

        #region AccountProfile

        /*   EXEC USERPROFILE_g @pUserId = 40, -- int
                @pSLID = 0, -- int
                @pFamMeembId = 0, -- int
                @pDESC = '', -- varchar(30)
                @pPUserid = 0 -- int
      * */
        /// <summary>
        /// Xml used for getting user profile details
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <param name="FamMeembId">family member id</param>
        /// <param name="DESC">description</param>
        /// <param name="PUserid">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetUserProfileDetails(int UserId, int SLID, int FamMeembId, string DESC, int PUserid)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "USERPROFILE_g"),
                new XElement("pUserId", UserId),
                new XElement("pSLID", SLID),
                //  new XElement("pFamMeembId", FamMeembId),Commented as per asked by Jatin Sir...
                new XElement("pDESC", DESC),
                new XElement("pPUserid", PUserid)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml used for getting employee performance
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetEmpAnalysis(int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                //new XElement("ProcId", "1231"),
                new XElement("ProcName", "EmpAnalysis_g"),
                new XElement("pUSERID", UserId)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml used for getting portal employee details
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetPortalEmpDetails(int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                // new XElement("ProcId", "1232"),
                new XElement("ProcName", "PORTALEMPDTL_G"),
                new XElement("pUSERID", UserId)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// Xml used for getting portal customer details
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetPortalCustomerDetails(int UserId, int SLID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                // new XElement("ProcId", "1233"),
                new XElement("ProcName", "PORTALCUSTOMERDTL_g"),
                new XElement("pUSERID", UserId),
                new XElement("pSLID", SLID)
                 ));
            return CreateXml;
        }

        /// <summary>
        /// Xml used for getting family details
        /// </summary>
        /// <param name="FMembIDm">family member id</param>
        /// <param name="UserId">user id</param>
        /// <param name="SLID">sales ledger id</param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetFamilyDetails(int FMembIDm, int UserId, int SLID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                //  new XElement("ProcId", "1234"),
                new XElement("ProcName", "PORTALFMLYDTL_G"),
                new XElement("pFMembID", FMembIDm),
                new XElement("pSLID", SLID),
                new XElement("pUSERID", UserId)
                 ));
            return CreateXml;
        }

        /* EXEC dbo.GetPortalProfileImg_g  @pSLID = 0, -- int
         @pUSERID = 0, -- int
         @pCOMMID = 0, -- int
         @pFMembID = 114 -- int*/

        /// <summary>
        ///  Xml for getting portal profile image detail
        /// </summary>
        /// <param name="SLID"></param>
        /// <param name="USERID"></param>
        /// <param name="COMMID"></param>
        /// <param name="FMembID"></param>
        /// <returns>Return data in  X Document</returns>
        public XDocument GetPortalProfileImgDetails(int SLID, int USERID, int COMMID, int FMembID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "GetPortalProfileImg_g"),
                new XElement("pSLID", SLID),
                new XElement("pUSERID", USERID),
                new XElement("pCOMMID", COMMID),
                new XElement("pFMembID", FMembID)
                 ));
            return CreateXml;
        }



        #endregion

    }
}
