using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace dictionary
{
    public partial class Search_Words : Window
    {
        private List<string> wordList;
        private List<Words> wordsList;

        public Search_Words()
        {
            InitializeComponent();
            LoadWordListFromJson();
            //SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void LoadWordListFromJson()
        {
            try
            {
                wordsList = Words.ReadWordsFromJson(); // Modificare aici
                wordList = wordsList.Select(word => word.Word).ToList();
                SearchBar.ItemsSource = wordList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading words from JSON file: " + ex.Message);
            }
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            // Filtrăm lista de cuvinte
            var filteredWords = wordList.Where(word => word.ToLower().StartsWith(searchText)).ToList();

            // Actualizăm lista afișată în ListBox
            SearchBar.ItemsSource = filteredWords;
        }

        private void SearchBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedWord = SearchBar.SelectedItem as string;
            if (selectedWord != null)
            {
                PopulateWordDetails(selectedWord);
            }
        }

        private void SearchWord_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBar.SelectedItem != null)
            {
           
                string searchedWord = SearchBar.SelectedItem.ToString();
                PopulateWordDetails(searchedWord);
            }
            else
            {
                MessageBox.Show("No word selected.");
            }
        }

        private void PopulateWordDetails(string searchedWord)
        {
            try
            {
                

                var word = wordsList.FirstOrDefault(w => w.Word.Equals(searchedWord, StringComparison.OrdinalIgnoreCase));
                

                if (word != null)
                {
                    SearchedWord.Text = $"Word: {word.Word}\nCategory: {word.Category}\nDescription: {word.Description}\n";
                    if (!string.IsNullOrEmpty(word.ImagePath))
                    {
                        ImageDisplay.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(word.ImagePath, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
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
                MessageBox.Show("An error occurred while populating word details: " + ex.Message);
            }
        }
    }
}
