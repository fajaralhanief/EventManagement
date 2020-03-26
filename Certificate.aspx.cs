using AIVOW.Business.Activity;
using AIVOW.Framework.Security;
using FRVN.Business.Components;
using FRVN.Data.DataAccess;
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
    public partial class Certificate : System.Web.UI.Page
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
                        Response.Redirect("Certificate.aspx");
                    }
                    else if (Request.QueryString["conf"] == "2")
                    {
                        CatalogFRVN.ResetDoorprizePeserta();
                        VendorGatheringData.GenerateDataPresensi();
                        Session["info"] = "C2";
                        Response.Redirect("Certificate.aspx");
                    }
                }

                if (Request.QueryString["act"] != null)
                {
                    if (Request.QueryString["act"] == "1")
                    {
                        string recordID = Cryptography.Blowfish.DecryptCBC(Request.QueryString["recid"]);
                        CatalogFRVN.DelimitPesertaFRVNByRecid(recordID, "admin");
                        Session["info"] = "A1";
                        Response.Redirect("Certificate.aspx");
                    }
                    else if (Request.QueryString["act"] == "3")
                    {
                        string regCode = Cryptography.Blowfish.DecryptCBC(Request.QueryString["regcd"]);
                        DataTable data = VendorGatheringData.GetPresensiPeserta(regCode);

                        string _namaPerusahaan = data.Rows[0]["NamaPerusahaan"].ToString();
                        string _alamat = data.Rows[0]["Alamat"].ToString();
                        string _email = data.Rows[0]["Email"].ToString();

                        try
                        {
                            //Delete last certificate
                            FileManager.DeleteFile(Server.MapPath("../Gallery/Certificate/" + regCode + ".PNG"));

                            //Create new certificate
                            CreateCertificate(CreateName(_namaPerusahaan, _alamat), regCode);

                            if (!VendorGatheringData.IsDownloadedSKT(Filtering.FilterValidSqlQuery(regCode)))
                            VendorGatheringData.InsertDownloadedSKT(regCode, "admin");

                            FRLNBus.Notification.Email.SendEmailAsAttachedCertificate(_email, _namaPerusahaan, Server.MapPath("../Gallery/Certificate/" + regCode + ".PNG"));

                            Session["info"] = "A6";
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Terjadi kegagalan pengiriman email!', type: 'warning'});", true);
                        }

                        Response.Redirect("Certificate.aspx");
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
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Pengiriman email notifikasi unduh sertifikat berhasil!', type: 'success'});", true);
                }
                else if (Session["info"].ToString() == "A7")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal({title: 'Terjadi kegagalan pengiriman email!', type: 'warning'});", true);
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

        protected void CreateCertificate(string scriptsHTML, string fileName)
        {
            System.Drawing.PointF point = new System.Drawing.PointF(-50, 50);
            System.Drawing.PointF _subPpoint = new System.Drawing.PointF(0, 0);
            System.Drawing.Image _image = System.Drawing.Image.FromFile(Server.MapPath("../Gallery/Contents/certificate.PNG"));

            //Before here define text as image
            _image = _image.GetThumbnailImage(5000, 3533, null, IntPtr.Zero);
            HtmlRender.RenderToImage(_image, scriptsHTML, point);

            //System.Drawing.Image _barcode = CodeGenerator.CreateBarCode(fileName);
            System.Drawing.Image _barcode = Code.CreateQrCode(fileName, 445);
            _barcode = _barcode.GetThumbnailImage(425, 425, null, IntPtr.Zero);
            //_barcode = _barcode.GetThumbnailImage(200, 70, null, IntPtr.Zero);

            HtmlRender.RenderToImage(_barcode, string.Empty, _subPpoint);
            //_barcode.RotateFlip(RotateFlipType.Rotate270FlipX);

            Bitmap bitmap = new Bitmap(5000, 3533);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(_image, 0, 0);
                graphics.DrawImage(_barcode, 4350, 2900);
                bitmap.Save(Server.MapPath("../Gallery/Certificate/" + fileName + ".PNG"), System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        protected string CreateName(string namaPerusahaan, string alamat)
        {
            StringBuilder element = new StringBuilder();

            element.Append(" <html> ");
            element.Append(" <head> ");
            element.Append(" <title></title> ");
            element.Append(" </head> ");
            element.Append(" <body> ");
            element.Append(" <form> ");
            element.Append(" <div style='margin-top: 1450px; text-align:center; font-family: Rockwell; font-size: 90px; color: #000;'> ");
            element.Append(namaPerusahaan);
            element.Append(" </div> ");
            element.Append(" <div style='margin-top: 20px; text-align:center; font-family: Rockwell; font-size: 60px; color: #000;'> ");
            element.Append(alamat);
            element.Append(" </div> ");

            element.Append(" </form> ");
            element.Append(" </body> ");
            element.Append(" </html> ");

            return element.ToString();
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
                element.Append("<a class='btn-success btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Generate dan kirim sertifikat surat keterangan terdaftar' data-placement='left' data-trigger='hover' href='Certificate.aspx?act=3&regcd=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-paper-plane'></i> </a> ");//kirim ulang sertifikat dengan notifikasi
            }
            else
            {
                element.Append("<a class='btn-success btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Generate dan kirim sertifikat surat keterangan terdaftar' data-placement='left' data-trigger='hover' href='Certificate.aspx?act=3&regcd=" + Cryptography.Blowfish.EncryptCBC(REGCD) + "'><i class='fa fa-paper-plane'></i> </a> ");//kirim ulang sertifikat dengan notifikasi
                element.Append("<a class='btn-blue btn popovers' style='margin-bottom:5px;margin-right:5px;' data-content='Unduh sertifikat surat keterangan terdaftar' data-placement='left' data-trigger='hover' target='_blank' href='../Gallery/Certificate/" + REGCD + ".png' download><i class='fa fa-download'></i> </a> ");//download tiket
            }

            return element.ToString();
        }

        protected string GetStatus(string status)
        {
            StringBuilder element = new StringBuilder();

            if (status == "0")
                element.Append("<span class='btn-warning btn popovers' data-content='Belum Download' data-placement='right' data-trigger='hover'><i class='fa fa-exclamation'></i> </span> ");
            else element.Append("<span class='btn-success btn popovers' data-content='Sudah Download' data-placement='right' data-trigger='hover'><i class='fa fa-check'></i> </span> ");


            return element.ToString();
        }

        protected String GenerateData()
        {
            StringBuilder _tableelement = new StringBuilder();

            DataTable table = VendorGatheringData.GetPresensiPeserta(string.Empty);
            bool isAny = true;

            var rows = table.Select("Atten = 'True'");

            if (rows.Any()) table = rows.CopyToDataTable();
            else isAny = false;

            if (Session["groupFilter"] != null)
                if (Session["groupFilter"].ToString() != "9")
                {
                    rows = table.Select("StatsSKT = " + Session["groupFilter"].ToString());

                    if (rows.Any()) table = rows.CopyToDataTable();
                    else isAny = false;
                }

            if (isAny)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    _tableelement.Append("<tr class=''>");
                    _tableelement.Append("<td>" + GetStatus(table.Rows[i]["StatsSKT"].ToString()) + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["KodeRegistrasi"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaPeserta"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaJabatan"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["EmailPeserta"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Phone"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaPerusahaan"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["Email"].ToString() + "</td>");
                    _tableelement.Append("<td>" + table.Rows[i]["NamaKota"].ToString() + "</td>");
                    _tableelement.Append("<td width='15%' align='center'>" + GetAction(table.Rows[i]["KodeRegistrasi"].ToString(), table.Rows[i]["StatsSKT"].ToString()) + "</td>");
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
            ddlFilter.Items.Add(new ListItem("Sudah Download", "1"));
            ddlFilter.Items.Add(new ListItem("Belum Download", "0"));
            ddlFilter.Items.Add(new ListItem("Semua", "9"));
            if (Session["groupFilter"] == null)
                Session["groupFilter"] = ddlFilter.SelectedValue;
            else ddlFilter.SelectedIndex = 0;
        }
    }
}