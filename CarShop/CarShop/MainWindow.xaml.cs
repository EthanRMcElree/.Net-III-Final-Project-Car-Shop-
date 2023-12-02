using DataObjects;
using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
            mnuSales.Visibility = Visibility.Hidden;
            mnuServiceAppointment.Visibility = Visibility.Hidden;
            mnuAddServApp.Visibility = Visibility.Hidden;
            mnuEmployees.Visibility = Visibility.Hidden;
            mnuUpdateEmployees.Visibility = Visibility.Hidden;
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
                        mnuSales.Visibility = Visibility.Visible;
                        mnuServiceAppointment.Visibility = Visibility.Visible;
                        mnuEmployees.Visibility = Visibility.Visible;
                        mnuUpdateEmployees.Visibility= Visibility.Visible;
                        break;
                    case "sales":
                        CarInventory.Visibility = Visibility.Visible;
                        mnuSales.Visibility= Visibility.Visible;
                        mnuUpdateEmployees.Visibility = Visibility.Visible;
                        break;
                    case "admin":
                        CarInventory.Visibility = Visibility.Visible;
                        InsertCar.Visibility = Visibility.Visible;
                        DeleteCar.Visibility = Visibility.Visible;
                        EditCar.Visibility = Visibility.Visible;
                        mnuServiceAppointment.Visibility = Visibility.Visible;
                        mnuUpdateEmployees.Visibility = Visibility.Visible;
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
            btnSignUp.Visibility = Visibility.Collapsed;            
        }

        private void updateUIforEmployee()
        {                    
            lblGreeting.Content = "Welcome " + loggedInEmployee.FirstName + ". You are logged in as: "
                + loggedInEmployee.Role + ".";
            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() + " at " +
                DateTime.Now.ToShortDateString() +
                ". Please remember to log out before leaving your workstation.";            
            setUpCarInventory();

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
            try
            {
                int CarID = Int32.Parse(txtEditCarID.Text);
                CarInventoryVM car = carInventoryManager.ViewCarByID(CarID);
                if (car != null)
                {
                    txtEditModel.Text = car.Model;
                    cboEditYear.Text = car.Year.ToString();
                    txtEditColor.Text = car.Color;
                    txtEditVIN.Text = car.VIN;
                    txtEditPrice.Text = car.Price.ToString();
                    txtEditMileage.Text = car.Mileage.ToString();
                    cboEditFuelType.Text = car.FuelType.ToString();
                    cboEditTransmissionType.Text = car.TransmissionType;
                    txtEditEngineSize.Text = car.EngineSize.ToString();
                    txtEditDescription.Text = car.Description;
                    EditCarGrid.Visibility = Visibility.Visible;
                    SubmitEditCar.Visibility = Visibility.Visible;
                    EditCarBody.Visibility = Visibility.Collapsed;
                    EditCarButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    throw new Exception("Can't find car.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Having trouble finding that car.  Try again.");
            }            
        }

        private void btnSubmitEditCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                carInventoryManager.UpdateCar(Int32.Parse(txtEditCarID.Text),
                txtEditModel.Text, Int32.Parse(cboEditYear.Text),
                txtEditColor.Text, txtEditVIN.Text, Double.Parse(txtEditPrice.Text),
                Int32.Parse(txtEditMileage.Text), cboEditFuelType.Text,
                cboEditTransmissionType.Text, Double.Parse(txtEditEngineSize.Text),
                txtEditDescription.Text);
                MainWindowTabs.SelectedIndex = 0;

                txtEditModel.Text = "";
                cboEditYear.Text = "";
                txtEditColor.Text = "";
                txtEditVIN.Text = "";
                txtEditPrice.Text = "";
                txtEditMileage.Text = "";
                cboEditFuelType.Text = "";
                cboEditTransmissionType.Text = "";
                txtEditEngineSize.Text = "";
                txtEditDescription.Text = "";
                EditCarGrid.Visibility = Visibility.Collapsed;
                SubmitEditCar.Visibility = Visibility.Collapsed;
                EditCarBody.Visibility = Visibility.Visible;
                EditCarButton.Visibility = Visibility.Visible;
                MyCars.ItemsSource = carInventoryManager.ViewCarInventory();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input in one or more of your text or comboboxes");
            }           
        }

        private void btnViewAllCars_Click(object sender, RoutedEventArgs e)
        {
            MyCars.ItemsSource = carInventoryManager.ViewCarInventory();
        }

        private void btnViewHighMileage_Click(object sender, RoutedEventArgs e)
        {
            MyCars.ItemsSource = carInventoryManager.FilterCarByHighMileage();
        }

        private void btnViewModerateMileage_Click(object sender, RoutedEventArgs e)
        {
            MyCars.ItemsSource = carInventoryManager.FilterCarByModerateMileage();
        }

        private void btnViewLowMileage_Click(object sender, RoutedEventArgs e)
        {
            MyCars.ItemsSource = carInventoryManager.FilterCarByLowMileage();
        }

        private void cboViewFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;
            MyCars.ItemsSource = carInventoryManager.FilterCarByFuelType(text);
        }
        private void setUpFuelTypeSelectors()
        {
            SortedSet<string> fuelTypes = new SortedSet<string>();
            List<CarInventoryVM> cars = carInventoryManager.ViewCarInventory();
            foreach (CarInventoryVM car in cars)
            {
                fuelTypes.Add(car.FuelType.ToString());
            }
            foreach (string fuelType in fuelTypes)
            {
                cboViewFuelType.Items.Add(fuelType);
            }
        }
        private void setUpCarInventory()
        {            
            MyCars.ItemsSource = carInventoryManager.ViewCarInventory();
        }

        private void mnuSalesWindow_Click(object sender, RoutedEventArgs e)
        {
            var salesWindow = new SalesWindow();
            salesWindow.Show();
        }

        private void mnuServiceAppointmentWindow_Click(object sender, RoutedEventArgs e)
        {
            var serviceAppointmentWindow = new ServiceAppointmentWindow();
            serviceAppointmentWindow.Show();
        }

        private void mnuEmployeesWindow_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = new EmployeeWindow();
            employeeWindow.Show();
        }

        private void mnuUpdateEmployeesWindow_Click(object sender, RoutedEventArgs e)
        {
            var updateEmployeeWindow = new UpdateEmployeeWindow();
            updateEmployeeWindow.Show();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            var createEmployeeAccountWindow = new CreateEmployeeAccountWindow();
            createEmployeeAccountWindow.Show();
        }
    }
}
