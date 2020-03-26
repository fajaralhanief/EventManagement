<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="FRVN.Administrator.Transaction" %>

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

    <title>FRLN 2018</title>
    
    <% Response.Write(StyleScripts.GetCoreStyle()); %>
    <% Response.Write(StyleScripts.GetFormStyle()); %>
    <% Response.Write(StyleScripts.GetCalendarStyle()); %>
    <% Response.Write(StyleScripts.GetCustomStyle()); %>
    <% Response.Write(StyleScripts.GetTableStyle()); %>
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
                            <header class="panel-heading" >
                                Transaksi FRLN 2018
                            </header>
                            <div class="panel-body" style="background-image:url(../Scripts/ParallaxScripts/images/background-shadow.jpg);background-size:cover;background-repeat:repeat;color:white;">
                                <form class="form-horizontal" runat="server">
                                    <div class="adv-table">
                                        <div align="center" class="form-group">
                                            <p>
                                                <label class="control-label"><strong>Detail Transaksi</strong></label>
                                            </p>
                                            <br>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblDate" class="col-sm-2 control-label" Text="Tanggal Transaksi" Style="font-weight: bold;"></asp:Label>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:TextBox ID="txtDate" runat="server" value="" size="16" class="form-control form-control-inline input-medium default-date-picker" placeholder="Tanggal Transaksi"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label ID="lblTransactionType" runat="server" class="col-sm-2 control-label" Style="font-weight: bold;" Text="Jenis Transaksi"></asp:Label>
                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton runat="server" GroupName="TransactionType" ID="rbtDebit" value="0" Text="Debit" OnCheckedChanged="rbtDebit_CheckedChanged" AutoPostBack="true" />
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton runat="server" GroupName="TransactionType" ID="rbtCredit" value="1" Text="Kredit" OnCheckedChanged="rbtDebit_CheckedChanged" AutoPostBack="true" />
                                                    </label>
                                                </div>
                                            </div>
                                            <asp:Label runat="server" ID="lblCategory" class="col-sm-2 control-label" Style="font-weight: bold;" Text="Group"></asp:Label>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control m-bot15"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblDeskripsi" class="col-sm-2 control-label" Text="Deskripsi" Style="font-weight: bold;"></asp:Label>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" class="form-control m-bot15" placeholder="Deskripsi"></asp:TextBox>
                                            </div>
                                            <asp:Label runat="server" ID="lblCatatan" Text="Catatan" class="col-sm-2 control-label" Style="font-weight: bold;"></asp:Label>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" class="form-control m-bot15" placeholder="(optional)"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblNilai" class="col-sm-2 control-label" Text="Nilai (Rp)" Style="font-weight: bold;"></asp:Label>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:TextBox ID="txtValue" runat="server" class="form-control m-bot15" placeholder="contoh : 100000"></asp:TextBox>
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
    <% Response.Write(JS.GetCalendarScript()); %>
    <% Response.Write(JS.GetCustomFormScript()); %>
    
    <% Response.Write(JS.GetDynamicTableScript()); %>
    <% Response.Write(JS.GetInitialisationScript()); %>
</body>
</html>