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
    public class RoleMast
    {
        XDocument xdoc;
        UserManagementXml objxml = new UserManagementXml();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        DataTable dt;

        public RoleMast()
        { }

        #region Properties

        public int ROLEID
        {
            get { return _ROLEID; }
            set { _ROLEID = value; }
        }
        private int _ROLEID;
        public int USERROLEID
        {
            get { return _USERROLEID; }
            set { _USERROLEID = value; }
        }
        private int _USERROLEID;
        public int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        private int _USERID;
        public string ROLEDESC
        {
            get { return _ROLEDESC; }
            set { _ROLEDESC = value; }
        }
        private string _ROLEDESC;
        [Required(ErrorMessage = "Please enter Role.")]
        [StringLength(50, ErrorMessage = "Description is not greater than 50 char.")]
        //[Remote("IsExistRole", "UserManagment", HttpMethod = "POST", AdditionalFields = "ROLEID", ErrorMessage = "Roles already exists")]
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private string _DESCRIPTION;

        public string DESCRIPTION1
        {
            get { return _DESCRIPTION1; }
            set { _DESCRIPTION1 = value; }
        }
        private string _DESCRIPTION1;

        [StringLength(1000, ErrorMessage = "Description is not greater than 1000 char.")]
        public string LONGTEXT
        {
            get { return _LONGTEXT; }
            set { _LONGTEXT = value; }
        }
        private string _LONGTEXT;

        public bool ISDOMAIN
        {
            get { return _ISDOMAIN; }
            set { _ISDOMAIN = value; }
        }
        private bool _ISDOMAIN;

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
        public int FUNID
        {
            get { return _FUNID; }
            set { _FUNID = value; }
        }
        private int _FUNID;

        public string FUNCODE
        {
            get { return _FUNCODE; }
            set { _FUNCODE = value; }
        }
        private string _FUNCODE;

        public string FUNDESC
        {
            get { return _FUNDESC; }
            set { _FUNDESC = value; }
        }
        private string _FUNDESC;

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

        public string Active { get; set; }

        #endregion


        public Random a = new Random(DateTime.Now.Ticks.GetHashCode());
        public List<int> randomList = new List<int>();
        int MyNumber = 0;
        /// <summary>
        /// This method is used for generate new random number
        /// </summary>
        /// <returns>new random number</returns>
        public int NewNumber()
        {
            MyNumber = a.Next(0, 1000);
            if (!randomList.Contains(MyNumber))
                randomList.Add(MyNumber);
            return MyNumber;
        }

        #region Methods
        /// <summary>
        /// Method used for getting user role list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="RoleID">role id</param>
        /// <returns>user role list</returns>
        public List<RoleMast> GetRoleMastList(int Flag, int RoleID)
        {
            xdoc = objxml.GetRoleMastList_CodeXml(Flag, RoleID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<RoleMast> model = dt.AsEnumerable().Select(row =>
                                new RoleMast
                                {
                                    ROLEID = row.Field<int>("ROLEID"),
                                    DESCRIPTION = row.Field<string>("DESCRIPTION"),
                                    LONGTEXT = (RoleID == 0) ? ("<label class='blue' style='cursor:pointer;' onclick=onRoleClick(" + row.Field<int>("ROLEID") + ") > " + row.Field<string>("LONGTEXT") + " </label> ") : row.Field<string>("LONGTEXT"),
                                    ISDOMAIN = row.Field<bool?>("ISDOMAIN") == null ? false : true,
                                    ISACTIVE = row.Field<bool>("ISACTIVE"),
                                    Active = row.Field<bool>("ISACTIVE") == true ? "<i class='icon-ok bigger-110 green'></i>" : "<i class='icon-remove bigger-110 red'></i>",
                                    ISAUTH = row.Field<bool>("ISAUTH")
                                }).ToList();

            return model;
        }
        /// <summary>
        /// Method used for getting function role map list
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="ROLEID">role id</param>
        /// <param name="ROLEFUNID">role function id</param>
        /// <returns>role map list</returns>
        public List<SelectListItem> GetFunRoleMAapList(short Flag, int ROLEID, int ROLEFUNID)
        {

            xdoc = objxml.GetRoleFunctionMap_CodeXml(Flag, ROLEID, ROLEFUNID);
            dt = objbui.GetKloudqBusinessList(xdoc);
            List<SelectListItem> IList = dt.AsEnumerable().Select(row =>
                                    new SelectListItem
                                    {
                                        Text = row.Field<string>("FUNDESC")
                                    }).ToList();
            return IList;
        }


        #endregion

    }
}
