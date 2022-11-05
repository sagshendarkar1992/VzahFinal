using DataAccessBusinessPortal;
using ModelPortal.BI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.Department
{
    public class DepartmentMast
    {
        public string FNAME { get; set; }
        public int PAGENO { get; set; }
        public int FLAG { get; set; }
        public int PAGESIZE { get; set; }
        public int TOTALROWS { get; set; }
        public int TOTALPAGES { get; set; }
        public int PAGECOUNT { get; set; }
        public string Str_Delete { get; set; }
        public string Str_Update { get; set; }
        public string CREATEDDATE { get; set; }
        public int DEPARTMENTID { get; set; }
        public string DEPARTMENTSHORTNAME { get; set; }
        public string DEPARTMENTDESC { get; set; }
        public Nullable<bool> Is_Active { get; set; }

        #region Methods
        //  DataAccessXmlPortal.AdminSetup.GeneralSettingXML objxml = new DataAccessXmlPortal.AdminSetup.GeneralSettingXML();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        KloudqBusiness objbui = new KloudqBusiness();
        /// <summary>
        /// This method is  used for getting list of DepartmentMast
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Deptid">department id</param>
        /// <param name="userid">user id</param>
        /// <returns>DepartmentMast master list</returns>
        public List<DepartmentMast> GetDepartmentDataList(int Page, int flag, int PLANTLOCATIONID, int UserId)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Kloudq",
           new XElement("XsdName", ""),
           new XElement("ProcName", "DEPARTMENT_g"),
           new XElement("pFLAG", flag),
           new XElement("pDEPARTMENTID", DEPARTMENTID),
           new XElement("pUSERID", UserId)));
            DepartmentMast obj = new DepartmentMast();
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new DepartmentMast
                            {
                                DEPARTMENTID = s.Field<int>("DEPARTMENTID"),
                                DEPARTMENTSHORTNAME = s.Field<string>("DEPARTMENTSHORTNAME"),
                                DEPARTMENTDESC = s.Field<string>("DEPARTMENTDESC"),
                                FNAME = s.Field<string>("FNAME"),
                                Str_Delete = s.Field<string>("Str_Delete"),
                                Str_Update = s.Field<string>("Str_Update"),
                                CREATEDDATE = s.Field<string>("CREATEDDATE"),
                                PAGECOUNT = s.Field<int>("PAGECOUNT"),
                                PAGESIZE = s.Field<int>("PAGESIZE"),
                                TOTALPAGES = s.Field<int>("TOTALPAGES"),
                                TOTALROWS = s.Field<int>("TOTALROWS")
                            }).ToList();
            return dbResult;
        }

        /// <summary>
        /// This method is used for insert update delete  record in DepartmentMast
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="model">model contain DepartmentMast</param>
        /// <param name="userid">user id</param>
        /// <return> Return BI error message </returns>
        public BIErrors Save(string Flag, DepartmentMast model, int UserId)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", ""),
            new XElement("Kloudq",
            new XElement("XsdName", ""),
            new XElement("ProcName", "DEPARTMENT_c"),
            new XElement("pDEPARTMENTID", model.DEPARTMENTID),
            new XElement("pDEPARTMENTSHORTNAME", model.DEPARTMENTSHORTNAME),
            new XElement("pDEPARTMENTDESC", model.DEPARTMENTDESC),
            new XElement("pFlag", Flag),
            new XElement("pUSERID", UserId)));
            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, Flag);
            return err;
        }
        #endregion
    }
}
