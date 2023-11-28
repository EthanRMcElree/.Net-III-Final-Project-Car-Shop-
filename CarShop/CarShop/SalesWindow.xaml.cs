using DataObjectsLayer;
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
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window      
    {
        SalesManager salesManager = null;
        public SalesWindow()
        {
            InitializeComponent();
            salesManager = new SalesManager();
            List<SalesVM> salesVMs = salesManager.ViewSales();
            List<Sales> sales = new List<Sales>();
            foreach(SalesVM s in salesVMs)
            {
                Sales sale = new Sales();
                sale.SaleDate = s.SaleDate;
                sale.SalePrice = s.SalePrice;
                sale.SaleID = s.SaleID;
                sale.CarID = s.CarID;
                sale.EmployeeID = s.EmployeeID;
                sale.CustomerID = s.CustomerID;
                sales.Add(sale);
            }
            MySales.ItemsSource = sales;
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsertSale_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
