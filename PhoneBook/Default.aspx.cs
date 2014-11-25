using PhoneBook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;



    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateTable();

        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionToLocalDB"].ToString());
        private SqlCommand cmd = new SqlCommand();
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con; //assigning connection to command
                cmd.CommandType = CommandType.Text; //representing type of command
                //cmd.CommandText = "INSERT INTO dbo.Taba (Fname,Lname,Email,Password,Gender,Dob,Mobile,Address) values(@Fname,@Lname,@Email,@Password,@Gender,@Dob,@Mobile,@Address)";
                cmd.CommandText = "INSERT into dbo.PhoneBook(Name,Mobile,Phone,Email)VALUES (@Name,@Mobile,@Phone,@Email)";

                //adding parameters with value
                cmd.Parameters.AddWithValue("@Name", ((TextBox)FindControl("txtName")).Text);
                cmd.Parameters.AddWithValue("@Email", ((TextBox)FindControl("txtEmail")).Text);
                cmd.Parameters.AddWithValue("@Mobile", ((TextBox)FindControl("txtMobile")).Text);
                cmd.Parameters.AddWithValue("@Phone", ((TextBox)FindControl("txtPhone")).Text);

                con.Open(); //opening connection
                cmd.ExecuteNonQuery();  //executing query
                con.Close(); //closing connection
                //FindControl("lblMsg").Text = "Registered Successfully..";
            }
            catch (Exception ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }


        protected DataTable CreateDataTable()
        {
            cmd.Connection = con; //assigning connection to command;
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            DataRow dr = null;
            con.Open();

            
            int count = 0;
            using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM dbo.PhoneBook", cmd.Connection))
                {
                    count = (int)cmdCount.ExecuteScalar();
                }
            List<string> lRow = new List<string>();
            cmd.CommandText = "SELECT * FROM PhoneBook";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    for (int i = 1; i < 6; i++)
                    {
                        try
                        {
                            string myString = (string)rdr.GetString(i);
                            lRow.Add(myString);
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException ex)
                        {
                            lRow.Add("");
                        }
                    }
                    
                }

            }

            SqlReader name = new SqlReader(cmd, "Name");
            SqlReader mobile = new SqlReader(cmd, "mobile");
            SqlReader email = new SqlReader(cmd, "email");
            SqlReader phone = new SqlReader(cmd, "phone");
            //name.listRows;
            //Add the first Row to each columns defined
            con.Close();

            //Create the Columns Definition
            //dt.Columns.Add(new DataColumn(name.listRows[0], typeof(string)));
            //dt.Columns.Add(new DataColumn("Mobile", typeof(string)));
            //dt.Columns.Add(new DataColumn("E-Mail", typeof(string)));
            //dt.Columns.Add(new DataColumn("Phone", typeof(string)));

            foreach (string listCol in name.listRows)
            {
                dt.Columns.Add(new DataColumn(listCol, typeof(string)));
            }
            int x = 0;
            int rows = lRow.Count/5;
            for(int i = 1; i<= 2 ; i++)
            {dr = dt.NewRow();
                foreach (string listCol in name.listRows)
                {
                    
                        dr[listCol] = lRow[x];
                    
                    x++;
            }dt.Rows.Add(dr);
                }
            //dr[name.listRows[0]] = name.listRows[0];
            //dr["Mobile"] = "B";
            //dr["E-Mail"] = "C";

            //

            ////Add the second Row to each columns defined
            //dr = dt.NewRow();

            //dr[name.listRows[0]] = "D";
            //dr["Mobile"] = "E";
            //dr["E-Mail"] = "F";

            //dt.Rows.Add(dr);

            //You can continue adding rows here

            return dt;
        }

        protected void GenerateTable()
        {
            DataTable dt = CreateDataTable();
            Table table = new Table();
            TableRow row = null;

            //Add the Headers
            row = new TableRow();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = dt.Columns[j].ColumnName;
                row.Cells.Add(headerCell);
            }
            table.Rows.Add(row);

            //Add the Column values
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = new TableRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = dt.Rows[i][j].ToString();
                    row.Cells.Add(cell);
                }
                // Add the TableRow to the Table
                table.Rows.Add(row);
            }
            // Add the the Table in the Form
            FindControl("PlaceHolder1").Controls.Add(table);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //refreshing/reloading page to clear all the controls
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
