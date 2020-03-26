using FRVN.Data.DataAgent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace FRVN.Data.DataAccess
{
    #region CatalogManager
    public class CatalogManager : DatabaseFactory
    {
        public static DateTime GetDateTimeServer()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_GetDateTimeServer");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "DateTime";
                return Convert.ToDateTime(data.Rows[0]["ServerDateTime"].ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static string GetDate()
        {
            return DateTime.Now.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"].ToString());
        }

        public static string DelimitByMinute()
        {
            return DateTime.Now.AddMinutes(-1).ToString(WebConfigurationManager.AppSettings["DateTimeFormat"].ToString());
        }

        public static string DelimitByDay()
        {
            return DateTime.Now.AddMinutes(-1).ToString(WebConfigurationManager.AppSettings["DateTimeFormat"].ToString());
        }

        public static string GetMaxDate()
        {
            return DateTime.MaxValue.ToString(WebConfigurationManager.AppSettings["DateTimeFormat"].ToString());
        }

        public static string GetIncludeQuery(List<string> parameter)
        {
            var _param = new StringBuilder();

            for (var i = 0; i < parameter.Count; i++)
            {
                if (i == (parameter.Count - 1))
                {
                    _param.Append("'" + parameter[i].ToString() + "'");
                }
                else
                {
                    _param.Append("'" + parameter[i].ToString() + "',");
                }
            }

            return _param.ToString();
        }
    }
    #endregion CatalogManager

    #region UserCatalog
    public class UserCatalog : CatalogManager
    {
        public static void InsertEmail(string PERNR, string EMAIL, string USRDT)
        {
            var conn = GetConnection();
            var query = @"INSERT INTO bioumum.USER_EMAIL (BEGDA, ENDDA, PERNR, EMAIL, CHGDT, USRDT)
                            VALUES ('" + GetDate() + "','" + GetMaxDate() + "','" + PERNR + "','" + EMAIL + "','" + GetDate() + "','" + USRDT + "');";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateEmail(string PERNR, string EMAIL, string USRDT)
        {
            var conn = GetConnection();
            var query = @"UPDATE bioumum.USER_EMAIL SET ENDDA = '" + DelimitByMinute() + "', CHGDT = '" + GetDate() + "', USRDT = '" + USRDT + "' WHEREPERNR = '" + PERNR + "' AND BEGDA <= GETDATE() AND ENDDA >= GETDATE());";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                InsertEmail(PERNR, EMAIL, USRDT);
            }
        }

        public static void EmptyLocalUserEmail()
        {
            var conn = GetConnection();
            var query = @"TRUNCATE TABLE bioumum.USER_EMAIL";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ValidateLocalUserEmail(string EMAIL)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                  ");
            query.Append(" COUNT(*)                ");
            query.Append(" FROM bioumum.USER_EMAIL ");
            query.Append(" WHERE                   ");
            query.Append(" EMAIL = '" + EMAIL + "' ");
            query.Append(" AND BEGDA <= GETDATE()  ");
            query.Append(" AND ENDDA >= GETDATE()  ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var result = 0;
                var reader = GetDataReader(cmd);

                while (reader.Read())
                {
                    result = Convert.ToInt16(reader[0]);
                }

                return result;
            }
            finally
            {
                conn.Close();
            }
        }

        public static string GetUserEmailByPERNR(string PERNR)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                  ");
            query.Append(" EMAIL                   ");
            query.Append(" FROM bioumum.USER_EMAIL ");
            query.Append(" WHERE                   ");
            query.Append(" PERNR = '" + PERNR + "' ");
            query.Append(" AND BEGDA <= GETDATE()  ");
            query.Append(" AND ENDDA >= GETDATE()  ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                string result = null;
                var reader = GetDataReader(cmd);

                while (reader.Read())
                {
                    result = reader[0].ToString();
                }

                return result;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertPassword(string PERNR, string PASSW, string USRDT)
        {
            var conn = GetConnection();
            var query = @"INSERT INTO bioumum.USER_PASSWORD (BEGDA, ENDDA, PERNR, PASSW, CHGDT, USRDT)
                            VALUES ('" + GetDate() + "','" + GetMaxDate() + "','" + PERNR + "','" + PASSW + "','" + GetDate() + "','" + USRDT + "');";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePassword(string PERNR, string PASSW, string USRDT)
        {
            var conn = GetConnection();
            var query = @"UPDATE bioumum.USER_PASSWORD SET ENDDA = '" + DelimitByMinute() + "', CHGDT = '" + GetDate() + "', USRDT = '" + USRDT + "' WHEREPERNR = '" + PERNR + "' AND BEGDA <= GETDATE() AND ENDDA >= GETDATE());";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                InsertPassword(PERNR, PASSW, USRDT);
            }
        }

        public static int GetPasswordActivePeriod(string PERNR)
        {
            var conn = GetConnection();
            var query = @"SELECT BEGDA, Convert(int,(GETDATE() - BEGDA))
                            FROM bioumum.USER_PASSWORD WHERE PERNR = '" + PERNR + "' AND BEGDA <= GETDATE() AND ENDDA >= GETDATE();";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                reader.Read();
                return Convert.ToInt16(reader[1]);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertUserRole(string PERNR, string ROLID, string USRDT)
        {
            var conn = GetConnection();
            var query = @"INSERT INTO gcg.USER_OTORITY (BEGDA, ENDDA, PERNR, ROLID, CHGDT, USRDT)
                            VALUES ('" + GetDate() + "','" + GetMaxDate() + "','" + PERNR + "','" + ROLID + "','" + GetDate() + "','" + USRDT + "');";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateUserRole(string PERNR, string ROLID, string USRDT)
        {
            var conn = GetConnection();
            var query = @"UPDATE gcg.USER_OTORITY SET ENDDA = '" + DelimitByMinute() + "', CHGDT = '" + GetDate() + "', USRDT = '" + USRDT + "' WHEREPERNR = '" + PERNR + "' AND BEGDA <= GETDATE() AND ENDDA >= GETDATE());";

            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                InsertPassword(PERNR, ROLID, USRDT);
            }
        }

        public static DataTable GetUserRole()
        {
            var conn = GetConnection();
            var query = @"SELECT DISTINCT ROLID, ROLNM
                            FROM gcg.USER_ROLE ORDER BY ROLNM";
            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Role";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        internal static DataTable GetUserApplicationAccount()
        {
            var conn = GetConnection();

            var query = new StringBuilder();

            query.Append(" SELECT UD.PERNR, UD.CNAME, UE.EMAIL, UR.ROLNM                       ");
            query.Append(" FROM                                                                ");
            query.Append(" gcg.USER_ROLE UR, gcg.USER_OTORITY UO,                              ");
            query.Append(" gcg.USER_DATA_NEW UD, gcg.USER_EMAIL UE                             ");
            query.Append(" WHERE                                                               ");
            query.Append(" UO.PERNR = UD.PERNR AND UD.PERNR = UE.PERNR AND UR.ROLID = UO.ROLID ");
            query.Append(" AND UO.BEGDA <= GETDATE() AND UO.ENDDA >= GETDATE()                 ");
            query.Append(" AND UR.BEGDA <= GETDATE() AND UR.ENDDA >= GETDATE()                 ");
            query.Append(" AND UE.BEGDA <= GETDATE() AND UE.ENDDA >= GETDATE()                 ");
            query.Append(" GROUP BY UD.PERNR, UD.CNAME, UE.EMAIL, UR.ROLNM                     ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "User";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetUserApplicationAccountByRole(List<string> ROLID)
        {
            var rolid = GetIncludeQuery(ROLID);
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT UD.PERNR, UD.CNAME, UE.EMAIL                 ");
            query.Append(" FROM                                                ");
            query.Append(" gcg.USER_ROLE UR, gcg.USER_OTORITY UO,              ");
            query.Append(" bioumum.USER_DATA_NEW UD, bioumum.USER_EMAIL UE     ");
            query.Append(" WHERE                                               ");
            query.Append(" UO.PERNR = UD.PERNR AND UD.PERNR = UE.PERNR AND     ");
            query.Append(" UR.ROLID = UO.ROLID AND UR.ROLID IN (" + rolid + ") ");
            query.Append(" AND UO.BEGDA <= GETDATE() AND UO.ENDDA >= GETDATE() ");
            query.Append(" AND UR.BEGDA <= GETDATE() AND UR.ENDDA >= GETDATE() ");
            query.Append(" AND UE.BEGDA <= GETDATE() AND UE.ENDDA >= GETDATE() ");
            query.Append(" GROUP BY UD.PERNR, UD.CNAME, UE.EMAIL               ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "User";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetUserInformation(string PERNR)
        {
            var conn = GetConnection();

            var query = new StringBuilder();

            query.Append(" SELECT UD.PERNR, UD.CNAME, UD.PRPOS, UD.PRORG,     ");
            query.Append(" UE.EMAIL                                           ");
            query.Append(" FROM                                               ");
            query.Append(" gcg.USER_OTORITY UO,                           ");
            query.Append(" gcg.USER_DATA_NEW UD, gcg.USER_EMAIL UE    ");
            query.Append(" WHERE                                              ");
            query.Append(" UO.PERNR = UD.PERNR AND UD.PERNR = UE.PERNR        ");
            query.Append(" AND UO.BEGDA <= GETDATE() AND UO.ENDDA >= GETDATE()");
            query.Append(" AND UE.BEGDA <= GETDATE() AND UE.ENDDA >= GETDATE()");
            query.Append(" AND UD.PERNR = '" + PERNR + "'                     ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "User";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ValidateNIKEmployee(string NIK)
        {
            var conn = GetConnection();
            var query = @"SELECT DISTINCT COUNT(*)
                            FROM bioumum.USER_DATA_NEW
                            WHERE PERNR = '" + NIK + "'";
            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                reader.Read();
                return Convert.ToInt16(reader[0]);
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetUserApplicationData(string PERNR)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT US.PERNR, US.CNAME, US.PRPOS, US.PRORG, US.GRPNM, US.SGRNM, US.PSGRP, UE.EMAIL, US.COCNM ");
            query.Append(" FROM bioumum.USER_DATA_NEW US, bioumum.USER_EMAIL UE                                            ");
            query.Append(" WHERE                                                                                           ");
            query.Append(" AND UE.BEGDA <= GETDATE() AND UE.ENDDA >= GETDATE()                                             ");
            query.Append(" AND US.BEGDA <= GETDATE() AND US.ENDDA >= GETDATE()                                             ");
            query.Append(" AND US.PERNR = UE.PERNR AND US.PERNR = '" + PERNR + "'                                          ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                var user = (object[])null;
                while (reader.Read())
                {
                    var values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString() };
                    user = values;
                }
                return user;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetUserData(string NIK)
        {
            var conn = GetConnection();
            var query = @"SELECT US.PERNR, US.CNAME, US.PRPOS, US.PRORG, US.GRPNM, US.SGRNM, US.PSGRP
                            FROM bioumum.USER_DATA_NEW US WHERE US.PERNR = '" + NIK + "'";
            var cmd = GetCommand(conn, query);

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                var user = (object[])null;
                while (reader.Read())
                {
                    var values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString() };
                    user = values;
                }
                return user;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetActiveUser(string PERNR, string PASSW)
        {
            SqlConnection conn = GetConnection();
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append(" SELECT                                                         ");
            sqlCmd.Append(" US.PERNR, PS.PASSW, EM.EMAIL, US.CNAME, US.COCTR, PS.PLOCK,    ");
            sqlCmd.Append(" DATEDIFF(DAY,PS.BEGDA,GETDATE()) AS PSPRD, US.PSTYP, US.POSID, ");
            sqlCmd.Append(" US.COCNM, US.PRORG                                             ");
            sqlCmd.Append(" FROM                                                           ");
            sqlCmd.Append(" bioumum.USER_DATA_NEW US,                                      ");
            sqlCmd.Append(" bioumum.USER_EMAIL EM,                                         ");
            sqlCmd.Append(" bioumum.USER_PASSWORD PS                                       ");
            sqlCmd.Append(" WHERE                                                          ");
            sqlCmd.Append(" US.PERNR = EM.PERNR AND US.PERNR = PS.PERNR                    ");
            sqlCmd.Append(" AND EM.BEGDA <= GETDATE() AND EM.ENDDA >= GETDATE()            ");
            sqlCmd.Append(" AND PS.BEGDA <= GETDATE() AND PS.ENDDA >= GETDATE()            ");
            sqlCmd.Append(" AND US.PERNR = '" + PERNR + "' AND PS.PASSW = '" + PASSW + "'  ");

            SqlCommand cmd = GetCommand(conn, sqlCmd.ToString());

            try
            {
                conn.Open();
                SqlDataReader reader = GetDataReader(cmd);
                object[] user = null;
                while (reader.Read())
                {
                    object[] values = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString() };
                    user = values;
                }

                return user;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetActiveUserFromCTI(string EMAIL)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                                                                      ");
            query.Append(" US.PERNR, PS.PASSW, EM.EMAIL, US.CNAME, US.COCTR, US.COCNM                  ");
            query.Append(" FROM                                                                        ");
            query.Append(" bioumum.USER_DATA_NEW US, bioumum.USER_EMAIL EM, bioumum.USER_PASSWORD PS   ");
            query.Append(" WHERE                                                                       ");
            query.Append(" US.PERNR = EM.PERNR AND US.PERNR = PS.PERNR AND EM.BEGDA <= GETDATE()       ");
            query.Append(" AND EM.ENDDA >= GETDATE() AND EM.EMAIL = '" + EMAIL + "'                    ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                var user = (object[])null;
                while (reader.Read())
                {
                    var values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                    user = values;
                }

                return user;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetAllActiveUser()
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                                                                  ");
            query.Append(" US.PERNR, US.CNAME, US.PRPOS, US.PRORG, US.GRPNM, US.SGRNM, US.PSGRP    ");
            query.Append(" FROM                                                                    ");
            query.Append(" bioumum.USER_DATA_NEW US                                                ");
            query.Append(" ORDER BY US.PERNR;                                                      ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "ActiveUser";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion UserCatalog

    #region CatalogUserRole
    public class CatalogUserRole : CatalogManager
    {
        public static void InsertUserOtority(string BEGDA, string ENDDA, string PERNR, string ROLID, string CHGDT, string USRDT)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO gcg.USER_OTORITY                                         ");
            query.Append(" (BEGDA, ENDDA, PERNR, ROLID, CHGDT, USRDT)                           ");
            query.Append(" VALUES                                                               ");
            query.Append(" ('" + BEGDA + "', '" + ENDDA + "', '" + PERNR + "', '" + ROLID + "', ");
            query.Append(" '" + CHGDT + "', '" + USRDT + "')                                    ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateUserOtority(string BEGDA, string ENDDA, string RECID, string PERNR, string ROLID, string CHGDT, string USRDT)
        {
            DelimitUserOtority(RECID, USRDT);
            InsertUserOtority(BEGDA, ENDDA, PERNR, ROLID, CHGDT, USRDT);
        }

        public static void DelimitUserOtority(string RECID, string USRDT)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" UPDATE gcg.USER_OTORITY                  ");
            query.Append(" SET ENDDA = '" + DelimitByMinute() + "', ");
            query.Append(" CHGDT = '" + GetDate() + "',             ");
            query.Append(" USRDT = '" + USRDT + "'                  ");
            query.Append(" WHERE RECID = '" + RECID + "' AND        ");
            query.Append(" BEGDA <= GETDATE() AND                   ");
            query.Append(" ENDDA >= GETDATE()                       ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteUserOtority(string RECID)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" DELETE FROM gcg.USER_OTORITY     ");
            query.Append(" WHERE RECID = '" + RECID + "' AND");
            query.Append(" BEGDA <= GETDATE() AND           ");
            query.Append(" ENDDA >= GETDATE()               ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetUserOtorityByRecid(string RECID)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                                   ");
            query.Append(" BEGDA, ENDDA, RECID, PERNR, ROLID,       ");
            query.Append(" CHGDT, USRDT                             ");
            query.Append(" FROM gcg.WBS                             ");
            query.Append(" WHERE                                    ");
            query.Append(" BEGDA <= GETDATE() AND                   ");
            query.Append(" ENDDA >= GETDATE() AND                   ");
            query.Append(" RECID = '" + RECID + "'                  ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                var data = (object[])null;
                while (reader.Read())
                {
                    data = new object[] { reader[0], reader[1], reader[2], reader[3], reader[4],
                                          reader[5], reader[6] };
                }
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetUserOtority()
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                                          ");
            query.Append(" BEGDA, ENDDA, RECID, PERNR, ROLID,              ");
            query.Append(" CHGDT, USRDT                                    ");
            query.Append(" FROM gcg.USER_OTORITY                           ");
            query.Append(" WHERE                                           ");
            query.Append(" BEGDA <= GETDATE() AND                          ");
            query.Append(" ENDDA >= GETDATE()                              ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "User Otority";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetAdmin()
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT                                                   ");
            query.Append(" UO.BEGDA, UO.ENDDA, UO.RECID, UO.PERNR, UO.ROLID,        ");
            query.Append(" UO.CHGDT, UO.USRDT, UD.CNAME                             ");
            query.Append(" FROM                                                     ");
            query.Append(" gcg.USER_OTORITY UO,                                     ");
            query.Append(" bioumum.USER_DATA_NEW UD                                 ");
            query.Append(" WHERE                                                    ");
            query.Append(" UO.PERNR = UD.PERNR AND                                  ");
            query.Append(" UO.BEGDA <= GETDATE() AND                                ");
            query.Append(" UD.ENDDA >= GETDATE()                                    ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Admin";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object[] GetAdminByID(string RECID)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("SELECT                                               ");
            query.Append("UO.BEGDA, UO.ENDDA, UO.RECID, UO.PERNR, UO.ROLID,    ");
            query.Append("UO.CHGDT, UO.USRDT, UD.CNAME                         ");
            query.Append("FROM                                                 ");
            query.Append("gcg.USER_OTORITY UO,                                 ");
            query.Append("bioumum.USER_DATA_NEW UD                             ");
            query.Append("WHERE                                                ");
            query.Append("UO.PERNR = UD.PERNR AND                              ");
            query.Append("UO.BEGDA <= GETDATE() AND                            ");
            query.Append("UD.ENDDA >= GETDATE() AND                            ");
            query.Append("RECID = '" + RECID + "'                              ");

            SqlCommand cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                SqlDataReader reader = GetDataReader(cmd);
                object[] data = null;
                while (reader.Read())
                {
                    data = new object[] { reader[0], reader[1], reader[2], reader[3], reader[4] };
                }
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetNewAdmin()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT PERNR,CNAME,PRORG     ");
            query.Append(" FROM bioumum.USER_DATA_NEW   ");
            query.Append(" WHERE CHIEF != 'X'           ");
            query.Append(" ORDER BY PRORG, CNAME        ");

            SqlCommand cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Admin";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion CatalogUserRole

    #region CatalogParameter
    public class CatalogParameter : CatalogManager
    {
        public static DataTable GetParameterByType(string prmty)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                       ");
            query.Append(" RECID, PRMTY, PRMKD, PRMNM   ");
            query.Append(" FROM                         ");
            query.Append(" general.PARAMETER            ");
            query.Append(" WHERE                        ");
            query.Append(" BEGDA <= GETDATE() AND       ");
            query.Append(" ENDDA >= GETDATE() AND       ");
            query.Append(" PRMTY = '" + prmty + "'      ");
            query.Append(" ORDER BY PRMNM               ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "parameter";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertParameter(string parameterType, string parameterCode, string parameterName, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_InsertParameter");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vparameterType", ParameterDirection.Input, parameterType);
            AddParameter(ref cmd, "@vparameterCode", ParameterDirection.Input, parameterCode);
            AddParameter(ref cmd, "@vparameterName", ParameterDirection.Input, parameterName);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DelimitParameter(string recordID, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_DelimitParameter");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRecordID", ParameterDirection.Input, recordID);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

    }
    #endregion CatalogParameter

    #region TourData
    public class TourData : CatalogManager
    {
        public static void InsertRegistrationHeader(string regID, string regCode, string scheduleID, string personelNumber, string email, string phoneNumber, string notes, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.InsertRegistration");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pRegistrationID", ParameterDirection.Input, regID);
            AddParameter(ref cmd, "@pRegistrationCode", ParameterDirection.Input, regCode);
            AddParameter(ref cmd, "@pScheduleID", ParameterDirection.Input, scheduleID);
            AddParameter(ref cmd, "@pPersonelNumber", ParameterDirection.Input, personelNumber);
            AddParameter(ref cmd, "@pEmail", ParameterDirection.Input, personelNumber);
            AddParameter(ref cmd, "@pPhoneNumber", ParameterDirection.Input, personelNumber);
            AddParameter(ref cmd, "@pNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@pModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertRegistrationLine(string regID, string name, string age, string type, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.InsertRegistrationLine");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pRegistrationID", ParameterDirection.Input, regID);
            AddParameter(ref cmd, "@pParticipantName", ParameterDirection.Input, name);
            AddParameter(ref cmd, "@pAge", ParameterDirection.Input, age);
            AddParameter(ref cmd, "@pRegistrationType", ParameterDirection.Input, type);
            AddParameter(ref cmd, "@pModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetMaxRegistrationID()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.GetMaxRegistrationID");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable IsCodeRegistered(string regCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.IsCodeRegistered");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pRegistrationCode", ParameterDirection.Input, regCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable IsEmployeeRegistered(string username)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.IsEmployeeRegistered");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pUsername", ParameterDirection.Input, username);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetRegistrationList(string scheduleID)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("tour.GetRegistrationList");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pScheduleID", ParameterDirection.Input, scheduleID);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetEmployeeFamily(string username, string personelNumber, string relation)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("common.GetEmployee");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pPersonelNumber", ParameterDirection.Input, personelNumber);
            AddParameter(ref cmd, "@pUsername", ParameterDirection.Input, username);
            AddParameter(ref cmd, "@pRelation", ParameterDirection.Input, relation);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetTourSchedule(string scheduleID)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("common.GetTourSchedule");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pScheduleID", ParameterDirection.Input, scheduleID);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Employee";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion TourData

    #region FinanceData
    public class FinanceData : CatalogManager
    {
        public static void InsertTransactionHeader(string transactionID, string transactionDate, string transactionType, string journal, string categoryType, string categoryName, string notes, string approvalStatus, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("finance.usp_InsertTransactionHeader");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vtransactionID", ParameterDirection.Input, transactionID);
            AddParameter(ref cmd, "@vtransactionDate", ParameterDirection.Input, transactionDate);
            AddParameter(ref cmd, "@vtransactionType", ParameterDirection.Input, transactionType);
            AddParameter(ref cmd, "@vjournal", ParameterDirection.Input, journal);
            AddParameter(ref cmd, "@vcategoryType", ParameterDirection.Input, categoryType);
            AddParameter(ref cmd, "@vcategoryName", ParameterDirection.Input, categoryName);
            AddParameter(ref cmd, "@vnotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vapprovalStatus", ParameterDirection.Input, approvalStatus);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertTransactionLine(string transactionID, string transactionDate, string transactionType, string description, string value, string notes, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("finance.usp_InsertTransactionLine");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vtransactionID", ParameterDirection.Input, transactionID);
            AddParameter(ref cmd, "@vtransactionDate", ParameterDirection.Input, transactionDate);
            AddParameter(ref cmd, "@vtransactionType", ParameterDirection.Input, transactionType);
            AddParameter(ref cmd, "@vdescription", ParameterDirection.Input, description);
            AddParameter(ref cmd, "@vvalue", ParameterDirection.Input, value);
            AddParameter(ref cmd, "@vnotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetSaldo(string lastDate)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("finance.usp_GetSaldo");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vDate", ParameterDirection.Input, lastDate);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Finance";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetTransactionDetail()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("finance.usp_GetTransactionDetail");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Finance";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetTransactionID()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("finance.usp_GetTransactionID");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "NEW_ID";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion FinanceData

    #region DoorprizeData
    public class DoorprizeData : CatalogManager
    {
        public static void InsertConfigAccess(string configType, string username, string passkey, string requirement, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_InsertConfigAccess");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vConfigType", ParameterDirection.Input, configType);
            AddParameter(ref cmd, "@vUsername", ParameterDirection.Input, username);
            AddParameter(ref cmd, "@vPasskey", ParameterDirection.Input, passkey);
            AddParameter(ref cmd, "@vRequirement", ParameterDirection.Input, requirement);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateConfigAccess(string username, string passkey, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_UpdateConfigAccess");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vUsername", ParameterDirection.Input, username);
            AddParameter(ref cmd, "@vPasskey", ParameterDirection.Input, passkey);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetConfigAccess(string username, string passkey)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("general.usp_GetConfigAccess");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vUsername", ParameterDirection.Input, username);
            AddParameter(ref cmd, "@vPasskey", ParameterDirection.Input, passkey);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "ConfigAccess";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion DoorprizeData

    #region VendorGatheringData
    public class VendorGatheringData : CatalogManager
    {
        public static bool IsExistPeserta(string kodeRegistrasi)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();
            string errorMessage = string.Empty;

            query.Append("vg.usp_IsPresensiPeserta ");
            
            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                bool data = false;
                var reader = GetDataReader(cmd);

                reader.Read();
                {
                    data = Convert.ToInt16(reader[0]) == 0 ? false : true;
                }

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaAll()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetPesertaAll");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPresensiPeserta(string kodeRegistrasi)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetPresensiPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetTotalPresensiPeserta()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetTotalPresensiPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetTotalStatusHadir()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetTotalStatusHadir");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPeserta(string registrationCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPerusahaan(string companyCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_GetPerusahaan");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, companyCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsPerusahaanTelahDaftar(string kodePerusahaan)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.[usp_IsPerusahaanTelahDaftar]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, kodePerusahaan);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Perusahaan";
                return (Convert.ToInt32(data.Rows[0]["TOTAL"].ToString()) > 0 ? true : false);
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsPesertaTelahDaftar(string email)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.[usp_IsPesertaTelahDaftar]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return (Convert.ToInt32(data.Rows[0]["TOTAL"].ToString()) > 0 ? true : false);
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsCompanyExist(string kodePerusahaan)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_IsPerusahaanExist");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, kodePerusahaan);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return (Convert.ToInt32(data.Rows[0]["TOTAL"].ToString())  > 0 ? true : false);
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsDownloadedSKT(string kodeRegistrasi)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_IsDownloadedSKT");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return (Convert.ToInt32(data.Rows[0]["COUNT"].ToString()) > 0 ? true : false);
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsPesertaHadir(string kodeRegistrasi)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_IsPesertaHadir");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return (Convert.ToInt32(data.Rows[0]["TOTAL"].ToString()) > 0 ? true : false);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertDownloadedSKT(string kodeRegistrasi, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_InsertDownloadSKT");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePerusahaan(string kodePerusahaan, string alamat, string kodeKota, string namaKota, string kodePos, string email, string phone, string fax, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_UpdatePerusahaan");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, kodePerusahaan);
            AddParameter(ref cmd, "@pAlamat", ParameterDirection.Input, alamat);
            AddParameter(ref cmd, "@pKodeKota", ParameterDirection.Input, kodeKota);
            AddParameter(ref cmd, "@pNamaKota", ParameterDirection.Input, namaKota);
            AddParameter(ref cmd, "@pKodePos", ParameterDirection.Input, kodePos);
            AddParameter(ref cmd, "@pEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@pPhone", ParameterDirection.Input, phone);
            AddParameter(ref cmd, "@pFax", ParameterDirection.Input, fax);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void GenerateDataPresensi()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_ResetPresensi");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertPeserta(string kodeRegistrasi, string kodePerusahaan, string namaPeserta, string jenisKelamin, string namaJabatan,
            string email, string phone, string jumlahPeserta, string namaPeserta2, string namaJabatan2,
            string email2, string phone2, string notes, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_InsertPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, kodePerusahaan);
            AddParameter(ref cmd, "@pNamaPeserta", ParameterDirection.Input, namaPeserta);
            AddParameter(ref cmd, "@pJenisKelamin", ParameterDirection.Input, jenisKelamin);
            AddParameter(ref cmd, "@pNamaJabatan", ParameterDirection.Input, namaJabatan);
            AddParameter(ref cmd, "@pEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@pPhone", ParameterDirection.Input, phone);
            AddParameter(ref cmd, "@pJumlahPeserta", ParameterDirection.Input, jumlahPeserta);
            AddParameter(ref cmd, "@pNamaPeserta2", ParameterDirection.Input, namaPeserta2);
            AddParameter(ref cmd, "@pNamaJabatan2", ParameterDirection.Input, namaJabatan2);
            AddParameter(ref cmd, "@pEmail2", ParameterDirection.Input, email2);
            AddParameter(ref cmd, "@pPhone2", ParameterDirection.Input, phone2);
            AddParameter(ref cmd, "@pNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePeserta(string kodeRegistrasi, string kodePerusahaan, string namaPeserta, string jenisKelamin,
            string namaJabatan, string email, string phone, string jumlahPesertaHadir, string namaPeserta2, string namaJabatan2, 
            string email2, string phone2, string notes, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_UpdatePeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@pKodePerusahaan", ParameterDirection.Input, kodePerusahaan);
            AddParameter(ref cmd, "@pNamaPeserta", ParameterDirection.Input, namaPeserta);
            AddParameter(ref cmd, "@pJenisKelamin", ParameterDirection.Input, jenisKelamin);
            AddParameter(ref cmd, "@pNamaJabatan", ParameterDirection.Input, namaJabatan);
            AddParameter(ref cmd, "@pEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@pPhone", ParameterDirection.Input, phone);
            AddParameter(ref cmd, "@pJumlahHadir", ParameterDirection.Input, jumlahPesertaHadir);

            AddParameter(ref cmd, "@pNamaPeserta2", ParameterDirection.Input, namaPeserta2);
            AddParameter(ref cmd, "@pNamaJabatan2", ParameterDirection.Input, namaJabatan2);
            AddParameter(ref cmd, "@pEmail2", ParameterDirection.Input, email2);
            AddParameter(ref cmd, "@pPhone2", ParameterDirection.Input, phone2);

            AddParameter(ref cmd, "@pNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePesertaJumlahHadir(string kodeRegistrasi, string jumlahPesertaHadir, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_UpdatePeserta_JumlahHadir");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@pKodeRegistrasi", ParameterDirection.Input, kodeRegistrasi);
            AddParameter(ref cmd, "@pJumlahHadir", ParameterDirection.Input, jumlahPesertaHadir);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable IsRegistrationCodeExist(string registrationCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("vg.usp_IsRegistrationCodeExist");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Verificator";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion VendorGatheringData

    #region CatalogFRVN
    public class CatalogFRVN : CatalogManager
    {

        public static int GetTotalParticipantByModifier(string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetTotalParticipantByModifier");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return Convert.ToInt32(data.Rows[0]["TOTAL"].ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDoorprizeWinnerByCode(string registrationCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetDoorprizeWinnerByCode");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Doorprize";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDoorprizeParticipant(string status)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetListDoorprize");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vStatus", ParameterDirection.Input, status);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Doorprize";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDoorprizeWinner(string totalWinner)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetDoorprizeWinner");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vTotal", ParameterDirection.Input, totalWinner);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Doorprize";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetGiftList()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetGiftList");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Doorprize";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaWithAliasCode(string registrationCode, string aliasCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_GetPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vAliasCode", ParameterDirection.Input, aliasCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Peserta";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable IsRegistrationCodeExist(string registrationCode)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_IsRegistrationCodeExist");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Verificator";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetEmployeeFamily(string personelNumber)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("bf.usp_GetEmployeeFamily");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, personelNumber);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Finance";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateGiftWinner(string registrationCode, string giftCode, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UpdateGiftWinner");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vGiftCode", ParameterDirection.Input, giftCode);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePesertaHadir(string aliasCode, string registrationCode, string notes)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UpdatePesertaHadir");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vAliasCode", ParameterDirection.Input, aliasCode);
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateDoorprizeWinner(string regCode, string notes)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UpdateDoorprizeWinner");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, regCode);
            AddParameter(ref cmd, "@vNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UndoDoorprizeWinner(string regCode, string notes)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UndoDoorprizeWinner");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, regCode);
            AddParameter(ref cmd, "@vNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateFamilyData(string aliasCode, string istri, string totalAnak, string anakB12)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("bf.usp_UpdateFamilyData");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, aliasCode);
            AddParameter(ref cmd, "@vMarriageStats", ParameterDirection.Input, istri);
            AddParameter(ref cmd, "@vChildTotal", ParameterDirection.Input, totalAnak);
            AddParameter(ref cmd, "@vChildB12", ParameterDirection.Input, anakB12);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateEmailPeserta(string registrationCode, string email)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UpdateEmailPeserta");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@vRegCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePesertaHadirWithEmail(string aliasCode, string registrationCode, string email, string notes)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("frvn.usp_UpdatePesertaHadirWithEmail");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vAliasCode", ParameterDirection.Input, aliasCode);
            AddParameter(ref cmd, "@vRegistrationCode", ParameterDirection.Input, registrationCode);
            AddParameter(ref cmd, "@vEmail", ParameterDirection.Input, email);
            AddParameter(ref cmd, "@vNotes", ParameterDirection.Input, notes);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DelimitPesertaFRVNByAliasCode(string acode, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PESERTA                  ");
            query.Append(" SET ENDDA = @ENDDA,                  ");
            query.Append(" CHGDT = GETDATE(), USRDT = @USRDT    ");
            query.Append(" WHERE BEGDA <= GETDATE() AND         ");
            query.Append(" ENDDA >= GETDATE() AND               ");
            query.Append(" EMAIL = @EMAIL AND ACODE = @ACODE    ");

            var cmd = GetCommand(conn, query.ToString());

            cmd.Parameters.Add(GetParameter("ENDDA", DelimitByMinute()));
            cmd.Parameters.Add(GetParameter("USRDT", usrdt));
            cmd.Parameters.Add(GetParameter("ACODE", acode));


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void InsertPesertaFRVNAlias(string REGCD, string ACODE, string PERNM, string JNSKL, string JENIS, string INSNM, string INADD, string TELNO, string FAXNO, string KOTNM, string NEGNM, string PHONE, string JOBGR, string JOBCD, string JOBNM, string EMAIL, string PARTY, string NPWP, string WORKC, string WORKG, string ARRCD, string ARRVL, string STYCD, string STAY, string DELFR, string NOTES, string STATS, string USRDT)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO FRVN.PESERTA                         ");
            query.Append(" (BEGDA, ENDDA, REGCD, ACODE, PERNM, JNSKL,              ");
            query.Append("  JENIS, JOBNM, INSNM, INADD, TELNO,              ");
            query.Append("  FAXNO, KOTNM, NEGNM, PHONE, EMAIL,              ");
            query.Append("  PARTY, NPWP, WORKG, DELFR, CHGDT,               ");
            query.Append("  ARRCD, ARRVL, STYCD, STAY, NOTES, STATS,        ");
            query.Append("  USRDT, JOBGR, JOBCD, WORKC)                     ");
            query.Append(" VALUES                                           ");
            query.Append(" (GETDATE(), @ENDDA, @REGCD, @ACODE, @PERNM, @JNSKL,      ");
            query.Append("  @JENIS, @JOBNM, @INSNM, @INADD, @TELNO,         ");
            query.Append("  @FAXNO, @KOTNM,@NEGNM, @PHONE, @EMAIL,          ");
            query.Append("  @PARTY, @NPWP, @WORKG, @DELFR, GETDATE(),       ");
            query.Append("  @ARRCD, @ARRVL, @STYCD, @STAY, @NOTES, @STATS,  ");
            query.Append("  @USRDT, @JOBGR, @JOBCD, @WORKC)                 ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("ENDDA", GetMaxDate()));
            cmd.Parameters.Add(GetParameter("REGCD", REGCD));
            cmd.Parameters.Add(GetParameter("ACODE", ACODE));
            cmd.Parameters.Add(GetParameter("PERNM", PERNM));
            cmd.Parameters.Add(GetParameter("JNSKL", JNSKL));
            cmd.Parameters.Add(GetParameter("JENIS", JENIS));
            cmd.Parameters.Add(GetParameter("INSNM", INSNM));
            cmd.Parameters.Add(GetParameter("INADD", INADD));
            cmd.Parameters.Add(GetParameter("TELNO", TELNO));
            cmd.Parameters.Add(GetParameter("FAXNO", FAXNO));
            cmd.Parameters.Add(GetParameter("KOTNM", KOTNM));
            cmd.Parameters.Add(GetParameter("NEGNM", NEGNM));
            cmd.Parameters.Add(GetParameter("PHONE", PHONE));
            cmd.Parameters.Add(GetParameter("JOBGR", JOBGR));
            cmd.Parameters.Add(GetParameter("JOBCD", JOBCD));
            cmd.Parameters.Add(GetParameter("JOBNM", JOBNM));
            cmd.Parameters.Add(GetParameter("EMAIL", EMAIL));
            cmd.Parameters.Add(GetParameter("PARTY", PARTY));
            cmd.Parameters.Add(GetParameter("NPWP", NPWP));
            cmd.Parameters.Add(GetParameter("WORKC", WORKC));
            cmd.Parameters.Add(GetParameter("WORKG", WORKG));
            cmd.Parameters.Add(GetParameter("ARRCD", ARRCD));
            cmd.Parameters.Add(GetParameter("ARRVL", ARRVL));
            cmd.Parameters.Add(GetParameter("STYCD", STYCD));
            cmd.Parameters.Add(GetParameter("STAY", STAY));
            cmd.Parameters.Add(GetParameter("DELFR", DELFR));
            cmd.Parameters.Add(GetParameter("NOTES", NOTES));
            cmd.Parameters.Add(GetParameter("STATS", STATS));
            cmd.Parameters.Add(GetParameter("USRDT", USRDT));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertPesertaFRVN(string REGCD, string PERNM, string JNSKL, string JENIS, string INSNM, string INADD, string TELNO, string FAXNO, string KOTNM, string NEGNM, string PHONE, string JOBGR, string JOBCD, string JOBNM, string EMAIL, string PARTY, string NPWP, string WORKC, string WORKG, string ARRCD, string ARRVL, string STYCD, string STAY, string DELFR, string NOTES, string STATS, string USRDT)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO FRVN.PESERTA                         ");
            query.Append(" (BEGDA, ENDDA, REGCD, PERNM, JNSKL,              ");
            query.Append("  JENIS, JOBNM, INSNM, INADD, TELNO,              ");
            query.Append("  FAXNO, KOTNM, NEGNM, PHONE, EMAIL,              ");
            query.Append("  PARTY, NPWP, WORKG, DELFR, CHGDT,               ");
            query.Append("  ARRCD, ARRVL, STYCD, STAY, NOTES, STATS,        ");
            query.Append("  USRDT, JOBGR, JOBCD, WORKC)                     ");
            query.Append(" VALUES                                           ");
            query.Append(" (GETDATE(), @ENDDA, @REGCD, @PERNM, @JNSKL,      ");
            query.Append("  @JENIS, @JOBNM, @INSNM, @INADD, @TELNO,         ");
            query.Append("  @FAXNO, @KOTNM,@NEGNM, @PHONE, @EMAIL,          ");
            query.Append("  @PARTY, @NPWP, @WORKG, @DELFR, GETDATE(),       ");
            query.Append("  @ARRCD, @ARRVL, @STYCD, @STAY, @NOTES, @STATS,  ");
            query.Append("  @USRDT, @JOBGR, @JOBCD, @WORKC)                 ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("ENDDA", GetMaxDate()));
            cmd.Parameters.Add(GetParameter("REGCD", REGCD));
            cmd.Parameters.Add(GetParameter("PERNM", PERNM));
            cmd.Parameters.Add(GetParameter("JNSKL", JNSKL));
            cmd.Parameters.Add(GetParameter("JENIS", JENIS));
            cmd.Parameters.Add(GetParameter("INSNM", INSNM));
            cmd.Parameters.Add(GetParameter("INADD", INADD));
            cmd.Parameters.Add(GetParameter("TELNO", TELNO));
            cmd.Parameters.Add(GetParameter("FAXNO", FAXNO));
            cmd.Parameters.Add(GetParameter("KOTNM", KOTNM));
            cmd.Parameters.Add(GetParameter("NEGNM", NEGNM));
            cmd.Parameters.Add(GetParameter("PHONE", PHONE));
            cmd.Parameters.Add(GetParameter("JOBGR", JOBGR));
            cmd.Parameters.Add(GetParameter("JOBCD", JOBCD));
            cmd.Parameters.Add(GetParameter("JOBNM", JOBNM));
            cmd.Parameters.Add(GetParameter("EMAIL", EMAIL));
            cmd.Parameters.Add(GetParameter("PARTY", PARTY));
            cmd.Parameters.Add(GetParameter("NPWP", NPWP));
            cmd.Parameters.Add(GetParameter("WORKC", WORKC));
            cmd.Parameters.Add(GetParameter("WORKG", WORKG));
            cmd.Parameters.Add(GetParameter("ARRCD", ARRCD));
            cmd.Parameters.Add(GetParameter("ARRVL", ARRVL));
            cmd.Parameters.Add(GetParameter("STYCD", STYCD));
            cmd.Parameters.Add(GetParameter("STAY", STAY));
            cmd.Parameters.Add(GetParameter("DELFR", DELFR));
            cmd.Parameters.Add(GetParameter("NOTES", NOTES));
            cmd.Parameters.Add(GetParameter("STATS", STATS));
            cmd.Parameters.Add(GetParameter("USRDT", USRDT));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePesertaFRVNByEmailRegistrationCode(string REGCD, string PERNM, string JNSKL, string JENIS, string INSNM, string INADD, string TELNO, string FAXNO, string KOTNM, string NEGNM, string PHONE, string JOBGR, string JOBCD, string JOBNM, string EMAILold, string EMAILnew, string PARTY, string NPWP, string WORKC, string WORKG, string ARRCD, string ARRVL, string STYCD, string STAY, string DELFR, string NOTES, string STATS, string USRDT)
        {
            DelimitPesertaFRVNByEmailRegistrationCode(EMAILold, REGCD, USRDT);
            InsertPesertaFRVN(REGCD, PERNM, JNSKL, JENIS, INSNM, INADD, TELNO, FAXNO, KOTNM, NEGNM, PHONE, JOBGR, JOBCD, JOBNM, EMAILnew, PARTY, NPWP, WORKC, WORKG, ARRCD, ARRVL, STYCD, STAY, DELFR, NOTES, STATS, USRDT);
        }

        public static void UpdateStatusPesertaFRVNByRegistrationCode(string registationCode, string status, string modifier)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PESERTA          ");
            query.Append(" SET STATS = @STATS,          ");
            query.Append(" USRDT = @USRDT               ");
            query.Append(" WHERE BEGDA <= GETDATE() AND ");
            query.Append(" ENDDA >= GETDATE() AND       ");
            query.Append("  REGCD = @REGCD              ");
            var cmd = GetCommand(conn, query.ToString());

            cmd.Parameters.Add(GetParameter("STATS", status));
            cmd.Parameters.Add(GetParameter("USRDT", modifier));
            cmd.Parameters.Add(GetParameter("REGCD", registationCode));


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DelimitPesertaFRVNByEmailRegistrationCode(string email, string regcd, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PESERTA                  ");
            query.Append(" SET ENDDA = @ENDDA,                  ");
            query.Append(" CHGDT = GETDATE(), USRDT = @USRDT    ");
            query.Append(" WHERE BEGDA <= GETDATE() AND         ");
            query.Append(" ENDDA >= GETDATE() AND               ");
            query.Append(" EMAIL = @EMAIL AND REGCD = @REGCD    ");

            var cmd = GetCommand(conn, query.ToString());

            cmd.Parameters.Add(GetParameter("ENDDA", DelimitByMinute()));
            cmd.Parameters.Add(GetParameter("USRDT", usrdt));
            cmd.Parameters.Add(GetParameter("REGCD", regcd));
            cmd.Parameters.Add(GetParameter("EMAIL", email));


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void GenerateDataPresensiWgCs()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" TRUNCATE TABLE               ");
            query.Append(" [frvn].[PRESENCE]            ");

            query.Append(" INSERT INTO                  ");
            query.Append(" [frvn].[PRESENCE]            ");
            query.Append(" ([BEGDA]                     ");
            query.Append(" ,[CHKIN]                     ");
            query.Append(" ,[OBJID]                     ");
            query.Append(" ,[REGCD]                     ");
            query.Append(" ,[CHECK]                     ");
            query.Append(" ,[ATTEN]                     ");
            query.Append(" ,[ABRES]                     ");
            query.Append(" ,[SERTI]                     ");
            query.Append(" ,[SERDT]                     ");
            query.Append(" ,[CHGDT]                     ");
            query.Append(" ,[USRDT])                    ");
            query.Append(" SELECT GETDATE(),            ");
            query.Append(" NULL, NULL,                  ");
            query.Append(" REGCD, 0, 0,                 ");
            query.Append(" NULL, NULL,                  ");
            query.Append(" NULL, GETDATE(),             ");
            query.Append(" 'system'                     ");
            query.Append(" FROM frvn.PESERTA            ");
            query.Append(" WHERE [STATS] = 1            ");
            query.Append(" AND (PARTY = 1 OR PARTY = 2) ");
            query.Append(" AND BEGDA <= GETDATE()       ");
            query.Append(" AND ENDDA >= GETDATE()       ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DelimitPesertaFRVNByRecid(string recid, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PESERTA               ");
            query.Append(" SET ENDDA = @ENDDA,               ");
            query.Append(" CHGDT = GETDATE(), USRDT = @USRDT ");
            query.Append(" WHERE RECID = @RECID              ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("ENDDA", DelimitByMinute()));
            cmd.Parameters.Add(GetParameter("USRDT", usrdt));
            cmd.Parameters.Add(GetParameter("RECID", recid));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaFRVNByEmailRegistrationCode(string email, string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                   ");
            query.Append(" REGCD, PERNM,            ");
            query.Append(" JNSKL, JENIS, JOBGR,     ");
            query.Append(" JOBCD, JOBNM, INSNM,     ");
            query.Append(" INADD, TELNO, FAXNO,     ");
            query.Append(" KOTNM, NEGNM, PHONE,     ");
            query.Append(" EMAIL, PARTY, NPWP,      ");
            query.Append(" WORKC, WORKG, ARRCD,     ");
            query.Append(" ARRVL, STYCD, STAY,      ");
            query.Append(" DELFR                    ");
            query.Append(" FROM frvn.PESERTA        ");
            query.Append(" WHERE BEGDA <= GETDATE() ");
            query.Append(" AND ENDDA >= GETDATE()   ");
            query.Append(" AND EMAIL = @EMAIL       ");
            query.Append(" AND REGCD = @REGCD       ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("EMAIL", email));
            cmd.Parameters.Add(GetParameter("REGCD", regcd));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaFRVNByRecid(string recid)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                   ");
            query.Append(" REGCD, PERNM,            ");
            query.Append(" JNSKL, JENIS, JOBGR,     ");
            query.Append(" JOBCD, JOBNM, INSNM,     ");
            query.Append(" INADD, TELNO, FAXNO,     ");
            query.Append(" KOTNM, NEGNM, PHONE,     ");
            query.Append(" EMAIL, PARTY, NPWP,      ");
            query.Append(" WORKC, WORKG, ARRCD,     ");
            query.Append(" ARRVL, STYCD, STAY,      ");
            query.Append(" DELFR                    ");
            query.Append(" FROM frvn.PESERTA        ");
            query.Append(" WHERE RECID = @RECID     ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("RECID", recid));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaFRVNByRegcd(string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT P1.RECID                 ");
            query.Append(" ,P1.[OBJID]                      ");
            query.Append(" ,P1.[REGCD]                     ");
            query.Append(" ,P1.[PERNM]                     ");
            query.Append(" ,P1.[JNSKL]                     ");
            query.Append(" ,P1.[JENIS]                     ");
            query.Append(" ,P1.[JOBGR]                     ");
            query.Append(" ,P1.[JOBCD]                     ");
            query.Append(" ,P1.[JOBNM]                     ");
            query.Append(" ,P1.[INSNM]                     ");
            query.Append(" ,P1.[INADD]                     ");
            query.Append(" ,P1.[TELNO]                     ");
            query.Append(" ,P1.[FAXNO]                     ");
            query.Append(" ,P1.[KOTNM]                     ");
            query.Append(" ,P1.[NEGNM]                     ");
            query.Append(" ,P1.[PHONE]                     ");
            query.Append(" ,P1.[EMAIL]                     ");
            query.Append(" ,P1.[PARTY]                     ");
            query.Append(" ,P1.[NPWP]                      ");
            query.Append(" ,P1.[WORKC]                     ");
            query.Append(" ,P1.[WORKG]                     ");
            query.Append(" ,P1.[ARRCD]                     ");
            query.Append(" ,P1.[ARRVL]                     ");
            query.Append(" ,P1.[STYCD]                     ");
            query.Append(" ,P1.[STAY]                      ");
            query.Append(" ,P1.[DELFR]                     ");
            query.Append(" ,P1.NOTES                     ");
            query.Append(" ,P1.STATS                     ");
            query.Append(" ,P2.PRMNM JOBTY                 ");
            query.Append(" FROM [frvn].[PESERTA] P1         ");
            query.Append(" JOIN general.PARAMETER P2  ");
            query.Append(" ON P1.JOBCD = P2.PRMKD          ");
            query.Append(" AND P1.REGCD = @REGCD           ");
            query.Append(" AND P1.BEGDA <= GETDATE()       ");
            query.Append(" AND P1.ENDDA >= GETDATE()       ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", regcd));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsValidPesertaFRVNByEmailRegistrationCode(string email, string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT COUNT(*) FROM FRVN.PESERTA                    ");
            query.Append(" WHERE BEGDA <= GETDATE() AND ENDDA >= GETDATE() AND  ");
            query.Append(" EMAIL = @EMAIL AND REGCD = @REGCD                    ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("EMAIL", email));
            cmd.Parameters.Add(GetParameter("REGCD", regcd));

            try
            {
                conn.Open();
                bool data = false;
                var reader = GetDataReader(cmd);

                reader.Read();
                {
                    data = Convert.ToInt16(reader[0]) == 0 ? false : true;
                }

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsExistPesertaFRVNByEmail(string email, string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            if (regcd == null)
            {
                query.Append(" SELECT COUNT(*) FROM FRVN.PESERTA                    ");
                query.Append(" WHERE BEGDA <= GETDATE() AND ENDDA >= GETDATE() AND  ");
                query.Append(" EMAIL = @EMAIL                                       ");
            }
            else
            {
                query.Append(" SELECT COUNT(*) FROM FRVN.PESERTA                    ");
                query.Append(" WHERE BEGDA <= GETDATE() AND ENDDA >= GETDATE() AND  ");
                query.Append(" EMAIL = @EMAIL AND REGCD != @REGCD                   ");
            }

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("EMAIL", email));

            if (regcd != null)
            {
                cmd.Parameters.Add(GetParameter("REGCD", regcd));
            }

            try
            {
                conn.Open();
                bool data = false;
                var reader = GetDataReader(cmd);

                reader.Read();
                {
                    data = Convert.ToInt16(reader[0]) == 0 ? false : true;
                }

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetRegisteredByEmail(string email)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                   ");
            query.Append(" EMAIL, CNAME             ");
            query.Append(" FROM                     ");
            query.Append(" frvn.REGISTERED          ");
            query.Append(" WHERE                    ");
            query.Append(" BEGDA <= GETDATE()       ");
            query.Append(" AND ENDDA >= GETDATE()   ");
            query.Append(" AND EMAIL = @EMAIL       ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("EMAIL", email));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaFamgath()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                                    ");
            query.Append(" PE.RECID, PE.REGCD, PE.PERNM,             ");
            query.Append(" PE.JNSKL, PE.JENIS, PE.JOBGR,             ");
            query.Append(" PE.JOBCD, PE.REGDT, PE.ACODE,             ");
            query.Append(" PE.JOBNM, PE.INSNM,                       ");
            query.Append(" PE.INADD, PE.TELNO, PE.FAXNO,             ");
            query.Append(" PE.KOTNM, PE.NEGNM, PE.PHONE,             ");
            query.Append(" PE.EMAIL, PE.PARTY, PE.NPWP,              ");
            query.Append(" PE.WORKC, PE.WORKG, PE.ARRCD,             ");
            query.Append(" PE.ARRVL, PE.STYCD, PE.STAY,              ");
            query.Append(" PE.DELFR, PE.BEGDA, PE.STATS, PE.NOTES    ");
            query.Append(" FROM frvn.PESERTA PE                      ");
            query.Append(" WHERE                                     ");
            query.Append(" PE.BEGDA <= GETDATE()                     ");
            query.Append(" AND PE.ENDDA >= GETDATE()                 ");
            query.Append(" ORDER BY PERNM                            ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPeserta()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                           ");
            query.Append(" PE.RECID, PE.REGCD, PE.PERNM,    ");
            query.Append(" PE.JNSKL, PE.JENIS, PE.JOBGR,    ");
            query.Append(" PE.JOBCD, PA.PRMNM,              ");
            query.Append(" PE.JOBNM, PE.INSNM,              ");
            query.Append(" PE.INADD, PE.TELNO, PE.FAXNO,    ");
            query.Append(" PE.KOTNM, PE.NEGNM, PE.PHONE,    ");
            query.Append(" PE.EMAIL, PE.PARTY, PE.NPWP,     ");
            query.Append(" PE.WORKC, PE.WORKG, PE.ARRCD,    ");
            query.Append(" PE.ARRVL, PE.STYCD, PE.STAY,     ");
            query.Append(" PE.DELFR, PE.BEGDA, PE.STATS, PE.NOTES   ");
            query.Append(" FROM frvn.PESERTA PE JOIN        ");
            query.Append(" general.PARAMETER PA ON          ");
            query.Append(" PE.JOBCD = PA.PRMKD              ");
            query.Append(" WHERE                            ");
            query.Append(" PE.BEGDA <= GETDATE()            ");
            query.Append(" AND PE.ENDDA >= GETDATE()        ");
            query.Append(" AND PA.BEGDA <= GETDATE()        ");
            query.Append(" AND PA.ENDDA >= GETDATE()        ");
            query.Append(" ORDER BY PERNM                   ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "parameter";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetVerifiedPeserta()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                           ");
            query.Append(" PE.RECID, PE.REGCD, PE.PERNM,    ");
            query.Append(" PE.JNSKL, PE.JENIS, PE.JOBGR,    ");
            query.Append(" PE.JOBCD, PA.PRMNM,              ");
            query.Append(" PE.JOBNM, PE.INSNM,              ");
            query.Append(" PE.INADD, PE.TELNO, PE.FAXNO,    ");
            query.Append(" PE.KOTNM, PE.NEGNM, PE.PHONE,    ");
            query.Append(" PE.EMAIL, PE.PARTY, PE.NPWP,     ");
            query.Append(" PE.WORKC, PE.WORKG, PE.ARRCD,    ");
            query.Append(" PE.ARRVL, PE.STYCD, PE.STAY,     ");
            query.Append(" PE.DELFR, PE.BEGDA, PE.STATS, PE.NOTES   ");
            query.Append(" FROM frvn.PESERTA PE JOIN        ");
            query.Append(" general.PARAMETER PA ON          ");
            query.Append(" PE.JOBCD = PA.PRMKD              ");
            query.Append(" WHERE PE.STATS = '1'             ");
            query.Append(" AND PE.BEGDA <= GETDATE()        ");
            query.Append(" AND PE.ENDDA >= GETDATE()        ");
            query.Append(" AND PA.BEGDA <= GETDATE()        ");
            query.Append(" AND PA.ENDDA >= GETDATE()        ");
            query.Append(" ORDER BY PERNM                   ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "parameter";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        //NEW VERSION IN LINE
        public static void InsertPresensiFRVN(string OBJID, string REGCD, string CHECK, string ATTEN, string ABRES, string SERTI, string USRDT)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO FRVN.PRESENCE                           ");
            query.Append(" (BEGDA, CHKIN,                                      ");
            query.Append(" OBJID, REGCD, [CHECK],                              ");
            query.Append(" ATTEN, ABRES, SERTI,                                ");
            query.Append(" SERDT, CHGDT, USRDT)                                ");
            query.Append(" VALUES                                              ");
            query.Append(" (GETDATE(), NULL,                                   ");
            query.Append(" '" + OBJID + "', '" + REGCD + "', '" + CHECK + "',  ");
            query.Append(" '" + ATTEN + "', '" + ABRES + "', '" + SERTI + "',  ");
            query.Append(" NULL, GETDATE(), '" + USRDT + "')                   ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertPresensiFRVNOnTheSpot(string REGCD, string USRDT)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO FRVN.PRESENCE           ");
            query.Append(" (BEGDA, CHKIN,                      ");
            query.Append(" OBJID, REGCD, [CHECK],              ");
            query.Append(" ATTEN, ABRES, SERTI,                ");
            query.Append(" SERDT, CHGDT, USRDT)                ");
            query.Append(" VALUES                              ");
            query.Append(" (GETDATE(), GETDATE(),              ");
            query.Append(" NULL, '" + REGCD + "', '1',         ");
            query.Append(" '1', NULL, NULL,                    ");
            query.Append(" NULL , GETDATE(), '" + USRDT + "')  ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePresensiFRVN(string regcd, string check, string atten, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE       ");
            query.Append(" SET CHKIN = GETDATE(),     ");
            query.Append(" [CHECK] = '" + check + "', ");
            query.Append(" ATTEN = '" + atten + "',   ");
            query.Append(" CHGDT = GETDATE(),         ");
            query.Append(" USRDT = '" + usrdt + "'    ");
            query.Append(" WHERE                      ");
            query.Append(" REGCD = '" + regcd + "'    ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdatePresensiCheckOutFRVN(string regcd, string check, string atten, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE         ");
            query.Append(" SET Check2Time = GETDATE(),  ");
            query.Append(" [Check2] = '" + check + "',  ");
            query.Append(" CHGDT = GETDATE(),           ");
            query.Append(" USRDT = '" + usrdt + "'      ");
            query.Append(" WHERE                        ");
            query.Append(" REGCD = '" + regcd + "'      ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UndoPresensiFRVN(string regcd, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE       ");
            query.Append(" SET CHKIN = NULL,          ");
            query.Append(" [CHECK] = 'False',         ");
            query.Append(" ATTEN = 'False',           ");
            query.Append(" CHGDT = GETDATE(),         ");
            query.Append(" USRDT = '" + usrdt + "'    ");
            query.Append(" WHERE                      ");
            query.Append(" REGCD = '" + regcd + "'    ");
            
            //update jumlah peserta menjadi 0
            query.Append(" UPDATE vg.PESERTA                ");
            query.Append(" SET JumlahPesertaHadir = 0,      ");
            query.Append(" ModifiedAt = GETDATE(),          ");
            query.Append(" ModifiedBy = '" + usrdt + "'     ");
            query.Append(" WHERE                            ");
            query.Append(" KodeRegistrasi = '" + regcd + "' ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateCertificateFRVN(string regcd, string serti, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE         ");
            query.Append(" SET SERTI = '" + serti + "', ");
            query.Append(" SERDT = GETDATE(),           ");
            query.Append(" CHGDT = GETDATE(),           ");
            query.Append(" USRDT = '" + usrdt + "'      ");
            query.Append(" WHERE                        ");
            query.Append(" REGCD = '" + regcd + "'      ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IsExistPesertaFRVNByRegCode(string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT COUNT(*)                          ");
            query.Append(" FROM FRVN.PRESENCE R JOIN FRVN.PESERTA P ");
            query.Append(" ON (P.REGCD = R.REGCD AND                ");
            query.Append(" P.BEGDA <= GETDATE()                     ");
            query.Append(" AND P.ENDDA >= GETDATE())                ");
            query.Append(" WHERE P.REGCD = '" + regcd + "'          ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                bool data = false;
                var reader = GetDataReader(cmd);

                reader.Read();
                {
                    data = Convert.ToInt16(reader[0]) == 0 ? false : true;
                }

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPesertaFRVNByRegCode(string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT R.REGCD, P.PERNM, P.INSNM,             ");
            query.Append(" P.PARTY, R.[CHECK], R.CHKIN, R.ATTEN, R.USRDT ");
            query.Append(" FROM FRVN.PRESENCE R JOIN FRVN.PESERTA P      ");
            query.Append(" ON (P.REGCD = R.REGCD AND                     ");
            query.Append(" P.BEGDA <= GETDATE()                          ");
            query.Append(" AND P.ENDDA >= GETDATE())                     ");
            query.Append(" WHERE P.REGCD = @REGCD                        ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", regcd));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetPresensiPesertaFRVN()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                                       ");
            query.Append(" P.REGCD, P.PERNM, P.INSNM, P.EMAIL,          ");
            query.Append(" P.KOTNM, P.PARTY, P.WORKG,                   ");
            query.Append(" P.DELFR, P.JOBNM, P.PHONE,                   ");
            query.Append(" R.[CHECK], R.ATTEN, R.CHKIN, R.USRDT         ");
            query.Append(" FROM FRVN.PRESENCE R JOIN FRVN.PESERTA P     ");
            query.Append(" ON (P.REGCD = R.REGCD AND                    ");
            query.Append(" P.BEGDA <= GETDATE()                         ");
            query.Append(" AND P.ENDDA >= GETDATE())                    ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetAttendedPesertaFRVN()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                                       ");
            query.Append(" P.REGCD, P.PERNM, P.INSNM,                   ");
            query.Append(" P.KOTNM, P.PARTY, P.WORKG,                   ");
            query.Append(" P.DELFR, P.JOBNM, P.PHONE,                   ");
            query.Append(" R.[CHECK], R.ATTEN, R.CHKIN                  ");
            query.Append(" FROM FRVN.PRESENCE R JOIN FRVN.PESERTA P     ");
            query.Append(" ON (P.REGCD = R.REGCD AND                    ");
            query.Append(" P.BEGDA <= GETDATE()                         ");
            query.Append(" AND P.ENDDA >= GETDATE())                    ");
            query.Append(" AND ATTEN != 0                               ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }



        public static DataTable GetAttendedPesertaFRVN(string registrationCode)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                                       ");
            query.Append(" P.REGCD, P.PERNM, P.INSNM,                   ");
            query.Append(" P.KOTNM, P.PARTY, P.WORKG,                   ");
            query.Append(" P.DELFR, P.JOBNM, P.PHONE,                   ");
            query.Append(" R.[CHECK], R.ATTEN, R.CHKIN                  ");
            query.Append(" FROM FRVN.PRESENCE R JOIN FRVN.PESERTA P     ");
            query.Append(" ON (P.REGCD = R.REGCD AND                    ");
            query.Append(" P.BEGDA <= GETDATE()                         ");
            query.Append(" AND P.ENDDA >= GETDATE())                    ");
            query.Append(" AND ATTEN = 1 AND SERTI is null              ");
            query.Append(" AND P.REGCD = @REGCD                         ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", registrationCode));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDownloadedCertificateInfo(string registrationCode)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT                           ");
            query.Append(" [CHKIN]                          ");
            query.Append(" ,[RECID]                         ");
            query.Append(" ,[OBJID]                         ");
            query.Append(" ,[REGCD]                         ");
            query.Append(" ,[CHECK]                         ");
            query.Append(" ,[ATTEN]                         ");
            query.Append(" ,[ABRES]                         ");
            query.Append(" ,[SERTI]                         ");
            query.Append(" ,[SERDT]                         ");
            query.Append(" FROM [frvn].[PRESENCE]    ");
            query.Append(" WHERE REGCD = @REGCD             ");
            query.Append(" AND SERTI = 1                    ");
            query.Append(" AND ATTEN = 1                    ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", registrationCode));

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateCertificateDownload(string registrationCode)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE ");
            query.Append(" SET SERTI = 1,       ");
            query.Append(" SERDT = GETDATE()    ");
            query.Append(" WHERE REGCD = @REGCD ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", registrationCode));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UndoCertificateDownload(string registrationCode)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.PRESENCE ");
            query.Append(" SET SERTI = 0,       ");
            query.Append(" SERDT = null    ");
            query.Append(" WHERE REGCD = @REGCD ");

            var cmd = GetCommand(conn, query.ToString());
            cmd.Parameters.Add(GetParameter("REGCD", registrationCode));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        //Doorprize
        public static void InsertDoorprizePesertaFRVN(string regcd, string pernm, string insnm, string party, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" INSERT INTO FRVN.DOORPRIZE        ");
            query.Append(" (REGCD,                           ");
            query.Append("  PERNM, INSNM,                    ");
            query.Append("  PARTY, [CHECK],                    ");
            query.Append("  CHGDT, USRDT)                    ");
            query.Append(" VALUES                            ");
            query.Append(" ('" + regcd + "',                 ");
            query.Append("  '" + pernm + "', '" + insnm + "',");
            query.Append("  '" + party + "', 'False',        ");
            query.Append("  GETDATE(), '" + usrdt + "')      ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdateDoorprizePesertaFRVN(string regcd, string usrdt)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE FRVN.DOORPRIZE        ");
            query.Append(" SET [CHECK] = 1,               ");
            query.Append(" CHGDT = GETDATE(),           ");
            query.Append(" USRDT = '" + usrdt + "'      ");
            query.Append(" WHERE REGCD = '" + regcd + "'");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteDoorprizePesertaFRVN(string regcd)
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" DELETE FROM                  ");
            query.Append(" FRVN.DOORPRIZE               ");
            query.Append(" WHERE REGCD = '" + regcd + "'");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ResetDoorprizePeserta()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append(" TRUNCATE TABLE               ");
            query.Append(" FRVN.DOORPRIZE               ");

            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDoorprizePesertaFRVN()
        {
            var conn = GetConnection();
            StringBuilder query = new StringBuilder();



            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "peserta";

                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion CatalogFRVN
}
