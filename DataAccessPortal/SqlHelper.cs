using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

namespace DataAccessPortal
{
    public class SqlHelper
    {
        private DataSet ds;
        // Assume the method GetConnectionString exists in your application and 
        // returns a valid connection string.

        private Database db = DatabaseFactory.CreateDatabase("Connection String");
        string constring = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;


        //~SqlHelper()
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strProc_Name"></param>
        /// <returns></returns>
        /// 
        private string GetProcName(XDocument dom)
        {
            return dom.Descendants("Vzah").Elements("ProcName").FirstOrDefault().Value;
        }

        public DataSet ExceuteProcWithoutParameter(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public DataSet GetDataSet(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();
                    parameter.Value = element.Value;
                    cmd.Parameters.Add(parameter);
                }
            }

            string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);

            cmd.CommandTimeout = 1800;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public void ExceuteNonQuery(XDocument dom)
        {

            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();
                    parameter.Value = element.Value;
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                db.ExecuteNonQuery(cmd);
#if DEBUG
                string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
#endif
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public int ExecuteScalar(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            cmd.CommandTimeout = 6000;
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();
                    parameter.Value = element.Value;
                    cmd.Parameters.Add(parameter);
                }
            }
#if DEBUG
            string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
#endif
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }

        }

        //Modified For Project Preogress 18 March 2014
        public int ExecuteScalarForVarbinaryData(XDocument dom, byte[] Data, byte[] Data1)
        {
            string Procname = GetProcName(dom);
            SqlParameter parameter;
           
            SqlConnection _con = new SqlConnection(constring);
            SqlCommand sqlcmd = new SqlCommand(Procname, _con);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;

            foreach (XElement element in elem_list)
            {
                parameter = sqlcmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();

                    if (element.Name.ToString() == "pDocImg" && Data != null)
                    {
                        sqlcmd.Parameters.Add(parameter.ParameterName, SqlDbType.VarBinary).Value = Data;
                    }
                    else if (element.Name.ToString() == "pCertImg" && Data1 != null)
                    {
                        sqlcmd.Parameters.Add(parameter.ParameterName, SqlDbType.VarBinary).Value = Data1;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        sqlcmd.Parameters.Add(parameter);
                    }
                }
            }

            try
            {
                sqlcmd.Connection.Open();
                int res = Convert.ToInt32(db.ExecuteScalar(sqlcmd));
                return res;
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)sqlcmd);
                throw ex;
            }
            finally
            {
                sqlcmd.Connection.Close();
            }
        }

        public int SaveFile(XDocument dom, byte[] Data)
        {

            string Procname = GetProcName(dom);
            SqlParameter parameter;
            SqlConnection _con = new SqlConnection(constring);
            SqlCommand sqlcmd = new SqlCommand(Procname, _con);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;

            foreach (XElement element in elem_list)
            {
                parameter = sqlcmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();

                    if (element.Name.ToString().Trim() == "pOUTDATA")
                    {
                        parameter.DbType = DbType.Int32;
                        parameter.Direction = ParameterDirection.Output;
                        sqlcmd.Parameters.Add(parameter);
                    }
                    else if (element.Name.ToString() == "pFILESDATA" && Data != null)
                    {
                        sqlcmd.Parameters.Add(parameter.ParameterName, SqlDbType.VarBinary).Value = Data;
                        parameter.Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                        sqlcmd.Parameters.Add(parameter);
                    }
                }
            }

            //sqlcmd.Connection.Open();
            //// int res = sqlcmd.ExecuteNonQuery();
            //int res = Convert.ToInt32(db.ExecuteScalar(sqlcmd));
            //sqlcmd.Connection.Close();
            //return res;

            try
            {
                sqlcmd.Connection.Open();
                db.ExecuteScalar(sqlcmd);
                var res = db.GetParameterValue(sqlcmd, "pOUTDATA");
                sqlcmd.Connection.Close();
                return Convert.ToInt32(res);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)sqlcmd);
                throw ex;
            }
        }

        public string ExecuteScalarforString(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim().ToLower() == "pintreturnno")
                    {
                        parameter.Size = 1000;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "@PintRETURNNO");
                return Convert.ToString(retval33);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string ExecuteScalarforStringXml(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();
                    parameter.Value = element.Value;
                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
                return Convert.ToString(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string[] GetTransactionXml(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pOUTPUT" || element.Name.ToString().Trim() == "pXMLMAST" || element.Name.ToString().Trim() == "pXMLTRANS"
                        || element.Name.ToString().Trim() == "pXMLFOOTER" || element.Name.ToString().Trim() == "pXMLFORMULA" || element.Name.ToString().Trim() == "pXMLEXTRA" || element.Name.ToString().Trim() == "pXMLEXTRA1")
                    {
                        //parameter.Size = 1000;
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;

                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                db.ExecuteNonQuery(cmd);

                var pXMLMAST = db.GetParameterValue(cmd, "@pXMLMAST");
                var pXMLTRANS = db.GetParameterValue(cmd, "@pXMLTRANS");
                var pXMLFOOTER = db.GetParameterValue(cmd, "@pXMLFOOTER");
                var pXMLFORMULA = db.GetParameterValue(cmd, "@pXMLFORMULA");
                var pXMLEXTRA = db.GetParameterValue(cmd, "@pXMLEXTRA");
                var pXMLEXTRA1 = db.GetParameterValue(cmd, "@pXMLEXTRA1");
                var pOUTPUT = db.GetParameterValue(cmd, "@pOUTPUT");

                string[] paramArr = new string[] { Convert.ToString(pXMLMAST), Convert.ToString(pXMLTRANS), Convert.ToString(pXMLFOOTER), Convert.ToString(pXMLFORMULA), Convert.ToString(pXMLEXTRA), Convert.ToString(pXMLEXTRA1), Convert.ToString(pOUTPUT) };
                return (paramArr);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string GetOutputXml(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pxmlOut")
                    {
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                #if DEBUG
                    string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
                #endif

                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "pxmlOut");
                return Convert.ToString(retval33);
                //return "";
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string GetOutXmlFormula(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pFORMULA")
                    {
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "@pFORMULA");
                return Convert.ToString(retval33);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public int ExecuteScalarforInteger(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pOUTFLAG")
                    {
                        parameter.Size = 1000;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "@pOUTFLAG");
                return Convert.ToInt32(retval33);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string InsertTransXml(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pERRORXML")
                    {
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "@pERRORXML");
#if DEBUG
                string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
#endif
                return Convert.ToString(retval33);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        public string GetXMLOutputPOSTING(XDocument dom)
        {
            string Procname = GetProcName(dom);
            DbCommand cmd = db.GetStoredProcCommand(Procname);
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "Vzah" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pRET")
                    {
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        parameter.Value = element.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                var retval33 = db.GetParameterValue(cmd, "@pRET");
                return Convert.ToString(retval33);
            }
            catch (Exception ex)
            {
                this.LogError((SqlCommand)cmd);
                throw ex;
            }
        }

        private void LogError(SqlCommand cmd)
        {
            try
            {
                string txtcommand = SqlCommandDumper.GetCommandText(cmd);
                XDocument CreateXml = new XDocument(
                     new XDeclaration("1.0", "utf-8", ""),
                     new XElement("Vzah",
                     new XElement("XsdName", ""),
                     new XElement("ProcName", "SQLErrorLog_i"),
                     new XElement("pUSERID", 0),
                     new XElement("pSQLQUERY", txtcommand)
                   ));
                this.ExceuteNonQuery(CreateXml);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
