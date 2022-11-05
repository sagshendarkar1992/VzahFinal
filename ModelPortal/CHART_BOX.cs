using CommonLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ModelPortal.BI;

namespace ModelPortal
{
    public class CHART_BOX
    {
        #region Properties
        public int ID { get; set; }
        public int DCMID { get; set; }
        public string strDCMID { get; set; }
        public int CHARTTYPE { get; set; }
        public string strCHARTTYPE { get; set; }
        public string TITLE { get; set; }
        public string SUBTITLE { get; set; }
        public string LABEL1 { get; set; }
        public string LABEL2 { get; set; }
        public string LABEL3 { get; set; }
        public string LABEL4 { get; set; }
        public string VALUE1 { get; set; }
        public string VALUE2 { get; set; }
        public string VALUE3 { get; set; }
        public string VALUE4 { get; set; }
        public string STYLE { get; set; }
        public string CLASS { get; set; }
        public string ICON1 { get; set; }
        public string ICON2 { get; set; }
        public string QUERY { get; set; }
        public int CREATEDBY { get; set; }
        public string strCREATEDBY { get; set; }
        public string CREATEDDATE { get; set; }
        #endregion

        #region Methods

        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
     
        public List<CHART_BOX> GetChartBoxList(int PageNo, int Flag, int Id, int UserId)
        {
            XDocument xdoc = new XDocument(
           new XDeclaration("1.0", "utf-8", ""),
           new XElement("Kloudq",
           new XElement("XsdName", ""),
           new XElement("ProcName", "CHART_BOX_g"),
           new XElement("pFLAG", Flag),
            new XElement("pID", Id),
            new XElement("pUSERID", UserId)
            ));

            CHART_BOX obj = new CHART_BOX();
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new CHART_BOX
                            {
                                ID = s.Field<int>("ID"),
                                DCMID = s.Field<int>("DCMID"),
                                strDCMID = s.Field<string>("strDCMID"),
                                CHARTTYPE = s.Field<int>("CHARTTYPE"),
                                strCHARTTYPE = s.Field<string>("strCHARTTYPE"),
                                TITLE = s.Field<string>("TITLE"),
                                SUBTITLE = s.Field<string>("SUBTITLE"),
                                LABEL1 = s.Field<string>("LABEL1"),
                                LABEL2 = s.Field<string>("LABEL2"),
                                LABEL3 = s.Field<string>("LABEL3"),
                                LABEL4 = s.Field<string>("LABEL4"),
                                VALUE1 = s.Field<string>("VALUE1"),
                                VALUE2 = s.Field<string>("VALUE2"),
                                VALUE3 = s.Field<string>("VALUE3"),
                                VALUE4 = s.Field<string>("VALUE4"),
                                STYLE = s.Field<string>("STYLE"),
                                CLASS = s.Field<string>("CLASS"),
                                ICON1 = s.Field<string>("ICON1"),
                                ICON2 = s.Field<string>("ICON2"),
                                QUERY = s.Field<string>("QUERY"),
                                CREATEDBY = s.Field<int>("CREATEDBY"),
                                strCREATEDBY = s.Field<string>("strCREATEDBY"),
                                CREATEDDATE = s.Field<DateTime>("CREATEDDATE").ToString()
                            }).ToList();
            return dbResult;
        }

       
        public BIErrors Save(string Flag, CHART_BOX model, int userid)
        {
          
            XDocument xdoc = new XDocument(
       new XDeclaration("1.0", "utf-8", ""),
       new XElement("Kloudq",
       new XElement("XsdName", ""),
       new XElement("ProcName", "CHART_BOX_c"),
       new XElement("pFlag", Flag),
       new XElement("pID", model.ID),
new XElement("pDCMID", model.DCMID),
new XElement("pCHARTTYPE", model.CHARTTYPE),
new XElement("pTITLE", model.TITLE),
new XElement("pSUBTITLE", model.SUBTITLE),
new XElement("pLABEL1", model.LABEL1),
new XElement("pLABEL2", model.LABEL2),
new XElement("pLABEL3", model.LABEL3),
new XElement("pLABEL4", model.LABEL4),
new XElement("pVALUE1", model.VALUE1),
new XElement("pVALUE2", model.VALUE2),
new XElement("pVALUE3", model.VALUE3),
new XElement("pVALUE4", model.VALUE4),
new XElement("pSTYLE", model.STYLE),
new XElement("pCLASS", model.CLASS),
new XElement("pICON1", model.ICON1),
new XElement("pICON2", model.ICON2),
new XElement("pQUERY", model.QUERY),
        new XElement("pUSERID", userid)

        ));
            int statuscheck = objbui.StatusCheck(xdoc);
            BIErrors err = new BIErrors(statuscheck, Flag);
            return err;

        }
        
        #endregion
    }
}
