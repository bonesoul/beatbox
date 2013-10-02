using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web;

namespace beatbox
{
    public class Settings
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        public string FilePath { get; set; }

        public void Write(string Section, string Key, string Value)
        {
            Write(Section, Key, Value, FilePath);
        }

        public string Read(string Section, string Key)
        {
            return Read(Section, Key, FilePath);
        }

        private void Write(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);
        }

        private string Read(string Section, string Key, string inipath)
        {
            int buffer;
            buffer = 255; // Define length
            StringBuilder temp = new StringBuilder(buffer);
            GetPrivateProfileString(Section, Key, "Default", temp, buffer, inipath);
            return temp.ToString();
        }


    }
}
