using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace ModelPortal.Models
{
    public class FuelConsumption
    {

        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();

        //LoginSessionDetails SessLogObj = new LoginSessionDetails();
        public int MACHINEID { get; set; }
        public string MACHINECODE { get; set; }

        public List<FuelConsumptionDetails> lstFuelConsumption { get; set; }

        public List<FuelConsumptionDetails> GetFuelConsumptionData(int FLAG, int BUID, int COMPANYID, string MACHINEID, int SITEID, int CLASSID, string Desc = "", int PageNo = 1, int UserId = 0, string FROMDATE = "", string TODATE = "")
        {
            XDocument CreateXml = new XDocument(
                  new XDeclaration("1.0", "utf-8", ""),
                  new XElement("Kloudq",
                  new XElement("XsdName", ""),
                  new XElement("ProcName", "FUEL_CONSUMPTION_g"),
                  new XElement("pFLAG", FLAG),
                  new XElement("pBUID", BUID),
                  new XElement("pMACHINEID", MACHINEID),
                  new XElement("pCOMPANYID", COMPANYID),
                  new XElement("pSITEID", SITEID),
                  new XElement("pPAGENO", PageNo),
                  //new XElement("pDESC", DESC),
                  new XElement("pUSERID", UserId),
                   new XElement("pFROMDATE", FROMDATE),
                   new XElement("pTODATE", TODATE),
                  //new XElement("pSTATUS", Status),
                  new XElement("pCLASSID", CLASSID)
                  ));

            DataTable Dt = objbui.GetKloudqBusinessList(CreateXml);
            List<FuelConsumptionDetails> dbresult = null;
            dbresult = (from s in Dt.AsEnumerable()
                        select new FuelConsumptionDetails
                        {
                            MACHINEID = s.Field<int>("MACHINEID"),
                            MACHINECODE = s.Field<string>("MACHINECODE"),
                            //strMACHINECODE = "<label class='blue' style='cursor:pointer;' data-type='" + s.Field<int>("MACHINEID") + "' onclick=machineClick(" + s.Field<int>("MACHINEID") + ",1) > " + s.Field<string>("MACHINECODE") + " </label>",
                            DATE = s.Field<string>("ENTRYDATE"),//.ToString("dd-MMM-yyyy"),
                            DAYFUELCONSUMPTION = s.Field<string>("FUELCONSUMPTION"),
                            FUEL_CONSUMED_RUNHRS = s.Field<string>("FUEL_CONSUMED_RUNHRS"),
                            NORM = s.Field<string>("NORM")
                            //MACHINESTATUS = "<a title='Cancel' style='color:white; background-color:" + s.Field<string>("STATUSCOLOUR") + "' class='btn btn-minier btn-sm ' onclick='CancelPendInvoice(" + s.Field<int>("MACHINEID") + ")'><i class='" + s.Field<string>("STATUSICON") + "'></i>&nbsp;" + s.Field<string>("STATUSDESC") + "</a>",

                        }).ToList();
            return dbresult;
        }

        public DataTable GetFuelConsumptionDataTableExport(int FLAG, int BUID, int COMPANYID, string MACHINEID, int SITEID, int CLASSID, string Desc = "", int PageNo = 1, int UserId = 0, string FROMDATE = "", string TODATE = "")
        {
            XDocument CreateXml = new XDocument(
                  new XDeclaration("1.0", "utf-8", ""),
                  new XElement("Kloudq",
                  new XElement("XsdName", ""),
                  new XElement("ProcName", "FUEL_CONSUMPTION_g"),
                  new XElement("pFLAG", FLAG),
                  new XElement("pBUID", BUID),
                  new XElement("pMACHINEID", MACHINEID),
                  new XElement("pCOMPANYID", COMPANYID),
                  new XElement("pSITEID", SITEID),
                  new XElement("pPAGENO", PageNo),
                  //new XElement("pDESC", DESC),
                  new XElement("pUSERID", UserId),
                   new XElement("pFROMDATE", FROMDATE),
                   new XElement("pTODATE", TODATE),
                  //new XElement("pSTATUS", Status),
                  new XElement("pCLASSID", CLASSID)
                  ));

            DataTable Dt = objbui.GetKloudqBusinessList(CreateXml);
            
            return Dt;
        }

        public List<FuelConsumptionDetails> GetFuelConsumptionDataTable(int FLAG, int BUID, int COMPANYID, string MACHINEID, int SITEID, int CLASSID, string Desc = "", int PageNo = 1, int UserId = 0, string FROMDATE = "", string TODATE = "")
        {
            var ImagePath = ConfigurationManager.AppSettings["ImagePath"];

            XDocument CreateXml = new XDocument(
                  new XDeclaration("1.0", "utf-8", ""),
                  new XElement("Kloudq",
                  new XElement("XsdName", ""),
                  new XElement("ProcName", "FUEL_CONSUMPTION_g"),
                  new XElement("pFLAG", FLAG),
                  new XElement("pBUID", BUID),
                  new XElement("pMACHINEID", MACHINEID),
                  new XElement("pCOMPANYID", COMPANYID),
                  new XElement("pSITEID", SITEID),
                  new XElement("pPAGENO", PageNo),
                  //new XElement("pDESC", DESC),
                  new XElement("pUSERID", UserId),
                   new XElement("pFROMDATE", FROMDATE),
                   new XElement("pTODATE", TODATE),
                  //new XElement("pSTATUS", Status),
                  new XElement("pCLASSID", CLASSID)
                  ));

            DataTable Dt = objbui.GetKloudqBusinessList(CreateXml);
            List<FuelConsumptionDetails> dbresult = null;


            dbresult = (from s in Dt.AsEnumerable()
                        select new FuelConsumptionDetails
                        {
                            MACHINEID = s.Field<int>("MACHINEID"),
                            MACHINECODE = s.Field<string>("MACHINECODE"),
                            strMACHINECODE = "<label class='blue' style='cursor:pointer;' data-type='" + s.Field<int>("MACHINEID") + "' onclick=machineClick(" + s.Field<int>("MACHINEID") + ",1) > " + s.Field<string>("MACHINECODE") + " </label>",
                            IMAGEPATH = "<img src='" + ImagePath + "" + s.Field<string>("IMAGE") + "' style='width:60px!important; height: 40px!important'/>",
                            SITENAME = s.Field<string>("SITENAME"),
                            //strMACHINECODE = "<label class='blue' style='cursor:pointer;' data-type='" + s.Field<int>("MACHINEID") + "' onclick=machineClick(" + s.Field<int>("MACHINEID") + ",1) > " + s.Field<string>("MACHINECODE") + " </label>",
                            DATE = s.Field<DateTime>("ENTRYDATE").ToString("dd-MMM-yyyy"),
                            KWHDIFF = s.Field<string>("KWHDIFF"),
                            UOM = s.Field<string>("UOM"),

                            //NORM = s.Field<string>("NORM"),
                            PDC_Total_Per_Day_Ltr = s.Field<string>("PDC_Total_Per_Day_Ltr"),
                            PDC_Idle_Per_Day_Ltr = s.Field<string>("PDC_Idle_Per_Day_Ltr"),
                            PDC_Prod_Per_Day_Ltr = s.Field<string>("PDC_Prod_Per_Day_Ltr"),
                            HMR_Total_Per_Day_Hrs = s.Field<string>("HMR_Total_Per_Day_Hrs"),
                            HMR_Idle_Per_Day_Hrs = s.Field<string>("HMR_Idle_Per_Day_Hrs"),
                            HMR_Prod_Per_Day_Hrs = s.Field<string>("HMR_Prod_Per_Day_Hrs"),


                            PHC_Total_Per_Hrs = s.Field<string>("PHC_Total_Per_Hrs"),

                            PHC_Norm_Per_Hrs = s.Field<string>("PHC_Norm_Per_Hrs"),
                            PHC_Idle_Per_Hrs = s.Field<string>("PHC_Idle_Per_Hrs"),
                            PHC_Prod_Per_Hrs = s.Field<string>("PHC_Prod_Per_Hrs"),
                            FUEL_CONSUMED_KWH = s.Field<string>("FUEL_CONSUMED_KWH"),

                            PAGECOUNT = s.Field<int>("PAGECOUNT"),
                            PAGESIZE = s.Field<int>("PAGESIZE"),
                            TOTALPAGES = s.Field<int>("TOTALPAGES"),
                            TOTALROWS = s.Field<int>("TOTALROWS"),
                            //MACHINESTATUS = "<a title='Cancel' style='color:white; background-color:" + s.Field<string>("STATUSCOLOUR") + "' class='btn btn-minier btn-sm ' onclick='CancelPendInvoice(" + s.Field<int>("MACHINEID") + ")'><i class='" + s.Field<string>("STATUSICON") + "'></i>&nbsp;" + s.Field<string>("STATUSDESC") + "</a>",

                        }).ToList();
            return dbresult;
        }

    }



    public class FuelConsumptionDetails
    {
        public int MACHINEID { get; set; }
        public string MACHINECODE { get; set; }
        public string strMACHINECODE { get; set; }
        public string IMAGEPATH { get; set; }
        public string SITENAME { get; set; }
        public string KWHDIFF { get; set; }
        public string UOM { get; set; }

        public string DAYFUELCONSUMPTION { get; set; }
        public string FUEL_CONSUMED_RUNHRS { get; set; }
        public string PDC_Total_Per_Day_Ltr { get; set; }
        public string PDC_Idle_Per_Day_Ltr { get; set; }
        public string PDC_Prod_Per_Day_Ltr { get; set; }
        public string HMR_Total_Per_Day_Hrs { get; set; }
        public string HMR_Idle_Per_Day_Hrs { get; set; }
        public string HMR_Prod_Per_Day_Hrs { get; set; }
        public string PHC_Total_Per_Hrs { get; set; }
        public string PHC_Norm_Per_Hrs { get; set; }
        public string PHC_Idle_Per_Hrs { get; set; }
        public string PHC_Prod_Per_Hrs { get; set; }
        public string FUEL_CONSUMED_KWH { get; set; }
    public string NORM { get; set; }
        public string DATE { get; set; }
        public int PAGESIZE { get; set; }
        public int TOTALROWS { get; set; }
        public int TOTALPAGES { get; set; }
        public int PAGECOUNT { get; set; }
    }
}
