using System;
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
        /// <summary>
        /// Property  that will take username from login form
        /// </summary>
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
        /// <summary>
        /// Property that will take password from login form
        /// </summary>
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
        /// <summary>
        /// Method validates jmbg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
            if (counter ==13 )
            {
                int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                char[] inputToArray = input.ToCharArray(0, 13); // string to char array

                // checking year of birth   
                char[] yearOfBirth = input.ToCharArray(4, 3); // extract digits that represent year
                int helpYear = 100 * (Convert.ToInt32(yearOfBirth[0] - '0')) +
                                 10 * (Convert.ToInt32(yearOfBirth[1] - '0')) +
                                       Convert.ToInt32(yearOfBirth[2] - '0'); // create year of birth

                if (yearOfBirth[0] == '0') // born in XXI century ...
                    helpYear += 2000;
                else
                    helpYear += 1000; // born in XX century

                if (helpYear < 1900) // entered year smaller than 1900
                {
                    MessageBox.Show("Entered year smaller than 1900");
                    return false;
                }
                else
                {
                    if (helpYear > DateTime.Now.Year) // entered year bigger than current year !
                    {
                        MessageBox.Show("Entered year bigger than current year");
                        return false;
                    }
                }

                // checking month of birth
                char[] monthOfBirth = input.ToCharArray(2, 2); // extract digits that represent month
                int helpMonth = 10 * (Convert.ToInt32(monthOfBirth[0] - '0')) +
                                      Convert.ToInt32(monthOfBirth[1] - '0');
                if (helpMonth > 12 || helpMonth < 1) // mont must be <= 12 and > 0 
                {
                    MessageBox.Show("Wrong month of birth (third and fourth digit)");
                    return false;
                }

                // check if february has 29 days
                if (((helpYear % 4) == 0) && (((helpYear % 100) != 0) || ((helpYear % 400) == 0))) // prestupna year
                {
                    daysInMonth[1] = 29; // correction for days in february
                }

                // check if month and days are compatible
                char[] dayOfBirth = input.ToCharArray(0, 2);
                int helpDay = 10 * (Convert.ToInt32(dayOfBirth[0] - '0')) +
                                   Convert.ToInt32(dayOfBirth[1] - '0');

                if (helpDay > daysInMonth[helpMonth - 1] || helpDay < 1)
                {
                    MessageBox.Show("Wrong day of birth (first and second digit)");
                    return false;
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
