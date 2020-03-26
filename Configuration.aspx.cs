using AIVOW.Framework.Security;
using FRVN.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FRVN.Administrator
{
    public partial class Configuration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionCreator();
            if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["act"] != null)
                {
                    if (Request.QueryString["act"] == "1")
                    {
                        string recordID = Cryptography.Blowfish.DecryptCBC(Request.QueryString["recid"]);
                        CatalogParameter.DelimitParameter(recordID, "admin");
                    }
                }
            }
        }
        
        protected String GenerateData()
        {
            StringBuilder _tableelement = new StringBuilder();

            DataTable table = CatalogParameter.GetParameterByType("WG");
            table.Merge(CatalogParameter.GetParameterByType("CS"));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                _tableelement.Append("<tr class=''>");
                _tableelement.Append("<td>" + table.Rows[i]["PRMTY"].ToString() + "</td>");
                _tableelement.Append("<td>" + table.Rows[i]["PRMKD"].ToString() + "</td>");
                _tableelement.Append("<td>" + table.Rows[i]["PRMNM"].ToString() + "</td>");
                _tableelement.Append("<td>" + GetAction(table.Rows[i]["RECID"].ToString()) + "</td>");
                _tableelement.Append("</tr>");
            }

            return _tableelement.ToString();
        }

        protected string GetAction(string RECID)
        {
            StringBuilder element = new StringBuilder();

            element.Append("<a class='btn-red btn popovers'  data-content='Hapus data' data-placement='left' data-trigger='hover' href='Configuration.aspx?act=1&recid=" + Cryptography.Blowfish.EncryptCBC(RECID) + "'><i class='fa fa-trash-o'></i> </a> ");

            return element.ToString();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            CatalogParameter.InsertParameter(ddlParameterType.SelectedValue, txtParameterCode.Text, txtParameterName.Text, "admin");
        }
    }
}