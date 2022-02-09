using System;
using System.Drawing;
using System.Windows.Forms;

namespace WPA_GuessAWord
{
    public partial class Form1 : Form
    {
        //===================================================================//
        //                          Setup Game Screen                        //
        //===================================================================//
        public Form1()
        {
            InitializeComponent();
            SetLetterButtonsActive(false);
            EnableNewWordPick(false);
        }
      
        string[] wordsArray = new string[10];
        string[] animals = { 
            "lew",
            "tygrys",
            "skunks",
            "żyrafa",
            "niedźwiedź",
            "słoń",
            "jeleń",
            "leniwiec",
            "nosorożec",
            "hipopotam"
        };
        string[] names = { 
            "Ryszard",
            "Alicja",
            "Witold",
            "Bogumił",
            "Aleksandra",
            "Władysław",
            "Danka",
            "Gargamel",
            "Martyna",
            "Piotr"
        };
        string[] countries = { 
            "Austria",
            "Ghana",
            "Turkmenistan",
            "Kanada",
            "Słowacja",
            "Słowenia",
            "Estonia",
            "Grecja",
            "Rosja",
            "Finlandia"
        };

        string newWord;
        string hiddenText = "";
        string wordToGuess;

        int wordLength;
        int lives;

        void SetLetterButtonsActive(bool state)
        {
            button1.Enabled = state;
            button2.Enabled = state;
            button3.Enabled = state;
            button4.Enabled = state;
            button5.Enabled = state;
            button6.Enabled = state;
            button7.Enabled = state;
            button8.Enabled = state;
            button9.Enabled = state;
            button10.Enabled = state;
            button11.Enabled = state;
            button12.Enabled = state;
            button13.Enabled = state;
            button14.Enabled = state;
            button15.Enabled = state;
            button16.Enabled = state;
            button17.Enabled = state;
            button18.Enabled = state;
            button19.Enabled = state;
            button20.Enabled = state;
            button21.Enabled = state;
            button22.Enabled = state;
            button23.Enabled = state;
            button24.Enabled = state;
            button25.Enabled = state;
            button26.Enabled = state;
            button27.Enabled = state;
            button28.Enabled = state;
            button29.Enabled = state;
            button30.Enabled = state;
            button31.Enabled = state;
            button32.Enabled = state;
        }

        void EnableNewWordPick(bool state)
        {
            button33.Enabled = state;
        }

        //===================================================================//
        //                       Pick Game Categorty                         //
        //===================================================================//
        private void animalsButton_Click(object sender, EventArgs e)
        {
            wordsArray = animals;
            SetChoiceButtonsColor(animalsButton, Color.Aquamarine);
            SetChoiceButtonsState(false);
            EnableNewWordPick(true);
        }

        private void namesButton_Click(object sender, EventArgs e)
        {
            wordsArray = names;
            SetChoiceButtonsColor(namesButton, Color.Aquamarine);
            SetChoiceButtonsState(false);
            EnableNewWordPick(true);
        }

        private void countriesButton_Click(object sender, EventArgs e)
        {
            wordsArray = countries;
            SetChoiceButtonsColor(countriesButton, Color.Aquamarine);
            SetChoiceButtonsState(false);
            EnableNewWordPick(true);
        }

        private void SetChoiceButtonsColor(Button button, Color color)
        {
            button.BackColor = color;
        }

        private void SetChoiceButtonsState(bool state)
        {

            animalsButton.Enabled = state;
            namesButton.Enabled = state;
            countriesButton.Enabled = state;
        }

        //===================================================================//
        //                          Start playing                            //
        //===================================================================//
        private void button33_Click(object sender, EventArgs e)
        {
            PickRandomWord();
            HideWord();
            SetTheDifficulty();
            SetLetterButtonsActive(true);
            EnableNewWordPick(false);
        }
        void PickRandomWord()
        {
            Random ranIndex = new Random();
            int index = ranIndex.Next(0, wordsArray.Length);
            newWord = wordsArray[index].ToUpper();

            wordLength = newWord.Length;
        }
        void HideWord()
        {
            
            for (int i = 0; i < wordLength; i++)
            {
                hiddenText = hiddenText.Insert(i, "-");
            }
            textBox1.Text = hiddenText;
        }
        void SetTheDifficulty()
        {
            if (wordLength <= 5)
            {
                lives = 3;
            }
            else
            {
                lives = 5;
            }
            textBox2.Text = lives.ToString();
        }


        void CheckForLetter(string chosenWord, string chosenLetter, Button chosenButton)
        {
            wordToGuess = textBox1.Text;
            string newWordCharacter; 
            int searchIndex = 0;
            int searchResult;
            int guessCount = 0;
            for (int i = 0; i < wordLength; i++)
            {
                // look for chosen letter within hidden word
                searchResult = newWord.IndexOf(chosenLetter, searchIndex, wordLength - searchIndex);

                if (searchResult >= 0) // if search successfull
                {
                    searchIndex = searchResult + 1; // to start new search of the same occurance from that index
                    newWordCharacter = newWord.Substring(searchResult, 1);

                    // replace " - " with found letters
                    wordToGuess = wordToGuess.Remove(searchResult, 1);
                    wordToGuess = wordToGuess.Insert(searchResult, newWordCharacter);
                   
                    guessCount++;
                }
                
            } 

            //=========== update and display lives ==========================//
            if (guessCount == 0) 
            {
                lives--; 
                textBox2.Text = lives.ToString(); 
            }
            //========== display defeat message, start new level ============//
            if (lives == 0)    
            {
                MessageBox.Show($"You lost!!! Correct word was: {newWord}");
                StartNextRound();
            }
            //========== display win message, start new level ===============//
            else
            {   
                textBox1.Text = wordToGuess;
                if (textBox1.Text.Equals(newWord))
                {
                    MessageBox.Show("Congratulations you have won!");
                    StartNextRound();
                }
            }
        }

        void StartNextRound()
        {
            hiddenText = "";
            SetChoiceButtonsState(true, Color.Gray);
            SetLetterButtonsActive(false);
        }
       
        private void SetChoiceButtonsState(bool state, Color color)
        {
            animalsButton.Enabled = state;
            animalsButton.BackColor = color;
            namesButton.Enabled = state;
            namesButton.BackColor = color;
            countriesButton.Enabled = state;
            countriesButton.BackColor = color;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button1.Text.ToUpper(), button1);
            //button1.BackColor = Color.DarkGray;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button2.Text.ToUpper(), button2);
            //button2.BackColor = Color.DarkGray;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button3.Text.ToUpper(), button3);
            //button3.BackColor = Color.DarkGray;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button4.Text.ToUpper(), button4);
            //button4.BackColor = Color.DarkGray;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button5.Text.ToUpper(), button5);
            //button5.BackColor = Color.DarkGray;
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button6.Text.ToUpper(), button6);
            //button6.BackColor = Color.DarkGray;
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button7.Text.ToUpper(), button7);
            //button7.BackColor = Color.DarkGray;
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button8.Text.ToUpper(), button8);
            //button8.BackColor = Color.DarkGray;
            button8.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button9.Text.ToUpper(), button9);
            //button9.BackColor = Color.DarkGray;
            button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button10.Text.ToUpper(), button10);
            //button10.BackColor = Color.DarkGray;
            button10.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button11.Text.ToUpper(), button11);
            //button11.BackColor = Color.DarkGray;
            button11.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button12.Text.ToUpper(), button12);
            //button12.BackColor = Color.DarkGray;
            button12.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button13.Text.ToUpper(), button13);
            //button13.BackColor = Color.DarkGray;
            button13.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button14.Text.ToUpper(), button14);
            //button14.BackColor = Color.DarkGray;
            button14.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button15.Text.ToUpper(), button15);
            //button15.BackColor = Color.DarkGray;
            button15.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button16.Text.ToUpper(), button16);
            //button16.BackColor = Color.DarkGray;
            button16.Enabled = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button17.Text.ToUpper(), button17);
            //button17.BackColor = Color.DarkGray;
            button17.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button18.Text.ToUpper(), button18);
            //button18.BackColor = Color.DarkGray;
            button18.Enabled = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button19.Text.ToUpper(), button19);
            //button19.BackColor = Color.DarkGray;
            button19.Enabled = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button20.Text.ToUpper(), button20);
            //button20.BackColor = Color.DarkGray;
            button20.Enabled = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button21.Text.ToUpper(), button21);
            //button21.BackColor = Color.DarkGray;
            button21.Enabled = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button22.Text.ToUpper(), button22);
            //button22.BackColor = Color.DarkGray;
            button22.Enabled = false;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button23.Text.ToUpper(), button23);
            //button23.BackColor = Color.DarkGray;
            button23.Enabled = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button24.Text.ToUpper(), button24);
            //button24.BackColor = Color.DarkGray;
            button24.Enabled = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button25.Text.ToUpper(), button25);
            //button25.BackColor = Color.DarkGray;
            button25.Enabled = false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button26.Text.ToUpper(), button26);
            //button26.BackColor = Color.DarkGray;
            button26.Enabled = false;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button27.Text.ToUpper(), button27);
            //button27.BackColor = Color.DarkGray;
            button27.Enabled = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button28.Text.ToUpper(), button28);
            //button28.BackColor = Color.DarkGray;
            button28.Enabled = false;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button29.Text.ToUpper(), button29);
            //button29.BackColor = Color.DarkGray;
            button29.Enabled = false;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button30.Text.ToUpper(), button30);
            //button30.BackColor = Color.DarkGray;
            button30.Enabled = false;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button31.Text.ToUpper(), button31);
            //button31.BackColor = Color.DarkGray;
            button31.Enabled = false;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            CheckForLetter(newWord, button32.Text.ToUpper(), button32);
            //button32.BackColor = Color.DarkGray;
            button32.Enabled = false;
        }   
    }

    
}
