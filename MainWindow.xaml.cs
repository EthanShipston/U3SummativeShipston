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
            placeholderWord = " ";
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
                    string preLetter = "";
                    string postLetter = "";
                    int location = word.IndexOf(txtInput.Text, 1);
                    if (location == 0)
                    {
                        postLetter = word.Substring(word.IndexOf(txtInput.Text) + 1, word.Length - preLetter.Length);
                        MessageBox.Show(postLetter);
                    }
                    else if (location == word.Length)
                    {
                        preLetter = word.Substring(0, word.IndexOf(txtInput.Text) - 1);
                        MessageBox.Show(preLetter);
                    }
                    else
                    {
                        preLetter = word.Substring(0, word.IndexOf(txtInput.Text) - 1);
                        MessageBox.Show(preLetter);
                        postLetter = word.Substring(word.IndexOf(txtInput.Text) + 1, word.Length - preLetter.Length + 1);
                        MessageBox.Show(postLetter);
                    }

                    placeholderWord = "";

                    for (int I = 0; I < preLetter.Length * 2; I++)
                    {
                        placeholderWord += "_ ";
                    }
                    placeholderWord += txtInput.Text;
                    for (int I = 0; I < postLetter.Length * 2; I++)
                    {
                        placeholderWord += " _";
                    }
                    lblOutput.Content = placeholderWord;
                    MessageBox.Show("correct");
                }
                else
                {
                    MessageBox.Show("Lame");
                    guesses++;
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //get working
        }
    }
}
