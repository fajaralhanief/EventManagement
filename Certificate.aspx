<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Certificate.aspx.cs" Inherits="FRVN.Administrator.Certificate" %>

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

    <title>Vendor Gathering 2019</title>
    
    <% Response.Write(StyleScripts.GetCoreStyle()); %>
    <% Response.Write(StyleScripts.GetCustomStyle()); %>
    <% Response.Write(StyleScripts.GetTableStyle()); %>
    <link href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" rel="stylesheet" />
    <%--<link href="https://cdn.datatables.net/buttons/1.4.0/css/buttons.dataTables.min.css" rel="stylesheet" />--%>
    <link href="../Scripts/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>

</head>

<body class="full-width">
    <section id="container">

        <!--header start-->
        <%Response.Write(SideBarMenu.TopMenuElement(Session["name"].ToString())); %>
        <%--<%Response.Write(SideBarMenu.TopMenuElement("Admin")); %>--%>
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
                                Vendor Gathering 2019
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
                                                <asp:DropDownList runat="server" ID="ddlFilter" class="form-control m-bot15" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"> 
                                                    
                                                </asp:DropDownList>
                                            </div>
                                            <div class="btn-group pull-right">
                                                <button class="btn btn-blue dropdown-toggle" data-toggle="dropdown" style="margin-right:5px;margin-left:5px;">
                                                    Navigation <i class="fa fa-angle-down"></i>
                                                </button>
                                                <ul class="dropdown-menu pull-right">
                                                    <li><a href="Peserta.aspx"><i class="fa fa-users"></i> Peserta</a></li>
                                                    <li><a href="Certificate.aspx"><i class="fa fa-certificate"></i> Sertifikat</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <table class="ta1ble table-striped table-hover table-bordered" id="dynamic-table">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Kode</th>
                                                    <th>Nama</th> 
                                                    <th>Jabatan</th>    
                                                    <th>Email Peserta</th>  
                                                    <th>Kontak Peserta</th>
                                                    <th>Perusahaan</th> 
                                                    <th>Email</th>
                                                    <th>Kota</th> 
                                                    <th></th>
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
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#dynamic-table').DataTable({
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                    { extend: 'csv' },
                    { extend: 'excel', title: 'Peserta' },
                ]
            });
        });


    </script>
</body>
</html>

