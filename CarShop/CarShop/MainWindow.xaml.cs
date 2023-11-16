using DataObjects;
using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
using WPFPresentation;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeManager _employeeManager = null;
        EmployeeVM loggedInEmployee = null;
        CustomerManager _customerManager = null;
        CustomerVM loggedInCustomer = null;
        string customerRole = "customer";
        CarInventoryManager carInventoryManager = null;
        public MainWindow()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            carInventoryManager = new CarInventoryManager();
            hideAllTabs();
            EditCarGrid.Visibility = Visibility.Collapsed;
            SubmitEditCar.Visibility = Visibility.Collapsed;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {                    
            txtEmail.Focus();            
            btnLogin.IsDefault = true;                        
        }

        private void hideAllTabs()
        {
            foreach (var tab in MainWindowTabs.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
            }
            MainWindowTabs.Visibility = Visibility.Hidden;
            MainWindowTabContainer.Visibility = Visibility.Hidden;
        }

        private void showTabsForRoles()
        {
            // loop through the user roles
            if (loggedInEmployee != null)
            {
                MainWindowTabs.Visibility = Visibility.Hidden;
                MainWindowTabContainer.Visibility = Visibility.Hidden;
                switch (loggedInEmployee.Role)
                {
                    case "manager":
                        CarInventory.Visibility = Visibility.Visible;
                        InsertCar.Visibility = Visibility.Visible;
                        DeleteCar.Visibility = Visibility.Visible;
                        EditCar.Visibility = Visibility.Visible;
                        break;
                    case "sales":
                        CarInventory.Visibility = Visibility.Visible;
                        break;
                    case "admin":
                        CarInventory.Visibility = Visibility.Visible;
                        InsertCar.Visibility = Visibility.Visible;
                        DeleteCar.Visibility = Visibility.Visible;
                        EditCar.Visibility = Visibility.Visible;
                        break;
                }
            }
            else
            {
                CarInventory.Visibility = Visibility.Visible;
            }              
            MainWindowTabs.Visibility = Visibility.Visible;
            MainWindowTabContainer.Visibility = Visibility.Visible;
        }

        private void updateUIforLogOut()
        {
            lblGreeting.Content = "Please log in if you haven't already.";            
              
            txtEmail.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log in";
            btnLogin.IsDefault = true;

            loggedInEmployee = null;
            hideAllTabs();
        }       

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log in")
            {
                var email = txtEmail.Text;
                var password = pwdPassword.Password;

                // error checks
                if (!email.IsValidEmail())
                {
                    MessageBox.Show("Invalid email address", "Invalid Email",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    txtEmail.SelectAll();
                    txtEmail.Focus();
                    return;
                }
                if (!password.IsValidPassword())
                {
                    MessageBox.Show("Invalid password", "Invalid Password",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.SelectAll();
                    pwdPassword.Focus();
                    return;
                }
                // try to login the user
                try
                {
                    loggedInEmployee = _employeeManager.AuthenticateEmployee(email, password);                    

                    if(loggedInEmployee == null) 
                    {
                        throw new Exception("Username and/or password not found");
                    }
                    // update the UI
                    updateUIforEmployee();
                }
                catch (Exception ex)
                {                   
                    MessageBox.Show("Login failed");
                    pwdPassword.Clear();
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }
            }
            else // log out
            {
                updateUIforLogOut();
            }
        }

        private void updateUIforEmployee()
        {                    
            lblGreeting.Content = "Welcome " + loggedInEmployee.FirstName + ". You are logged in as: "
                + loggedInEmployee.Role + ".";
            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() + " at " +
                DateTime.Now.ToShortDateString() +
                ". Please remember to log out before leaving your workstation.";
            MyCars.ItemsSource = carInventoryManager.ViewCarInventory();

            // clear the login section
            txtEmail.Text = "";
            txtEmail.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

            showTabsForRoles();
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
                try
                {
                    var passwordWindow = new ChangePasswordWindow(loggedInEmployee.Email);
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
                }
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmitNewCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Model = txtModel.Text;
                int Year = Int32.Parse(cboYear.Text);
                string Color = txtColor.Text;
                string VIN = txtVIN.Text;
                double Price = Double.Parse(txtPrice.Text);
                int Mileage = Int32.Parse(txtMileage.Text);
                string FuelType = cboFuelType.Text;
                string TransmissionType = cboTransmissionType.Text;
                double EngineSize = Double.Parse(txtEngineSize.Text);
                string Description = txtDescription.Text;
                int rows = carInventoryManager.InsertNewCar(Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
                if (rows != 1)
                {
                    MessageBox.Show("Unable to insert your new car.");
                }
                else
                {
                    MessageBox.Show("Successfully added new car.");
                    MyCars.ItemsSource = carInventoryManager.ViewCarInventory();
                    txtModel.Text = "";
                    cboYear.Text = "";
                    txtColor.Text = "";
                    txtVIN.Text = "";
                    txtPrice.Text = "";
                    txtMileage.Text = "";
                    txtMileage.Text = "";
                    cboFuelType.Text = "";
                    cboTransmissionType.Text = "";
                    txtEngineSize.Text = "";
                    txtDescription.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem inserting your new car.  Check your inputs.");
            }            
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int CarID = Int32.Parse(txtCarID.Text);
                int rows = carInventoryManager.DeleteCarByID(CarID);
                if (rows != 1)
                {
                    MessageBox.Show("Unable to delete car.");
                }
                else
                {
                    MessageBox.Show("Successfully deleted car.");
                    MyCars.ItemsSource = carInventoryManager.ViewCarInventory();
                    txtCarID.Text = "";                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem deleting your car.  Check your input and try again.");
            }
        }

        private void btnEditCar_Click(object sender, RoutedEventArgs e)
        {
            EditCarGrid.Visibility = Visibility.Visible;
            SubmitEditCar.Visibility = Visibility.Visible;
        }

        private void btnSubmitEditCar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
