using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        /// <summary>
        /// Method for determining which user has logged in
        /// </summary>
        private void LoginExecute()
        {
            try
            {
                //iz admin is logged
                if (Username == "Zaposleni" && Password == "Zaposleni")
                {
                    Admin admin = new Admin();
                    admin.ShowDialog();
                }
                //if user is logged
                else if (Username.Length == 13 && NumbersOnly(Username) == true && Password == "Gost")
                {
                    User userView = new User(Username);
                    userView.ShowDialog();
                }
                //if invalid parametres are inputed
                else
                {
                    MessageBox.Show("Invalid parametres, Username must be your JMBG");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
        }
        //can press button only if both fields are not empty
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        //method validates jmbg
        private bool NumbersOnly(string input)
        {
            input = Username;

            char[] array = input.ToCharArray();

            int counter = 0;
            //there must be 13 characaters
            for (int i = 0; i < array.Length; i++)
            {
                if (Char.IsDigit(array[i]))
                {
                    counter++;
                }
            }
            //first and thirs number must be correct
            if (counter == 13 && Convert.ToInt32(array[0].ToString()) < 4 && Convert.ToInt32(array[2].ToString()) < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
