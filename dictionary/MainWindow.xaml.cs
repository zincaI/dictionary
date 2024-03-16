﻿using System;
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

        private void SearchWords_Click(object sender, EventArgs e)
        {
            // Instantiate SecondForm
            Search_Words window = new Search_Words();

            // Display SecondForm
            window.Show();
        }

        private void Entertainment_Click(object sender, EventArgs e)
        {
            // Instantiate SecondForm
            Entertainment startGame = new Entertainment();

            // Display SecondForm
            startGame.Show();
        }
    }


}
