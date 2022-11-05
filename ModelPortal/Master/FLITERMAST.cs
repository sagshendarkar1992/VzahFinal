using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ModelPortal.Master
{
   public class FLITERMAST
    {
            public int ID { get; set; }
            public int DCMID { get; set; }
            public int FILTERTYPEID { get; set; }
            public string NAME { get; set; }
            public string PLACEHOLDER { get; set; }
            public string VAL { get; set; }
            public string PARAM { get; set; }

        public List<FLITERMAST> LST { get; set; }

        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();

            public List<FLITERMAST> GetFLITERMAST(string FLAG = "F", int ID = 0, int DCMID = 0, int FILTERTYPEID = 0, string NAME = "" , int USERID = 0)
            {
                XDocument xdoc = new XDocument(
                     new XDeclaration("1.0", "utf-8", ""),
                     new XElement("Kloudq",
                      new XElement("XsdName", ""),
                      new XElement("ProcName", "CHART_FLITERMAST_g"),
                      new XElement("pFLAG", FLAG),
                      new XElement("pID", ID),
                      new XElement("pDCMID", DCMID),
                      new XElement("pFILTERTYPEID", FILTERTYPEID),
                      new XElement("pNAME", NAME),
                      new XElement("pUSERID", USERID)
                     ));
                DataTable dt = objbui.GetKloudqBusinessList(xdoc);
                List<FLITERMAST> dbresult = null;
                dbresult = (from s in dt.AsEnumerable()
                            select new FLITERMAST
                            {
                                ID = s.Field<int>("ID"),
                                DCMID = s.Field<int>("DCMID"),
                                FILTERTYPEID = s.Field<int>("FILTERTYPEID"),
                                NAME = s.Field<string>("NAME"),
                                PLACEHOLDER = s.Field<string>("PLACEHOLDER"),
                                PARAM = s.Field<string>("PARAM")
                            }).ToList();
                return dbresult;
            }

     }

    public class FiltersVal
    {
        public int ID { get; set; }
        public int DCMID { get; set; }
        public string VAL { get; set; }
    }
}
