<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailPeserta.aspx.cs" Inherits="FRVN.Administrator.DetailPeserta" %>

<%@ Import Namespace="FRVN.Data.DataAccess" %>
<%@ Import Namespace="FRVN.Entity.Dictionary" %>
<%@ Import Namespace="FRVN.Frameworks.Validation" %>
<%@ Import Namespace="FRVN.Frameworks.Security" %>
<%@ Import Namespace="FRVN.Presentation.Components" %>
<%@ Import Namespace="FRVN.Frameworks.Converter" %>
<%@ Import Namespace="FRVN.Entity.Dictionary" %>
<%@ Import Namespace="FRVN.Business.Activities" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html>
<script runat="server">
    
    
</script>

<html lang="en">
<head>
    <% Response.Write(BasicScripts.GetMetaScript()); %>

    <title>Vendor Gathering 2019</title>
    
    <% Response.Write(StyleScripts.GetCoreStyle()); %>
    <% Response.Write(StyleScripts.GetCustomStyle()); %>
    <% Response.Write(StyleScripts.GetTableStyle()); %>

    <link href="../Scripts/sweetalert.css" rel="stylesheet" />
    <link href="../Scripts/addition.css" rel="stylesheet" />

    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../Scripts/addition.js"></script>
</head>

<body class="full-width">

    <section id="container">

        <!--header start-->
        <%Response.Write(SideBarMenu.TopMenuElement(Session["name"].ToString())); %>
        <!--header end-->

        <!--left side bar start-->

        <!--left side bar end-->

        <!--main content start-->
        <section id="main-content">
            <section class="wrapper">
                <!-- page start-->
                <div class="row">
                    <div class="col-sm-12">
                        <section class="panel">
                            <header class="panel-heading">
                                Informasi Peserta Vendor Gathering
                            </header>
                            <div class="panel-body" style="background-image: url(../Scripts/ParallaxScripts/images/background-shadow.jpg); background-size: cover; background-repeat: repeat; color: white;">
                                <form class="form-horizontal" runat="server" style="color: white;margin-left:20px;margin-right:20px">

                                    <p style="align-content: center; color: white; font-size: 20px;"><b>INFORMASI PERUSAHAAN</b></p>
                                    <br />
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Nama Perusahaan</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:DropDownList ID="ddlPerusahaan" class="chosen form-control m-bot15" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Email Perusahaan</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtEmailPerusahaan" runat="server" class="form-control m-bot15" placeholder="e.g. name@domain.com"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Alamat</label>
                                        <div class="col-lg-10 col-md-10 col-sm-10">
                                            <asp:TextBox ID="txtAlamat" TextMode="MultiLine" runat="server" class="form-control m-bot15" placeholder="Alamat Perusahaan"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Kota</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:DropDownList ID="ddlKota" runat="server" class="chosen form-control m-bot15" placeholder="Kota"></asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Kode Pos</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtKodePos" runat="server" class="form-control m-bot15" placeholder="kode pos"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Telepon</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtTelp" runat="server" class="form-control m-bot15" placeholder="No. Telepon Instansi"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Fax</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtFax" runat="server" class="form-control m-bot15" placeholder="No. Fax"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Status SKT</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:Label ID="txtStatusSKT" style="background-color:#C4C4C4" runat="server" class="form-control m-bot15"></asp:Label>
                                        </div>
                                    </div>
                                    

                                    <br />
                                    <p style="align-content: center; color: white; font-size: 20px;"><b>INFORMASI PERSONEL</b></p>
                                    <br />

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Nama Lengkap</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtNamaPeserta" runat="server" class="form-control m-bot15" placeholder="Nama Lengkap"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Email Pribadi</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control m-bot15" placeholder="e.g. name@domain.com"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Jenis Kelamin</label>
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton runat="server" GroupName="Gender" ID="rbtLaki" value="1" Text="Laki-laki" />
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButton runat="server" GroupName="Gender" ID="rbtPerempuan" value="2" Text="Perempuan" />
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Nama Jabatan</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtNamaJabatan" runat="server" class="form-control m-bot15" placeholder="e.g. Kepala Bagian Pengadaan"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">No. HP</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtMobile" runat="server" class="form-control m-bot15" placeholder="e.g. 08123456789"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Nama peserta 2</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox Visible="false" ID="txtJumlahPeserta" runat="server" class="form-control m-bot15" placeholder="e.g. 2"></asp:TextBox>
                                            <asp:TextBox ID="txtNamaPeserta2" runat="server" class="form-control m-bot15" placeholder="Nama peserta 2"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Email peserta 2</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtEmail2" runat="server" class="form-control m-bot15" placeholder="e.g. name@domain.com"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">Jabatan peserta 2</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox Visible="false" ID="TextBox2" runat="server" class="form-control m-bot15" placeholder="e.g. 2"></asp:TextBox>
                                            <asp:TextBox ID="txtNamaJabatan2" runat="server" class="form-control m-bot15" placeholder="Jabatan peserta 2"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label" style="font-weight: bold;">No. HP peserta 2</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtMobile2" runat="server" class="form-control m-bot15" placeholder="e.g. 08123456789"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" ID="Label1" Text="Notes" class="col-sm-2 control-label" Style="font-weight: bold;"></asp:Label>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="2" runat="server" class="form-control m-bot15" placeholder="Catatan Peserta"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="margin-top: 30px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div align="center">
                                                <asp:LinkButton class="btn btn-round btn-primary" ID="btnBack" runat="server" Text="<i class='fa fa-arrow-left'></i> Back" OnClick="btnBack_Click" />
                                                <asp:LinkButton class="btn btn-round btn-primary" ID="btnUpdate" runat="server" Text="<i class='fa fa-sign-in'></i> Update" OnClick="btnUpdate_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </form>
                            </div>
                        </section>
                    </div>
                </div>
                <!-- page end-->
            </section>
    </section>
    <!--main content end-->
    </section>

    <!-- Placed js at the end of the document so the pages load faster -->
    <% Response.Write(JS.GetCoreScript()); %>
    <% Response.Write(JS.GetCustomFormScript()); %>
    <% Response.Write(JS.GetDynamicTableScript()); %>
    <% Response.Write(JS.GetInitialisationScript()); %>
</body>
</html>
