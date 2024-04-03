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

                
                bool isValid = People.FindAdmin(name, password);

                if (isValid)
                {
                    ManageWords words = new ManageWords();
                    words.PopulateWordListBox();
                    Close();
                    words.Show();

                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
