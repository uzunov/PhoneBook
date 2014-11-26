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

    private SqlConnection localDBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionToLocalDB"].ToString());
    private SqlCommand sqlCommand = new SqlCommand();

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            sqlCommand.Connection = localDBConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "INSERT into dbo.PhoneBook(Name,Mobile,Phone,Email)VALUES (@Name,@Mobile,@Phone,@Email)";

            //adding parameters with value
            sqlCommand.Parameters.AddWithValue("@Name", ((TextBox)FindControl("txtName")).Text);
            sqlCommand.Parameters.AddWithValue("@Email", ((TextBox)FindControl("txtEmail")).Text);
            sqlCommand.Parameters.AddWithValue("@Mobile", ((TextBox)FindControl("txtMobile")).Text);
            sqlCommand.Parameters.AddWithValue("@Phone", ((TextBox)FindControl("txtPhone")).Text);

            localDBConnection.Open();
            sqlCommand.ExecuteNonQuery();
            localDBConnection.Close();
            GenerateTable();
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
        sqlCommand.Connection = localDBConnection; //assigning connection to command;
        sqlCommand.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        DataRow dr = null;
        localDBConnection.Open();

        int count = 0;
        using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM dbo.PhoneBook", sqlCommand.Connection))
        {
            count = (int)cmdCount.ExecuteScalar();
        }
        List<String> lRow = new List<String>();
        sqlCommand.CommandText = "SELECT * FROM PhoneBook";
        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
        {
            while (dataReader.Read())
            {
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        String myString = (string)dataReader.GetString(i);
                        lRow.Add(myString);
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException ex)
                    {
                        lRow.Add("");
                    }
                    catch (System.InvalidCastException ex)
                    {
                        String myString = string.Empty + (dataReader.GetInt32(i));
                        lRow.Add(myString);
                    }
                }
            }
        }

        SqlReader valuesList = new SqlReader(sqlCommand);

        localDBConnection.Close();

        foreach (string value in valuesList.listAllValues)
        {
            dt.Columns.Add(new DataColumn(value, typeof(string)));
        }
        int x = 0;
        int rows = lRow.Count / 6;
        for (int i = 1; i <= rows; i++)
        {
            dr = dt.NewRow();
            foreach (string value in valuesList.listAllValues)
            {
                dr[value] = lRow[x];

                x++;
            } dt.Rows.Add(dr);
        }

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
        FindControl("PlaceHolder1").Controls.Clear();
        FindControl("PlaceHolder1").Controls.Add(table);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //refreshing/reloading page to clear all the controls
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
}