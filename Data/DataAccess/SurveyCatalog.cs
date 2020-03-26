using FRVN.Data.DataAgent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FRVN.Data.DataAccess
{
    public class SurveyCatalog : DatabaseFactory
    {
        public static int GetUserMaxID()
        {
            var conn = GetConnection();
            var sqlCmd = @"SELECT COALESCE(MAX(USRID),0) FROM biosurvey.SURVEY_USER";
            var cmd = GetCommand(conn, sqlCmd);
            var id = "0";
            try
            {
                conn.Open();
                var reader = GetDataReader(cmd);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        id = reader[0].ToString() + string.Empty;
                    }
                }
                return Convert.ToInt16(id);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertPersonalData(string BEGDA, string ENDDA, string USRID, string CNAME, string GRADE, string ORGID, string ORGNM, string GNDER, string YROLD, string ANNOT, string USRDT)
        {
            DateTime.Now.ToString();
            DateTime.MaxValue.ToString();
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" INSERT INTO biosurvey.SURVEY_USER                            ");
            query.Append(" (BEGDA, ENDDA, USRID, GRADE, ORGID, CNAME, ANNOT,            ");
            query.Append(" ORGNM, GNDER, YROLD, CHGDT, USRDT)                           ");
            query.Append(" VALUES                                                       ");
            query.Append(" ('" + BEGDA + "','" + ENDDA + "','" + USRID + "','" + GRADE + "','" + ORGID + "','" + CNAME + "','" + ANNOT + "',");
            query.Append(" '" + ORGNM + "','" + GNDER + "','" + YROLD + "',GETDATE(),'" + USRDT + "')   ");

            var cmd = DatabaseFactory.GetCommand(conn, query.ToString());

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

        public static void InsertSurvey(string BEGDA, string ENDDA, string USRID, string QSTID, string QSTCD, string QSTCT, string ANSTY, string ANSVL, string ANSAN, string USRDT)
        {
            DateTime.Now.ToString();
            DateTime.MaxValue.ToString();
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" DELETE FROM                                      ");
            query.Append(" [BIOFARMA].[biosurvey].[SURVEY]                  ");
            query.Append(" WHERE                                            ");
            query.Append(" USRID = '" + USRID + "' AND QSTCD = '" + QSTCD + "'");
            query.Append(" AND QSTID = '" + QSTID + "'                      ");

            query.Append(" INSERT INTO biosurvey.SURVEY                                 ");
            query.Append(" (BEGDA, ENDDA, USRID, QSTID, QSTCD,                          ");
            query.Append(" QSTCT, ANSTY, ANSVL, ANSAN, CHGDT,                           ");
            query.Append(" USRDT)                                                       ");
            query.Append(" VALUES                                                       ");
            query.Append(" ('" + BEGDA + "','" + ENDDA + "','" + USRID + "','" + QSTID + "','" + QSTCD + "',");
            query.Append(" '" + QSTCT + "','" + ANSTY + "','" + ANSVL + "','" + ANSAN + "',GETDATE(),   ");
            query.Append(" '" + USRDT + "')                                                 ");

            var cmd = DatabaseFactory.GetCommand(conn, query.ToString());

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

        public static DataTable ShowUraianQuestioner(string quect)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT QUESI, QUECT, QCTNM                      ");
            query.Append(" FROM gcg.COC_QUESTIONER                         ");
            query.Append(" WHERE                                           ");
            query.Append(" QUECT = '" + quect + "'                         ");



            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "COC_QUESTIONER";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertAnswerQuestioner(string BEGDA, string ENDDA, string USRDT,string PERNR, string QUECT, string QUESI)
        {
            DateTime.Now.ToString();
            DateTime.MaxValue.ToString();
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" INSERT INTO gcg.COC_QUESTIONER_ANSWER                                                ");
            query.Append(" (BEGDA, ENDDA, YEAR, PERNR,                                                          ");
            query.Append(" QUECT, QUESI, USRDT, CHGDT)                                                          ");
            query.Append(" VALUES                                                                               ");
            query.Append(" ('" + BEGDA + "', '" + ENDDA + "',YEAR(GETDATE()),'" + PERNR + "',                   ");
            query.Append(" '" + QUECT + "' ,'" + QUESI + "' , '" + USRDT + "', GETDATE())                       ");                                             
                
            var cmd = DatabaseFactory.GetCommand(conn, query.ToString());

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

        public static DataTable ShowQuestionerCategoryName(string quect)
        {
            var conn = GetConnection();
            var query = new StringBuilder();

            query.Append(" SELECT QCTNM                                   ");
            query.Append(" FROM gcg.COC_QUESTIONER                         ");
            query.Append(" WHERE                                           ");
            query.Append(" QUECT = '" + quect + "'                         ");



            var cmd = GetCommand(conn, query.ToString());

            try
            {
                conn.Open();
                var data = new DataTable();
                var adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "COC_QUESTIONER";
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int GetMaxQUECT()
        {
            var conn = GetConnection();
            var query = new StringBuilder();
            query.Append(" select max (QUECT) from gcg.COC_QUESTIONER ");
            int values = 1;

            try
            {
                conn.Open();
                var cmd = GetCommand(conn, query.ToString());
                var reader = GetDataReader(cmd);
                while (reader.Read())
                {
                    values = Convert.ToInt16(reader[0]);
                }
                return values;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int GetMinQuect()
        {
            var conn = GetConnection();
            var query = new StringBuilder();
            query.Append(" select min (QUECT) from gcg.COC_QUESTIONER ");
            int values = 1;

            try
            {
                conn.Open();
                var cmd = GetCommand(conn, query.ToString());
                var reader = GetDataReader(cmd);
                while (reader.Read())
                {
                    values = Convert.ToInt16(reader[0]);
                }
                return values;
            }
            finally
            {
                conn.Close();
            }
        }

    }

}
