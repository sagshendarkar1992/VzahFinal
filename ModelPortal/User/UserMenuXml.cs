using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal.User
{
    public class UserMenuXml
    {
        /// <summary>
        /// This method is used for User Menu List Xml
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ModuleID"></param>
        /// <param name="MenuId"></param>
        /// <param name="AllMenuFor"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument GetUserMenuListXml(int UserID, int ModuleID, int MenuId, string AllMenuFor)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "UserAuth_g"),
                new XElement("pUserID", UserID),
                new XElement("pModuleID", ModuleID),
                new XElement("pMENUID", MenuId),
                new XElement("AllMenuFor", AllMenuFor)
                 ));
            return CreateXml;
        }
        /// <summary>
        /// This method is used for getuser action
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SysDocID"></param>
        /// <param name="ActGrp"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument getUserActionXml(int UserId, int SysDocID, int ActGrp)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "UserMNUACTRights_g"),
                new XElement("pUserID", UserId),
                new XElement("pSYSDOCID", SysDocID),
                new XElement("pACTGRP", ActGrp)
                ));
            return CreateXml;
        }
        /// <summary>
        /// This method is used for get search user menu
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ModuleID"></param>
        /// <param name="Desc"></param>
        /// <param name="FindDesc"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument Getmenuserach(int UserId, int ModuleID, string Desc, string FindDesc)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "MENUCODESEARCH_h"),
                new XElement("pUserID", UserId),
                new XElement("pDESC", Desc),
                new XElement("pFINDDESC", FindDesc),
                 new XElement("pMODULEID", ModuleID)

                ));
            return CreateXml;
        }
        /// <summary>
        /// This method is used for get user transaction action  validation
        /// </summary>
        /// <param name="SYSDOCID"></param>
        /// <param name="DOCID"></param>
        /// <param name="MID"></param>
        /// <param name="ACTGRP"></param>
        /// <param name="TRANSSTATUS"></param>
        /// <param name="ISAUTHO"></param>
        /// <param name="UserId"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument GetTransactionActionList_CodeXml(int SYSDOCID, int DOCID, int MID, int ACTGRP, int TRANSSTATUS, int ISAUTHO, int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "PORTALUSERDOCACT_g"),
                new XElement("pXML", new XElement("USERDOCRIGHTS",
                                      new XElement("SYSDOCID", SYSDOCID),
                                      new XElement("DOCID", DOCID),
                                      new XElement("MID", MID),
                                      new XElement("USERID", UserId),
                                      new XElement("ACTGRP", ACTGRP),
                                      new XElement("TRANSSTATUS", TRANSSTATUS),
                                      new XElement("ISAUTHO", ISAUTHO)).ToString())));

            return CreateXml;
        }
        /// <summary>
        /// This method is used for get user transaction action  validation
        /// </summary>
        /// <param name="DOCID"></param>
        /// <param name="MID"></param>
        /// <param name="TID"></param>
        /// <param name="ACTION"></param>
        /// <param name="LINEWISE"></param>
        /// <param name="UserId"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument TransValidation_CodeXml(int DOCID, int MID, int TID, int ACTION, int LINEWISE, int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "VALIDATION_v"),
                new XElement("pDOCID", DOCID),
                new XElement("pMID", MID),
                new XElement("pTID", TID),
                new XElement("pACTION", ACTION),
                new XElement("pLINEWISE", LINEWISE),
                new XElement("pUSERID", UserId),
                new XElement("pERRORXML", "")
                )
                );
            return CreateXml;
        }
        /// <summary>
        /// This method is used for get user transaction action  validation
        /// </summary>
        /// <param name="DOCID"></param>
        /// <param name="MID"></param>
        /// <param name="TID"></param>
        /// <param name="UserId"></param>
        /// <returns>Return data in  X Document</returns>

        public XDocument TransValidationNew_CodeXml(int DOCID, int MID, int TID, int UserId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "DOCDELETE_i"),
                new XElement("pDOCID", DOCID),
                new XElement("pMID", MID),
                new XElement("pTID", TID),
                new XElement("pUSERID", UserId),
                new XElement("pERRORXML", "")
                )
                );
            return CreateXml;
        }
        /// <summary>
        /// This method is used for get user transaction action 
        /// </summary>
        /// <param name="DOCID"></param>
        /// <param name="SYSDOCID"></param>
        /// <param name="MID"></param>
        /// <param name="ACTIONID"></param>
        /// <param name="UserId"></param>
        /// <returns>Return data in  X Document</returns>


        public XDocument TransSetStatusAction_CodeXml(int DOCID, int SYSDOCID, int MID, int ACTIONID, int UserId, string Reason = "")
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Kloudq",
                new XElement("XsdName", ""),
                new XElement("ProcName", "SETSTATUS_c"),
                new XElement("pDOCID", DOCID),
                new XElement("pSYSDOCID", SYSDOCID),
                new XElement("pMID", MID),
                new XElement("pACTIONID", ACTIONID),
                new XElement("pREASON", Reason),
                new XElement("USERID", UserId),
                new XElement("pERRORXML", "")
                )
                );
            return CreateXml;
        }
    }
}
