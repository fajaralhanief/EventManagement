
using FRVN.Entity.Dictionary;
using FRVN.Frameworks.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FRVN.Data.DataAccess
{
    public class DataReport
    {
        public static DataSet GetDataTicket(string nama, string instansi, string kode, string party)
        {
            DataSet data = new DataSet();
            data.Tables.Add("ticket");

            data.Tables["ticket"].Columns.Add("PERNM", typeof(string));
            data.Tables["ticket"].Columns.Add("INSNM", typeof(string));
            data.Tables["ticket"].Columns.Add("REGCD", typeof(string));
            data.Tables["ticket"].Columns.Add("PARTY", typeof(string));
            data.Tables["ticket"].Rows.Add(new object[] { nama, instansi, kode, party });
            
            return data;
        }

        private static string GetNameInvited(string name, string delegation)
        {
            if (delegation == string.Empty)
            {
                return name;
            }
            else
            {
                return name + " (" + delegation + ")";
            }
        }

        private static string GetParticipantType(string party)
        {
            if (party == ((int)ParticipantType.Konsorsium).ToString())
            {
                return ParticipantType.Konsorsium.ToString();
            }
            else if (party == ((int)ParticipantType.WorkingGroup).ToString())
            {
                return "Working Group";
            }
            else if (party == ((int)ParticipantType.Undangan).ToString())
            {
                return ParticipantType.Undangan.ToString();
            }
            else
            {
                return ParticipantType.Panitia.ToString();
            }
        }

        public static DataSet GetDataPeserta()
        {
            DataSet data = new DataSet();
            DataTable table = CatalogFRVN.GetVerifiedPeserta(); //CatalogFRVN.GetPeserta();

            data.Tables.Add("peserta");
            data.Tables["peserta"].Columns.Add("PERNO", typeof(string));
            data.Tables["peserta"].Columns.Add("BEGDA", typeof(string));
            data.Tables["peserta"].Columns.Add("PERNM", typeof(string));
            data.Tables["peserta"].Columns.Add("GENDR", typeof(string));
            data.Tables["peserta"].Columns.Add("INSNM", typeof(string));
            data.Tables["peserta"].Columns.Add("JOBCD", typeof(string));
            data.Tables["peserta"].Columns.Add("JOBNM", typeof(string));
            data.Tables["peserta"].Columns.Add("ADDRS", typeof(string));
            data.Tables["peserta"].Columns.Add("CITY", typeof(string));
            data.Tables["peserta"].Columns.Add("TLFAX", typeof(string));
            data.Tables["peserta"].Columns.Add("HPMAI", typeof(string));
            data.Tables["peserta"].Columns.Add("PARTY", typeof(string));
            data.Tables["peserta"].Columns.Add("WORKG", typeof(string));
            data.Tables["peserta"].Columns.Add("NPWP", typeof(string));
            data.Tables["peserta"].Columns.Add("VISIT", typeof(string));
            data.Tables["peserta"].Columns.Add("STAY", typeof(string));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                data.Tables["peserta"].Rows.Add(new object[] { (i + 1), table.Rows[i]["BEGDA"], GetNameInvited(table.Rows[i]["PERNM"].ToString(),  table.Rows[i]["DELFR"].ToString()), table.Rows[i]["JENIS"].ToString(), table.Rows[i]["INSNM"], table.Rows[i]["PRMNM"], table.Rows[i]["JOBNM"], table.Rows[i]["INADD"], table.Rows[i]["KOTNM"], table.Rows[i]["TELNO"] + " / " + table.Rows[i]["FAXNO"], table.Rows[i]["PHONE"] + " / " + table.Rows[i]["EMAIL"], GetParticipantType(table.Rows[i]["PARTY"].ToString()), table.Rows[i]["WORKG"], table.Rows[i]["NPWP"], (table.Rows[i]["ARRCD"].ToString() == "0" ? string.Empty : table.Rows[i]["ARRVL"]), table.Rows[i]["STAY"] });
            }

            data.Tables.Add("footer");
            data.Tables["footer"].Columns.Add("PRINT", typeof(string));
            data.Tables["footer"].Rows.Add(new object[] { "Di-print pada " + DateTimeConverter.GetDateFormat(DateTime.Now.ToString("MM/dd/yyyy")) + " pukul " + DateTime.Now.ToString("HH:mm") });
            
            return data;
        }
    }
}
