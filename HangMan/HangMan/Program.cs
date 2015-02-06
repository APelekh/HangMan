using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class Program
    {
        static bool playing = true;
        static int numberOfTries = 7;
        static Random rng = new Random();
        static string wordToGuess = String.Empty;
        static string guessedLetters = String.Empty;
        static string returnString = String.Empty;
        //creating a list of random words
        static List<string> wordsBank = new List<string>() { "Apple", "Window", "Pineapple" };
        
        static void Main(string[] args)
        {
            
            Game();
            
            
        }

        /// <summary>
        /// Runs the entire game
        /// </summary>
        static void Game()
        {
            //picking a random word from words list and saving it
            wordToGuess = wordsBank[rng.Next(0, wordsBank.Count)].ToLower();
            //welcome screen with game instructions
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();
            string greeting = "Hello player " + userName + " !\nYour task is to guess the word that displayed on the screen.\nYou can guess one letter or entire word at a time.";
            for (int i = 0; i < greeting.Length; i++)
            {
                Console.Write(greeting[i]);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine("");

            //main game loop
            while (playing)
            {
                //calling function that prints out the word to guess
                displayingWordToGuess();
                //calling function that asks for input and process it
                checkingTheInput();
                //cleaning console after each try
                Console.Clear();
            }
            
            //this part responsible for asking if player wants to play again
            //creating a bool for "what to play again" part
            bool yesOrNo = true;
            //loop is asking the user if he wants to play again
            while (yesOrNo)
            {
                //printing out the question and reading user's input
                Console.WriteLine("Do you want to play again?");
                Console.Write("Answer: ");
                string userInput = Console.ReadLine().ToLower();
                //checking if user wants to play again
                if (userInput.Contains("yes") || userInput.Contains("y"))
                {
                    //reseting all global variable to their original values
                    playing = true;
                    guessedLetters = String.Empty;
                    numberOfTries = 7;
                    //clearing the console and running game again
                    Console.Clear();
                    Game();
                }
                //checking if user doesn't want to play again
                else if (userInput.Contains("no") || userInput.Contains("n"))
                {
                    //changing boolean value to stop the loop
                    yesOrNo = false;
                }
                //executes if input was invalid
                else
                {
                    //printing out the message and holding it for 1 second
                    Console.WriteLine("Wrong input. Try again.");
                }
                Console.Clear();
            }
         
        }
        /// <summary>
        /// Checking if the input value is valid or not
        /// </summary>
        /// <param name="userInput">Input to be checked</param>
        /// <returns>Returns true or false whether input was valid or not</returns>
        static bool validateInput(string userInput)
        {
            //checking if the input length was longer then the length of word to guess
            if (userInput.Length > wordToGuess.Length)
            {
                return false;
            }
            //checking if the input is a single letter
            else if (userInput.Length == 1) 
            {
                //checking for any specific chacters or numbers in the input letter
                if ( !(char.ToLower(userInput[0]) >= 'a' && char.ToLower(userInput[0]) <= 'z') )
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checking the input value
        /// </summary>
        static void checkingTheInput()
        {
            //checking if any tries left
            if (numberOfTries == 0)
            {
                //if no more tries left then printing message and stops the main game loop
                Console.WriteLine("Game over. No more tries left.");
                System.Threading.Thread.Sleep(1000);
                playing = false;
            }
            //executes if there are any tries left
            else
            {
                //asking user for input and reading it
                Console.Write("Please enter your guess - letter or word: ");
                string userInput = Console.ReadLine();
                //executes if input is valid
                if (validateInput(userInput))
                {
                    //checking if input is a letter
                    if (userInput.Length == 1)
                    {
                        //calling function to check the input letter
                        checkingTheLetter(userInput);
                    }
                    //if it's not a letter that it's a word
                    else
                    {
                        //calling function to check a word
                        checkingTheWord(userInput);
                    }
                }
                //executes if input is invalid
                else
                {
                    //printing out the message and waiting for 1 second
                    Console.WriteLine("Input is invalid. Try again.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
                
                
        }
        /// <summary>
        /// Checking a single letter as a user input
        /// </summary>
        /// <param name="userInput">Letter to be checked</param>
        static void checkingTheLetter(string userInput)
        {
            //executes if a guess is correct
            if (wordToGuess.Contains(userInput.ToLower()) && !(guessedLetters.Contains(userInput.ToLower())))
            {
                //if it has, then printing the message
                Console.WriteLine("Guess is correct!");
                System.Threading.Thread.Sleep(1000);
                guessedLetters += userInput.ToString().ToLower();
            }
            //checking if a letter was guessed already
            else if (guessedLetters.Contains(userInput.ToLower()))
            {
                Console.WriteLine("This letter was already guessed.");
                System.Threading.Thread.Sleep(1000);
            }
            //executes if the guess was wrong
            else
            {
                //printing out a message, waiting for 1 second and decreasing the number of tries
                Console.WriteLine("Your guess is wrong. One try less.");
                System.Threading.Thread.Sleep(1000);
                numberOfTries--;
            }
        }
                    
        /// <summary>
        /// Checking the word as a user input
        /// </summary>
        /// <param name="input">Word to be checked</param>
        static void checkingTheWord(string input)
        {
            //executes if user guesses the word
            if (input.ToLower() == wordToGuess.ToLower())
            {
                //printing the messsage, waiting for 1 second and changing boolean value so the main game loop stops
                Console.WriteLine("You are right! That is the word!");
                System.Threading.Thread.Sleep(1000);
                playing = false;
            }
            //executes if user guess is wrong
            else if (input.ToLower() != wordToGuess.ToLower())
            {
                //printing out a message, waiting for 1 second and decreasing the number of tries
                Console.WriteLine("Your guess is wrong. One try less.");
                System.Threading.Thread.Sleep(1000);
                numberOfTries--;
            }
        }
        /// <summary>
        /// Displaying the word to be guessed and guessed letters if there are any; otherwise displays underscore.
        /// </summary>
        static void displayingWordToGuess()
        {
            //looping through each letter in the word to guess
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                //checking if a letter was already guessed by user
                if (guessedLetters.Contains(wordToGuess[i].ToString().ToLower()))
                {
                    //if yes, then adding it into return string
                    returnString += wordToGuess[i].ToString() + " ";
                }
                //if the letter wasn't guessed then replacing it with underscore
                else
                {
                    returnString += "_ ";
                }
            }
            //printing out the the word to be guessed and the number of tries left
            Console.WriteLine( "\nWord to guess: {0}", returnString.ToUpper());
            Console.WriteLine("Guessed letters: {0}",guessedLetters.ToUpper());
            Console.WriteLine("Tries left: {0} \n", numberOfTries);
            //reseting the return string for correct output for next try
            returnString = String.Empty;
        }
    }
}
