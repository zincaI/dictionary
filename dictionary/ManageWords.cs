using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

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

                // Clear the ListBox's Items
                wordListBox.Items.Clear();

                // Populate the ListBox with words and images
                foreach (var word in wordsList)
                {
                    StackPanel wordPanel = new StackPanel();

                    // TextBlock for word details
                    TextBlock wordTextBlock = new TextBlock();
                    wordTextBlock.Text = $"Word: {word.Word}\nCategory: {word.Category}\nDescription: {word.Description}\n";
                    wordPanel.Children.Add(wordTextBlock);

                    // Image
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    BitmapImage bitmapImage = new BitmapImage(new Uri(word.ImagePath)); // Assuming 'ImagePath' property holds the path of the image
                    image.Source = bitmapImage;
                    image.Width = 100; // Set width as needed
                    image.Height = 100; // Set height as needed
                    wordPanel.Children.Add(image);

                    wordListBox.Items.Add(wordPanel);
                }
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
            if (AddButton != null)
            {
                AddButton.Visibility = Visibility.Visible;
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
            if (AddButton != null)
            {
                AddButton.Visibility = Visibility.Collapsed;
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
                if (imagePath == "")
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Path.Combine(basePath, "images", "default_image.jpg");
                }
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
                textBoxImagePath.Text = "";
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            SearchBox.Visibility = Visibility.Visible;
            SearchText.Visibility = Visibility.Visible;
            textBoxWordUpdate.Visibility = Visibility.Visible;
            textBoxDescriptionUpdate.Visibility = Visibility.Visible;
            textBoxCategoryUpdate.Visibility = Visibility.Visible;
            textBoxImagePathUpdate.Visibility = Visibility.Visible;
            WordUpdate.Visibility = Visibility.Visible;
            CategoryUpdate.Visibility = Visibility.Visible;
            DescriptionUpdate.Visibility = Visibility.Visible;
            PathUpdate.Visibility = Visibility.Visible;
         


            if (UpdateButton != null && AbandonUpdateButton != null)
            {
                UpdateButton.Visibility = Visibility.Visible;
                AbandonUpdateButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Handle the case where the UpdateButton or AbandonUpdateButton is not properly initialized
                MessageBox.Show("UpdateButton or AbandonUpdateButton is not properly initialized.");
            }
        }

        private void UpdateWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve the content of the text boxes
                string updatedWord = textBoxWordUpdate.Text;
                string updatedDescription = textBoxDescriptionUpdate.Text;
                string updatedCategory = textBoxCategoryUpdate.Text;
                string updatedImagePath = textBoxImagePathUpdate.Text;
                string initialWord= SearchBox.Text;

                List<Words> wordsList = Words.ReadWordsFromJson();

                // Find the word to update in the wordsList
                Words selectedWord = wordsList.FirstOrDefault(w => w.Word.Equals(initialWord, StringComparison.OrdinalIgnoreCase));
                MessageBox.Show(initialWord);
                if (selectedWord != null)
                {
                    // Update the properties of the selected word
                    if (updatedDescription != null)
                        selectedWord.Description = updatedDescription;
                    if (updatedCategory != null)
                        selectedWord.Category = updatedCategory;
                    if (updatedImagePath != null)
                        selectedWord.ImagePath = updatedImagePath;
                    if (updatedWord != null)
                        selectedWord.Word = updatedWord;

                    // Write the updated list of words back to the JSON file
                    Words.WriteWordsToJson(wordsList);

                    // Optionally, you can show a message box to indicate success
                    MessageBox.Show("Word updated successfully!");
                    RetrieveVisabilityUpdate(sender, e);
                }
                else
                {
                    MessageBox.Show("Word not found for update.");
                }

                // Clear the text boxes after updating the word
                textBoxWordUpdate.Text = "";
                textBoxDescriptionUpdate.Text = "";
                textBoxCategoryUpdate.Text = "";
                textBoxImagePathUpdate.Text = "";
                SearchBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }




        private void RetrieveVisabilityUpdate(object sender, RoutedEventArgs e)
        {
            // Show the text boxes when the button is clicked
            textBoxWordUpdate.Visibility = Visibility.Collapsed;
            textBoxDescriptionUpdate.Visibility = Visibility.Collapsed;
            textBoxCategoryUpdate.Visibility = Visibility.Collapsed;
            textBoxImagePathUpdate.Visibility = Visibility.Collapsed;
            WordUpdate.Visibility = Visibility.Collapsed;
            CategoryUpdate.Visibility = Visibility.Collapsed;
            DescriptionUpdate.Visibility = Visibility.Collapsed;
            PathUpdate.Visibility = Visibility.Collapsed;
            SearchBox.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;

            // Show the "Update" button if it's properly initialized and accessible
            if (UpdateButton != null)
            {
                UpdateButton.Visibility = Visibility.Collapsed;
                AbandonUpdateButton.Visibility = Visibility.Collapsed;
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

        private void AbandonModifyWordButton_Click(object sender, RoutedEventArgs e)
        {
            RetrieveVisabilityUpdate(sender, e);
        }

    }
}
