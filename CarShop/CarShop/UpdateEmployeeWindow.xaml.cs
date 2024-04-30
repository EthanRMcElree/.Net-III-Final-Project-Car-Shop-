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
using DataObjectsLayer;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeWindow.xaml
    /// </summary>
    public partial class UpdateEmployeeWindow : Window
    {
        Employee employee = null;
        UserManager manager = null;
        public UpdateEmployeeWindow()
        {
            InitializeComponent();
            manager = new UserManager();
            EditEmployeeAccountGrid.Visibility = Visibility.Collapsed;
            SubmitEditCar.Visibility = Visibility.Collapsed;
        }

        private void btnSubmitEditEmployeeAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cboEditRole.SelectedValue != null)
                {
                    string role = cboEditRole.SelectedValue.ToString().ToLower();
                    employee.Role = role;
                }
                employee.FirstName = txtEditFirstName.Text;
                employee.LastName = txtEditLastName.Text;
                employee.PhoneNumber = txtEditPhoneNumber.Text;
                manager.UpdateEmployee(employee.FirstName, employee.LastName, employee.PhoneNumber, employee.Email, employee.Role);
                MessageBox.Show("Record updated.");
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEditEmployeeEmail.Text;
                employee = manager.GetEmployeeVMByEmail(email);
                txtEditFirstName.Text = employee.FirstName;
                txtEditLastName.Text = employee.LastName;
                txtEditPhoneNumber.Text = employee.PhoneNumber;
                EditEmployeeAccountBody.Visibility = Visibility.Collapsed;
                EditEmployeeAccountButton.Visibility = Visibility.Collapsed;
                EditEmployeeAccountGrid.Visibility = Visibility.Visible;
                SubmitEditCar.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houston we have a problem.");
            }           
        }
    }
}
