using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleGames.Games; // here use name of your project

public class Wordle : Game
{
    // PUBLIC PROPERTIES (Eigenschaften)S
    public override string Name => "Wordle";
    public override string Description => "Erraten Sie das Geheime Wort bevor sie keine Leben mehr haben";
    public override string Rules => "Errate das gesuchte Wort in möglichst wenig Versuchen.";
    public override string Credits => "Gian Engel, giaengel@ksr.ch";
    public override int Year => 2023;
    public override bool TheHigherTheBetter => false;
    public override int LevelMax => 5;
    public override Score HighScore { get; set; }
    // No variable declarations in this area!!




    public override Score Play(int level)
    {
        bool gameover = false;
        string input = null;
        string secretWord = readsecretword(level);
        int xpos = 0;
        int ypos = 0;


        // Class um zu Zählen wie oft ein Buchstabe in Wort ist
        Dictionary<char, int> letterFrequencies = new Dictionary<char, int>();
        foreach (char c in secretWord)
        {
            if (letterFrequencies.ContainsKey(c))
                letterFrequencies[c]++;
            else
                letterFrequencies[c] = 1;
        }

        while (!gameover)
        {

            //Console.SetWindowSize(1220, 1220);
            Console.ResetColor();
            Console.WriteLine(secretWord); // Nur zum Testen um echten Spiel dann nichtmeht
            Console.WriteLine("Versuche das Wort zu erraten!");
            input = Console.ReadLine();
            input = input.ToUpper();


            if (input == secretWord)
            {
                Console.WriteLine("Glückwunsch du hast das Wort " + secretWord + " richtig erraten");
                gameover = true;
            }
            else
            {
                // Wie oft der Buchstabe verwendet wird in temporäre Variable
                Dictionary<char, int> tempFrequencies = new Dictionary<char, int>(letterFrequencies);

                for (int bStab = 0; bStab < secretWord.Length; bStab++)
                {
                    if (secretWord[bStab] == input[bStab]) // Richtige Positon
                    {
                        Display.DrawChar(input[bStab], ConsoleColor.Green, xpos, ypos);
                        tempFrequencies[input[bStab]]--; // Zähler runtermachen

                    }
                    else
                    {
                        if (secretWord.Contains(input[bStab]) && tempFrequencies[input[bStab]] > 0) // Falsche Position aber Buchstabe im Wort
                        {
                           // Console.BackgroundColor = ConsoleColor.Yellow;
                            Display.DrawChar(input[bStab], ConsoleColor.Yellow, xpos, ypos);
                            tempFrequencies[input[bStab]]--; 

                        }
                        else // Buchstabe nicht im Wort oder Buchstabe schon zugeordnet
                        {
                            //Console.BackgroundColor = ConsoleColor.Red;
                            Display.DrawChar(input[bStab], ConsoleColor.Red, xpos, ypos);
                            //Console.Write(input[bStab]);

                        }
                    }

                    if (bStab == secretWord.Length - 1) // Zeilen Abstand
                    {
                        Console.WriteLine("");
                    }
                }
            }
        }

        return new Score();
    }



    private string readsecretword(int level)
    {
        string[] wordsSimple = new string[] { "Hund", "Elfe", "Haus", "Auto", "Edel", "Fett", "Bube", "Buch", "Chef", "Brav", "Mond", "Baum", "Zeit", "Wind", "Brot", "Boot", "Lied", "Ball", "Kind", "Leid", "Wolf", "Gold", "Faul", "Kamm", "Tuch" };
        string[] wordsMedium = new string[] { "Medium" };
        string[] wordsAdvanced = new string[] { "Advanced" };

        Random rand = new Random();
        string secretWord = "";
        if (level == 0)
        {
            secretWord = wordsSimple[rand.Next(0, wordsSimple.Length)];
        }
        if (level == 1)
        {
            secretWord = wordsMedium[rand.Next(0, wordsMedium.Length)];

        }
        if (level == 2)
        {
            secretWord = wordsAdvanced[rand.Next(0, wordsAdvanced.Length)];
        }



        secretWord = secretWord.ToUpper();
        return secretWord;


    }


    









    class Display
    {
        internal static void DrawChar(char c, ConsoleColor col, int xpos, int ypos)
        {

            Console.ForegroundColor = col;
            Console.SetCursorPosition(xpos, ypos);
            Console.WriteLine(font[(int)c - 65]);
            Console.ResetColor();
            xpos = xpos + 100;
            ypos = ypos + 100;




        }
        static string[] font =
        {

                        @"

 .----------------.   
| .--------------. |  
| |      __      | |  
| |     /  \     | |  
| |    / /\ \    | |  
| |   / ____ \   | |  
| | _/ /    \ \_ | |  
| ||____|  |____|| |  
| |              | |  
| '--------------' |  
 '----------------'   

",

@"




 .----------------. 
| .--------------. |
| |   ______     | |
| |  |_   _ \    | |
| |    | |_) |   | |
| |    |  __'.   | |
| |   _| |__) |  | |
| |  |_______/   | |
| |              | |
| '--------------' |
 '----------------' 
",
@"
 .----------------.
| .--------------. |
| |     ______   | |
| |   .' ___  |  | |
| |  / .'   \_|  | |
| |  | |         | |
| |  \ `.___.'\  | |
| |   `._____.'  | |
| |              | |
| '--------------' |
 '----------------'
",
@"
 .----------------. 
| .--------------. |
| |  ________    | |
| | |_   ___ `.  | |
| |   | |   `. \ | |
| |   | |    | | | |
| |  _| |___.' / | |
| | |________.'  | |
| |              | |
| '--------------' |
 '----------------' 
",
@"
 .----------------. 
| .--------------. |
| |  _________   | |
| | |_   ___  |  | |
| |   | |_  \_|  | |
| |   |  _|  _   | |
| |  _| |___/ |  | |
| | |_________|  | |
| |              | |
| '--------------' |
 '----------------' 
",
@"
 .----------------. 
| .--------------. |
| |  _________   | |
| | |_   ___  |  | |
| |   | |_  \_|  | |
| |   |  _|      | |
| |  _| |_       | |
| | |_____|      | |
| |              | |
| '--------------' |
 '----------------' 
",
@"
 .----------------. 
| .--------------. |
| |    ______    | |
| |  .' ___  |   | |
| | / .'   \_|   | |
| | | |    ____  | |
| | \ `.___]  _| | |
| |  `._____.'   | |
| |              | |
| '--------------' |
 '----------------' 
",
@"
 .----------------. 
| .--------------. |
| |  ____  ____  | |
| | |_   ||   _| | |
| |   | |__| |   | |
| |   |  __  |   | |
| |  _| |  | |_  | |
| | |____||____| | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |     _____    | |
| |    |_   _|   | |
| |      | |     | |
| |      | |     | |
| |     _| |_    | |
| |    |_____|   | |
| |              | |
| '--------------' |
 '----------------' 

",

@"
 .----------------. 
| .--------------. |
| |     _____    | |
| |    |_   _|   | |
| |      | |     | |
| |   _  | |     | |
| |  | |_' |     | |
| |  `.___.'     | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |  ___  ____   | |
| | |_  ||_  _|  | |
| |   | |_/ /    | |
| |   |  __'.    | |
| |  _| |  \ \_  | |
| | |____||____| | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |   _____      | |
| |  |_   _|     | |
| |    | |       | |
| |    | |   _   | |
| |   _| |__/ |  | |
| |  |________|  | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| | ____    ____ | |
| ||_   \  /   _|| |
| |  |   \/   |  | |
| |  | |\  /| |  | |
| | _| |_\/_| |_ | |
| ||_____||_____|| |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .-----------------.
| .--------------. |
| | ____  _____  | |
| ||_   \|_   _| | |
| |  |   \ | |   | |
| |  | |\ \| |   | |
| | _| |_\   |_  | |
| ||_____|\____| | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |     ____     | |
| |   .'    `.   | |
| |  /  .--.  \  | |
| |  | |    | |  | |
| |  \  `--'  /  | |
| |   `.____.'   | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |   ______     | |
| |  |_   __ \   | |
| |    | |__) |  | |
| |    |  ___/   | |
| |   _| |_      | |
| |  |_____|     | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |    ___       | |
| |  .'   '.     | |
| | /  .-.  \    | |
| | | |   | |    | |
| | \  `-'  \_   | |
| |  `.___.\__|  | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |  _______     | |
| | |_   __ \    | |
| |   | |__) |   | |
| |   |  __ /    | |
| |  _| |  \ \_  | |
| | |____| |___| | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |    _______   | |
| |   /  ___  |  | |
| |  |  (__ \_|  | |
| |   '.___`-.   | |
| |  |`\____) |  | |
| |  |_______.'  | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |  _________   | |
| | |  _   _  |  | |
| | |_/ | | \_|  | |
| |     | |      | |
| |    _| |_     | |
| |   |_____|    | |
| |              | |
| '--------------' |
 '----------------' 

",

@"
 .----------------. 
| .--------------. |
| | _____  _____ | |
| ||_   _||_   _|| |
| |  | |    | |  | |
| |  | '    ' |  | |
| |   \ `--' /   | |
| |    `.__.'    | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| | ____   ____  | |
| ||_  _| |_  _| | |
| |  \ \   / /   | |
| |   \ \ / /    | |
| |    \ ' /     | |
| |     \_/      | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| | _____  _____ | |
| ||_   _||_   _|| |
| |  | | /\ | |  | |
| |  | |/  \| |  | |
| |  |   /\   |  | |
| |  |__/  \__|  | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |  ____  ____  | |
| | |_  _||_  _| | |
| |   \ \  / /   | |
| |    > `' <    | |
| |  _/ /'`\ \_  | |
| | |____||____| | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |  ____  ____  | |
| | |_  _||_  _| | |
| |   \ \  / /   | |
| |    \ \/ /    | |
| |    _|  |_    | |
| |   |______|   | |
| |              | |
| '--------------' |
 '----------------' 

",
@"
 .----------------. 
| .--------------. |
| |   ________   | |
| |  |  __   _|  | |
| |  |_/  / /    | |
| |     .'.' _   | |
| |   _/ /__/ |  | |
| |  |________|  | |
| |              | |
| '--------------' |
 '----------------' 

",















    };
            
            
            
































    }
















}


