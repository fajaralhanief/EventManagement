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
    public partial class ListEvent : System.Web.UI.Page
    {
        StringBuilder table = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string conn = "Data Source=LAPTOP-DOPHK2QV;Initial Catalog=cobadb;Integrated Security=True";
            //SqlConnection conDataBase = new SqlConnection(conn);
            //SqlCommand cmdDataBase = new SqlCommand("select * from event", conDataBase);
            //try
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter();
            //    sda.SelectCommand = cmdDataBase;
            //    DataTable dbdataset = new DataTable();
            //    sda.Fill(dbdataset);
            //    BindingSou = 
            //}

            if (!Page.IsPostBack)
            {
                //SqlConnection con = new SqlConnection(@"Data Source=192.168.212.153;Initial Catalog=EVENT_PORTAL;Persist Security Info=True;User ID=sysdev;Password=P@ssw0rd123");
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-DOPHK2QV;Initial Catalog=EVENT_PORTAL;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "Select * from [VG].[EVENT]";
                cmd.CommandText = "Select * from [EVENT]";
                cmd.Connection = con;
                SqlDataReader rd = cmd.ExecuteReader();
                //table.Append("<table border='1'>");
                //table.Append("<tr><th>tanggal</th><th>nama event</th><th>Action</th>");
                //table.Append("</tr>");

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        table.Append("<tr>");
                        table.Append("<td>" + rd[2] + "</td>");
                        table.Append("<td>" + rd[0] + "</td>");
                        table.Append("<td>" + rd[1] + "</td>");
                        table.Append("<td>" + rd[3] + "</td>");
                        table.Append("<td>" + rd[4] + "</td>");
                        table.Append("<td>" + rd[5] + "</td>");
                        table.Append("<td>" + rd[6] + "</td>");
                        table.Append("<td>" + rd[7] + "</td>");
                        table.Append("<td ><a id = 'update' href='#' onClick='update()'><i class='fa fa-pencil' style='font-size:20px; '></i>&nbsp;</a>&nbsp;&nbsp;");
                        //table.Append("<td>");
                        //table.AppendFormat("<asp:TemplateField><ItemTemplate><asp:LinkButton ID='lnkView' runat='server' CommandArgument='<%# Eval('ContactID') %>' OnClick='lnk_OnClick' CssClass='ab'>View</asp:LinkButton></ItemTemplate></asp:TemplateField>");
                        table.Append("<a id='hapus' href='#' runat='server' onclick = 'hapus()'><i class='fa fa-trash-o' style='font-size:20px;'></i>&nbsp;</a></td>");
                        table.Append("</tr>");
                    }
                }


                //WSPlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });

                rd.Close();

            }
            //protected String GenerateData()
            //{

            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormEvent.aspx");
        }
        protected void hapus(object sender, EventArgs e)
        {
            Response.Redirect("FormEvent.aspx");
        }

    }
}