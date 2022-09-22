using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Interop;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //btnLogin.IsEnabled = false;

            emailTxt.Text = "bryan@hotmail.com";
            passTxt.Password = "bryankang";
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration regi = new Registration();
            regi.Show();
            this.Close();
        }

        private void passTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(passTxt.Password.Length > 0)
            {
                passwordHint.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordHint.Visibility = Visibility.Visible;
            }
        }

        private void passTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string pass = passTxt.Password;

            if (string.IsNullOrEmpty(pass) || pass.Length <= 6)
            {
                passError.Visibility = Visibility.Visible;
                passExc.Visibility = Visibility.Visible;
                btnLogin.IsEnabled = false;
            }
            else
            {
                passError.Visibility = Visibility.Hidden;
                passExc.Visibility = Visibility.Hidden;
                btnLogin.IsEnabled = true;
            }
        }

        private void emailTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = emailTxt.Text;

            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                emailError.Visibility = Visibility.Visible;
                emailExc.Visibility = Visibility.Visible;
                btnLogin.IsEnabled = false;
            }
            else
            {
                emailError.Visibility = Visibility.Hidden;
                emailExc.Visibility = Visibility.Hidden;
                btnLogin.IsEnabled = true;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTxt.Text;
            string pass = passTxt.Password;
            if (File.Exists(@"user\" + email + ".txt"))
            {
                StreamReader streamReader = new StreamReader(@"user\" + email + ".txt");
                string[] lines = File.ReadAllLines(@"user\" + email + ".txt");
                if(pass != lines[0])
                {
                    MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string name = lines[1];
                    int contact = int.Parse(lines[2]);
                    bool renter = bool.Parse(lines[3]);
                    bool owner = bool.Parse(lines[4]);
                    User user = new User(email, pass, name, contact, renter, owner);
                    MessageBox.Show("Logging In", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginSession.Email = email;
                    LoginSession.Password = pass;
                    LoginSession.Name = name;
                    LoginSession.Contact = contact;
                    LoginSession.Renter = renter;
                    LoginSession.Owner = owner;
                    RenterInterface rent = new RenterInterface();
                    rent.Show();
                    Close();

                }
            }
            else
            {
                MessageBox.Show("User does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                emailTxt.Text = "";
                passTxt.Password = "";
            }
        }
    }
}
