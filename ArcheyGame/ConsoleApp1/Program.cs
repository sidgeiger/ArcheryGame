using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArcheyGame
{
    //The strategy game is a two-player archery game.A player has:
    //TODO Each player has a name; this must be specified at the beginning of the game
    //TODO Each player has a life point (we start with 3)
    //TODO Each player has arrows (we start with 0)
    //TODO There is some distance between the two players (initially 5).
    class Program
    {
        internal class Player
        {
            internal string Name { get; set; }
            internal byte PlayerLifePoints = 3;
            bool Winner;
        }
        internal class AI : Player
        {
           
        }
        static void Rules()
        {
            Console.WriteLine("Each player has a life points of 3.");
            Console.WriteLine("There is some distance between the two players(initially 5). In each round, players can choose from:");
            Console.WriteLine("\nCreate an arrow(max number of arrows 12)\nMove closer(this reduces the distance between the two players; min distance: 1)\nMove further afield(increasing the distance between the two players)\nShooting; This reduces the number of arrows and, if the opponent is hit, his points are reduced.\nThe probability of shooting success is inversely proportional to the distance.");
            Console.WriteLine("\nHere's how to play:\n");
            Console.WriteLine("\nThe two players choose something from each round, from the options above. After they determined the number of turns, \nthey play until the game is over");
            Console.WriteLine("\nor either player is defeated (their life points are reduced to 0 or below).");
            Console.WriteLine("\nPress a key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            Console.Title = "Archery Game";
            Console.WriteLine("WELCOME TO THE ARCHERY GAME!");
            bool isGameOn = true;
            bool validGameOption = false;
            string menuSelection = string.Empty;
            byte gameRounds = 0;
            do
            {
                Console.WriteLine("Please, choose an option:");
                Console.WriteLine("\n*** [1] New Game ***");
                Console.WriteLine("*** [2] Rules    ***");
                Console.WriteLine("*** [3] Quit     ***");
                do
                {
                    do
                    {
                        menuSelection = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(menuSelection))
                        {
                            Console.WriteLine("\nPlease, select a valid option... :");
                        }
                    }
                    while (string.IsNullOrWhiteSpace(menuSelection));
                    if (menuSelection == "1")
                    {
                        validGameOption = true;
                        //New game : players must input their names and a battle commences
                        Console.Clear();
                        Console.WriteLine("\nNew game : players must input their names and a battle commences... TBA...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else if (menuSelection == "2")
                    {
                        validGameOption = true;
                        //writing a the rules on screen
                        Console.WriteLine("\nARCHERY GAME RULES:");
                        Rules();
                    }
                    else if (menuSelection == "3")
                    {
                        validGameOption = true;
                        //quiting the game
                        Console.WriteLine("\nQuitting the game...");
                        isGameOn = false;
                        Thread.Sleep(2000);
                    }
                }
                while (!validGameOption);
            }
            while (isGameOn);
        }
    }
}
