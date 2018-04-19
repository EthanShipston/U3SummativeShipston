/* Ethan Shipston
 * u3SummativeShipston
 * 4/17/2018
 * This program is a fun game of hangman
 */
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

namespace u3SummativeShipston
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int guesses = 0;
        string word = "Nothing";
        string placeholderWord = "";
        Random r = new Random();
        public MainWindow()
        {
            ChooseWord();
            InitializeComponent();
            lblOutput.Content = GetPlaceholderWord();
        }

        private void ChooseWord()
        {
            System.IO.StreamReader streamReader = new System.IO.StreamReader("Words.txt");
            try
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line.Contains(r.Next(60).ToString()))
                    {
                        line = line.Remove(0, line.IndexOf(" ") + 1);
                        word = line;
                        streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetPlaceholderWord()
        {
            int length = word.Length;
            for (int i = 0; i <= length; i++)
            {
                placeholderWord += "_ ";
            }
            return placeholderWord;
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            int txtInt = txtInput.Text.Length;
            if (txtInt == 1)
            {
                if (word.Contains(txtInput.Text))
                {
                    int location = word.IndexOf(txtInput.Text, 1);
                    for (int i = 0; i > placeholderWord.Length; i++)
                    {
                        placeholderWord = placeholderWord.Remove(placeholderWord.IndexOf(" "));
                    }
                }
                else
                {
                    MessageBox.Show("Lame");
                    guesses++;
                }
            }
        }
    }
}
