<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finance.aspx.cs" Inherits="FRVN.Administrator.Finance" %>

<%@ Import Namespace="FRVN.Data.DataAccess" %>
<%@ Import Namespace="FRVN.Entity.Dictionary" %>
<%@ Import Namespace="FRVN.Frameworks.Validation" %>
<%@ Import Namespace="FRVN.Frameworks.Security" %>
<%@ Import Namespace="FRVN.Presentation.Components" %>
<%@ Import Namespace="FRVN.Frameworks.Converter" %>
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
                            <header class="panel-heading">
                                Finance FRLN 2018
                          <span class="tools pull-right">
                          </span>
                            </header>
                            <form runat="server">
                                <div class="panel-body">
                                    <%--<h6>Annotation :</h6>
                                    <p>                                    
                                    <span class='label label-warning'><i class='fa fa-file-o'></i></span> &nbsp; untuk "Detail dan Perubahan Data" &nbsp;&nbsp;
                                    <span class='label label-danger'><i class='fa fa-times'></i></span> &nbsp; untuk "Hapus Data". &nbsp;&nbsp;
                                    </p>
                                    <br>--%>
                                    <div class="adv-table">
                                        <div class="clearfix">
                                            <div class="btn-group">
                                                   <b> Saldo Akhir : <% Response.Write(CurrentcyConverter.SetNominalToRupiahFormat((Convert.ToInt64(Convert.ToDecimal(FinanceData.GetSaldo(DateTime.Now.ToString()).Rows[0]["Saldo"].ToString()))).ToString())); %></b>

                                               <%-- <asp:DropDownList runat="server" ID="ddlFilter" class="form-control m-bot15" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"> 
                                                    
                                                </asp:DropDownList>--%>
                                            </div>
                                            <div class="btn-group pull-right">
                                                <a class="btn btn-blue" href="Transaction.aspx" target="_blank"><i class="fa fa-file"></i> Add Transaction</a>

                                                <button class="btn btn-blue dropdown-toggle" data-toggle="dropdown" style="margin-right:5px;margin-left:5px;">
                                                    Navigation <i class="fa fa-angle-down"></i>
                                                </button>
                                                <ul class="dropdown-menu pull-right">
                                                    <li><a href="Finance.aspx"><i class="fa fa-dollar"></i> Finance</a></li>
                                                    <li><a href="Peserta.aspx"><i class="fa fa-users"></i> Participant</a></li>
                                                    <li><a href="#"><i class="fa fa-certificate"></i> Certificate</a></li>
                                                    <li><a href="Configuration.aspx"><i class="fa fa-gear"></i> Configuration</a></li>
                                                    
                                                </ul>
                                                
                                                
                                                <%--<ul class="dropdown-menu pull-right">
                                                    <li><a href="PrintDaftarPeserta.aspx" target="_blank"><i class="fa fa-print"></i> Print Daftar Peserta</a></li>
                                                </ul>--%>
                                            </div>
                                        </div> 
                                        <table class="table table-striped table-hover table-bordered" style="margin-top:25px;" id="tabFinance">
                                            <thead>
                                                <tr>
                                                    <th>Tanggal</th>
													<th>Jenis Transaksi</th>
                                                    <th>Deskripsi</th>        
                                                    <th>Catatan</th>  
                                                    <th>Kredit</th>
                                                    <th>Debit</th>       
                                                    <th>Saldo</th> 
                                                    <%--<th></th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%  Response.Write(GenerateData()); %>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </form>
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

