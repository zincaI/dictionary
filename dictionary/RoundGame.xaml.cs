﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace dictionary
{
    /// <summary>
    /// Interaction logic for RoundGame.xaml
    /// </summary>
    public partial class RoundGame : Window
    {
        List<Words> wordsList = Words.ReadWordsFromJson();

        bool correct/*,guessed=false*/;
        UInt16 score = 0;
        UInt16 rounds = 0;
        private int roundNumber;


        //public RoundGame()
        //{
        //    InitializeComponent();

        //    // Obține cuvântul aleatoriu
        //    Words randomWord = GetRandomWord(wordsList);


        //    // Actualizează conținutul Label-ului cu cuvântul ales aleatoriu
        //    //RandomWordLabel.Content = "Random Word: " + randomWord.Word;

        //    string guess = Guess.Text;


        //    // Deschide fereastra de joc de 5 ori
        //    for (int i = 0; i < 5; i++)
        //    {
        //        // Incrementăm numărul de runde
        //        rounds++;

        //        // Afișăm fereastra de joc și așteptăm închiderea ei
        //        RoundGame gameWindow = new RoundGame();

        //        gameWindow.ShowDialog();

        //        // Verificăm dacă a fost corect ghicit cuvântul în fereastra de joc
        //        if (IsCorrect(guess))
        //        {
        //            // Incrementăm scorul
        //            score++;
        //        }

        //        // Actualizăm etichetele pentru scor și numărul de runde
        //        ScoreLabel.Content = "Score: " + score;
        //        RoundLabel.Content = "Rounds: " + rounds;
        //    }

        //    // Afisam un mesaj cu scorul final
        //    MessageBox.Show("Final score: " + score);
        //}


        //public bool IsCorrect(string guess)
        //{
        //    Words randWord = GetRandomWord(wordsList);
        //    if(guess==randWord.Word)
        //        return true;
        //    return false;
        //}

        private Words GetRandomWord(List<Words> wordsList)
        {
            // Verifică dacă lista de cuvinte este goală
            if (wordsList.Count == 0)
            {
                return null;
            }

            // Generează un index aleatoriu între 0 și lungimea listei de cuvinte - 1
            Random random = new Random();
            int randomIndex = random.Next(0, wordsList.Count);

            // Returnează cuvântul de la indexul generat aleatoriu
            return wordsList[randomIndex];
        }

        private int GenerateRandomNumber()
        {
            // Creează o instanță a clasei Random
            Random random = new Random();

            // Generează un număr întreg aleatoriu între 1 și 2 (inclusiv 1, fără 3)
            return random.Next(1, 3);
        }

        private int remainingRounds;

        public RoundGame(int numberOfRounds)
        {
            InitializeComponent();
            remainingRounds = numberOfRounds;
            int option = GenerateRandomNumber();
            Words randomWord = GetRandomWord(wordsList);
            string path;
            string imagesDirectory = Environment.CurrentDirectory + "\\images\\";

            path = imagesDirectory + "delaut.jpg";

            if (randomWord.ImagePath != path)
                option = GenerateRandomNumber();
            else
            {
                option = 1;
            }

            if (option == 1)
                {
                    RandomWordLabel.Visibility = Visibility.Visible;
                    RandomWordLabel.Text =randomWord.Description;
                }
                else
                {
                    ImageDisplay.Visibility = Visibility.Visible;
                    ImageDisplay.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(randomWord.ImagePath, UriKind.RelativeOrAbsolute));

                }
            

        }

        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            remainingRounds--;

            if (remainingRounds == 0)
            {
                Next.Visibility = Visibility.Collapsed;
                Finish.Visibility = Visibility.Visible;
            }

            RoundNumberLabel.Content = "Round: " + ( 5-remainingRounds);
            ScoreLabel.Content = "Score: " + score;
            RandomWordLabel.Visibility = Visibility.Collapsed;
            ImageDisplay.Visibility= Visibility.Collapsed;

            int option = GenerateRandomNumber();
            Words randomWord = GetRandomWord(wordsList);

            string path;
            string imagesDirectory = Environment.CurrentDirectory + "\\images\\";

            path = imagesDirectory + "delaut.jpg";

            if (randomWord.ImagePath != path)
                option = GenerateRandomNumber();
            else
            {
                option = 1;
            }

            if (option == 1)
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

        private void FinishRound_Click(object sender, RoutedEventArgs e)
        {
           
            Close(); 
        }


    }
}