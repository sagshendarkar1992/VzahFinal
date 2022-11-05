using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.User
{
    public class UserControlXml
    {
        public XDocument GetWorkFlowStripCountList(DateTime INVCDATE, int UserID, string ReportFor)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Cloud9",
                new XElement("XsdName", ""),
                new XElement("ProcId", "1158"),
                new XElement("pXml", new XElement("SLBillHdr", new XElement("INVCDATE", INVCDATE), new XElement("ReportFor", ReportFor)).ToString()),
                new XElement("pUserID", UserID)));

            return CreateXml;
        }

        public XDocument GetWorkFlowActionXml(int SysdocID, int TranTypeID, int MID, int Actions, int DOCID, int PROCESSID, int UserId)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1160"),
                            new XElement("pSysdocID", SysdocID),
                            new XElement("pDOCID", DOCID),
                            new XElement("pTranTypeID", TranTypeID),
                            new XElement("pMID", MID),
                            new XElement("pxml", new XElement("ROOT", new XElement("WKFLSet", new XElement("Actions", Actions), new XElement("UserID", UserId)), new XElement("PROCESSID", PROCESSID)).ToString())));

            return CreateXml;
        }

        public XDocument GetTransModeHelpXml(int Flag, int TrnMdID, string SearchStr, int UserId)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1168"),//TransMode_h
                            new XElement("Flag", Flag),
                            new XElement("Xml", new XElement("TransMode", new XElement("TrnMdID", TrnMdID), new XElement("SearchStr", SearchStr)).ToString()),
 new XElement("UserID", UserId)));
            return CreateXml;
        }

        //public XDocument GetBankAcccountHelpXml(int BACCOUNTID, int FLAG, int DOCID, string SearchStr, int UserId)
        //{
        //    XDocument CreateXml = new XDocument(
        //                    new XDeclaration("1.0", "utf-8", ""),
        //                    new XElement("Cloud9",
        //                    new XElement("XsdName", ""),
        //                    new XElement("ProcId", "1171"),//BANKACCOUNTMAST_h                          
        //                    new XElement("XMLFILE", new XElement("BANKMAST", new XAttribute("FLAG", FLAG), new XAttribute("BACCOUNTID", BACCOUNTID), new XAttribute("DOCID", DOCID), new XAttribute("USERID", UserId)).ToString())));
        //    return CreateXml;
        //}

        public XDocument GetBankAcccountHelpXml(int BACCOUNTID, int ACCOUNTID, int DOCID, int UserId)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1171"),//BANKACCOUNTMAST_h                          
                            new XElement("XMLFILE", new XElement("BANKMAST", new XAttribute("ACCOUNTID", ACCOUNTID), new XAttribute("BACCOUNTID", BACCOUNTID), new XAttribute("DOCID", DOCID), new XAttribute("USERID", UserId)).ToString())));
            return CreateXml;
        }

        public XDocument GetCustUnitDetailsHelpXml(int SLID, int UnitID)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1174"),//CustUnitDetails_g                          
                            new XElement("pSLID", SLID), new XElement("pUnitID", UnitID)));
            return CreateXml;
        }

        public XDocument GetCustUnitPaymentDetailsXml(int SLID, int UnitID, decimal NewRptAmount, int receiptid)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1179"),//SLUnitPaymentStatus                          
                            new XElement("pSLID", SLID), new XElement("pUnitID", UnitID), new XElement("pNewRptAmount", NewRptAmount), new XElement("pEditRptMID", receiptid)));
            return CreateXml;
        }

        #region Reminder ControlXML


        public XDocument SetReminder_Email_Msg(string Xml)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1212"),//SLUnitPaymentStatus                          
                            new XElement("pRmdXML", Xml)
                            )
                            );
            return CreateXml;
        }


        public XElement SetReminder_CodeXML(int SysDocID, int DocID, int MID, int LeadActivityID, string SetRmndFor, string RmdDate
              , string RmdTime, string RmdToUserID, string MsgSend, string ProcCode, string CatCode, string CreatedBy, string CreatedOn, bool IsCompleted)
        {
            XElement xle = new XElement("Rmnd",
                new XElement("SysDocID", SysDocID),
                new XElement("DocID", DocID),
                new XElement("MID", MID),
                new XElement("LeadActivityID", LeadActivityID),
                new XElement("SetRmndFor", SetRmndFor),
                new XElement("RmdDate", RmdDate),
                new XElement("RmdTime", RmdTime),
                new XElement("RmdToUserID", RmdToUserID),
                new XElement("MsgSend", MsgSend),

                 new XElement("ProcCode", ProcCode),
                new XElement("CatCode", CatCode),
                new XElement("CreatedBy", CreatedBy),
                new XElement("CreatedOn", CreatedOn),
                new XElement("IsCompleted", Convert.ToByte(IsCompleted))
                );

            return xle;
        }


        #endregion

        #region Salutaion Help

        public XDocument GetSalutationHelpXml(int Flag, string Desc, string Code)
        {
            XDocument CreateXml = new XDocument(
                            new XDeclaration("1.0", "utf-8", ""),
                            new XElement("Cloud9",
                            new XElement("XsdName", ""),
                            new XElement("ProcId", "1219"),
                            new XElement("pFlag", Flag),
                            new XElement("pDesc", Desc),
                            new XElement("pCode", Code)));

            return CreateXml;
        }

        #endregion
    }
}
