using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.Master
{
    public class RouteMast
    {
        public int ID { get; set; }
        public int MACHINEID { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string FROMLOCATION { get; set; }
        public string TOLOCATION { get; set; }

        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();

        public List<RouteMast> GetDashboardConfigMast(int FLAG, string VIEWNAME = "", int USERID = 0)
        {
            XDocument xdoc = new XDocument(
                 new XDeclaration("1.0", "utf-8", ""),
                 new XElement("Kloudq",
                  new XElement("XsdName", ""),
                  new XElement("ProcName", "DASHBOARD_CONFIG_MAST_g"),
                  new XElement("pFLAG", FLAG),
                  new XElement("pVIEWNAME", VIEWNAME),
                  new XElement("pUSERID", USERID)
                 ));
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            List<RouteMast> dbresult = null;
            dbresult = (from s in dt.AsEnumerable()
                        select new RouteMast
                        {
                            ID = s.Field<int>("ID"),
                            MACHINEID = s.Field<int>("MACHINEID"),
                            FROMDATE = s.Field<string>("FROMDATE"),
                            TODATE = s.Field<string>("TODATE"),
                            FROMLOCATION = s.Field<string>("FROMLOCATION"),
                            TOLOCATION = s.Field<string>("TOLOCATION"),
                            
                        }).ToList();
            return dbresult;
        }

    }
}
