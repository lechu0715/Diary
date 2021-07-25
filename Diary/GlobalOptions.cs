using Diary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public static class GlobalOptions
    {

        public static string Login
        {
            get
            {
                return Settings.Default.Login;
            }
            set
            {
                Settings.Default.Login = value;
            }
        }

        public static string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }

        public static string DataBase
        {
            get
            {
                return Settings.Default.DataBase;
            }
            set
            {
                Settings.Default.DataBase = value;
            }
        }

        public static string AdressServer
        {
            get
            {
                return Settings.Default.AdressServer;
            }
            set
            {
                Settings.Default.AdressServer = value;
            }
        }

        public static string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
    }
}
