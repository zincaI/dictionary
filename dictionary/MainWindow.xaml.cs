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

        private void Login_Click(object sender, EventArgs e)
        {
            // Instantiate SecondForm
            Login login = new Login();

            // Display SecondForm
            login.Show();
        }
    }

    //enterAdmin_Click
    public partial class Login : Window
    {
        //private void CloseWindow()
        //{
        //    this.Close();
        //}
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

public partial class ManageWords : Window
{
        //public ManageWords()
        //{
        //    InitializeComponent();
        //}

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            textBoxWord.Visibility = Visibility.Visible;
            textBoxDescription.Visibility = Visibility.Visible;
            textBoxCategory.Visibility = Visibility.Visible;
            textBoxImagePath.Visibility = Visibility.Visible;
            Word.Visibility = Visibility.Visible;
            Category.Visibility = Visibility.Visible;
            Description.Visibility = Visibility.Visible;
            Path.Visibility = Visibility.Visible;

            // Show the "Update" button if it's properly initialized and accessible
            if (UpdateButton != null)
            {
                UpdateButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Handle the case where the UpdateButton is not properly initialized
                MessageBox.Show("UpdateButton is not properly initialized.");
            }
        }


        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve the content of the text boxes
                string word = textBoxWord.Text;
                string description = textBoxDescription.Text;
                string category = textBoxCategory.Text;
                string imagePath = textBoxImagePath.Text;

                // Create an instance of the Words class
                Words words = new Words();

                // Call the AddWord method to add the word
                words.AddWord(word, description, category, imagePath);

                // Optionally, you can show a message box to indicate success
                MessageBox.Show("Word added successfully!");

                // Optionally, you can clear the text boxes after adding the word
                textBoxWord.Text = "";
                textBoxDescription.Text = "";
                textBoxCategory.Text = "";
                textBoxImagePath.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }

}
