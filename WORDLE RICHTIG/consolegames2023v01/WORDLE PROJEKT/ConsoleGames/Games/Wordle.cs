using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;

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
        Point point = new Point(0, 0);
        int count = 0;



        Console.WriteLine("Versuche das Wort zu erraten!");

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


            point.X = 0;
            int YKORD = count * 10;
            count++;
            Console.SetWindowSize(1220, 1220);
            Console.ResetColor();
           // Console.WriteLine(secretWord); // Nur zum Testen um echten Spiel dann nichtmeht
            input = Console.ReadLine();
            input = input.ToUpper();

            if (input == secretWord)
            {
                for (int i = 0; i < secretWord.Length; i++ )
              //  DrawChar(input[i], ConsoleColor.Green, ref point);
                Console.WriteLine("Glückwunsch du hast das Wort " + secretWord + " richtig erraten drücke eine beliebige Taste um Fortzufahren!");

                Console.ReadKey();
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
                        DrawChar(input[bStab], ConsoleColor.Green, ref point);
                        tempFrequencies[input[bStab]]--; // Zähler runtermachen
                        point.X += 20;
                        point.Y = YKORD;
                    }
                    else
                    {
                        if (secretWord.Contains(input[bStab]) && tempFrequencies[input[bStab]] > 1) // Falsche Position aber Buchstabe im Wort
                        {
                            DrawChar(input[bStab], ConsoleColor.DarkYellow, ref point);
                            tempFrequencies[input[bStab]]--;
                            point.X += 20;
                            point.Y = YKORD;
                        }
                        else // Buchstabe nicht im Wort oder Buchstabe schon zugeordnet
                        {
                            DrawChar(input[bStab], ConsoleColor.Red, ref point);
                            point.X += 20;
                            point.Y = YKORD;
                        }
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





        private Point DrawChar(char c, ConsoleColor col, ref Point point)
        {

        string letter = (font[(int)c - 65]);
        string[] array1 = letter.Split('\n');
        for (int i = 0; i < array1.Length; i++)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.ForegroundColor = col;
            Console.WriteLine(array1[i]);
            Console.ResetColor();
            point.Y++;

        }
        return point;
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
 '----------------'  ",
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
 '----------------' ",
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
 '----------------'",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",
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
 '----------------' ",















    };
            
    }




