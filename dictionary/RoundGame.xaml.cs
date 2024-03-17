using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace dictionary
{
    public partial class RoundGame : Window
    {
        List<Words> wordsList = Words.ReadWordsFromJson();
        int score;
        private int remainingRounds;

        string path = Environment.CurrentDirectory + "\\images\\delault_image.jpg";
        //path = path.Replace("\\", "\\\\");

        Words randomWord ;

        public RoundGame(int numberOfRounds)
        {
            InitializeComponent();
            remainingRounds = numberOfRounds;
            score = 0;
            UpdateRound();
        }

        private void UpdateRound()
        {
            if(remainingRounds==5)
            path = path.Replace("\\", "\\\\");

            RandomWordLabel.Visibility = Visibility.Collapsed;
            ImageDisplay.Visibility = Visibility.Collapsed;

            remainingRounds--;

            Verify.Visibility = Visibility.Visible;

            if (remainingRounds == 0)
            {
                Next.Visibility = Visibility.Collapsed;
                Finish.Visibility = Visibility.Visible;
            }

            RoundNumberLabel.Content = "Round: " + (5 - remainingRounds);
            ScoreLabel.Content = "Score: " + score;

            randomWord = GetRandomWord(wordsList);

            MessageBox.Show(path);
            MessageBox.Show(randomWord.ImagePath);

            int option;
            if (randomWord.ImagePath != path)
                option = GenerateRandomNumber();
            else
            {
                option = 1;
            }

            if (option==1)
            {
                RandomWordLabel.Visibility = Visibility.Visible;
                RandomWordLabel.Text = randomWord.Description;
            }
            else
            {
                ImageDisplay.Visibility = Visibility.Visible;
                ImageDisplay.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(randomWord.ImagePath, UriKind.RelativeOrAbsolute));

            }
        }
        private int GenerateRandomNumber()
        {
            // Creează o instanță a clasei Random
            Random random = new Random();

            // Generează un număr întreg aleatoriu între 1 și 2 (inclusiv 1, fără 3)
            return random.Next(1, 3);
        }

        private Words GetRandomWord(List<Words> wordsList)
        {
           
            if (wordsList.Count == 0)
                return null;

            Random random = new Random();
            int randomIndex = random.Next(0, wordsList.Count);

            return wordsList[randomIndex];
        }

        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            Correct.Visibility = Visibility.Collapsed;
            Incorrect.Visibility = Visibility.Collapsed;
            UpdateRound();
        }

        private void FinishRound_Click(object sender, RoutedEventArgs e)
        {
            RoundNumberLabel.Content = "Round: " + (5 - remainingRounds);
            ScoreLabel.Content = "Score: " + score;
            Close();
        }

        private void Guess_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox; 
            string guess = textBox.Text;

            Verify.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrEmpty(guess) && guess == randomWord.Word)
            {
                Correct.Visibility = Visibility.Visible;
                Incorrect.Visibility = Visibility.Collapsed;
                score++;
                
            }
            else
            {
                Correct.Visibility = Visibility.Collapsed;
                Incorrect.Visibility = Visibility.Visible;
                MessageBox.Show("The correct word was:"+randomWord.Word);

            }

            ScoreLabel.Content = "Score: " + score;
        }

        private void GuessedButton_Click(object sender, RoutedEventArgs e)
        {
            if (remainingRounds > 0)
            {
                Next.Visibility = Visibility.Visible;

            }
            else
            {
                Finish.Visibility = Visibility.Visible; 
            ScoreLabel.Content = "Score: " + score;
            }
            Guess_TextChanged(Guess, null);
            if(remainingRounds==0)
            Verify.Visibility = Visibility.Collapsed;
        }

    }
}
