using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dictionary
{

    public partial class ManageWords : Window
    {
        public void PopulateWordListBox()
        {
            try
            {
                // Read the list of words from the JSON file
                List<Words> wordsList = Words.ReadWordsFromJson();

                // Clear the ListBox's ItemSource
                wordListBox.ItemsSource = null;

                // Set the ListBox's ItemSource to the new data
                wordListBox.ItemsSource = wordsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading from the JSON file: " + ex.Message);
            }
        }

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
                AbandonAddButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Handle the case where the UpdateButton is not properly initialized
                MessageBox.Show("UpdateButton is not properly initialized.");
            }
        }

        private void RetrieveVisabilityAdd(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            textBoxWord.Visibility = Visibility.Collapsed;
            textBoxDescription.Visibility = Visibility.Collapsed;
            textBoxCategory.Visibility = Visibility.Collapsed;
            textBoxImagePath.Visibility = Visibility.Collapsed;
            Word.Visibility = Visibility.Collapsed;
            Category.Visibility = Visibility.Collapsed;
            Description.Visibility = Visibility.Collapsed;
            Path.Visibility = Visibility.Collapsed;

            // Show the "Update" button if it's properly initialized and accessible
            if (UpdateButton != null)
            {
                UpdateButton.Visibility = Visibility.Collapsed;
                AbandonAddButton.Visibility= Visibility.Collapsed;
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
                if (word != "" && description != "" && category != "")
                {// Call the AddWord method to add the word
                    words.AddWord(word, description, category, imagePath);


                    // Optionally, you can show a message box to indicate success
                    MessageBox.Show("Word added successfully!");
                    RetrieveVisabilityAdd(sender, e);
                }
                else
                {
                    MessageBox.Show("Invalid word!");
                }

                // Optionally, you can clear the text boxes after adding the word
                textBoxWord.Text = "";
                textBoxDescription.Text = "";
                textBoxCategory.Text = "";
                textBoxImagePath.Text = "\"C:\\Users\\zinca\\OneDrive\\Desktop\\anul2\\sem2\\MAP\\tema 1-dictionar\\dictionary\\dictionary\\bin\\Debug\\images\\delault_image.jpg\"";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            textBoxDeletedWord.Visibility = Visibility.Visible;

            // Show the "Update" button if it's properly initialized and accessible
            if (DeleteButton != null)
            {
                DeleteButton.Visibility = Visibility.Visible;
                AbandonDeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Handle the case where the UpdateButton is not properly initialized
                MessageBox.Show("UpdateButton is not properly initialized.");
            }
        }

        private void RetrieveVisabilityDelete(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            textBoxDeletedWord.Visibility = Visibility.Collapsed;

            // Show the "Update" button if it's properly initialized and accessible
            if (DeleteButton != null)
            {
                DeleteButton.Visibility = Visibility.Collapsed;
                AbandonDeleteButton.Visibility= Visibility.Collapsed;
            }
            else
            {
                // Handle the case where the UpdateButton is not properly initialized
                MessageBox.Show("DeleteButton is not properly initialized.");
            }
        }


        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve the word to be deleted from the textbox
                string deletedWord = textBoxDeletedWord.Text;

                // Read the list of words from the JSON file
                List<Words> wordsList = Words.ReadWordsFromJson();

                // Find the word to delete in the wordsList
                Words wordToDelete = wordsList.FirstOrDefault(w => w.Word.Equals(deletedWord, StringComparison.OrdinalIgnoreCase));

                if (wordToDelete != null)
                {
                    // Remove the word from the wordsList
                    wordsList.Remove(wordToDelete);

                    // Write the updated list of words back to the JSON file
                    Words.WriteWordsToJson(wordsList);

                    // Optionally, you can show a message box to indicate success
                    MessageBox.Show("Word deleted successfully!");
                    RetrieveVisabilityDelete(sender, e);
                }
                else
                {
                    MessageBox.Show("Word does not exist in the list.");
                }

                // Optionally, you can clear the textbox after deleting the word
                textBoxDeletedWord.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void AbandonAddButton_Click(object sender, RoutedEventArgs e)
        {
            RetrieveVisabilityAdd(sender, e);

        }

        private void AbandonDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RetrieveVisabilityDelete(sender, e);

        }

        private void AbandonModifyWord_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
