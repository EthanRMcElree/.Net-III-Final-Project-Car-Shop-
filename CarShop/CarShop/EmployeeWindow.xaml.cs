using LogicLayer;
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
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        EmployeeManager employeeManager = null;
        public EmployeeWindow()
        {
            InitializeComponent();
            employeeManager = new EmployeeManager();
            MyEmployees.ItemsSource = employeeManager.SelectAllEmployee();
        }

        private void btnDeleteEmployeeAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int EmployeeID = Int32.Parse(txtEmployeeAccountID.Text);
                int rows = employeeManager.DeleteEmployeeAccountByID(EmployeeID);
                if (rows == 0)
                {
                    MessageBox.Show("Unable to delete employee account.");
                }
                else
                {
                    MessageBox.Show("Successfully deleted employee account.");
                    MyEmployees.ItemsSource = employeeManager.SelectAllEmployee();
                    txtEmployeeAccountID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem deleting the employee account.  Check your input and try again.");
            }
        }
    }
}
