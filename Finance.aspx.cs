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

namespace FRVN.Administrator
{
    public partial class Finance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null) Response.Redirect("SignIn.aspx");
            if (Session["username"].ToString() != "admin") Response.Redirect("SignIn.aspx");

            if (!IsPostBack)
            {
                
            }
        }

        public enum TransactionType : byte
        {
            Debet = 0,
            Credit
        }

        public static DataTable GeTransaction()
        {
            DataTable data = FinanceData.GetTransactionDetail();
            decimal saldo = 0;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                decimal _transSaldo = Convert.ToDecimal(data.Rows[i]["VALUE"].ToString());
                TransactionType type = (TransactionType)Convert.ToInt16(data.Rows[i]["TRSTY"].ToString());

                if (type == TransactionType.Credit)
                {
                    saldo = saldo + _transSaldo;
                    data.Rows[i]["CREDT"] = FRVN.Frameworks.Converter.CurrentcyConverter.SetNominalToRupiahFormat((Convert.ToInt64(Convert.ToDecimal(data.Rows[i]["VALUE"].ToString()))).ToString());
                }
                else
                {
                    saldo = saldo - _transSaldo;
                    data.Rows[i]["DEBET"] = FRVN.Frameworks.Converter.CurrentcyConverter.SetNominalToRupiahFormat((Convert.ToInt64(Convert.ToDecimal(data.Rows[i]["VALUE"].ToString()))).ToString());
                }

                data.Rows[i]["SALDO"] =  FRVN.Frameworks.Converter.CurrentcyConverter.SetNominalToRupiahFormat((Convert.ToInt64(saldo)).ToString());
            }
            return data;
        }

        protected String GenerateData()
        {
            StringBuilder _tableelement = new StringBuilder();

            DataTable table = GeTransaction();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                _tableelement.Append("<tr class=''>");
                _tableelement.Append("<td>" + FRVN.Frameworks.Converter.DateTimeConverter.GetDateFormat(table.Rows[i]["BEGDA"].ToString()) + "</td>");
                _tableelement.Append("<td>" + table.Rows[i]["CATNM"].ToString() + "</td>");
                _tableelement.Append("<td>" + table.Rows[i]["DESCR"].ToString() + "</td>");
                _tableelement.Append("<td>" + table.Rows[i]["NOTES"].ToString() + "</td>");
                _tableelement.Append("<td align='right'>" + table.Rows[i]["CREDT"].ToString() + "</td>");
                _tableelement.Append("<td align='right'>" + table.Rows[i]["DEBET"].ToString() + "</td>");
                _tableelement.Append("<td align='right'>" + table.Rows[i]["SALDO"].ToString() + "</td>");
                //_tableelement.Append("<td>" + string.Empty + "</td>");
                _tableelement.Append("</tr>");
            }

            return _tableelement.ToString();
        }
    }
}