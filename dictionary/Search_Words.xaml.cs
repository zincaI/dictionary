using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace dictionary
{
    public partial class Search_Words : Window
    {
        public Search_Words()
        {
            InitializeComponent();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //var words = new List<string>(); 
            string searchedWords = SearchBar.Text;
            //foreach (var word in words)
            //{

            //}
        }

        public void PopulateWordDetails(string searchedWord)
        {
            try
            {
                // Read the list of words from the JSON file
                List<Words> wordsList = Words.ReadWordsFromJson();

                // Clear the TextBlock's content
                SearchedWord.Text = "";

                // Find the word in the list
                var word = wordsList.FirstOrDefault(w => w.Word.Equals(searchedWord, StringComparison.OrdinalIgnoreCase));

                if (word != null)
                {
                    // Update the TextBlock with word details
                    SearchedWord.Text = $"Word: {word.Word}\nCategory: {word.Category}\nDescription: {word.Description}\n";

                    // Load and display image
                    if (!string.IsNullOrEmpty(word.ImagePath))
                    {
                        // Assuming you have an Image control named ImageDisplay
                        ImageDisplay.Source = new BitmapImage(new Uri(word.ImagePath, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        // Clear the image if no image path is provided
                        ImageDisplay.Source = null;
                    }
                }
                else
                {
                    MessageBox.Show("Word not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading from the JSON file: " + ex.Message);
            }
        }

        private void SearchWord_Click(object sender, RoutedEventArgs e)
        {
            string searchedWord = SearchBar.Text.Trim();

            if (!string.IsNullOrEmpty(searchedWord))
            {
                // Call the PopulateWordDetails function with the searched word
                PopulateWordDetails(searchedWord);
            }
            else
            {
                MessageBox.Show("Please enter a word to search.");
            }
        }

        private void SearchBar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }


}
