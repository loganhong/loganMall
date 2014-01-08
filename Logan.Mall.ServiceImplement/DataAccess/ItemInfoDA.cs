using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;

using Logan.Mall.Contract.DataContracts;
using Logan.Mall.BaseLib.Utilities;


namespace Logan.Mall.ServiceImplement.DataAccess
{
    public class ItemInfoDA
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["IMTest"].ConnectionString;
        internal static List<ItemInfo> GetAll()
        {
            try
            {
                string cmdText = "select * from ItemInfo";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    IDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return EntityRefUtil<ItemInfo>.ToEntityList(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal static ItemInfo Get(string itemNO)
        {
            try
            {
                string cmdText = "select top 1* from ItemInfo";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    IDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return EntityRefUtil<ItemInfo>.ToEntityList(ds.Tables[0]).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}