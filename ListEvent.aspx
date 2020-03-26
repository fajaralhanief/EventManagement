<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListEvent.aspx.cs" Inherits="FRVN.Administrator.ListEvent" %>

<!DOCTYPE html>
<html>
<head>

    <meta charset="utf-8">
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
    <title>List Event</title>
    <!-- mobile meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="generator" content="Woozy">
    <!-- main style -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/normalize.css">
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/base.css">
    <link rel="stylesheet" type="text/css" href="../Scripts/ParallaxScripts/css/style.css">
    <!-- font-awesome stlye -->
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet">
    <!-- favion -->
    <link rel="shortcut icon" type="image/x-icon" href="../Scripts/ParallaxScripts/images/fav.png">
    <!-- apple touch icon -->
    <link rel="apple-touch-icon" href="../Scripts/ParallaxScripts/images/ico.png">
    <!-- Loading Css -->


    <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png">
    <link rel="icon" type="image/png" href="../assets/img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>List Event</title>
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
     <form id="form1" runat="server">
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
                        <a class="navbar-brand" href="#">Table List</a>
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
                                <h4 class="card-title">Table Event</h4>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:LinkButton ID="Button1" runat="server" Text="TAMBAH EVENT" BackColor="#f96332" BorderStyle="None" CausesValidation="False" CssClass="auto-style3" ForeColor="White" Width="160px" OnClick="Button1_Click"><i class='fa fa-plus' style="font-weight:normal;font-size:15px;"></i>&nbsp;&nbsp;TAMBAH EVENT</asp:LinkButton>

                                    <table id="example" class="display" data-ix="scroll-fade-out-21">
                                        <thead>
                                            <tr>
                                                <th>ID Event</th>
                                                <th>Tanggal Mulai</th>
                                                <th>Tanggal Selesai</th>
                                                <th>Nama Event</th>
                                                <th>Theme</th>
                                                <th>Venue</th>
                                                <th>Map</th>
                                                <th>Author</th>
                                                <th style="width:80px;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
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
</form>    
    <!-- JQUERY SCRIPTS -->
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/woozy.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/jquery.countTo.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/form.js"></script>
    <script type="text/javascript" src="../Scripts/ParallaxScripts/js/modernizr.js"></script>
    <!-- google web fonts -->
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.4.7/webfont.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script> 
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script> 
    <script>
        WebFont.load({
            google: {
                families: ["Open Sans:300,300italic,400,400italic,600,600italic,700,700italic,800,800italic"]
            }
        });
    </script>
    <!--[if lte IE 9]><script src="https://cdnjs.cloudflare.com/ajax/libs/placeholders/3.0.2/placeholders.min.js"></script><![endif]-->
    <script>
        $(document).scroll(function () {
            "use strict";
            var y = $(this).scrollTop();
            if (y >= 300) {
                $('.dot-container').fadeIn();
            } else {
                $('.dot-container').fadeOut();
            }
        });
    </script>
    <script>
        $(document).ready(function (){
           // alert('s salary is:');
            //var data = table.row( $(this).parents('tr') ).data();
            //alert( data[0] +"'s salary is: "+ data[ 5 ] );
            //var table = document.getElementById('example');
            
            //var data = table.row( $(this).parents('tr') ).data();
            //alert( data[0] +"'s salary is: "+ data[ 5 ] );
            //for (var r = 0, n = table.rows.length; r < n; r++) {
            //    for (var c = 0, m = table.rows[r].cells.length; c < m; c++) {
            //        alert(table.rows[r].cells[c].innerHTML);
            //    }
            //}
            $("#example #update").click(function(){
               $(this).parent().parent().addClass('selected').siblings().removeClass('selected');    
               var value=$(this).parent().parent().find('td:first').html();
               //alert(value);
               window.location.href = "UpdateEvent.aspx?id="+value;
            });
            $("#example #hapus").click(function(){
               $(this).parent().parent().addClass('selected').siblings().removeClass('selected');    
               var value=$(this).parent().parent().find('td:first').html();
               //alert(value);
               window.location.href = "hapus.aspx?id="+value;
            });

        });
    </script>
    <script>
        function hapus() {
            PageMethods.Delete();
        }
    </script>
    <script>
        $(document).ready(function () {
            
            $("#example").DataTable( {
                columnDefs: [ {
                    targets: [ 0 ],
                    orderData: [ 0, 1 ]
                }, {
                    targets: [ 1 ],
                    orderData: [ 1, 0 ]
                }, {
                    targets: [ 4 ],
                    orderData: [ 4, 0 ]
                } ]
            });

            
        } );
    </script>
    <script>
        $(window).load(function () {
            "use strict";
            $('.loading').fadeOut();
        });
    </script>

    <script type="text/javascript"><!--
    $('.timer').countTo();
    //--></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnShow').click(function () {
                $("#dialog").dialog();
                $("#frame").attr("src", "Files/Ketentuan_Sertifikat_Digital.pdf");
            });
        });
</script>
  <script>
    function openNav() {
      document.getElementById("mySidenav").style.width = "250px";
    }

    function closeNav() {
      document.getElementById("mySidenav").style.width = "0";
    }

      function a() {
          window.location.href = "http://stackoverflow.com";
      }
  </script>
  
   
  
</body>

</html>

