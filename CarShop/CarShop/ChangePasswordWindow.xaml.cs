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
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private string _email;
        public ChangePasswordWindow(string email)
        {
            _email = email;

            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            UserManager employeeManager = new UserManager();

            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string retypePassword = pwdRetypePassword.Password;    

            // newuser check
            if (newPassword == "newuser")
            {
                MessageBox.Show("Nice try. Change your password.", "Invalid password.",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            // reusing password check
            if (newPassword == oldPassword)
            {
                MessageBox.Show("You can't reuse the same password.", "Invalid password.",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            // retype error checks         
            if (newPassword != retypePassword)
            {
                MessageBox.Show("New Password and Retype Password must be the same.", "Invalid password.",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            try
            {
                if (employeeManager.ResetPassword(_email,
                    pwdOldPassword.Password, pwdNewPassword.Password))
                {
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsDefault = true;
            pwdOldPassword.Focus();
        }
    }
}
