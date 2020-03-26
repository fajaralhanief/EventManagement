using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace FRVN.Administrator
{
    public partial class UpdateEvent : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");
                string getid = Request.QueryString["id"];
                con.Open();
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "Select * from [VG].[EVENT] WHERE RecordID ='"+getid+"'";
                cmd.CommandText = "Select * from [EVENT] WHERE RecordID ='" + getid + "'";
                cmd.Connection = con;
                SqlDataReader rd = cmd.ExecuteReader();
                //table.Append("<table border='1'>");
                //table.Append("<tr><th>tanggal</th><th>nama event</th><th>Action</th>");
                //table.Append("</tr>");

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {   
                      
                        tanggalmulai.Text = ""+rd[0];
                        tanggalselesai.Text = "" +rd[1];
                        temaevent.Text = "" + rd[4];
                        namaevent.Text = ""+rd[3];
                        venue.Text = ""+rd[5];
                        mapcoordinate.Text = ""+rd[6];
                        author.Text = "" + rd[7];

                    }
                }


                rd.Close();

            }
        }
        
        
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListEvent.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            con.Open();
            string v = Request.QueryString["id"];
            //String query = "UPDATE [vg].[EVENT] SET BeginDate = '" + tanggalmulai.Text + "',EndDate = '" + tanggalselesai.Text + "',  Theme = '" + temaevent.Text + "' , EventName = '" + namaevent.Text + "', Venue = '" + venue.Text + "', LocationCoordinate = '" + mapcoordinate.Text + "', Author = '" + author.Text + "' WHERE RecordID = '" + v + "' ";
            String query = "UPDATE [EVENT] SET BeginDate = '" + tanggalmulai.Text + "',EndDate = '" + tanggalselesai.Text + "',  Theme = '" + temaevent.Text + "' , EventName = '" + namaevent.Text + "', Venue = '" + venue.Text + "', LocationCoordinate = '" + mapcoordinate.Text + "', Author = '" + author.Text + "' WHERE RecordID = '" + v + "' ";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            
            Response.Write("<script>alert('Data Berhasil Di Update');</script>");
            Response.Write("<script>window.location.href = 'ListEvent.aspx';</script>");
            //Response.Redirect("ListEvent.aspx");
        }
    }
}