using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace FRVN.Administrator
{
    public partial class hapus : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            
            con.Open();
            string v = Request.QueryString["id"];
            String query = "DELETE FROM [EVENT] WHERE RecordID = '" + v + "' ";
            //String query = "DELETE FROM [VG].[EVENT] WHERE RecordID = '" + v + "' ";
            //String query = "DELETE FROM event WHERE tanggal = '" + v + "' ";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();

            Response.Write("<script>alert('Data Berhasil Di Hapus');</script>");
            Response.Write("<script>window.location.href = 'ListEvent.aspx';</script>");
            //Response.Redirect("ListEvent.aspx");
        }
       
        
    }
}