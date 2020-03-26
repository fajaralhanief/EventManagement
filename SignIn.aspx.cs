using FRVN.Frameworks.Security;
using Comp = FRVN.Business.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

namespace FRVN.Administrator
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Request.Cookies.Clear();

            if (!IsPostBack)
            {
                if (Request.QueryString["logout"] != null)
                {
                    WebConfigurationManager.AppSettings["UserMenu"] = "";
                    Session.Clear();
                }
            }

            HtmlGenericControl css = new HtmlGenericControl("head");
        }

        protected void ValidateLogin()
        {
            if (!ValidateNullInputUsername(txtUsername.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Username harus diisi." + "');", true);
            }
            else if (!ValidateNullInputPassword(txtPassword.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Username harus diisi." + "');", true);
            }
            else if (!Comp.Administrator.GetDefaultAccess(txtUsername.Value, txtPassword.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Akses ditolak!" + "');", true);
            }
            else
            {
                try
                {
                    ValidateAndRegisterDataToSession();
                    Response.Redirect("Peserta.aspx");
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Terjadi kegagalan sistem! "+ ex.Message + "');", true);
                }
                
            }
            return;
        }

        protected bool ValidateNullInputUsername(string username)
        {
            if (username.Trim().Length > 0)
                return true;
            else
                return false;
        }

        protected bool ValidateNullInputPassword(string password)
        {
            if (password.Trim().Length > 0)
                return true;
            else
                return false;
        }

        protected void ValidateAndRegisterDataToSession()
        {
            if (Comp.Administrator.GetDefaultAccess(txtUsername.Value, txtPassword.Value))
            {
                Session.Clear();
                Session["username"] = "admin";
                Session["password"] = CryptographEngine.Encrypt(txtPassword.Value, true);
                Session["name"] = txtUsername.Value;
                WebConfigurationManager.AppSettings["Gateway"] = "local";
                Session.Timeout = 300;
            }
            else
            {
                return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateLogin();
        }
    }
}