using AIVOW.Business.Activity;
using AIVOW.Framework.Security;
using FRVN.Business.Components;
using FRVN.Data.DataAccess;
using FRVN.Entity.Dictionary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheArtOfDev.HtmlRenderer.WinForms;
using FRLNBus = FRLN.Business.Component;

namespace FRVN.Administrator
{
    public partial class DetailPeserta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionCreator();
            if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");
            if (Session["username"].ToString() != "admin") Response.Redirect("SignIn.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["acode"] != null)
                {
                    SetPerusahaan();
                    SetParameterCity();

                    SetDataToForm(Cryptography.Blowfish.DecryptCBC(Request.QueryString["acode"]));
                }
                else
                {
                    Response.Redirect("Peserta.aspx");
                }
            }
        }

        protected void SetDataToForm(string kodeRegistrasi)
        {
            DataTable peserta = VendorGatheringData.GetPeserta(kodeRegistrasi);
            DataTable perusahaan = VendorGatheringData.GetPerusahaan(peserta.Rows[0]["KodePerusahaan"].ToString());
            string jenisKelamin = peserta.Rows[0]["JenisKelamin"].ToString() == "1" ? "Laki-laki" : "Perempuan";

            if (peserta.Rows.Count > 0)
            {
                ddlPerusahaan.SelectedValue = perusahaan.Rows[0]["KodePerusahaan"].ToString();
                txtEmailPerusahaan.Text = perusahaan.Rows[0]["Email"].ToString();
                txtAlamat.Text = perusahaan.Rows[0]["Alamat"].ToString();
                ddlKota.SelectedValue = perusahaan.Rows[0]["KodeKota"].ToString();
                txtTelp.Text = perusahaan.Rows[0]["Telpon"].ToString();
                txtFax.Text = perusahaan.Rows[0]["Fax"].ToString();
                txtKodePos.Text = perusahaan.Rows[0]["KodePos"].ToString();
                txtStatusSKT.Text = perusahaan.Rows[0]["TypeSKT"].ToString() == "1" ? "Wajib" : "Tidak Wajib";

                txtNamaPeserta.Text = peserta.Rows[0]["NamaPeserta"].ToString();
                txtNamaJabatan.Text = peserta.Rows[0]["NamaJabatan"].ToString();
                txtEmail.Text = peserta.Rows[0]["Email"].ToString();
                txtMobile.Text = peserta.Rows[0]["Phone"].ToString();

                txtNamaPeserta2.Text = peserta.Rows[0]["NamaPeserta2"].ToString();
                txtNamaJabatan2.Text = peserta.Rows[0]["NamaJabatan2"].ToString();
                txtEmail2.Text = peserta.Rows[0]["Email2"].ToString();
                txtMobile2.Text = peserta.Rows[0]["Phone2"].ToString();

                txtNotes.Text = peserta.Rows[0]["Catatan"].ToString(); 

                if (peserta.Rows[0]["JenisKelamin"].ToString() == "1") rbtLaki.Checked = true;
                else rbtPerempuan.Checked = true;
            }
        }

        protected string GenerateCode()
        {
            string regCode = Code.CreateVerificationCode(6);

            while (Convert.ToInt16(VendorGatheringData.IsRegistrationCodeExist(regCode).Rows[0]["COUNT"]) > 0)
            {
                regCode = Code.CreateAlternateVerificationCode(6, 128);
            }

            Session["regCode"] = regCode;

            return regCode;
        }

        protected void SetParameterCity()
        {
            ddlKota.Items.Clear();
            DataTable data = CatalogParameter.GetParameterByType("CT");

            for (int i = 0; i < data.Rows.Count; i++)
            {
                ddlKota.Items.Add(new ListItem(data.Rows[i]["PRMNM"].ToString(), data.Rows[i]["PRMKD"].ToString()));
            }
        }

        protected void SetPerusahaan()
        {
            ddlPerusahaan.Items.Clear();
            DataTable data = VendorGatheringData.GetPerusahaan(string.Empty);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                ddlPerusahaan.Items.Add(new ListItem(data.Rows[i]["NamaPerusahaan"].ToString(), data.Rows[i]["KodePerusahaan"].ToString()));
            }
        }

        protected void UpdateDataIntoDatabase(string _kodeRegistrasi)
        {
            string _jenisKelamin = rbtLaki.Checked ? rbtLaki.Attributes["Value"] : rbtPerempuan.Attributes["Value"];

            VendorGatheringData.UpdatePerusahaan(ddlPerusahaan.SelectedValue, txtAlamat.Text, ddlKota.SelectedValue, ddlKota.SelectedItem.Text, txtKodePos.Text, txtEmailPerusahaan.Text, txtTelp.Text, txtFax.Text, _kodeRegistrasi);
            VendorGatheringData.UpdatePeserta(_kodeRegistrasi, ddlPerusahaan.SelectedValue, txtNamaPeserta.Text,
                _jenisKelamin, txtNamaJabatan.Text, txtEmail.Text, txtMobile.Text, string.Empty, txtNamaPeserta2.Text,
                txtNamaJabatan2.Text, txtEmail2.Text, txtMobile2.Text, txtNotes.Text, _kodeRegistrasi);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Peserta.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Perusahaan wajib diisi" + "');", true);
            }
            else if (txtEmail.Text.Trim().Length > 70)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Perusahaan tidak lebih dari 70 karakter" + "');", true);
            }
            else if (txtAlamat.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Alamat Perusahaan wajib diisi" + "');", true);
            }
            else if (txtAlamat.Text.Trim().Length > 200)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Alamat Perusahaan tidak lebih dari 200 karakter" + "');", true);
            }
            else if (txtKodePos.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Kode Pos wajib diisi" + "');", true);
            }
            else if (txtKodePos.Text.Trim().Length > 5)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Kode Pos tidak lebih dari 5 karakter" + "');", true);
            }
            else if (txtTelp.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor Telpon Perusahaan wajib diisi" + "');", true);
            }
            else if (txtTelp.Text.Trim().Length > 70)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor Telpon Perusahaan tidak lebih dari 30 karakter" + "');", true);
            }
            else if (txtFax.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor Fax Perusahaan wajib diisi" + "');", true);
            }
            else if (txtFax.Text.Trim().Length > 70)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor Fax Perusahaan tidak lebih dari 30 karakter" + "');", true);
            }


            else if (txtNamaPeserta.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nama Peserta wajib diisi" + "');", true);
            }
            else if (txtNamaPeserta.Text.Trim().Length > 120)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nama Peserta tidak lebih dari 120 karakter" + "');", true);
            }
            else if (txtEmail.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Peserta wajib diisi" + "');", true);
            }
            else if (txtEmail.Text.Trim().Length > 70)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Peserta tidak lebih dari 70 karakter" + "');", true);
            }
            else if (!(rbtLaki.Checked || rbtPerempuan.Checked))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Jenis Kelamin wajib diisi" + "');", true);
            }
            else if (txtNamaJabatan.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nama Jabatan wajib diisi" + "');", true);
            }
            else if (txtNamaJabatan.Text.Trim().Length > 120)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nama Jabatan tidak lebih dari 120 karakter" + "');", true);
            }
            else if (txtMobile.Text.Trim().Length == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor HP Personal wajib diisi" + "');", true);
            }
            else if (txtMobile.Text.Trim().Length > 30)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Nomor HP Personal tidak lebih dari 30 karakter" + "');", true);
            }
            else if (txtEmail2.Text.Trim().Length > 0 && !txtEmail2.Text.Contains("@"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Peserta 2 tidak valid" + "');", true);
            }
            else if (txtEmail2.Text.Trim().Length > 70)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Email Peserta 2 tidak lebih dari 70 karakter" + "');", true);
            }
            else
            {
                UpdateDataIntoDatabase(Cryptography.Blowfish.DecryptCBC(Request.QueryString["acode"]));
                Session["info"] = "A3";
                Response.Redirect("Peserta.aspx");
            }
        }
    }
}