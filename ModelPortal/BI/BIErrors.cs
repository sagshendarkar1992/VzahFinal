using DataAccessBusinessPortal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml.Linq;

namespace ModelPortal.BI
{
    public class BIErrors //class for business intelligence errors
    {
        public string REDIRECTURL { get; set; }
        public int errorID { get; set; }
        public string errorMsg { get; set; }
        public string flag { get; set; }
        public string Inserted { get; set; }
        public BIErrors() { }
        public BIErrors(int id, string Flag)//method for display success message
        {
            this.errorID = id;
            this.flag = Flag;
            string errmsg = "";
            switch (Flag)
            {
                case "I":
                    errmsg = id > 0 ? "Successfully Saved" : "Problem Occured while Save";
                    break;
                case "E":
                    errmsg = id > 0 ? "Successfully Updated" : "Problem Occured while update";
                    break;
                case "D":
                    errmsg = id > 0 ? "Successfully Deleted" : "Problem Occured while Delete";
                    break;
                case "C":
                    errmsg = id > 0 ? "Successfully Cancelled" : "Problem Occured while Cancel";
                    break;
                case "S":
                    errmsg = id > 0 ? "Order confirm Successfully" : "Order not confirm";
                    break;
                case "A":
                    errmsg = id > 0 ? "Record Approved Successfully" : "Record not Approved";
                    break;
                case "R":
                    errmsg = id > 0 ? "Record Rejected Successfully" : "Record not Rejected";
                    break;
            }
            this.errorMsg = errmsg;
        }
        public void ReadBIErrors(string str)//method for read bi error message
        {
            XDocument xmlSuccess = XDocument.Parse(str.ToString());
            var result = (from i in xmlSuccess.Descendants("SYSMSGS")
                          select new
                          {
                              errorID = Convert.ToString(i.Element("ID").Value),
                              errorMsg = i.Element("ERRORMSGS").Value
                          });
            this.errorID = Convert.ToInt32(result.FirstOrDefault().errorID);
            this.errorMsg = result.FirstOrDefault().errorMsg;
        }

    }

    public class BIPrintClass //class for print bi error
    {
        public int BIRPTID { get; set; }
        public int pUSERID { get; set; }
        public string PageSize { get; set; }
        public bool IsLandScape { get; set; }
        public string ReportXml { get; set; }
        BIXml objxml = new BIXml();
        DataAccessBusinessPortal.VzahBusiness objbui = new DataAccessBusinessPortal.VzahBusiness();

        public string BIxmlgeneration()//code for bi xml generation
        {
            XDocument xdoc;
            XElement xml = new XElement("PRINTREPORT");
            xml.SetAttributeValue("PAGESIZE", this.PageSize);
            xml.SetAttributeValue("LANDSCAPE", this.IsLandScape);
            xdoc = objxml.BIGenerateXML_CodeXml(this.BIRPTID, this.pUSERID, xml.ToString(), "");
            //xdoc = objxml.BIGenerateXML_CodeXml(this.BIRPTID,2, xml.ToString(), "");
            this.ReportXml = objbui.GetXMLFormat(xdoc);
            return this.ReportXml;
        }

        public void BIUserpage(string NAME, string VALUE, int RPTID, int USERID)//code for  bi user page
        {
            int statuscount;
            XDocument xdoc;
            xdoc = objxml.BIUserPage_CodeXml(NAME, VALUE, RPTID, USERID);
            statuscount = Convert.ToInt32(objbui.StatusCheck(xdoc));
        }
        //code for column width 
        public void ColumnWidth(int BIRPTID, int DTCOLID, int USERID, string Type, string Value)
        {
            int statuscount;
            XDocument xdoc;
            xdoc = objxml.BIRptColorFormatting_CodeXml(BIRPTID, DTCOLID, USERID, Type, Value);
            statuscount = Convert.ToInt32(objbui.StatusCheck(xdoc));
        }

    }

    public class BIXml
    {
        //xml for getting report
        public XDocument GetReport_CodeXml(string Flag, int DSID, int RPTID, int USERID, string OUTPUTXML)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "getDSColum_g"),
                new XElement("pFlag", Flag),
                new XElement("pDSID", DSID),
                new XElement("pRPTID", RPTID),
                new XElement("pUSERID", USERID),
                new XElement("pxmlOut", OUTPUTXML)
                ));
            return CreateXml;
        }

        /*
      EXEC dbo.getReportData_g @pRPTID = 0, -- int
      @pFILTERXML = NULL, -- xml
      @pUSERID = 0 -- int */

        public XDocument GetReportData_CodeXml(int RPTID, string FILTERXML, int USERID, int GraphID = 0)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "getReportData_g"),
                new XElement("pRPTID", RPTID),
                new XElement("pFILTERXML", FILTERXML),
                new XElement("pUSERID", USERID),
                new XElement("pGRAPHID", GraphID)  //Added by Vipin - 3/3/17  (Given by Nilesh)
                ));
            return CreateXml;
        }

        /*
        Insert and Update Functionality SP        
        DECLARE @pERRORXML XML
        DECLARE @pXmlFile XML
        SET @pXmlFile = '<ROOT>
          <RPTCOLUMN>
            <COLUMN BIRPTID="0" DSID="1" RPTNAME="SAMPLETEST7" RPTTYP="S" RPTDESC="T" USERID="1" REPTFID="0" />
          </RPTCOLUMN>
          <DTCOLUMNS>
            <COLUMN DTCOLID="1" SEQ="" />
            <COLUMN DTCOLID="2" SEQ="" />
            <COLUMN DTCOLID="3" SEQ="" />
            <COLUMN DTCOLID="11" SEQ="" />
            <COLUMN DTCOLID="14" SEQ="" />
            <COLUMN DTCOLID="13" SEQ="" />
            <COLUMN DTCOLID="15" SEQ="" />
          </DTCOLUMNS>
          <GROUP>
            <COLUMN DTCOLID="3" TYP="0" SEQ="0" />
            <COLUMN DTCOLID="5" TYP="0" SEQ="0" />
            <COLUMN DTCOLID="6" TYP="1" SEQ="0" />
            <COLUMN DTCOLID="8" TYP="1" SEQ="0" />
          </GROUP>
          <FORMULA>
            <FORMULA DTCOLID="2" FORMULA="test" FTYPE="M"  />
            <FORMULA DTCOLID="11" FORMULA="test" FTYPE="S" />
            <FORMULA DTCOLID="14" DTID="3" FORMULA="test2" />
            <FORMULA DTCOLID="15"  FORMULA="test4" FTYPE="N" />
          </FORMULA>
        </ROOT>'
        EXEC dbo.getDSColum_c @pFLAG = 'i', -- varchar(1)
            @pBIRPTID = 0, @pXmlFile = @pXmlFile, @pUSERID = 1, @pERRORXML = @pERRORXML OUTPUT 
            SELECT @pERRORXML*/

        //xml for getting column data
        public XDocument GetDSColumnData_CodeXml(string FLAG, int BIRPTID, string XML, int USERID, string xmlOut)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "getDSColum_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pBIRPTID", BIRPTID),
                new XElement("pXmlFile", XML),
                new XElement("pUSERID", USERID),
                new XElement("pxmlOut", xmlOut)
                ));
            return CreateXml;
        }

        /* 
            Report Folder Help SP
            EXEC  BIRPTFOLDER_h 
	        @pFlag = 1,
	        @pREPTFID = 3,
	        @pREPTFNAME = '',
	        @pUSERID = 1          
         */
        //xml for getting folder help
        public XDocument GetFolderHelp_CodeXml(int Flag, int REPTFID, string REPTFNAME, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIRPTFOLDER_h"),
                new XElement("pFlag", Flag),
                new XElement("pREPTFID", REPTFID),
                new XElement("pREPTFNAME", REPTFNAME),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }
        //xml for set report folder
        public XDocument SetRptFolder_CodeXml(string Flag, int REPTFID, string REPTFNAME, string REPTFDESC, string REPTFTYP, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIRPTFOLDER_c"),
                new XElement("pFlag", Flag),
                new XElement("pREPTFID", REPTFID),
                new XElement("pREPTFNAME", REPTFNAME),
                new XElement("pREPTFDESC", REPTFDESC),
                new XElement("pREPTFTYP", REPTFTYP),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        /*
        Report List
        EXEC BIRPTLIST_g 
        @pFlag = 1 ,
        @pREPTFID = 3,
        @pRPTNAME = '',
        @pUSERID = 1 */

        //xml for getting bi report list
        public XDocument GetBIReportList_CodeXml(int Flag, int REPTFID, string RPTNAME, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIRPTLIST_g"),
                new XElement("pFlag", Flag),
                new XElement("pREPTFID", REPTFID),
                new XElement("pRPTNAME", RPTNAME),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }


        /*
        Favourites Insert and Delete Functionality
        EXEC BIFAV_c @pFLAG = 'D', -- char(1)
        @pBIRPTID = 7, -- int
        @pUSERID = 1 -- int*/

        //xml for bi favourties
        public XDocument Favourites_CodeXml(string FLAG, int BIRPTID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIFAV_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pBIRPTID", BIRPTID),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        /*
     Default Dashboard List
     EXEC BIFAV_g @pFLAG = 1, -- int
	 @pBIRPTID = 0, -- int
	 @pUSERID = 1 -- int*/

        //xml for getting default dashboard
        public XDocument DefaultDashboard_CodeXml(int FLAG, int BIRPTID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIFAV_g"),
                new XElement("pFLAG", FLAG),
                new XElement("pBIRPTID", BIRPTID),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }
        //xml for getting bi graph data
        public XDocument GetGraphData_CodeXml(string FLAG, int GRAPHID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIGRAPH_g"),
                new XElement("pFlag", FLAG),
                new XElement("pGRAPHID", GRAPHID),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }
        //xml for insert bi graph
        public XDocument SetGraphData_CodeXml(string FLAG, int GRAPHID, string XmlFile, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIGRAPH_c"),
                new XElement("pFlag", FLAG),
                new XElement("pGRAPHID", GRAPHID),
                new XElement("XmlFile", XmlFile),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }
        //xml for getting bi date
        public XDocument BiGetDate_CodeXml(int pType)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BiGetDate_g"),
                new XElement("pType", pType)
                ));
            return CreateXml;
        }

        /*EXEC BIGRAPHLIST_g @pFlag = 1, -- smallint
        @pGRAPHID = 0, -- int
        @pRPTID = 6, -- int
        @pCHARTNAME = '', -- varchar(50)
        @pUSERID = 1 -- int*/
        //xml for getting bi graph list
        public XDocument GetGraphList_CodeXml(int Flag, int GRAPHID, int RPTID, string CHARTNAME, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIGRAPHLIST_g"),
                new XElement("pFlag", Flag),
                new XElement("pGRAPHID", GRAPHID),
                new XElement("pRPTID", RPTID),
                new XElement("pCHARTNAME", CHARTNAME),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        /*
        Report Filter List
        DECLARE @v XML
        EXEC dbo.BIFilter_g @pRPTID = 6, -- int
        @pUSERID = 1, -- int
        @pxmlOut = @v OUT -- xml
        SELECT  @v*/
        //xml for report filter list
        public XDocument GetReportFilterList_CodeXml(int RPTID, int USERID, string OUTPUTXML)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIFilter_g"),
                new XElement("pRPTID", RPTID),
                new XElement("pUSERID", USERID),
                new XElement("pxmlOut", OUTPUTXML)
                ));
            return CreateXml;
        }

        /* EXEC dbo.BIFilter_c @pFLAG = 'E', -- varchar(1)
         @pUSERID = 1, -- int
         @pRPTID =6,--int
         @XmlFile = '<SYSREPT>
       <BIFILTER>
         <COLUMN FILTERID="1" CONTROLTYP="1" DISPLAYNAME="TEST" CONTROLNAME="TEST" BIRPTID="6" DATA="MACK" LISTDATA="&lt;TEST&gt;&lt;DATA&gt;1&lt;/DATA&gt;&lt;/TEST&gt;" />
         <COLUMN FILTERID="3" CONTROLTYP="3" DISPLAYNAME="DATEPICKER" CONTROLNAME="DATEPICKER" BIRPTID="6" DATA="TEST" LISTDATA="&lt;DATA&gt;&lt;FROMDATE&gt;1-1-1999&lt;/FROMDATE&gt;&lt;TODATE&gt;1-1-1999&lt;/TODATE&gt;&lt;/DATA&gt;" />
       </BIFILTER>
       <BIFILTERCUSTOM>
         <COLUMN ID="4" BIRPTID="6" FILTERCOLUMNID="30" FILTERCONDITION="4" FILTERVALUE="51" />
         <COLUMN ID="5" BIRPTID="6" FILTERCOLUMNID="23" FILTERCONDITION="6" FILTERVALUE="43" />
         <COLUMN ID="6" BIRPTID="6" FILTERCOLUMNID="23" FILTERCONDITION="6" FILTERVALUE="43" />
       </BIFILTERCUSTOM>
     </SYSREPT>' -- xml
    
     select * from  BIFILTERCUSTOM
     select * from BIUSERFILT*/
        //xml for bi filter
        public XDocument ReportFilter_CodeXml(string FLAG, int USERID, int RPTID, string XMLFILE)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIFilter_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pUSERID", USERID),
                new XElement("pRPTID", RPTID),
                new XElement("pXMLFILE", XMLFILE)
                ));
            return CreateXml;
        }
        //xml for get dashboard
        public XDocument GetDashboard_CodeXml(int DashboardId, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIDashboard_g"),
                new XElement("pRPTID", DashboardId),
                new XElement("pUSERID", USERID),
                new XElement("pOUTXML", "")
                ));
            return CreateXml;
        }
        //xml for insert dasboard
        public XDocument SetDashboard_CodeXml(string FLAG, int USERID, int DashboardId, string XMLFILE)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIDashboard_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pRPTID", DashboardId),
                new XElement("pUSERID", USERID),
                new XElement("pSYSDATA", XMLFILE)
                ));
            return CreateXml;
        }

        /* DECLARE @V XML
        EXEC dbo.BIDelete_c @pFLAG = 1, -- int
            @pDELETEID = 390, -- int
            @pUSERID = 1, -- int
            @pxmlOut = @V OUTPUT -- xml
        SELECT @V*/

        /*@pFLAG CASES:
         REPORT:1
         DASHBOARD:2
         GRAPH:3        

        @pDELETEID CASES
         REPORT:BIRPTID
         DASHBOARD:BIRPTID
         GRAPH:GRAPHID
          */
        //xml for bi delete 
        public XDocument BIDelete_CodeXml(int FLAG, int RPTID, int USERID, string XMLOUT)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIDelete_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pDELETEID", RPTID),
                new XElement("pUSERID", USERID),
                new XElement("pxmlOut", XMLOUT)
                ));
            return CreateXml;
        }
        //xml for bi xml generation
        public XDocument BIGenerateXML_CodeXml(int BIRPTID, int USERID, string XML, string XMLFILTER)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIxmlgeneration_g"),
                new XElement("pBIRPTID", BIRPTID),
                new XElement("pXML", XML),
                new XElement("pXMLFILTER", XMLFILTER),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }


        /*EXEC BIUSERPAGE_c @pNAME = '', -- varchar(30)
        @pVALUE = '', -- varchar(30)
        @pRPTID = 2, -- int
        @pUSERID = 1 -- int*/

        //xml for insert biuserpage
        public XDocument BIUserPage_CodeXml(string NAME, string VALUE, int RPTID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIUSERPAGE_c"),
                new XElement("pNAME", NAME),
                new XElement("pVALUE", VALUE),
                new XElement("pRPTID", RPTID),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }


        /*EXEC BIUSERPAGE_g @pRPTID =1, -- int
        @pUSERID = 1 -- int*/

        //xml for getting biuserpage 
        public XDocument GetBIUserPageList_CodeXml(int RPTID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIUSERPAGE_g"),
                new XElement("pRPTID", RPTID),
                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        //EXEC BIRPTCOLFORMATING_c @vBIRPTID = 1, -- int
        //@vDTCOLID = 1, -- int
        //@vUSERID = 1, -- int
        //@vType = 'width', -- varchar(10)
        //@vValue = '300' -- varchar(10)

        //xml for bi report column formating 
        public XDocument BIRptColorFormatting_CodeXml(int BIRPTID, int DTCOLID, int USERID, string vType, string Value)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIRPTCOLFORMATING_c"),
                new XElement("vBIRPTID", BIRPTID),
                new XElement("vDTCOLID", DTCOLID),
                new XElement("vUSERID", USERID),
                new XElement("vType", vType),
                new XElement("vValue", Value)
                ));
            return CreateXml;
        }


        //============For ShareRunReport==================================//

        public XDocument GetReportDataUserName_CodeXml(string Desc, int UserId, int RptId)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BI_GETUSERMASTERRPT_g"),
                new XElement("pSearchText", Desc),
                new XElement("pUSERID", UserId),
                new XElement("pRPTID", RptId)
                ));
            return CreateXml;
        }

        public XDocument SendUserId_CodeXml(string FLAG, int RPTID, string XMLTRANS, string REPORTNAME)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIShareRptEntry_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pRPTID", RPTID),
                 new XElement("pXMLUSER", XMLTRANS),
                 new XElement("pREPORTNAME", REPORTNAME)
                ));
            return CreateXml;
        }

        //============================For Dashboard Graph=================================//

        public XDocument GetGraphDash(int FLAG, int GRAPHID, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIUSERDASHBOARD_g"),
                new XElement("pFlag", FLAG),
                new XElement("pGRAPHID", GRAPHID),

                new XElement("pUSERID", USERID)
                ));
            return CreateXml;
        }

        public XDocument SendGraphDash(string FLAG, int GRAPHID, int POSITION, int USERID)
        {
            XDocument CreateXml = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Vzah",
                new XElement("XsdName", ""),
                new XElement("ProcName", "BIUSERDASHBOARD_c"),
                new XElement("pFLAG", FLAG),
                new XElement("pGRAPHID", GRAPHID),
                     new XElement("pPOSITION", POSITION),
                 new XElement("pUSERID", USERID)

                ));
            return CreateXml;
        }

    }
}
