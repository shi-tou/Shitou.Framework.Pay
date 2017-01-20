using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Shitou.Framework.Pay
{
    public class LogHelper
    {
        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strContent"></param>
        public static void SaveFileLog(string strTitle, string strContent)
        {
            try
            {
                DateTime time = DateTime.Now;
                string Path = AppDomain.CurrentDomain.BaseDirectory + "Log/" + time.Year + "/" + time.Month + "/";
                string FilePath = Path + time.Day + "_Log.txt";
                if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
                if (!File.Exists(FilePath))
                {
                    FileStream FsCreate = new FileStream(FilePath, FileMode.Create);
                    FsCreate.Close();
                    FsCreate.Dispose();
                }
                FileStream FsWrite = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                StreamWriter SwWrite = new StreamWriter(FsWrite);
                SwWrite.WriteLine(string.Format("{0}{1}[{2}]{3}", "--------------------------------", strTitle, time.ToString("HH:mm"), "--------------------------------"));
                SwWrite.Write(strContent);
                SwWrite.WriteLine("\r\n");
                SwWrite.WriteLine(" ");
                SwWrite.Flush();
                SwWrite.Close();
            }
            catch { }
        }
    }
}