<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard2.aspx.cs" Inherits="FRVN.Administrator.Dashboard2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
    body {
      font-family: "Lato", sans-serif;
    }
    table, td, th {  
      border: 1px solid #ddd;
      text-align: left;
    }
    th{
        text-align:center;
    }
    input {
        width:100px;
        font-size:10px;
    }
    .a{
        margin-bottom:30px;
    }
    table {
      border-collapse: collapse;
      
    }

    th, td {
      padding: 10px;
      height:50px;
      font-size:15px;
    }
    .sidenav {
      height: 100%;
      width: 0;
      position: fixed;
      z-index: 1;
      top: 0;
      left: 0;
      background-color: #111;
      overflow-x: hidden;
      transition: 0.5s;
      padding-top: 60px;
    }

    .sidenav a {
      padding: 8px 8px 8px 32px;
      text-decoration: none;
      font-size: 25px;
      color: #818181;
      display: block;
      transition: 0.3s;
    }

    .sidenav a:hover {
      color: #f1f1f1;
    }
    .BUTTON1:hover{
        outline:none;
    }
    ul li p{
        color:white;
    }
    ul .active p{
        color:#f96332;
    }

    .sidenav .closebtn {
      position: absolute;
      top: 0;
      right: 25px;
      font-size: 36px;
      margin-left: 50px;
    }

    @media screen and (max-height: 450px) {
      .sidenav {padding-top: 15px;}
      .sidenav a {font-size: 18px;}
    }
            .auto-style1 {
                cursor: pointer;
                float: left;
            }
            .auto-style2 {
                height: 283px;
            }
            .auto-style3 {
                -webkit-transition: all 500ms ease;
                -o-transition: all 500ms ease;
                transition: all 500ms ease;
                border-radius: 5px;
                color: #373c40;
                font-size: 13px;
                font-weight: 700;
                text-decoration: none;
                text-transform: uppercase;
                padding: 12px 14px;
                margin-bottom:20px;
            }
            .auto-style3:hover{
                outline:none;
            }
            #hapus{
                -webkit-transition: all 500ms ease;
                -o-transition: all 500ms ease;
                transition: all 500ms ease;
                border-radius: 5px;
                background-color:red;
                border:none;

                color: white;
                font-size: 13px;
                font-weight: 700;
                text-decoration: none;
                text-transform: uppercase;
                padding: 12px 14px;
            }
             #update{
                -webkit-transition: all 500ms ease;
                -o-transition: all 500ms ease;
                transition: all 500ms ease;
                border-radius: 5px;
                background-color:lawngreen;
                border:none;

                color: white;
                font-size: 13px;
                font-weight: 700;
                text-decoration: none;
                text-transform: uppercase;
                padding: 12px 14px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="panel-header panel-header-sm">
            </div>
            <div class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title"></h4>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <section class="about-section" id="about">
        <div class="w-container about-container">
            <div style="text-align:left">
            <h2 data-ix="scroll-fade-out-20"><span class="light">total</span> event : <asp:Label ID="Label1" runat="server" Text="N"></asp:Label></h2>
            <h2 data-ix="scroll-fade-out-20"><span class="light">total upcoming</span> event : <asp:Label ID="Label2" runat="server" Text="N"></asp:Label></h2>
            </div><div class="hero-line" data-ix="scroll-fade-out-21"></div><br><br><br>
            <h2 data-ix="scroll-fade-out-20"><span class="light">upcoming</span> event (<asp:Label ID="Label3" runat="server" Text="N"></asp:Label>)<br></h2>
            
            <div class="subtex" data-ix="scroll-fade-out-22">
                <asp:gridview id="gridview1" runat="server" autogeneratecolumns="false" datasourceid="sqldatasource1" emptydatatext="there are no data records to display." align="center" CssClass="a">
                    <columns>
                        <asp:boundfield datafield="begindate" headertext="Tanggal" sortexpression="begindate" />
                        <asp:boundfield datafield="eventname" headertext="Nama Event" sortexpression="eventname" />
                    </columns>
                </asp:gridview>
                <%--<asp:sqldatasource id="sqldatasource1" runat="server" connectionstring="<%$ connectionstrings:event_portalconnectionstring2 %>" providername="<%$ connectionstrings:event_portalconnectionstring2.providername %>" selectcommand="select top 3 [begindate],[enddate],[recordid],[eventname],[theme],[venue],[locationcoordinate],[author] from [event_portal].[vg].[event]"></asp:sqldatasource>--%>
                <asp:sqldatasource id="sqldatasource1" runat="server" connectionstring="<%$ ConnectionStrings:EVENT_PORTALConnectionString %>" selectcommand="select top 3 [BeginDate],[EndDate],[RecordId],[EventName],[Theme],[Venue],[LocationCoordinate],[Author] from [EVENT]"></asp:sqldatasource>
            </div>
        </div>
    </section>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
