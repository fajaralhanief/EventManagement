using System;
using System.Collections.Generic;
using System.Configuration;
using FRVN.Frameworks.Security;
using System.Linq;
using System.Web;

namespace FRVN.Business.Components
{
    public class Administrator
    {
        public static bool GetDefaultAccess(string username, string password)
        {
            string _username = username;
            string _password = password;
            if (_username == ConfigurationManager.AppSettings["defusername"] && _password == ConfigurationManager.AppSettings["defpassword"])
            { 
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetDefaultRegistrationAccess(string username, string password)
        {
            string _username = username;
            string _password = password;
            if ((_username == ConfigurationManager.AppSettings["gate1"] || _username == ConfigurationManager.AppSettings["gate2"] || _username == ConfigurationManager.AppSettings["gate3"]) && _password == ConfigurationManager.AppSettings["gatepass"])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetDefaultDoorprizeAccess(string username, string password)
        {
            string _username = username;
            string _password = password;
            if (_username == ConfigurationManager.AppSettings["doorprizeUsername"] && _password == ConfigurationManager.AppSettings["doorprizePassword"])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}