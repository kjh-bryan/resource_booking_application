using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            this.DataContext = this;
            btnRegister.IsEnabled = false;
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }


        private void nameTxt_LostFocus(object sender, RoutedEventArgs e)
        {

            string name = nameTxt.Text;
            if (string.IsNullOrEmpty(name))
            {
                nameError.Visibility = Visibility.Visible;
                nameExc.Visibility = Visibility.Visible;
                btnRegister.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnRegister.IsEnabled = false;
            }
            else
            {
                nameError.Visibility = Visibility.Hidden;
                nameExc.Visibility = Visibility.Hidden;
                btnRegister.IsEnabled = true;
            }
        }

        private void confirmPassTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string cfmPass = confirmPassTxt.Password;
            string pass = passTxt.Password;

            if(string.IsNullOrEmpty(cfmPass) || pass != cfmPass)
            {
                cfmPassError.Visibility = Visibility.Visible;
                cfmPassExc.Visibility = Visibility.Visible;
                btnRegister.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnRegister.IsEnabled = false;
            }
            else
            {
                cfmPassError.Visibility = Visibility.Hidden;
                cfmPassExc.Visibility = Visibility.Hidden;
                btnRegister.IsEnabled = true;
            }
        }

        private void passTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string pass = passTxt.Password;

            if (string.IsNullOrEmpty(pass) || pass.Length <= 6)
            {
                passError.Visibility = Visibility.Visible;
                passExc.Visibility = Visibility.Visible;
                btnRegister.IsEnabled = false;
            }
            else if(!checkValidation())
            {
                btnRegister.IsEnabled = false;
            }
            else
            {
                passError.Visibility = Visibility.Hidden;
                passExc.Visibility = Visibility.Hidden;
                btnRegister.IsEnabled = true;
            }
        }

        private void emailTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = emailTxt.Text;

            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                emailError.Visibility = Visibility.Visible;
                emailExc.Visibility = Visibility.Visible;
                btnRegister.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnRegister.IsEnabled = false;
            }
            else
            {
                emailError.Visibility = Visibility.Hidden;
                emailExc.Visibility = Visibility.Hidden;
                btnRegister.IsEnabled = true;
            }
        }

        private void contactTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string strcontact = contactTxt.Text;
            int contact;

            if (string.IsNullOrEmpty(strcontact) || !int.TryParse(strcontact,out contact) || strcontact.Length != 8)
            {
                contactError.Visibility = Visibility.Visible;
                contactExc.Visibility = Visibility.Visible;
                btnRegister.IsEnabled = false;
            }
            else if (!checkValidation())
            {
                btnRegister.IsEnabled = false;
            }
            else
            {
                contactError.Visibility = Visibility.Hidden;
                contactExc.Visibility = Visibility.Hidden;
                btnRegister.IsEnabled = true;
            }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            
            int contact = int.Parse(contactTxt.Text);
            string email = emailTxt.Text;
            string name = nameTxt.Text;
            string pass = passTxt.Password;
            bool renter = false;
            bool owner = false;
            if(renterBtn.IsChecked == true)
            {
                renter = true;
                owner = false;
            }
            else if (flatownerBtn.IsChecked == true)
            {
                owner = true;
                renter = false;
            }

            if (File.Exists(@"user\" + email + ".txt"))
            {
                MessageBox.Show("User exists!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passTxt.Password = "";
                emailTxt.Text = "";
                nameTxt.Text = "";
                contactTxt.Text = "";
                confirmPassTxt.Password = "";
            }
            else
            {

                StreamWriter streamWriter = new StreamWriter(@"user\" + email + ".txt");
                streamWriter.WriteLine(pass);
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(contact);
                streamWriter.WriteLine(renter);
                streamWriter.WriteLine(owner);
                streamWriter.Close();
                MessageBox.Show("Registered Successfully!","Result", MessageBoxButton.OK,MessageBoxImage.Information);
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            

        }


        private void passTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passTxt.Password.Length > 0)
            {
                passwordHint.Visibility = Visibility.Collapsed;
            }
            else
            if (passTxt.Password.Length == 0)
            {
                passwordHint.Visibility = Visibility.Visible;
            }
        }

        private void confirmPassTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
            if (confirmPassTxt.Password.Length > 0)
            {
                cfmPasswordHint.Visibility = Visibility.Collapsed;
            }
            else
            if (confirmPassTxt.Password.Length == 0)
            {
                cfmPasswordHint.Visibility = Visibility.Visible;
            }
        }

        public bool checkValidation()
        {
            string email = emailTxt.Text;
            string name = nameTxt.Text;
            string pass = passTxt.Password;
            string cfmpass = confirmPassTxt.Password;
            string contact = contactTxt.Text;
            bool check;
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(pass) && string.IsNullOrEmpty(cfmpass) && string.IsNullOrEmpty(contact))
            {
                check = false;
            }
            else
            {
                check = true;
            }

            return check;
        }
    }
    
}
