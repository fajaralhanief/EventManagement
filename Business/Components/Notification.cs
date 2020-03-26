using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FRVN.Business.Components
{
    public class Notification
    {
        public class FadeIn
        {
            public static String GetSuccess(string message)
            {
                StringBuilder script = new StringBuilder();
                script.Append(" <div class='alert alert-block alert-success fade in' align='center'>    ");
                script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>      ");
                script.Append(" <i class='fa fa-times'></i>                                             ");
                script.Append(" </button>                                                               ");
                script.Append(" <h1>                                                                    ");
                script.Append(" <i class='icon-ok-sign'></i>                                            ");
                script.Append(" Success!                                                                ");
                script.Append(" </h1>                                                                   ");
                script.Append(" <p style='font-size:30px;'> " + message + "</p>                         ");
                script.Append(" </div>                                                                  ");
                return script.ToString();
            }

            public static String GetFailed(string message)
            {
                StringBuilder script = new StringBuilder();
                script.Append(" <div class='alert alert-block alert-danger fade in' align='center'> ");
                script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
                script.Append(" <i class='fa fa-times'></i>                                         ");
                script.Append(" </button>                                                           ");
                script.Append(" <strong>Failed!</strong>                                            ");
                script.Append(" <p style='font-size:30px;'>" + message + " </p>                     ");
                script.Append(" </div>                                                              ");
                return script.ToString();
            }

            public static String GetInfo(string message)
            {
                StringBuilder script = new StringBuilder();
                script.Append(" <div class='alert alert-info fade in' align='center'>               ");
                script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
                script.Append(" <i class='fa fa-times'></i>                                         ");
                script.Append(" </button>                                                           ");
                script.Append(" <strong>" + message + "</strong>                                    ");
                script.Append(" </div>                                                              ");
                return script.ToString();
            }
        }
    }
}