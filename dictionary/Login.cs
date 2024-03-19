using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dictionary
{
    //enterAdmin_Click
    public partial class Login : Window
    {
        
        private void EnterAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = textBoxUsername.Text;
                string password = passwordBoxPassword.Password;

                // Call the FindAdmin method to check if the credentials are valid
                bool isValid = People.FindAdmin(name, password);

                if (isValid)
                {
                    Close();
                    // Instantiate SecondForm
                    ManageWords words = new ManageWords();
                    words.PopulateWordListBox();

                    // Display SecondForm
                    words.Show();

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
