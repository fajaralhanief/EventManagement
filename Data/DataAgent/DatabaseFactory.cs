using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FRVN.Data.DataAgent
{
    public class DatabaseFactory
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        }

        public static SqlDataAdapter GetAdapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }

        public static SqlCommand GetCommand(SqlConnection con, string sqlCommand)
        {
            return new SqlCommand(sqlCommand, (SqlConnection)con);
        }

        public static SqlDataReader GetDataReader(SqlCommand cmd)
        {
            return cmd.ExecuteReader();
        }

        public static SqlParameter GetParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public static SqlCommand GetStoredProcedureCommand(SqlConnection conn, string storedProcedureName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = storedProcedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            return cmd;
        }

        public static void AddParameter(ref SqlCommand command, string parameterName, ParameterDirection direction, object parameterValue)
        {
            SqlParameter parmStatusName = new SqlParameter(parameterName, parameterValue);
            parmStatusName.Direction = direction;
            command.Parameters.Add(parmStatusName);
        }

        public static String GetParameterValue(SqlCommand command, string parameterName)
        {
            return command.Parameters[parameterName].Value.ToString();
        }

        public static void AddParameter(ref SqlCommand command, string parameterName, SqlDbType dbType, int size, ParameterDirection direction)
        {
            SqlParameter parmStatusName = new SqlParameter(parameterName, dbType);
            parmStatusName.Size = size;
            parmStatusName.Direction = direction;
            command.Parameters.Add(parmStatusName);
        }

        public static void AddParameter(ref SqlCommand command, string parameterName, SqlDbType dbType, int size, ParameterDirection direction, object parameterValue)
        {
            SqlParameter parmStatusName = new SqlParameter(parameterName, dbType, size);
            parmStatusName.Value = parameterValue;
            parmStatusName.Direction = direction;
            command.Parameters.Add(parmStatusName);
        }

        public static void AddParameter(ref SqlCommand command, string parameterName, SqlDbType dbType, ParameterDirection direction)
        {
            SqlParameter parmStatusName = new SqlParameter(parameterName, dbType);
            parmStatusName.Direction = direction;
            command.Parameters.Add(parmStatusName);
        }
    }
}
