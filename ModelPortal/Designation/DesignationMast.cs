using DataAccessBusinessPortal;
using ModelPortal.BI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.Designation
{
    public class DesignationMast
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
        public int DESIGNATIONID { get; set; }
        public string DESIGNATIONSHORTNAME { get; set; }
        public string DESIGNATIONDESC { get; set; }
        public Nullable<bool> Is_Active { get; set; }

        #region Methods
        //  DataAccessXmlPortal.AdminSetup.GeneralSettingXML objxml = new DataAccessXmlPortal.AdminSetup.GeneralSettingXML();
        /// <summary>
        /// This is used for access data form data base use it  function  for functionality to acces data from database
        /// </summary>
        KloudqBusiness objbui = new KloudqBusiness();
        /// <summary>
        /// This method is  used for getting list of DesignationMast
        /// </summary>
        /// <param name="flag">flag</param>
        /// <param name="Deptid">department id</param>
        /// <param name="userid">user id</param>
        /// <returns>DesignationMast master list</returns>
        public List<DesignationMast> GetDesignationDataList(int Page, int flag, int DesignationID, int UserId)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Kloudq",
           new XElement("XsdName", ""),
           new XElement("ProcName", "DESIGNATION_g"),
           new XElement("pFLAG", flag),
           new XElement("pDESIGNATIONID", DESIGNATIONID),
           new XElement("pUSERID", UserId)));
            DesignationMast obj = new DesignationMast();
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new DesignationMast
                            {
                                DESIGNATIONID = s.Field<int>("DESIGNATIONID"),
                                DESIGNATIONSHORTNAME = s.Field<string>("DESIGNATIONSHORTNAME"),
                                DESIGNATIONDESC = s.Field<string>("DESIGNATIONDESC"),
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
        /// This method is used for insert update delete  record in DesignationMast
        /// </summary>
        /// <param name="Flag">flag</param>
        /// <param name="model">model contain DesignationMast</param>
        /// <param name="userid">user id</param>
        /// <return> Return BI error message </returns>
        public BIErrors Save(string Flag, DesignationMast model, int UserId)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", ""),
            new XElement("Kloudq",
            new XElement("XsdName", ""),
            new XElement("ProcName", "DESIGNATION_c"),
            new XElement("pDESIGNATIONID", model.DESIGNATIONID),
            new XElement("pDESIGNATIONSHORTNAME", model.DESIGNATIONSHORTNAME),
            new XElement("pDESIGNATIONDESC", model.DESIGNATIONDESC),
            new XElement("pFlag", Flag),
            new XElement("pUSERID", UserId)));
            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, Flag);
            return err;
        }
        #endregion
    }
}
