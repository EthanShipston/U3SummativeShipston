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
        //Global Variables
        int guesses = 0;
        string word = "";
        string placeHolderWord = "";
        Random r = new Random();
        string strInput = "";

        public MainWindow()
        {
            getNewWord();
            InitializeComponent();
            txtOutput.Text = GetPlaceholderWord();
        }
        private void getNewWord()         //Collects word from word list
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

        private string GetPlaceholderWord() //Creates string with "_ " in place of each character in the selected word
        {
            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                placeHolderWord += " _";
            }
            return placeHolderWord;
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            strInput = txtInput.Text; //Input to a string for simplification
            if (lblLettersGuessed.Content.ToString().Substring(17).Contains(strInput))
            {
                MessageBox.Show("Letter already guessed!");
            }
            else if (word.Contains(txtInput.Text))
            {
                if (strInput.Length > 1)
                {
                    if (strInput == word) //Distributes victory if word guess is correct
                    {
                        txtOutput.Text = strInput;
                        MessageBox.Show("Congratulations! You win!" + "\n" + "You guessed " + guesses.ToString() + " times!");
                        guesses = 0;
                        getNewWord();
                        placeHolderWord = "";
                        //resets word
                        GetPlaceholderWord();
                        txtOutput.Text = placeHolderWord;
                    }
                    else //informs of an Incorrect guess
                    {
                        MessageBox.Show("Incorrect!");
                        guesses++;
                    }
                }
                else if (word.Contains(strInput))
                {
                    lblLettersGuessed.Content += strInput + ", ";
                    string tempWord = "";
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word.Substring(i, 1) == strInput)
                        {
                            tempWord += " " + strInput;
                        }
                        else
                        {
                            tempWord += placeHolderWord.Substring(i * 2, 2);
                        }
                    }
                    txtOutput.Text = tempWord;
                    placeHolderWord = tempWord;
                }
                else
                {
                    MessageBox.Show("You cannot leave the input blank!");
                }
            }
            else
            {
                lblLettersGuessed.Content += strInput + ", ";
                MessageBox.Show("Incorrect!");
                guesses++;
                if (guesses == 10) //lose condition
                {
                    MessageBox.Show("You Lose!");
                    getNewWord();
                    GetPlaceholderWord();
                    txtOutput.Text = placeHolderWord;
                    lblLettersGuessed.Content.ToString().Remove(lblLettersGuessed.Content.ToString().Substring(17).Length, lblLettersGuessed.Content.ToString().Length - lblLettersGuessed.Content.ToString().Substring(17).Length);
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
            placeHolderWord = "";
            MessageBox.Show("You Reset!");
            getNewWord();
            GetPlaceholderWord();
            txtOutput.Text = placeHolderWord;
            string tempString = lblLettersGuessed.Content.ToString().Substring(0, 17);
            lblLettersGuessed.Content = tempString;
        }
    }
}