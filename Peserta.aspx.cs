using AIVOW.Business.Activity;
using AIVOW.Framework.Security;
using FRVN.Data.DataAccess;
using FRVN.Entity.Dictionary;
using FRVN.Frameworks.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FRLNBus = FRLN.Business.Component;
using TheArtOfDev.HtmlRenderer.WinForms;
using FRVN.Business.Components;
using System.Configuration;

namespace FRVN.Administrator
{
    public partial class Peserta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionCreator();
            if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");
            if (Session["username"].ToString() != "admin") Response.Redirect("SignIn.aspx");

            if (!IsPostBack)
            {
                SetFilterValue();

                if (Request.QueryString["conf"] != null)
                {
                    if (Request.QueryString["conf"] == "1")
                    {
                        CatalogFRVN.GenerateDataPresensiWgCs();
                        Session["info"] = "C1";
                        Response.Redirect("Peserta.aspx");
                    }
                    else if (Request.QueryString["conf"] == "2")
                    {
                        CatalogFRVN.ResetDoorprizePeserta();
                        VendorGatheringData.GenerateDataPresensi();
                        Session["info"] = "C2";
                        Response.Redirect("Peserta.aspx");
                    }
                }

                if (Request.QueryString["act"] != null)
                {
                    if (Request.QueryString["act"] == "1")
                    {
                        string recordID = Cryptography.Blowfish.DecryptCBC(Request.QueryString["recid"]);
                        CatalogFRVN.DelimitPesertaFRVNByRecid(recordID, "admin");
                        Session["info"] = "A1";
                        Response.Redirect("Peserta.aspx");
                    }
                    else if (Request.QueryString["act"] == "3")
                    {
                        string kodeRegistrasi = Cryptography.Blowfish.DecryptCBC(Request.QueryString["regcd"]);

                        DataTable peserta = VendorGatheringData.GetPeserta(kodeRegistrasi);
                        DataTable perusahaan = VendorGatheringData.GetPerusahaan(peserta.Rows[0]["KodePerusahaan"].ToString());
                        string jenisKelamin = peserta.Rows[0]["JenisKelamin"].ToString() == "1" ? "Laki-laki" : "Perempuan";


                        //Deleting old ticket
                        FileManager.DeleteFile(Server.MapPath("../Gallery/Ticket/" + kodeRegistrasi + ".PNG"));

                        CreateImageFileWithBarcode(CreateTicketDesignWithBarcode(kodeRegistrasi, peserta.Rows[0]["NamaPeserta"].ToString(), perusahaan.Rows[0]["NamaPerusahaan"].ToString(), string.Empty, string.Empty), kodeRegistrasi, kodeRegistrasi);

                        //sending new ticket same code
                        try
                        {
                          
                            string _namaPeserta2 = peserta.Rows[0]["NamaPeserta2"].ToString();
                            string _email2 = peserta.Rows[0]["Email2"].ToString();
                            string _namaJabatan2 = peserta.Rows[0]["NamaJabatan2"].ToString();
                            string _phone2 = peserta.Rows[0]["Phone2"].ToString();

                            FRLNBus.Notification.Email.SendRegistrationNotification(peserta.Rows[0]["Email"].ToString(), kodeRegistrasi, peserta.Rows[0]["NamaPeserta"].ToString(), 
                                jenisKelamin, peserta.Rows[0]["NamaJabatan"].ToString(), peserta.Rows[0]["Email"].ToString(), peserta.Rows[0]["Phone"].ToString(), 
                                peserta.Rows[0]["Catatan"].ToString(), perusahaan.Rows[0]["NamaPerusahaan"].ToString(), perusahaan.Rows[0]["Email"].ToString(), 
                                perusahaan.Rows[0]["Alamat"].ToString(), perusahaan.Rows[0]["NamaKota"].ToString(), perusahaan.Rows[0]["KodePos"].ToString(),
                                perusahaan.Rows[0]["Telpon"].ToString(), perusahaan.Rows[0]["Fax"].ToString(), _namaPeserta2, _email2, _namaJabatan2, 
                                _phone2, Server.MapPath("../Gallery/Ticket/" + kodeRegistrasi + ".PNG"));
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Terjadi kegagalan pengiriman email!', type: 'warning'});", true);
                        }

                        Session["info"] = "A6";
                        Response.Redirect("Peserta.aspx");
                    }
                }
            }

            if (Session["info"] != null)
            {
                if (Session["info"].ToString() == "C2")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Update peserta ke sistem presensi berhasil!', type: 'success'});", true);
                }
                else if (Session["info"].ToString() == "A1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Peserta berhasil dihapus dari sistem', type: 'warning'});", true);
                }
                else if (Session["info"].ToString() == "A3")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Update data peserta berhasil!', type: 'success'});", true);
                }
                else if (Session["info"].ToString() == "A6")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Pengiriman email notifikasi berhasil!', type: 'success'});", true);
                }
                else
                {

                }

                Session["info"] = null;
            }
        }

        protected void SessionCreator()
        {
            Session["username"] = "1906";
            Session["name"] = "FRVN Administrator";
            Session["password"] = "admin";
        }

        protected void CreateContent(string ticket)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Server.MapPath("../Gallery/Ticket/" + ticket + ".PNG"));
            string contentlength = fileInfo.Length.ToString();

            Context.Response.Buffer = true;
            Context.Response.Clear();
            Context.Response.AddHeader("content-disposition", "attachment; filename=" + ticket + ".PNG");
            Context.Response.AddHeader("content-length", contentlength);
            Context.Response.ContentType = "image/png";
            Context.Response.WriteFile(Server.MapPath("../Gallery/Ticket/" + ticket + ".PNG"));
            Context.Response.End();
        }

        protected void CreateImageFileWithBarcode(string scriptsHTML, string fileName, string code)
        {
            try
            {
                System.Drawing.PointF point = new System.Drawing.PointF(0, 0);
                System.Drawing.PointF _subPpoint = new System.Drawing.PointF(0, 0);
                System.Drawing.Image _image = System.Drawing.Image.FromFile(Server.MapPath("../Gallery/Contents/ticket.PNG"));

                _image = _image.GetThumbnailImage(755, 415, null, IntPtr.Zero);
                HtmlRender.RenderToImage(_image, scriptsHTML, point);

                System.Drawing.Image _barcode = CodeGenerator.CreateBarCode(code);
                //System.Drawing.Image _barcode = Code.CreateQrCode(code, 200);
                //_barcode = _barcode.GetThumbnailImage(150, 150, null, IntPtr.Zero);
                _barcode = _barcode.GetThumbnailImage(200, 70, null, IntPtr.Zero);

                HtmlRender.RenderToImage(_barcode, string.Empty, _subPpoint);
                _barcode.RotateFlip(RotateFlipType.Rotate270FlipX);


                Bitmap bitmap = new Bitmap(755, 415);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawImage(_image, 0, 0);
                    graphics.DrawImage(_barcode, 630, 115);
                    //graphics.DrawImage(_barcode, 550, 150);
                    bitmap.Save(Server.MapPath("../Gallery/Ticket/" + fileName + ".PNG"), System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            
        }

        protected string CreateTicketDesignWithBarcode(string regcd, string pernm, string insnm, string party, string group)
        {
            StringBuilder element = new StringBuilder();

            element.Append(" <html> ");
            element.Append(" <head> ");
            element.Append(" <title></title> ");
            element.Append(" </head> ");
            element.Append(" <body> ");
            element.Append(" <form> ");
            element.Append(" <div> ");
            element.Append(" <div style='background-size: cover; width: 755px; height: 415px;text-align:center;font-weight:bold;' > ");
            element.Append(" <p style='margin-top: 20px; font-family: Rockwell; font-size: 28px; color: #07519c;'> ");
            element.Append(" BIO FARMA VENDOR GATHERING<br />");
            element.Append(" BANDUNG 24 JUNI 2019<br /><br />");
            element.Append(" <table style='border:none;margin-top:-20px;font-family:Calibri;font-weight:bold;font-size:21px; color: #07519c;'> ");
            element.Append(" <tr> ");
            element.Append(" <td style='vertical-align:central;width:150px;text-align:left' >Kode Registrasi</td> ");
            element.Append(" <td style='vertical-align:central;width:10px;text-align:center' > : </td> ");
            element.Append(" <td style='vertical-align:central;text-align:left'> " + regcd + "</td> ");
            element.Append(" <td style='vertical-align:central;text-align:left;width:20%;'> </td> ");
            element.Append(" </tr> ");
            element.Append(" <tr> ");
            element.Append(" <td style='vertical-align:central;width:150px;text-align:left' >Nama Peserta</td> ");
            element.Append(" <td style='vertical-align:central;width:10px;text-align:center' > : </td> ");
            element.Append(" <td style='vertical-align:central;text-align:left'> " + pernm + "</td> ");
            element.Append(" <td></td> ");
            element.Append(" </tr> ");
            element.Append(" <tr> ");
            element.Append(" <td style='vertical-align:central;width:150px;text-align:left' >Perusahaan</td> ");
            element.Append(" <td style='vertical-align:central;width:10px;text-align:center' > : </td> ");
            element.Append(" <td style='vertical-align:central;text-align:left'> " + insnm + "</td> ");
            element.Append(" <td></td> ");
            element.Append(" </tr> ");
            element.Append(" </table> ");
            element.Append(" </div>");
            element.Append(" </div> ");
            element.Append(" </form> ");
            element.Append(" </body> ");
            element.Append(" </html> ");

            return element.ToString();
        }


        protected string GetAction(string REGCD, string STATS)
        {
            StringBuilder element = new StringBuilder();

            if (STATS == "0")
            {
                //element.Append("<a class='btn-success btn popovers' data-content='Daftar acara' data-placement='left' data-trigger='hover' href='Peserta.aspx?act=2&regcd=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-envelope'></i> </a> ");//update status

                //element.Append("<a class='btn-success btn popovers' data-content='Daftar Acara (Tanpa Email)' data-placement='left' data-trigger='hover' href='Peserta.aspx?act=6&regcd=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-check'></i> </a> ");//update status

            }
            else 
            {
                element.Append("<a class='btn-yellow btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Preview dan perbarui data' data-placement='left' data-trigger='hover' href='DetailPeserta.aspx?acode=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-file-o'></i> </a> ");
                element.Append("<a class='btn-success btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Kirim ulang tiket' data-placement='left' data-trigger='hover' href='Peserta.aspx?act=3&regcd=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-paper-plane'></i> </a> ");//kirim ulang tiket dengan notifikasi
                element.Append("<a class='btn-blue btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Unduh tiket' data-placement='left' data-trigger='hover' target='_blank' href='../Gallery/Ticket/" + REGCD + ".png' download><i class='fa fa-download'></i> </a> ");//download tiket
            }

            return element.ToString();
        }

        protected string GetStatus(string status)
        {
            StringBuilder element = new StringBuilder();

            if (status == "0")
                element.Append("<span class='btn-warning btn popovers' data-content='Belum Daftar' data-placement='right' data-trigger='hover'><i class='fa fa-exclamation'></i> </span> ");
            else element.Append("<span class='btn-success btn popovers' data-content='Terdaftar' data-placement='right' data-trigger='hover'><i class='fa fa-check'></i> </span> ");


            return element.ToString();
        }

        protected String GenerateData()
        {
            StringBuilder _tableelement = new StringBuilder();

            DataTable table = VendorGatheringData.GetPesertaAll();
            bool isAny = true;

            if (Session["groupFilter"] != null)
                if (Session["groupFilter"].ToString() != "9")
                {
                    var rows = table.Select("STATS = " + Session["groupFilter"].ToString());

                    if (rows.Any()) table = rows.CopyToDataTable();
                    else isAny = false;
                }

            if (isAny)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    _tableelement.Append("<tr class=''>");
                    _tableelement.Append("<td>" + GetStatus(table.Rows[i]["STATS"].ToString()) + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["KodeRegistrasi"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["TanggalRegistrasi"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaPeserta"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaJabatan"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["EmailPeserta"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Phone"].ToString() + "</td>");

                    _tableelement.Append("<td>" + table.Rows[i]["NamaPeserta2"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaJabatan2"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Email2"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Phone2"].ToString() + "</td>");

                    _tableelement.Append("<td>" + table.Rows[i]["NamaPerusahaan"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaKota"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Catatan"].ToString() + "</td>");
                    _tableelement.Append("<td width='15%' align='center'>" + GetAction(table.Rows[i]["KodeRegistrasi"].ToString(), table.Rows[i]["STATS"].ToString()) + "</td>");
                    _tableelement.Append("</tr>");
                }
            }

            return _tableelement.ToString();
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["groupFilter"] = ddlFilter.SelectedValue;
        }

        protected void SetFilterValue()
        {
            ddlFilter.Items.Clear();
            ddlFilter.Items.Add(new ListItem("Terdaftar","1"));
            ddlFilter.Items.Add(new ListItem("Belum Daftar","0"));
            ddlFilter.Items.Add(new ListItem("Semua", "9"));
            if (Session["groupFilter"] == null)
                Session["groupFilter"] = ddlFilter.SelectedValue;
            else ddlFilter.SelectedIndex = 0;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Entri password diperlukan!', type: 'warning'});", true);
            }
            else
            {
                if (txtPassword.Text == ConfigurationManager.AppSettings["ResetPassKey"])
                {
                    Response.Redirect("Peserta.aspx?conf=2");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Password yang dientrikan salah!', type: 'warning'});", true);
                }
            }
        }
    }
}