using Diary.Commands;
using Diary.Models.Wrappers;
using Diary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class ServerSettingsViewModel : ViewModelBase
    {
        public ServerSettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

        }


        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        private string _serverAdres = Settings.Default.AdressServer;

        public string ServerAdres
        {
            get { return _serverAdres; }
            set
            {
                _serverAdres = value;
                OnPropertyChanged();
            }
        }

        private string _nameServer = Settings.Default.ServerName;

        public string NameServer
        {
            get { return _nameServer; }
            set
            {
                _nameServer = value;
                OnPropertyChanged();
            }
        }

        private string _dataBase = Settings.Default.DataBase;

        public string DataBase
        {
            get { return _dataBase; }
            set
            {
                _dataBase = value;
                OnPropertyChanged();
            }
        }


        private string _login = Settings.Default.Login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password = Settings.Default.Password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private void Confirm(object obj)
        {
            Settings.Default.AdressServer = _serverAdres;
            Settings.Default.ServerName = _nameServer;
            Settings.Default.DataBase = _dataBase;
            Settings.Default.Login = _login;
            Settings.Default.Password = _password;

            Settings.Default.Save();

            CloseWindow(obj as Window);

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
