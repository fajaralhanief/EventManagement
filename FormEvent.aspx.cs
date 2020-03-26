using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace FRVN.Administrator
{
    public partial class FormEvent : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            con.Open();
            //String query = "INSERT INTO event (tanggal,tipe_event,nama_event,venue,kota,map,author) VALUES('"+tanggalmulai.Text+"','"+tipeevent.Text+ "','" + namaevent.Text + "','" + venue.Text + "','" + kota.Text + "','" + mapcoordinate.Text + "','" + author.Text + "')";
            String query = "INSERT INTO EVENT (BeginDate,EndDate,EventName,Theme, Venue, LocationCoordinate,Author) VALUES('" + tanggalmulai.Text + "','" + tanggalselesai.Text + "','" + namaevent.Text + "','" + temaevent.Text + "','" + venue.Text + "','" + mapcoordinate.Text + "','" + author.Text + "')";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Data Berhasil Masuk');</script>");

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListEvent.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            con.Open();
            //String query = "INSERT INTO [vg].[EVENT] (BeginDate, EndDate, EventName,Theme, Venue, LocationCoordinate, Author) VALUES('" + tanggalmulai.Text + "','" + tanggalselesai.Text + "','" + namaevent.Text + "','" + temaevent.Text + "','" + venue.Text + "','" + mapcoordinate.Text + "','" + author.Text + "')";
            String query = "INSERT INTO [EVENT] (BeginDate, EndDate, EventName,Theme, Venue, LocationCoordinate, Author) VALUES('" + tanggalmulai.Text+" "+ txtTime.Text +"','" + tanggalselesai.Text + " " + txtTime2.Text + "','" + namaevent.Text + "','" + temaevent.Text + "','" + venue.Text + "','" + mapcoordinate.Text + "','" + author.Text + "')";

            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Data Berhasil Masuk');</script>");
            Response.Redirect("ListEvent.aspx");
        }
    }
}