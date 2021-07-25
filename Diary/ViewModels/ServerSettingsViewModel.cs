using Diary.Commands;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class ServerSettingsViewModel : ViewModelBase
    {
        public ServerSettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private void Confirm(object obj)
        {
            /*if (Student.IsValid)
                return;

            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();*/

            if (!ServerAdress)
                return;

            CloseWindow(obj as Window);
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
