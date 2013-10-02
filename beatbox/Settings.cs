using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web;

namespace FLOG.Setting
{
    public static class Settings
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        private static string File
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Settings.ini");
            }
        }

        public static void Write(string key, string value)
        {
            WritePrivateProfileString("Settings", key, value, File);
        }

        public static string Read(string key)
        {
            if (HttpContext.Current.Application[key] != null)
            {

                string oldValue = Read("Settings", key, File);

                if (oldValue == HttpContext.Current.Application[key].ToString())
                {
                    return HttpContext.Current.Application[key].ToString();
                }
                else
                {
                    return SetAndReturn(key);
                }
            }
            else
            {
                return SetAndReturn(key);
            }
        }

        private static string SetAndReturn(string key)
        {
            string value = Read("Settings", key, File);
            HttpContext.Current.Application[key] = value;
            return value;
        }

        public static void Write(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);
        }

        public static string Read(string Section, string Key, string inipath)
        {
            int buffer;
            buffer = 255; // Define length
            StringBuilder temp = new StringBuilder(buffer);
            GetPrivateProfileString(Section, Key, "Default", temp, buffer, inipath);
            return temp.ToString();
        }


    }
}
