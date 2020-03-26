using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRVN.Data.DataAccess;

namespace FRVN.Business.Components
{
    public class EventManagementReference
    {
        public class Master
        {
            #region Event
            public static void InsertEvent(string BeginDate, string EndDate, string RecordID, string EventName, string Theme, string Venue, string LocationCoordinate, string Author, string modifier)
            {
                //string _mitraID = GetMaxIDTable("master.Mitra", "MitraID", false) + 1;
                EventManagementCatalog.Master.InsertEvent(BeginDate, EndDate, RecordID, EventName, Theme, Venue, LocationCoordinate, Author, modifier);
            }
            #endregion
        }
    }
}
