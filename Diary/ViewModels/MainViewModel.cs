using Diary.Commands;
using Diary.Models;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Properties;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        DispatcherTimer timer = new DispatcherTimer();


        public MainViewModel()
        {
            /*using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();

                    var students = context.Students.ToList();
                }
                catch
                {
                    var result = MessageBox.Show("Niepoprawne dane", "Nie udało się połączyć z bazą danych, czy chcesz podać nowe dane?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var serverSettings = new ServerSettings();
                        serverSettings.ShowDialog();
                    }

                }

            }*/


            var isConnected = IsConnected();

            if (!isConnected)
            {
                var result = MessageBox.Show("Niepoprawne dane", "Nie udało się połączyć z bazą danych, czy chcesz podać nowe dane?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.Yes)
                {
                    var serverSettings = new ServerSettings();
                    serverSettings.ShowDialog();


                    
                }
                else if (result == MessageBoxResult.No)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                using (var context = new ApplicationDbContext())
                {
                    var students = context.Students.ToList();
                }
            }

                

            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            ChangeSettingsCommand = new RelayCommand(ChangeSettings);

            RefreshDiary();
            InitGroups();

        }

        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand ChangeSettingsCommand { get; set; }



        private ObservableCollection<StudentWrapper> _students;
        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }

        }



        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }

        }



        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Group> _group; //_gropus
        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }

        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }
/*
        private async Task ChangeConnection()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Niepoprawne dane", "Nie udało się połączyć z bazą danych, czy chcesz podać nowe dane?", MessageDialogStyle.AffirmativeAndNegative);
            

            if (dialog != MessageDialogResult.Negative)
            {
                var serverSettings = new ServerSettings();
                serverSettings.ShowDialog();
            }
            else
            {
                return;
            }
        }*/

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia",
                $"Czy na pewno chcesz usunąc ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            //usuwanie ucznia z bazy
            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(
                _repository.GetStudents(SelectedGroupId));
        }

        private void ChangeSettings(object obj)
        {
            var serverSettings = new ServerSettings();
            serverSettings.ShowDialog();
        }

        private bool IsConnected()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                    
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
