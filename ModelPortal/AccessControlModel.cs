using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelPortal
{
    public class AccessControlModel
    {
        public string Checked_IsGraph { get; set; }
        public string Checked_IsPHisotry { get; set; }
        public string Checked_IsMachSpec { get; set; }
        public bool ChkPgId { get; set; }
        public bool Check { get; set; }
        public bool Check_IsMachSpec { get; set; }
        public bool Check_IsPHisotry { get; set; }
        public bool Check_IsGraph { get; set; }

        public string TAGDETAILS { get; set; }
        public int ID { get; set; }
        public int PARAMID { get; set; }
        public int MapUserid { get; set; }
        public bool IsGraph { get; set; }
        public bool IsPHisotry { get; set; }
        public bool IsMachSpec { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        DataAccessBusinessPortal.KloudqBusiness objbui = new DataAccessBusinessPortal.KloudqBusiness();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString);

        public List<AccessControlModel> BINDPARAMS(int UserId, int MACHINEID = 0, int Flag = 0)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", ""),
            new XElement("Kloudq",
            new XElement("XsdName", ""),
            new XElement("ProcName", "UserParamMap_g"),
            new XElement("Login_Id", UserId), 
            new XElement("MACHINEID", MACHINEID),
            new XElement("Flag", Flag)
            ));
            AccessControlModel obj = new AccessControlModel();
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new AccessControlModel
                            {
                                PARAMID = s.Field<int>("PARAMID"),
                                TAGDETAILS = s.Field<string>("TAGDETAILS"),
                            }).ToList();
            return dbResult;
        }
        public List<AccessControlModel> SELECTPARAMETERS(int UserId, int MACHINEID, int Flag = 0)
        {
            XDocument xdoc = new XDocument(
            new XDeclaration("1.0", "utf-8", ""),
            new XElement("Kloudq",
            new XElement("XsdName", ""),
            new XElement("ProcName", "UserParamMap_g"),
            new XElement("Login_Id", UserId),
            new XElement("MACHINEID", MACHINEID),
            new XElement("Flag", Flag)
            ));
            AccessControlModel obj = new AccessControlModel();
            DataTable dt = objbui.GetKloudqBusinessList(xdoc);
            var dbResult = (from s in dt.AsEnumerable()
                            select new AccessControlModel
                            {
                                PARAMID = s.Field<int>("PARAMID"),
                                TAGDETAILS = s.Field<string>("TAGDETAILS"),
                                IsGraph = s.Field<bool>("IsGraph"),
                                IsPHisotry = s.Field<bool>("IsPHisotry"),
                                IsMachSpec = s.Field<bool>("IsMachSpec"),
                            }).ToList();
            return dbResult;
        }

        public int AddPermission(List<AccessControlModel> c, int Login_Id)
        {
            var r = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("PARAMID", typeof(int));
            dt.Columns.Add("MapUserid", typeof(int));
            dt.Columns.Add("IsPHisotry", typeof(bool));
            dt.Columns.Add("IsMachSpec", typeof(bool));
            dt.Columns.Add("IsGraph", typeof(bool));  
            foreach (var item in c)
            {
                dt.Rows.Add(
                    item.PARAMID,
                    item.MapUserid,
                    item.IsPHisotry, item.IsMachSpec,
                    item.IsGraph 
                    );
            }
            using (SqlCommand cmd = new SqlCommand("UserParamMap_c"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@INFO_ARRAY", dt);
                cmd.Parameters.AddWithValue("@Login_Id", Login_Id);
                con.Open();
                r = cmd.ExecuteNonQuery();
                con.Close();
            }

            return r;
        }

    }
}
