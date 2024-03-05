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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            // Instantiate SecondForm
            Login secondForm = new Login();

            // Display SecondForm
            secondForm.Show();
        }
    }

    //enterAdmin_Click
    public partial class Login : Window
    {
        private void CloseWindow()
        {
            this.Close();
        }
        private void enterAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = textBoxUsername.Text;
                string password = passwordBoxPassword.Password;

                // Call the FindAdmin method to check if the credentials are valid
                bool isValid = People.FindAdmin(name, password);

                if (isValid)
                {
                    MessageBox.Show("Login successful!");
                    // Proceed with the logic for successful login
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                    // Handle invalid login attempt
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
