using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for AddServAppWindow.xaml
    /// </summary>
    public partial class AddServAppWindow : Window
    {
        public AddServAppWindow()
        {
            InitializeComponent();
        }

        private void mnuAddServAppExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreateServApp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
