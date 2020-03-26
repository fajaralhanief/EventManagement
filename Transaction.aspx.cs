using FRVN.Data.DataAccess;
using FRVN.Frameworks.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FRVN.Administrator
{
    public partial class Transaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");
            if (Session["username"].ToString() != "admin") Response.Redirect("SignIn.aspx");

            if (!IsPostBack)
            {

            }
        }

        protected void SessionCreator()
        {
            Session["username"] = "1906";
            Session["name"] = "FRVN Administrator";
            Session["password"] = "admin";
        }

        protected void SetDataGroup()
        {
            if (rbtCredit.Checked)
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("Pemasukan", "51"));
            }
            else if (rbtDebit.Checked)
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("Acara", "11"));
                ddlCategory.Items.Add(new ListItem("Perlengkapan", "12"));
                ddlCategory.Items.Add(new ListItem("Publikasi", "13"));
                ddlCategory.Items.Add(new ListItem("Dekorasi", "14"));
            }
            else
            {
                ddlCategory.Items.Clear();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Finance.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDate.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Tanggal Transaksi wajib diisi" + "');", true);
            }
            else if (!(rbtCredit.Checked || rbtDebit.Checked))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Jenis Transaksi wajib diisi" + "');", true);
            }
            else if (txtDescription.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deskripsi wajib diisi" + "');", true);
            }
            else if (txtDescription.Text.Trim().Length > 200)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Deskipsi tidak lebih dari 200 karakter" + "');", true);
            }
            else if (txtValue.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nilai wajib diisi" + "');", true);
            }
            else if (!Validation.IsValidInteger(txtValue.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Format nilai adalah bilangan integer atau bilangan bulat" + "');", true);
            }
            else
            {
                UpdateDataIntoDatabase();
                Response.Redirect("Finance.aspx");
            }
        }

        protected void UpdateDataIntoDatabase()
        {
            int newID = Convert.ToInt32(FinanceData.GetTransactionID().Rows[0]["OBJID"]) + 1;
            string _transactionDate = Convert.ToDateTime(txtDate.Text).ToString();
            string _transactionType = rbtCredit.Checked ? rbtCredit.Attributes["Value"] : rbtDebit.Attributes["Value"];
            string _journal = "00000";
            string _categoryType = ddlCategory.SelectedValue;
            string _categoryName = ddlCategory.SelectedItem.Text;
            string _notes = txtNotes.Text;
            string _approvalStatus = "1";
            string _description = txtDescription.Text;
            string _value = txtValue.Text;
            string _modifier = "admin";


            FinanceData.InsertTransactionHeader(newID.ToString(), _transactionDate, _transactionType, _journal, _categoryType, _categoryName, _notes, _approvalStatus, _modifier);
            FinanceData.InsertTransactionLine(newID.ToString(), _transactionDate, _transactionType, _description, _value, _notes, _modifier);
        }

        protected void rbtDebit_CheckedChanged(object sender, EventArgs e)
        {
            SetDataGroup();
        }
    }
}