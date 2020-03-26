<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormEvent.aspx.cs" Inherits="FRVN.Administrator.FormEvent" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
        <style>
    body {
      font-family: "Lato", sans-serif;
    }
    .drop{
        border-radius:30px;
    }
    table, td, th {  
      
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
    <!-- mobile meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="generator" content="Woozy">
    <%--datepicker--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <!-- main style -->
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/normalize.css">
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/base.css">
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/style.css">
    <!-- font-awesome stlye -->
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.2/css/font-awesome.css" rel="stylesheet">
    <!-- favion -->
    <link rel="shortcut icon" type="image/x-icon" href="../Scripts/ParallaxScripts/images/fav.png">
    <!-- apple touch icon -->
    <link rel="apple-touch-icon" href="../Scripts/ParallaxScripts/images/ico.png">
    <!-- Loading Css -->

     <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png">
    <link rel="icon" type="image/png" href="../assets/img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Tambah Event</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <!--     Fonts and icons     -->
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,200" rel="stylesheet" />
    <link href="https://use.fontawesome.com/releases/v4.7.0/css/all.css" rel="stylesheet">
    <!-- CSS Files -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/now-ui-dashboard.css?v=1.0.1" rel="stylesheet" />
    <!-- CSS Just for demo purpose, don't include it in your project -->
    <link href="../assets/demo/demo.css" rel="stylesheet" />
</head>
<body class="" style="margin-top:-100px;" >
 <div class="wrapper ">
     <form id="form1" runat="server" autocomplete="off">
        <div class="sidebar" data-color="orange">
            <!--
        Tip 1: You can change the color of the sidebar using: data-color="blue | green | orange | red | yellow"
    -->
           <div class="logo">
                <a href="#" class="simple-text logo-mini">
                    <img src="../Scripts/ParallaxScripts/images/fav.png" alt="logo biofarma">
                </a>
                <a href="#" class="simple-text logo-normal">
                    EVENT MANAGEMENT
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li >
                        <a href="Dashboard.aspx">
                            <i class="fa fa-home" aria-hidden="true"></i>
                            <p>Dashboard</p>
                        </a>
                    </li>
                    <li class="active">
                        <a href="ListEvent.aspx">
                            <i class="fa fa-calendar" aria-hidden="true"></i>
                            <p>EVENT</p>
                        </a>
                    </li>
                    <li>
                        <a href="Peserta.aspx">
                            <i class="fa fa-users" aria-hidden="true"></i>
                            <p>PARTICIPANTS</p>
                        </a>
                    </li>
                    <li>
                        <a href="Certificate.aspx">
                            <i class="fa fa-certificate" aria-hidden="true"></i>
                            <p>CERTIFICATE</p>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="fa fa-phone" aria-hidden="true"></i>
                            <p>KONTAK</p>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="main-panel" style="padding-top:-100px;">
            <!-- Navbar -->
            <nav class="navbar navbar-expand-lg navbar-transparent  navbar-absolute bg-primary fixed-top">
                <div class="container-fluid">
                    <div class="navbar-wrapper">
                        <div class="navbar-toggle">
                            <button type="button" class="navbar-toggler">
                                <span class="navbar-toggler-bar bar1"></span>
                                <span class="navbar-toggler-bar bar2"></span>
                                <span class="navbar-toggler-bar bar3"></span>
                            </button>
                        </div>
                        <a class="navbar-brand" href="#">Dashboard</a>
                    </div>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                        <span class="navbar-toggler-bar navbar-kebab"></span>
                    </button>
                    <div class="collapse navbar-collapse justify-content-end" id="navigation">
                        <ul class="navbar-nav">
                            <li class="nav-item">
                                <a class="nav-link" href="#">
                                    <i class="now-ui-icons media-2_sound-wave"></i>
                                    <p>
                                        <span class="d-lg-none d-md-block">Stats</span>
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="http://example.com" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="now-ui-icons location_world"></i>
                                    <p>
                                        <span class="d-lg-none d-md-block">Some Actions</span>
                                    </p>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="#">Action</a>
                                    <a class="dropdown-item" href="#">Another action</a>
                                    <a class="dropdown-item" href="#">Something else here</a>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">
                                    <i class="now-ui-icons users_single-02"></i>
                                    <p>
                                        <span class="d-lg-none d-md-block">Account</span>
                                    </p>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <!-- End Navbar -->
            <div class="panel-header panel-header-sm">
            </div>
            <div class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Form Penambahan Event</h4>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                     <div class="form-group" style="margin-bottom:25px;">
                            <%--<p style="font-size:20px;color:white;">Silakan isi dan pastikan data sudah benar&nbsp; </p>--%>
                            
                    </div>

                    <div class="form-group">
                        <table style="margin-left:210px;">
                           <tr>
                            <td class="auto-style1"><label  style="font-weight: bold;margin-right:73px;margin-bottom:20px;text-align:center">Tanggal</label></td>
                            <td><asp:textbox id="tanggalmulai" runat="server" class="form-control m-bot15" placeholder="tanggal mulai" width="169px" required=""></asp:textbox>
                            </td>
                            <td><asp:textbox id="tanggalselesai" runat="server" class="form-control m-bot15" placeholder="tanggal selesai" width="169px" required=""></asp:textbox>
                            </td>
                            <%--<td class="auto-style1"> <input class="form-control m-bot15" type="text" ID="tanggalmulai" placeholder="Tanggal Mulai" runat="server"></td>
                            <td class="auto-style1"> <input class="form-control m-bot15" type="text" ID="tanggalselesai" placeholder="Tanggal Selesai" runat="server"></td>
                           --%></tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;margin-right:55px;" >Jam</label></td>
                                <td><asp:TextBox ID="txtTime" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtTime2" runat="server"></asp:TextBox></td>
                            </tr>
                            
                            </table><table style="margin-left:210px;margin-bottom:30px;">
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;margin-right:55px;">Tipe Event</label></td>
                                <td>
                                    <asp:DropDownList ID="tipeevent" runat="server" Height="40px" Width="170px" CssClass="drop">
                                        <asp:ListItem Enabled="True" Selected="True">-Pilih Tipe event-</asp:ListItem>
                                        <asp:ListItem>Public</asp:ListItem>
                                        <asp:ListItem>Private</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Nama Event</label></td>
                                <td><asp:TextBox ID="namaevent"   runat="server" class="form-control m-bot15" Width="360px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Tema Event</label></td>
                                <td><asp:TextBox ID="temaevent"    runat="server" class="form-control m-bot15" Width="360px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Venue</label></td>
                                <td><asp:TextBox ID="venue"   runat="server" class="form-control m-bot15" Width="360px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Kota</label></td>
                                <td><asp:TextBox ID="kota"    runat="server" class="form-control m-bot15" Width="169px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Map Coordinate</label></td>
                                <td><asp:TextBox ID="mapcoordinate"    runat="server" class="form-control m-bot15" Width="169px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label  style="font-weight: bold;margin-bottom:20px;">Author</label></td>
                                <td><asp:TextBox ID="author"   runat="server" class="form-control m-bot15" Width="169px"></asp:TextBox></td>
                            </tr>
                            </table>
                    </div>
                   


                    <div class="form-group" style="margin-top:25px;">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div align="center">

                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary" OnClick="LinkButton2_Click" Width="115px" style ="margin-right:50px;"><i class="fa fa-chevron-circle-left" aria-hidden="true" style="font-size:16px;"></i>&nbsp;&nbsp;KEMBALI</asp:LinkButton>
                                <asp:LinkButton ID="submit" runat="server" CssClass="btn btn-primary" OnClick="SubmitEvent()"><i class="fa fa-floppy-o" aria-hidden="true" style="font-size:16px;"></i>&nbsp;&nbsp;SIMPAN</asp:LinkButton>
                                &nbsp;</div>
                                <button id="submit">cb html button</button>
                        </div>
                    </div>

                </form>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="footer">
                <div class="container-fluid" style="color:white;">
                    <nav>
                        <ul>
                            <li>
                                <a href="#">
                                    Creative Tim
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    About Us
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    Blog
                                </a>
                            </li>
                        </ul>
                    </nav>
                    <div class="copyright">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>, Designed by
                        <a href="#" target="_blank">Biofarma</a>
                    </div>
                </div>
            </footer>
        </div>
    </div>


    <!-- JQUERY SCRIPTS -->
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/woozy.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/jquery.countTo.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/form.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/modernizr.js"></script>
    <!-- google web fonts -->
    <%--script datepicker--%>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $( function() {
            $( "#tanggalmulai" ).datepicker();
        });

        $( function() {
            $( "#tanggalselesai" ).datepicker();
        });

        $(document).ready(function () {
        
            $('#submit').click(function () {
                SubmitEvent();
            });

            $('#btnEdit').click(function () {
                editEvent();
            });

            $('#btnDelete').click(function () {
                deleteEvent();
            });
        });


        function SubmitEvent() {
            var tanggal_mulai = $("#tanggalmulai").val() + " " + $("#txtTime").val();
            var tanggal_selesai = $("#tanggalselesai").val() +" "+ $("#txtTime2").val();
            var event_name = $("#namaevent").val();
            var theme_event = $("#temaevent").val();
            var venue_event = $("#venue").val();
            var lokasi_event = $("#mapcoordinate").val();
            var author_event = $("#author").val();
            var desc = $("#txtDeskripsi").val();
            
            console.log();
            $.ajax({
                type: 'POST',
                url: '../WebServices/EventManagement.asmx/InsertEvent',
                data: JSON.stringify({
                    BeginDate:tanggal_mulai,
                    EndDate: tanggal_selesai,
                    EventName: event_name,
                    Theme : theme_event,
                    Venue : venue_event,
                    LocationCoordinate: lokasi_event,
                    Author: author_event
                }),
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: 'text',
                success: function (d) {

                    d = d.replace('{"d":null}', "");
                    dt = JSON.parse(d).data;
                        
                    swal({
                        title: dt.head,
                        text: dt.notes,
                        type: dt.type,
                        //confirmButtonText: "Yes, delete it!",   
                        //cancelButtonText: "No, cancel plx!",   
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }, function (isConfirm) {
                        swal.close();
                        //InitTable();

                    });
                },
                error: function (d) {
                    NotifAjaxError();
                }

            });

        }

        //function deleteMitra() {
        //    // simpen mitraID nya di modal terus kalo udh masuk ke delete nya baru dia sesuaiin sama id yang di simpen di modal nya itu
        //    swal({
        //        title: "Are you sure?",
        //        text: "You will not be able to recover this record!",
        //        type: "warning",
        //        showCancelButton: true,
        //        confirmButtonColor: "#DD6B55",
        //        confirmButtonText: "Yes, delete it!",
        //        closeOnConfirm: false
        //    },          
        //    //DelimitMitra(string mitraID, string mitraName
        //    function () {
        //            var mitraID = $("#txtMitraID").val();

        //        $.ajax({
        //            type: 'POST',
        //            url: '../../WebServices/MasterContract.asmx/DelimitMitra',
        //            data: JSON.stringify({
        //                mitraID: mitraID,
        //            }),
        //            contentType: "application/json; charset=utf-8",
        //            async: false,
        //            dataType: 'text',
        //            success: function (d) {

        //                d = d.replace('{"d":null}', "");
        //                dt = JSON.parse(d).data;

        //                swal({
        //                    title: dt.head,
        //                    text: dt.notes,
        //                    type: dt.type,
        //                    //confirmButtonText: "Yes, delete it!",   
        //                    //cancelButtonText: "No, cancel plx!",   
        //                    closeOnConfirm: false,
        //                    closeOnCancel: false
        //                }, function (isConfirm) {
        //                    swal.close();
        //                    InitTable();

        //                });
        //            },
        //            error: function (d) {
        //                NotifAjaxError();
        //            }

        //        });
        //    });

        //}

        //function editMitra() {
        //    $.ajax({
        //        type: 'POST',
        //        url: '../../WebServices/MasterContract.asmx/InsertMitra',
        //        data: {
        //            mitraName: mitraName,
        //            desc: desc
        //        },
        //        contentType: "application/json; charset=utf-8",
        //        dataType: 'json',
        //        succes: function () {

        //            alert("data has been add successfully");
                    
        //        },
        //        failure: function () {
        //            NotifAjaxError();
        //        }
        //    });                
        //}
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.4.7/webfont.js"></script>
    <script>
        WebFont.load({
            google: {
                families: ["Open Sans:300,300italic,400,400italic,600,600italic,700,700italic,800,800italic"]
            }
        });

        $(document).scroll(function () {
            "use strict";
            var y = $(this).scrollTop();
            if (y >= 300) {
                $('.dot-container').fadeIn();
            } else {
                $('.dot-container').fadeOut();
            }
        });
        $(window).load(function () {
            "use strict";
            $('.loading').fadeOut();
        });
    </script>

    <script type="text/javascript"><!--
    $('.timer').countTo();
    //--></script>


        function openNav() {
            document.getElementById("mySidenav").style.width = "250px";
        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
        }
    </script>
  
    </form>
  
</body>
</html>

