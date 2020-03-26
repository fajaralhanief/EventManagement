using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRVN.Business.Activities
{
    public class NotificationManager
    {
        public static String GetSuccessNotification(string NamaPerusahaan, string jumlahPeserta)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-block alert-success fade in' align='center'>    ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>      ");
            script.Append(" <i class='fa fa-times'></i>                                             ");
            script.Append(" </button>                                                               ");
            script.Append(" <h4 style='font-size:15px;font-weight:bold;'>                           ");
            script.Append(" <i class='icon-ok-sign'></i>                                            ");
            script.Append(" Konfirmasi Kehadiran Berhasil!                                          ");
            script.Append(" </h4>                                                                   ");
            script.Append(" <p style='font-size:30px;color:#3C763D;line-height:120%;'> " + NamaPerusahaan + " dengan jumlah " + jumlahPeserta + " peserta.</p> ");
            script.Append(" </div>                                                                  ");
            return script.ToString();
        }

        public static String GetOverPresenceNotification(string NamaPerusahaan, string jumlahPeserta)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-info fade in' align='center'>               ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
            script.Append(" <i class='fa fa-times'></i>                                         ");
            script.Append(" </button>                                                           ");
            script.Append(" <strong><p style='font-size:15px;color:#31708F;font-weight:bold;line-height:120%;'>" + NamaPerusahaan + " SUDAH MELAKUKAN KONFIRMASI KEHADIRAN DENGAN TOTAL " + jumlahPeserta + " PESERTA</p></strong> ");
            script.Append(" </div>                                                              ");
            return script.ToString();
        }

        public static String GetFailedNotification()
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-block alert-danger fade in' align='center'> ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
            script.Append(" <i class='fa fa-times'></i>                                         ");
            script.Append(" </button>                                                           ");
            script.Append(" <strong><h4 style='font-size:25px;font-weight:bold;line-height:120%;'><b>KODE REGISTRASI TIDAK DITEMUKAN</b></h4></strong>                       ");
            script.Append(" </div>                                                              ");
            return script.ToString();
        }

        public static String GetDuplicateNotification(string CNAME)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-block alert-danger fade in' align='center'> ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
            script.Append(" <i class='fa fa-times'></i>                                         ");
            script.Append(" </button>                                                           ");
            script.Append(" <strong><h4 style='font-size:25px;font-weight:bold;line-height:120%;'><b>" + CNAME + " SUDAH KONFIRMASI HADIR</b></h4></strong>   ");
            script.Append(" </div>                                                              ");
            return script.ToString();
        }

        public static String GetInfoNotification(string NamaPerusahaan, string jumlahPeserta)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-info fade in' align='center'>               ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
            script.Append(" <i class='fa fa-times'></i>                                         ");
            script.Append(" </button>                                                           ");
            script.Append(" <strong><p style='font-size:15px;color:#31708F;font-weight:bold;line-height:120%;'>" + NamaPerusahaan + " SUDAH MELAKUKAN KONFIRMASI KEHADIRAN DENGAN TOTAL " + jumlahPeserta + " PESERTA</p></strong> ");
            script.Append(" </div>                                                              ");
            return script.ToString();
        }



        public static String GetSuccessUndo(string PERNM)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-warning fade in'>                          ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'> ");
            script.Append(" <i class='fa fa-times'></i>                                        ");
            script.Append(" </button>                                                          ");
            script.Append(" <strong>Undo</strong> konfirmasi kehadiran " + PERNM + " berhasil! ");
            script.Append(" </div>                                                             ");
            return script.ToString();
        }

        public static String GetSuccessConfirmation(string PERNM)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-success fade in'>                           ");                    
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'>  ");
            script.Append(" <i class='fa fa-times'></i>                                         ");
            script.Append(" </button>                                                           ");
            script.Append(" <strong>Konfirmasi kehadiran</strong> " + PERNM + " Berhasil!       ");
            script.Append(" </div>                                                              ");
            return script.ToString();
        }

        public  static String GetSuccessAbsent(string PERNM)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-block alert-danger fade in'>               ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'> ");
            script.Append(" <i class='fa fa-times'></i>                                        ");
            script.Append(" </button>                                                          ");
            script.Append(" <strong>Absen</strong> " + PERNM + " Berhasil!                     ");
            script.Append(" </div>                                                             ");
            return script.ToString();
        }

        public  static String GetInfoDuplicated(string PERNM)
        {
            StringBuilder script = new StringBuilder();
            script.Append(" <div class='alert alert-info fade in'>                             ");
            script.Append(" <button data-dismiss='alert' class='close close-sm' type='button'> ");
            script.Append(" <i class='fa fa-times'></i>                                        ");
            script.Append(" </button>                                                          ");
            script.Append(" <strong>Informasi</strong> " + PERNM + " telah dikonfirmasi hadir! ");
            script.Append(" </div>                                                             ");
            return script.ToString();
        }
        
    }
}