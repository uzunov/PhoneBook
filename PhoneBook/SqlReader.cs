using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook
{
    public class SqlReader
    {
        public List<string> listRows = new List<string>();

        public SqlReader(SqlCommand cmd, int col)
        {
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var myString = rdr.GetString(col);
                    listRows.Add(myString);
                }
            }
        }

        
        public SqlReader(SqlCommand cmd, string col)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COLUMN_NAME,* FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PhoneBook'";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    string myString = rdr.GetString(0);
                    listRows.Add(myString);
                }
                
            }

            
           
        }

        public void clearList()
        {
            listRows = null;
        }
    }
}