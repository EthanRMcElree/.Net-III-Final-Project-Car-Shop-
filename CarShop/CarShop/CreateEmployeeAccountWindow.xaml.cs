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
    /// Interaction logic for CreateEmployeeAccountWindow.xaml
    /// </summary>
    public partial class CreateEmployeeAccountWindow : Window
    {
        EmployeeManager employeeManager = null;
        public CreateEmployeeAccountWindow()
        {
            InitializeComponent();
            employeeManager = new EmployeeManager();
        }

        private void btnCreateEmployeeAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FirstName = txtFirstName.Text;
                string LastName = txtLastName.Text;
                string Email = txtEmail.Text;
                string Password = txtPassword.Text;
                string PhoneNumber = txtPhoneNumber.Text;
                employeeManager.CreateCreateEmployeeAccount(FirstName, LastName, Password, PhoneNumber, Email, "employee");
                MessageBox.Show("Account created.  Welcome to the team.");
                this.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("There was a problem.  Check your inputs.");
            }           
        }
    }
}
