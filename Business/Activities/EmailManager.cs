using FRVN.Data.DataAccess;
using FRVN.Service.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace FRVN.Business.Activities
{
    public class EmailManager : AccessEmail
    {    
        public class PurchaseRequisition
        {
            private static string CreateNotificationMessage(string CNAME, string TBODY)
            {
                StringBuilder _message = new StringBuilder();
                _message.Append("Dear " + CNAME + ",");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Tertera List Purchase Requisition (PR) dengan status Rejected dan atau Canceled : ");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(TBODY);
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Terima kasih atas perhatiannya.");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Salam,");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("PR Administrator.");

                return _message.ToString();
            }

            public static void SendEmail(string CNAME, string EMAIL, string TBODY)
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(WebConfigurationManager.AppSettings["MailModerator"], WebConfigurationManager.AppSettings["MailModeratorName"]);

                mailMsg.To.Add(EMAIL);

                mailMsg.Subject = "[NO-REPLY] PR Notification";
                mailMsg.Body = CreateNotificationMessage(CNAME, TBODY);
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;

                SendMail(mailMsg);
            }
        }

        public class ToolsCalibration
        {
            private static string CreateNotificationMessage(string CNAME, string TBODY)
            {
                StringBuilder _message = new StringBuilder();
                _message.Append("Dear " + CNAME + ",");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("List alat yang dalam periode 30 hari ini harus dilakukan kalibrasi : ");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(TBODY);
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Terima kasih atas perhatiannya.");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Salam,");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("QA Service.");

                return _message.ToString();
            }

            public static void SendEmailNotification(string CNAME, string EMAIL, string TBODY)
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(WebConfigurationManager.AppSettings["MailModerator"], WebConfigurationManager.AppSettings["MailModeratorName"]);

                mailMsg.To.Add(EMAIL);

                mailMsg.Subject = "[NO-REPLY] Preventive Maintenance Notification";
                mailMsg.Body = CreateNotificationMessage(CNAME, TBODY);
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;

                SendMail(mailMsg);
            }

            private static string CreateConfirmationMessage(string CNAME, string TBODY)
            {
                StringBuilder _message = new StringBuilder();
                _message.Append("Dear " + CNAME + ",");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("List alat yang sudah dilakukan kalibrasi : ");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(TBODY);
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Terima kasih atas perhatiannya.");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Salam,");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("QA Service.");

                return _message.ToString();
            }

            public static void SendEmailConfirmation(string CNAME, string EMAIL, string TBODY)
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(WebConfigurationManager.AppSettings["MailModerator"], WebConfigurationManager.AppSettings["MailModeratorName"]);

                mailMsg.To.Add(EMAIL);

                mailMsg.Subject = "[NO-REPLY] Preventive Maintenance Notification";
                mailMsg.Body = CreateConfirmationMessage(CNAME, TBODY);
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;

                SendMail(mailMsg);
            }
        }

        public class Prodution
        {
            private static string CreateNotificationMessage(string CNAME, string TBODY)
            {
                StringBuilder _message = new StringBuilder();
                _message.Append("Dear " + CNAME + ",");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("List Alat yang bulan ini harus dilakukan kalibrasi : ");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(TBODY);
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Terima kasih atas perhatiannya.");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Salam,");
                _message.Append(Environment.NewLine + "<br />");
                _message.Append("Preventive Maintenance Administrator.");

                return _message.ToString();
            }

            public static void SendEmail(string CNAME, string EMAIL, string TBODY)
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(WebConfigurationManager.AppSettings["MailModerator"], WebConfigurationManager.AppSettings["MailModeratorName"]);

                mailMsg.To.Add(EMAIL);

                mailMsg.Subject = "[NO-REPLY] Preventive Maintenance Notification";
                mailMsg.Body = CreateNotificationMessage(CNAME, TBODY);
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;

                SendMail(mailMsg);
            }
        }

    }
}