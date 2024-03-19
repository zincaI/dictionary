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
using System.Windows.Shapes;

namespace dictionary
{
   
    public partial class Entertainment : Window
    {
        public Entertainment()
        {
            InitializeComponent();
        }

        public void Start_Click(object sender, RoutedEventArgs e)
        {
            int numberOfRounds = 5;

            RoundGame roundGame = new RoundGame(numberOfRounds);
            roundGame.Show();
            Close(); 
        }
    }
}
