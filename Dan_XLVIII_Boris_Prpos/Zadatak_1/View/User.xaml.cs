using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User(string username)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this,username);
        }
        private void NumbersOnlyTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbSmall_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSmall.Text) && !string.IsNullOrEmpty(tbMedium.Text) && !string.IsNullOrEmpty(tbBig.Text) && !string.IsNullOrEmpty(tbFamily.Text) && !string.IsNullOrEmpty(tbSpecial.Text))
                tbTotal.Text = (Convert.ToInt32(tbSmall.Text) * 350 + Convert.ToInt32(tbMedium.Text) * 490 + Convert.ToInt32(tbBig.Text) * 630 + Convert.ToInt32(tbSpecial.Text) * 980 + Convert.ToInt32(tbFamily.Text) * 750).ToString();
        }

        private void TbMedium_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSmall.Text) && !string.IsNullOrEmpty(tbMedium.Text) && !string.IsNullOrEmpty(tbBig.Text) && !string.IsNullOrEmpty(tbFamily.Text) && !string.IsNullOrEmpty(tbSpecial.Text))
                tbTotal.Text = (Convert.ToInt32(tbSmall.Text) * 350 + Convert.ToInt32(tbMedium.Text) * 490 + Convert.ToInt32(tbBig.Text) * 630 + Convert.ToInt32(tbSpecial.Text) * 980 + Convert.ToInt32(tbFamily.Text) * 750).ToString();
        }

        private void TbBig_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSmall.Text) && !string.IsNullOrEmpty(tbMedium.Text) && !string.IsNullOrEmpty(tbBig.Text) && !string.IsNullOrEmpty(tbFamily.Text) && !string.IsNullOrEmpty(tbSpecial.Text))
                tbTotal.Text = (Convert.ToInt32(tbSmall.Text) * 350 + Convert.ToInt32(tbMedium.Text) * 490 + Convert.ToInt32(tbBig.Text) * 630 + Convert.ToInt32(tbSpecial.Text) * 980 + Convert.ToInt32(tbFamily.Text) * 750).ToString();
        }

        private void TbFamily_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSmall.Text) && !string.IsNullOrEmpty(tbMedium.Text) && !string.IsNullOrEmpty(tbBig.Text) && !string.IsNullOrEmpty(tbFamily.Text) && !string.IsNullOrEmpty(tbSpecial.Text))
                tbTotal.Text = (Convert.ToInt32(tbSmall.Text) * 350 + Convert.ToInt32(tbMedium.Text) * 490 + Convert.ToInt32(tbBig.Text) * 630 + Convert.ToInt32(tbSpecial.Text) * 980 + Convert.ToInt32(tbFamily.Text) * 750).ToString();
        }

        private void TbSpecial_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSmall.Text) && !string.IsNullOrEmpty(tbMedium.Text) && !string.IsNullOrEmpty(tbBig.Text) && !string.IsNullOrEmpty(tbFamily.Text) && !string.IsNullOrEmpty(tbSpecial.Text))
                tbTotal.Text = (Convert.ToInt32(tbSmall.Text) * 350 + Convert.ToInt32(tbMedium.Text) * 490 + Convert.ToInt32(tbBig.Text) * 630 + Convert.ToInt32(tbSpecial.Text) * 980 + Convert.ToInt32(tbFamily.Text) * 750).ToString();
        }
    }
}
