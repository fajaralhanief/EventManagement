using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FRVN.Data.DataAgent;

namespace FRVN.Data.DataAccess
{
    public class DataCatalog : Database
    {

    }
    public class EventManagementCatalog : DataCatalog
    { 
        public class Master
        {
            public static void InsertEvent(string BeginDate, string EndDate, string RecordID, string EventName, string Theme, string Venue, string LocationCoordinate, string Author, string modifier)
            {
                SqlConnection conn = GetConnection();
                StringBuilder query = new StringBuilder();

                query.Append("[dbo].[usp_InsertEvent]");
                string errorMessage = string.Empty;

                SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
                AddParameter(ref cmd, "@vRecordID", ParameterDirection.Input, RecordID);
                AddParameter(ref cmd, "@vEventName", ParameterDirection.Input, EventName);
                AddParameter(ref cmd, "@vTheme", ParameterDirection.Input, Theme);
                AddParameter(ref cmd, "@vVenue", ParameterDirection.Input, Venue);
                AddParameter(ref cmd, "@vLocationCoordinate", ParameterDirection.Input, LocationCoordinate);
                AddParameter(ref cmd, "@vAuthor", ParameterDirection.Input, Author);
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
        
    }
}