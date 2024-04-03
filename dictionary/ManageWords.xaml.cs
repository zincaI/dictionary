using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ManageWords()
        {
            InitializeComponent();
            PopulateCategoryListBox(); 
        }

        string imagesDirectory = Environment.CurrentDirectory + "\\images\\";
        


       
        private bool IsAbsolutePath(string path)
        {
            return !string.IsNullOrEmpty(path) && (path[0] == '/' || path[0] == '\\' || path.Contains(":/"));
        }

        public string BuildImagePath(string path)
        {
            return imagesDirectory + path;
        }

        public void BuildImagePaths(List<Words> list)
        {
            for(int i=0;i< list.Count;i++)
            {
                BuildImagePath(list[i].ImagePath);
            }
        }


        public void PopulateWordListBox()
        {
            try
            {
                List<Words> wordsList = Words.ReadWordsFromJson();

                BuildImagePaths(wordsList);

                PopulateCategoryList(wordsList);

                wordListBox.Items.Clear();

                foreach (var word in wordsList)
                {
                    StackPanel wordPanel = new StackPanel();
                    TextBlock wordTextBlock = new TextBlock();
                    wordTextBlock.Text = $"Word: {word.Word}\nCategory: {word.Category}\nDescription: {word.Description}\n";
                    wordPanel.Children.Add(wordTextBlock);
                    string imageCompletePath = imagesDirectory + word.ImagePath;
                    imageCompletePath = imageCompletePath.Replace("\\", "\\\\");

                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    BitmapImage bitmapImage = new BitmapImage(new Uri(imageCompletePath)); 
                    image.Source = bitmapImage;
                    image.Width = 100; 
                    image.Height = 100; 
                    wordPanel.Children.Add(image);

                    wordListBox.Items.Add(wordPanel);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading from the JSON file: " + ex.Message);
            }
        }



        private List<string> categories = new List<string>();

        private void PopulateCategoryList(List<Words> wordsList)
        {
            foreach (Words word in wordsList)
            {
                categories.Add(word.Category);
            }
        }

        List<Words> wordsList = Words.ReadWordsFromJson();

        private void PopulateCategoryListBox()
        {
            listBoxCategory.Items.Clear();

            PopulateCategoryList(wordsList);

            foreach (string category in categories)
            {
                if (!listBoxCategory.Items.Contains(category))
                {
                    listBoxCategory.Items.Add(category);
                }
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            textBoxWord.Visibility = Visibility.Visible;
            textBoxDescription.Visibility = Visibility.Visible;
            listBoxCategory.Visibility = Visibility.Visible;
            textBoxNewCategory.Visibility = Visibility.Visible;
            textBoxImagePath.Visibility = Visibility.Visible;
            Word.Visibility = Visibility.Visible;
            Category.Visibility = Visibility.Visible;
            Description.Visibility = Visibility.Visible;
            Path.Visibility = Visibility.Visible;

            PopulateCategoryListBox();

            if (AddButton != null && AbandonAddButton != null)
            {
                AddButton.Visibility = Visibility.Visible;
                AbandonAddButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("AddButton or AbandonAddButton are not properly initialized.");
            }
        }


        private void RetrieveVisabilityAdd(object sender, RoutedEventArgs e)
        {
            
            textBoxWord.Visibility = Visibility.Collapsed;
            textBoxDescription.Visibility = Visibility.Collapsed;
            listBoxCategory.Visibility = Visibility.Collapsed;
            textBoxNewCategory.Visibility = Visibility.Collapsed;
            textBoxImagePath.Visibility = Visibility.Collapsed;
            Word.Visibility = Visibility.Collapsed;
            Category.Visibility = Visibility.Collapsed;
            Description.Visibility = Visibility.Collapsed;
            Path.Visibility = Visibility.Collapsed;

            
            if (AddButton != null)
            {
                AddButton.Visibility = Visibility.Collapsed;
                AbandonAddButton.Visibility = Visibility.Collapsed;
            }

        }

        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string word = textBoxWord.Text;
                string description = textBoxDescription.Text;
                string categoryText = listBoxCategory.SelectedItem?.ToString() ?? textBoxNewCategory.Text;
                string categoryList = listBoxCategory.SelectedItem?.ToString();

                string imagePath = textBoxImagePath.Text;
                string category;

                if(categoryText!="")
                    category=categoryText;
                else category=categoryList;

                
                if (!IsAbsolutePath(imagePath))
                {
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        imagePath = "delault_image.jpg";
                    }
                    
                    //imagePath = imagesDirectory + imagePath;
                    //imagePath = imagePath.Replace("\\", "\\\\");
                }

                
                Words words = new Words();
                if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(category))
                {
                    
                    words.AddWord(word, description, category, imagePath);
                    MessageBox.Show("Word added successfully!");
                    RetrieveVisabilityAdd(sender, e);
                    PopulateWordListBox();

                    if (!string.IsNullOrEmpty(categoryText))
                        categories.Add(categoryText);
                    PopulateCategoryListBox();

                }
                else
                {
                    MessageBox.Show("Invalid word!");
                }

               
                textBoxWord.Text = "";
                textBoxDescription.Text = "";
                textBoxImagePath.Text = "";
                textBoxNewCategory.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            textBoxDeletedWord.Visibility = Visibility.Visible;
            if (DeleteButton != null)
            {
                DeleteButton.Visibility = Visibility.Visible;
                AbandonDeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("UpdateButton is not properly initialized.");
            }
        }

        private void RetrieveVisabilityDelete(object sender, RoutedEventArgs e)
        {
            
            textBoxDeletedWord.Visibility = Visibility.Collapsed;
            if (DeleteButton != null)
            {
                DeleteButton.Visibility = Visibility.Collapsed;
                AbandonDeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("DeleteButton is not properly initialized.");
            }
        }


        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string deletedWord = textBoxDeletedWord.Text;

                List<Words> wordsList = Words.ReadWordsFromJson();

               
                Words wordToDelete = wordsList.FirstOrDefault(w => w.Word.Equals(deletedWord, StringComparison.OrdinalIgnoreCase));

                if (wordToDelete != null)
                {
                    
                    wordsList.Remove(wordToDelete);
                    Words.WriteWordsToJson(wordsList);

                    MessageBox.Show("Word deleted successfully!");
                    RetrieveVisabilityDelete(sender, e);
                    PopulateWordListBox();

                }
                else
                {
                    MessageBox.Show("Word does not exist in the list.");
                }

                textBoxDeletedWord.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            
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
            listBoxCategory.Visibility = Visibility.Visible;




            if (UpdateButton != null && AbandonUpdateButton != null)
            {
                UpdateButton.Visibility = Visibility.Visible;
                AbandonUpdateButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("UpdateButton or AbandonUpdateButton is not properly initialized.");
            }
        }

        private void UpdateWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string updatedWord = textBoxWordUpdate.Text;
                string updatedDescription = textBoxDescriptionUpdate.Text;
                string updateCategoryText = listBoxCategory.SelectedItem?.ToString() ?? textBoxCategoryUpdate.Text;
                string updateCategoryList = listBoxCategory.SelectedItem?.ToString();
                string updatedImagePath = textBoxImagePathUpdate.Text;
                string initialWord = SearchBox.Text;
               

                List<Words> wordsList = Words.ReadWordsFromJson();

                Words selectedWord = wordsList.FirstOrDefault(w => w.Word.Equals(initialWord, StringComparison.OrdinalIgnoreCase));

                if (selectedWord != null)
                {
                    if (updatedDescription != "")
                        selectedWord.Description = updatedDescription;
                    if (updateCategoryText != "")
                        selectedWord.Category = updateCategoryText;
                    if (!string.IsNullOrEmpty(updateCategoryList))
                        selectedWord.Category = updateCategoryList;
                    if (updatedImagePath != "")
                    {
                        if (!IsAbsolutePath(updatedImagePath))
                        {
                            if (updatedImagePath == "")
                                updatedImagePath = "delault_image.jpg";
                            //updatedImagePath = imagesDirectory + updatedImagePath;
                            updatedImagePath = updatedImagePath.Replace("\\", "\\\\");
                            selectedWord.ImagePath = updatedImagePath;



                        }
                    }
                    if (updatedWord != "")
                        selectedWord.Word = updatedWord;


                    Words.WriteWordsToJson(wordsList);

                    MessageBox.Show("Word updated successfully!");
                    RetrieveVisabilityUpdate(sender, e);
                    PopulateWordListBox();
                    if (updateCategoryText != "")
                    { 
                        categories.Add(updateCategoryText);
                        PopulateCategoryListBox(); 
                    }

                }
                else
                {
                    MessageBox.Show("Word not found for update.");
                }

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
            listBoxCategory.Visibility = Visibility.Collapsed;


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
