using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // EmployeeManager _employeeManager = null;
        EmployeeVM loggedInEmployee = null;
        public MainWindow()
        {
            InitializeComponent();
            CarInventoryManager carInventoryManager = new CarInventoryManager();
            List<CarInventoryVM> cars = carInventoryManager.ViewCarInventory();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            // _employeeManager = new EmployeeManager();
            txtEmail.Focus();            
            btnLogin.IsDefault = true;
        }      

        private void updateUIforLogOut()
        {
            lblGreeting.Content = "You are not currently logged in.";
            // statMessage.Content = "Welcome. Please log in to continue";
              
            txtEmail.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log in";
            btnLogin.IsDefault = true;

            loggedInEmployee = null;
        }       

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log in")
            {
                var email = txtEmail.Text;
                var password = pwdPassword.Password;

                // error checks
                /* if (!email.IsValidEmail())
                {
                    MessageBox.Show("Invalid email address", "Invalid Email",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    txtEmail.SelectAll();
                    txtEmail.Focus();
                    return;
                }*/
                /*if (!password.IsValidPassword())
                {
                    MessageBox.Show("Invalid password", "Invalid Password",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.SelectAll();
                    pwdPassword.Focus();
                    return;
                }*/
                // try to login the user
                /*try
                {
                    loggedInEmployee = _employeeManager.LoginEmployee(email, password);                    

                    // update the UI
                    updateUIforEmployee();
                }
                catch (Exception ex)
                {
                    // you may never throw exceptions from the presentation layer
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.Clear();
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }*/



            }
            else // log out
            {
                updateUIforLogOut();
            }
        }

        private void mnuUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInEmployee == null)
            {
                MessageBox.Show("You must be logged in to update your password", "Login Required",
                    MessageBoxButton.OK, MessageBoxImage.Information); return;
            }
            else
            {
                /*try
                {
                    var passwordWindow = new PasswordChangeWindow(loggedInEmployee.Email);
                    var result = passwordWindow.ShowDialog();
                    if (result == true)
                    {
                        MessageBox.Show("Password updated.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Password not changed.", "Operation Aborted",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }*/
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
