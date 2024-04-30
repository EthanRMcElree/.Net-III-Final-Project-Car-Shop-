using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window      
    {
        CarInventoryManager carInventoryManager = null;
        SalesManager salesManager = null;
        UserManager employeeManager = null;
        CustomerManager customerManager = null;
        public SalesWindow()
        {
            InitializeComponent();            
            salesManager = new SalesManager(); 
            employeeManager = new UserManager();
            customerManager = new CustomerManager();
            carInventoryManager = new CarInventoryManager();            
            InsertSaleGrid.Visibility = Visibility.Collapsed;
            SubmitSale.Visibility = Visibility.Collapsed;
            setUpSalesData();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsertSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int CarID = Int32.Parse(txtInsertSaleCarID.Text);                               
                CarInventoryVM car = carInventoryManager.ViewCarByID(CarID);
                if (getSaleByCarID(CarID) != null)
                {
                    throw new Exception("That car already has a sale on it.");
                }
                if (car != null)
                {                                     
                    InsertSaleBody.Visibility = Visibility.Collapsed;
                    InsertSaleButton.Visibility = Visibility.Collapsed;
                    InsertSaleGrid.Visibility = Visibility.Visible;
                    SubmitSale.Visibility = Visibility.Visible;                    
                }
                else
                {
                    throw new Exception("Having trouble finding that car.  Please try again.");
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmitSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                int CarID = Int32.Parse(txtInsertSaleCarID.Text);
                int EmployeeID = Int32.Parse(txtSaleEmployeeID.Text);
                int CustomerID = Int32.Parse(txtSaleCustomerID.Text);
                DateTime SaleDate = DateTime.Parse(txtSaleDate.Text);
                double SalePrice = Double.Parse(txtSalePrice.Text);
                try
                {   
                    if(employeeManager.GetEmployeeVMByID(EmployeeID) == null)
                    {
                        throw new Exception("Invalid Employee ID.");
                    }
                    if(customerManager.SelectCustomerVMByID(CustomerID) == null)
                    {
                        throw new Exception("Invalid Customer ID.");
                    }
                    if (SalePrice >= carInventoryManager.ViewCarByID(CarID).Price)
                    {
                        throw new Exception("Price can't be greater or equal to the car's original price.");
                    }                   
                    int rows = salesManager.CreateSale(EmployeeID, CarID, CustomerID, SaleDate, SalePrice);
                    if (rows != 1)
                    {
                        MessageBox.Show("Unable to insert the new sale.");
                    }
                    else
                    {
                        MessageBox.Show("Successfully inserted new sale.");
                        setUpSalesData();
                        txtInsertSaleCarID.Text = "";
                        txtSaleEmployeeID.Text = "";
                        txtSaleCustomerID.Text = "";
                        txtSaleDate.Text = "";
                        txtSalePrice.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("Price can't be greater or equal to the car's original price.");
                }                                                         
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem inserting your new sale.  Check your inputs.");
            }            
        }

        private void setUpSalesData()
        {
            List<SalesVM> salesVMs = salesManager.ViewSales();
            List<SalesDisplayItem> sales = new List<SalesDisplayItem>();
            foreach (SalesVM s in salesVMs)
            {
                SalesDisplayItem sale = new SalesDisplayItem();
                sale.SaleID = s.SaleID;
                sale.Car = s.Car;
                sale.employee = s.Employee;
                sale.customer = s.Customer;
                sale.saleDate = s.SaleDate;
                sale.salePrice = s.SalePrice;
                sales.Add(sale);
            }
            MySales.ItemsSource = sales;
        }

        private SalesVM getSaleByCarID(int CarID)
        {
            List<SalesVM> salesVMs = salesManager.ViewSales();
            List<Sales> sales = new List<Sales>();
            foreach (SalesVM s in salesVMs)
            {
                if(s.CarID == CarID)
                {
                    return s;
                }
            }
            return null;
        }      

        private void btnDeleteSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int SaleID = Int32.Parse(txtSaleID.Text);
                int rows = salesManager.DeleteSaleByID(SaleID);
                if (rows != 1)
                {
                    MessageBox.Show("Unable to delete sale.");
                }
                else
                {
                    MessageBox.Show("Successfully deleted sale.");
                    setUpSalesData();
                    txtSaleID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem deleting the sale.  Check your input and try again.");
            }
        }
    }
    class SalesDisplayItem
    {
        public int SaleID { get; set; }

        public CarInventory Car { get; set; }

        public Employee employee { get; set; }

        public Customer customer { get; set; }

        public DateTime saleDate { get; set; }

        public Double salePrice { get; set; }
    }
}
