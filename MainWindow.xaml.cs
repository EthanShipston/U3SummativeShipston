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
        string word = "";
        string placeHolderWord = "";
        Random r = new Random();
        string strInput = "";
        public MainWindow()
        {
            getNewWord();
            InitializeComponent();
            lblOutput.Content = GetPlaceholderWord();
        }

        private void getNewWord()
        {
            System.IO.StreamReader streamReader = new System.IO.StreamReader("Words.txt");
            try
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    //if (line.Contains(r.Next(60).ToString()))
                    if (line.Contains(3.ToString()))
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
                placeHolderWord += " _";
            }
            return placeHolderWord;
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            strInput = txtInput.Text;
            if (word.Contains(txtInput.Text))
            {
                if (strInput.Length > 1)
                {
                    if (strInput == word)
                    {
                        lblOutput.Content = strInput;
                        MessageBox.Show("Congratulations! You win!" + "\n" + "You guessed " + guesses.ToString() + " times!");
                        guesses = 0;
                        getNewWord();
                        placeHolderWord = "";
                        GetPlaceholderWord();
                        lblOutput.Content = placeHolderWord;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect!");
                        guesses++;
                    }
                }

                else if (word.Contains(strInput))
                {
                    if (lblLettersGuessed.Content.ToString().Substring(17).Contains(strInput))
                    {
                        MessageBox.Show("Letter already guessed!");
                    }
                    else
                    {
                        string preLetter = placeHolderWord.Substring(0, word.IndexOf(strInput) * 2);
                        MessageBox.Show(preLetter);
                        string postLetter = placeHolderWord.Substring(word.IndexOf(strInput) * 2 + 2, placeHolderWord.Length - preLetter.Length - 2);
                        MessageBox.Show(preLetter + strInput + postLetter);
                        placeHolderWord = preLetter + strInput + postLetter;
                        MessageBox.Show(placeHolderWord);
                        lblLettersGuessed.Content += strInput + ", ";
                        lblOutput.Content = placeHolderWord;
                    }
                }
                else
                {
                    MessageBox.Show("You cannot leave the input blank!");
                }
            }
            else
            {
                MessageBox.Show("Incorrect!");
                guesses++;
                if (guesses == 10)
                {
                    MessageBox.Show("You Lose!");
                    getNewWord();
                    GetPlaceholderWord();

                }
            }
        }
    }
}
