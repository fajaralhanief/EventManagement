<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDaftarPeserta.aspx.cs" Inherits="FRVN.Administrator.PrintDaftarPeserta" %>

<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<%@ Import Namespace="FRVN.Administrator.Reports" %>
<%@ Import Namespace="FRVN.Data.DataAccess" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");

        dcvDocument.Report = new DaftarPeserta();
        dcvDocument.Report.DataSource = DataReport.GetDataPeserta();
        dcvDocument.Report.DataMember = "peserta";
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxDocumentViewer ID="dcvDocument" runat="server">
            <ToolbarItems>
                <dx:ReportToolbarButton ItemKind="PrintReport"></dx:ReportToolbarButton>
                <dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
                <dx:ReportToolbarButton ItemKind="FirstPage"></dx:ReportToolbarButton>
                <dx:ReportToolbarButton ItemKind="PreviousPage"></dx:ReportToolbarButton>
                <dx:ReportToolbarButton ItemKind="NextPage"></dx:ReportToolbarButton>
                <dx:ReportToolbarButton ItemKind="LastPage"></dx:ReportToolbarButton>
                <dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
                <dx:ReportToolbarButton ItemKind="SaveToDisk"></dx:ReportToolbarButton>
                <dx:ReportToolbarComboBox Width="70px" ItemKind="SaveFormat">
                    <Elements>
                        <dx:ListElement Value="pdf"/>
                        <dx:ListElement Value="xls"/>
                    </Elements>
                </dx:ReportToolbarComboBox>
            </ToolbarItems>
        </dx:ASPxDocumentViewer>
    
    </div>
    </form>
</body>
</html>
