using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FRVN.Business.Components
{
    public class FileManager
    {
        public static string RenameFile(string name, string format)
        {
            return name + "." + format.Split('.')[0];
        }

        public static void DeleteFile(string filePath)
        {
            string fileName = (filePath);

            if (fileName != null || fileName != string.Empty)
            {
                if ((File.Exists(fileName)))
                {
                    try 
	                {
                        File.Delete(fileName);
	                }
	                catch (Exception ex)
	                {
                        string a = ex.Message;
	                }
    
                }
            }
        }

        protected static bool IsFileLocked(string filename)
        {
            FileStream stream = null;

            try
            {
                //stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream = File.OpenRead(filename);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
