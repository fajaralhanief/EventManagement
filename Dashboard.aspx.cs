using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

namespace FRVN.Administrator
{
    public partial class Dashboard : System.Web.UI.Page
    {
        StringBuilder table = new StringBuilder();
        StringBuilder table2 = new StringBuilder();
        //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                SqlCommand x = new SqlCommand();
                //cmd.CommandText = "Select * from [VG].[EVENT]";
                cmd.CommandText = "Select count(*) from [EVENT]";
                x.CommandText = "SELECT COUNT(*) FROM (SELECT TOP 3 [BeginDate],[EndDate],[RecordID] ,[EventName],[Theme],[Venue],[LocationCoordinate],[Author],[ModifiedAt],[ModifiedBy] FROM[EVENT_PORTAL].[dbo].[EVENT]) AS subquery ";
                cmd.Connection = con;
                x.Connection = con;
                SqlDataReader rd = cmd.ExecuteReader();

                //table.Append("<table border='1'>");
                //table.Append("<tr><th>tanggal</th><th>nama event</th><th>Action</th>");
                //table.Append("</tr>");

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                       table.Append(""+ rd[0] +"");
                    }
                }



                Label1.Controls.Add(new Literal { Text = table.ToString() });
                Label2.Controls.Add(new Literal { Text = table.ToString() });
                
                rd.Close();
                SqlDataReader xd = x.ExecuteReader();
                if (xd.HasRows)
                {
                    while (xd.Read())
                    {
                        table2.Append("" + xd[0] + "");
                    }
                }
                Label3.Controls.Add(new Literal { Text = table2.ToString() });
                xd.Close();
            }
        }
    }
}