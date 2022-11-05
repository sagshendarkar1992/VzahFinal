using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using DataAccessPortal;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessBusinessPortal
{
    public class VzahBusiness
    {
        DataSet ds;

        public DataTable GetCloud9BusinessList(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            ds = SqlObj.GetDataSet(Xdoc);
            return ds.Tables.Count>0? ds.Tables[0]:null;
        }

        public DataSet GetCloud9BusinessDS(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            ds = SqlObj.GetDataSet(Xdoc);
            return ds;
        }

        public int StatusCheck(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.ExecuteScalar(Xdoc);

        }
        
        public void AddEditDelete(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            SqlObj.ExceuteNonQuery(Xdoc);
        }

        public string GetOutputXml(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.GetOutputXml(Xdoc);
        }

        public string GetXMLFormat(XDocument xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            string result = SqlObj.ExecuteScalarforStringXml(xdoc);
            return result;
        }

        public int StatusCheckForVarBinary(XDocument Xdoc, byte[] Data, byte[] Data1)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.ExecuteScalarForVarbinaryData(Xdoc, Data, Data1);

        }

        public int SaveFile(XDocument Xdoc, byte[] Data)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.SaveFile(Xdoc, Data);
        }


        public string InsertTransXml(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.InsertTransXml(Xdoc);
        }

        public string GetOutputXMLPOSTING(XDocument Xdoc)
        {
            SqlHelper SqlObj = new SqlHelper();
            return SqlObj.GetXMLOutputPOSTING(Xdoc);
        }

        public string[] GetMastTransXml(XDocument xdoc)
        {
            SqlHelper sqlObj = new SqlHelper();
            return sqlObj.GetTransactionXml(xdoc);
        }


        public object GetDt(SqlCommand cmd, int FlagSort = 0)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            try
            {

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]);
                cmd.Connection = conn;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);


                if (FlagSort == 1)
                {
                    dt.DefaultView.Sort = "logdate Asc";
                    dt = dt.DefaultView.ToTable();
                }
                return dt.AsEnumerable().FirstOrDefault() != null ? dt.AsEnumerable().FirstOrDefault().Table : null;
            }
            catch { throw; }
            finally
            {
                dr.Close();
                cmd.Dispose();
                conn.Close();

            }
        }

        public object Insert(SqlCommand cmd)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                cmd.Connection = conn;
                var id = cmd.ExecuteScalar();
                return id == null ? "" : id;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

        }
    }
}
