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
            if (counter ==13 )
            {
                int[] danaUmjesecu = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                char[] niz = input.ToCharArray(0, 13); // pretvori u niz karaktera

                // prvo provjera unosa godine rodjenja
                char[] godinaRodjenja = input.ToCharArray(4, 3); // izvuci cifre koje se odnose na godinu rodjenja
                int pomGodina = 100 * (Convert.ToInt32(godinaRodjenja[0] - '0')) +
                                 10 * (Convert.ToInt32(godinaRodjenja[1] - '0')) +
                                       Convert.ToInt32(godinaRodjenja[2] - '0'); // napravi godinu rodjenja

                if (godinaRodjenja[0] == '0') // neko ko je rodjen u XXI vijeku ...
                    pomGodina += 2000;
                else
                    pomGodina += 1000; // ko je rodjen u XX vijeku

                if (pomGodina < 1900) // trenutno, godina ne moze biti manja od 1900-e !
                {
                    MessageBox.Show("Entered year smaller than 1900");
                    return false;
                }
                else
                {
                    if (pomGodina > DateTime.Now.Year) // niti veca od tekuce godine !
                    {
                        MessageBox.Show("Entered year bigger than current year");
                        return false;
                    }
                }

                // provjera unosa mjeseca rodjenja
                char[] mjesecRodjenja = input.ToCharArray(2, 2); // izvuci cifre koje se odnose na mjesec rodjenja
                int pomMjesec = 10 * (Convert.ToInt32(mjesecRodjenja[0] - '0')) +
                                      Convert.ToInt32(mjesecRodjenja[1] - '0');
                if (pomMjesec > 12 || pomMjesec < 1) // mjesec mora biti <= 12 i > 0 
                {
                    MessageBox.Show("Wrong month of birth (third and fourth digit)");
                    return false;
                }

                // provjera da li godina prestupna (zbog broja dana u mjesecu)
                if (((pomGodina % 4) == 0) && (((pomGodina % 100) != 0) || ((pomGodina % 400) == 0))) // prestupna godina
                {
                    danaUmjesecu[1] = 29; // koriguj broj dana za februar
                }

                // provjera unosa dana po mjesecu ...
                char[] danRodjenja = input.ToCharArray(0, 2);
                int pomDan = 10 * (Convert.ToInt32(danRodjenja[0] - '0')) +
                                   Convert.ToInt32(danRodjenja[1] - '0');

                if (pomDan > danaUmjesecu[pomMjesec - 1] || pomDan < 1)
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
