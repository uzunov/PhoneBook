using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //refreshing/reloading page to clear all the controls
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
}