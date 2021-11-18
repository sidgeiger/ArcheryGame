using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArcheyGame
{
    //The strategy game is a two-player archery game.A player has:
    //TODO Batte method and its functions: menu selection, handling expetions and user inputs
    //TODO There is some distance between the two players (initially 5).
    class Program
    {
        internal class Player
        {
            internal string Name { get; set; }
            internal byte PlayerLifePoints = 3;
            internal byte Arrows = 3;
            internal bool Winner;
        }
        internal class AI : Player { }
        static void Rules()
        {
            Console.WriteLine("\nEach player has a life points of 3 and a set of arrows of 3, as well.");
            Console.WriteLine("There is some distance between the two players(initially 5). The bigger this disance, the harder to hit your target. In each round, players can choose from:");
            Console.WriteLine("\n1. Create an arrow(max number of arrows 12)\n2. Move closer(this reduces the distance between the two players; min distance: 1)\n3. Move further a unit(increasing the distance between the two players)\n4. Shooting; This reduces the number of arrows and, if the opponent is hit, his points are reduced.\nThe probability of shooting success is inversely proportional to the distance.");
            Console.WriteLine("\nHere's how to play:\n");
            Console.WriteLine("\nThe two players choose something from each round, from the options above. After they determined the number of turns, \nthey play until the game is over");
            Console.WriteLine("\nor either player is defeated (their life points are reduced to 0 or below).");
            Console.WriteLine("\nPress a key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        internal void PvPSetup(byte _gameRounds)
        {
            Player p1 = new Player();
            Player p2 = new Player();
            _gameRounds = 1;
            bool namesAreValid;
            bool gameRoundIsValid;
            Console.Clear();
            Console.WriteLine("\nPlayer 1, please enter your name (minimum 1 character):");
            do
            {
                try
                {
                    do
                    {
                        p1.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(p1.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(p1.Name));
                    Console.WriteLine("'" + p1.Name + "'" + " player is created!");
                    Console.WriteLine("\nPlayer 2, please enter your name (minimum 1 character):");
                    do
                    {
                        p2.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(p2.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(p2.Name));
                    Console.WriteLine("'" + p2.Name + "'" + " player is created!");
                }
                catch (Exception)
                {

                    Console.WriteLine("\nPlease, enter a valid name for your player character! . . .  ");
                }
                namesAreValid = true;
            } while (!namesAreValid);
            Console.WriteLine("\nAdd the number of game rounds you want to play!:");
            do
            {
                try
                {
                    _gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (3 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (_gameRounds < 3)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("You cannot play for 2 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
            Battle_PvP(_gameRounds, p1, p2);
            return;
        }
        internal void Battle_PvP(byte _gameRounds, Player _p1, Player _p2)
        {
            Console.Clear();
            //a két játékos távolságának távolság beállítása
            byte distance = 5;
            bool pvpMenuSelection = false;
            int menuItem = 1;
            for (int i = 1; i <= _gameRounds; i++)
            {
                Player1Turn(_p1, _p2, distance, pvpMenuSelection, menuItem, i);
                if (_p1.Winner)
                {
                    Console.WriteLine("\nThe winner is: " + _p1.Name + "CONGRATULATIONS ! ! !");
                    break;
                }
                Player2Turn(_p1, _p2, distance, pvpMenuSelection, menuItem, i);
                if (_p2.Winner)
                {
                    Console.WriteLine("\nThe winner is: " + _p2.Name + "CONGRATULATIONS ! ! !");
                    break;
                }
            }
            if (_p1.PlayerLifePoints > _p2.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _p1.Name + "CONGRATULATIONS ! ! !");
            }
            else
            {
                Console.WriteLine("\nThe winner is: " + _p2.Name + "CONGRATULATIONS ! ! !");
            }
        }
        internal void Player1Turn(Player _p1, Player _p2, byte _distance, bool _pvpMenuSelection, int _menuItem, int _i)
        {
            Console.Clear();
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _p1.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\n Lifepoints: " + _p1.PlayerLifePoints + "\n What will you do?:\n");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\n1) Create an arrow (max. 12)");
            Console.WriteLine("2) Move 1 Unit closer to enemy (Cannot go closer than 1 Unit)");
            Console.WriteLine("3) Move 1 Unit further from enemy");
            Console.WriteLine("4) Shoot\n");
            Console.WriteLine("--> * --> * --> * --> * --> * --> * -->");
            do
            {
                try
                {
                    _pvpMenuSelection = int.TryParse(Console.ReadLine(), out _menuItem);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nThis is not a valid option (1/2/3/4)!\n");
                }
            } while (!_pvpMenuSelection);
            switch (_menuItem)
            {
                case 1:
                    {
                        if (_p1.Arrows == 12)
                        {
                            Console.WriteLine("Your quiver is full already. You simply missed you turn...");
                        }
                        else
                        {
                            _p1.Arrows++;
                        }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine("You are to close to your enemy. You simply missed you turn...");
                        }
                        else
                        {
                            _distance--;
                        }
                    }
                    break;
                case 3:
                    {
                        _distance++;
                    }
                    break;
                case 4:
                    {
                        Player1Shot(_p1, _p2, _distance);
                    }
                    break;
            }
            if (_p2.PlayerLifePoints == 0)
            {
                Console.WriteLine(_p2.Name + "\n is killed... Game is over!");
                _p1.Winner = true;
            }
        }
        internal void Player1Shot(Player _p1, Player _p2, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _p2.PlayerLifePoints--;
            }
            return;
        }
        internal void Player2Turn(Player _p1, Player _p2, byte _distance, bool _pvpMenuSelection, int _menuItem, int _i)
        {
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _p2.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\n Lifepoints: " + _p2.PlayerLifePoints + "\n What will you do?:\n");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\n1) Create an arrow (max. 12)");
            Console.WriteLine("2) Move 1 Unit closer to enemy (Cannot go closer than 1 Unit)");
            Console.WriteLine("3) Move 1 Unit further from enemy");
            Console.WriteLine("4) Shoot\n");
            Console.WriteLine("--> * --> * --> * --> * --> * --> * -->");
            do
            {
                try
                {
                    _pvpMenuSelection = int.TryParse(Console.ReadLine(), out _menuItem);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nThis is not a valid option (1/2/3/4)!\n");
                }
            } while (!_pvpMenuSelection);
            switch (_menuItem)
            {
                case 1:
                    {
                        if (_p1.Arrows == 12)
                        {
                            Console.WriteLine("Your quiver is full already. You simply missed you turn...");
                        }
                        else
                        {
                            _p2.Arrows++;
                        }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine("You are to close to your enemy. You simply missed you turn...");
                        }
                        else
                        {
                            _distance--;
                        }
                    }
                    break;
                case 3:
                    {
                        _distance++;
                    }
                    break;
                case 4:
                    {
                        Player2Shot(_p1, _p2, _distance);
                    }
                    break;
            }
            if (_p1.PlayerLifePoints == 0)
            {
                Console.WriteLine(_p1.Name + "\n is killed... Game is over!");
                _p2.Winner = true;
            }
        }
        internal void Player2Shot(Player _p1, Player _p2, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _p1.PlayerLifePoints--;
            }
            return;
        }
        internal static byte PvESetup(out byte _gameRounds)
        {
            Player p1 = new Player();
            AI a1 = new AI();
            _gameRounds = 1;
            bool namesAreValid;
            bool gameRoundIsValid;
            //byte gameRounds = 0;
            Console.WriteLine("\nPlayer 1, please enter your name (minimum 1 character):");
            do
            {
                try
                {
                    do
                    {
                        p1.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(p1.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(p1.Name));
                    Console.WriteLine("'" + p1.Name + "'" + " player is created!");
                    Console.WriteLine("\nPlease enter your AI opponent's name (minimum 1 character):");
                    do
                    {
                        a1.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(a1.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(a1.Name));
                    Console.WriteLine("'" + a1.Name + "'" + " AI enemy player is created!");
                }
                catch (Exception)
                {

                    Console.WriteLine("\nPlease, enter a valid name for the character! . . .  ");
                }
                namesAreValid = true;
            } while (!namesAreValid);
            Console.WriteLine("\nAdd the number of game rounds you want to play!:");
            do
            {
                try
                {
                    _gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (1 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (_gameRounds == 0)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("Cannot play for 0 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            return _gameRounds;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
        }
        internal static byte EvESetup(out byte _gameRounds)
        {
            AI a1 = new AI();
            AI a2 = new AI();
            _gameRounds = 1;
            bool namesAreValid;
            bool gameRoundIsValid;
            //byte gameRounds = 0;
            Console.WriteLine("\nPlease enter AI Player 1's name (minimum 1 character):");
            do
            {
                try
                {
                    do
                    {
                        a1.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(a1.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(a1.Name));
                    Console.WriteLine("'" + a1.Name + "'" + " AI Player 1 is created!");
                    Console.WriteLine("\nPlease, enter AI Player 2's name (minimum 1 character):");
                    do
                    {
                        a2.Name = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(a2.Name))
                        {
                            Console.WriteLine("The name must contain 1 character at least...");
                        }
                    } while (string.IsNullOrWhiteSpace(a2.Name));
                    Console.WriteLine("'" + a2.Name + "'" + " AI enemy player is created!");
                }
                catch (Exception)
                {

                    Console.WriteLine("\nPlease, enter a valid name for the character! . . .  ");
                }
                namesAreValid = true;
            } while (!namesAreValid);
            Console.WriteLine("\nAdd the number of game rounds you want to play!:");
            do
            {
                try
                {
                    _gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (1 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (_gameRounds == 0)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("Cannot play for 0 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            return _gameRounds;
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
        }
        static void Main(string[] args)
        {
            Console.Title = "Archery Game";
            Console.WriteLine("WELCOME TO THE ARCHERY GAME!");
            bool isGameOn = true;
            bool validGameOption = false;
            string menuSelection = string.Empty;
            byte gameRounds = 3;
            do
            {
                Console.WriteLine("\nPlease, choose an option:");
                Console.WriteLine("\n*** [1] Start Player VS Player Game (PVP)   ***");
                Console.WriteLine("*** [2] Start Player VS AI Game (PVE)       ***");
                Console.WriteLine("*** [3] Start AI VS AI Game (EVE)           ***");
                Console.WriteLine("*** [4] Rules                               ***");
                Console.WriteLine("*** [5] Quit                                ***");
                do
                {
                    do
                    {
                        menuSelection = Convert.ToString(Console.ReadLine());
                        if (string.IsNullOrWhiteSpace(menuSelection))
                        {
                            Console.WriteLine("\nPlease, select a valid option... (1/2/3/4/5):");
                        }
                    }
                    while (string.IsNullOrWhiteSpace(menuSelection));
                    if (menuSelection == "1")
                    {
                        validGameOption = true;
                        //New game : players must input their names and a battle commences
                        Console.Clear();
                        Console.WriteLine("\nNew game: PvP Mode!");
                        PvPSetup(_gameRounds);
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        //Harc kezdete , Battle függvény
                        Console.Clear();
                        
                    }
                    else if (menuSelection == "2")
                    {
                        validGameOption = true;
                        ///New game : players must input their names and a battle commences
                        Console.Clear();
                        Console.WriteLine("\nNew game PvE Mode!");
                        PvESetup(out gameRounds);
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else if (menuSelection == "3")
                    {
                        validGameOption = true;
                        //writing a the rules on screen
                        Console.Clear();
                        Console.WriteLine("\nNew game EvE Mode!");
                        EvESetup(out gameRounds);
                        Console.Clear();
                    }
                    else if (menuSelection == "4")
                    {
                        validGameOption = true;
                        //writing a the rules on screen  
                        Console.Clear();
                        Console.WriteLine("\nARCHERY GAME RULES:");
                        Rules();
                    }
                    else if (menuSelection == "5")
                    {
                        validGameOption = true;
                        //quiting the game
                        Console.WriteLine("\nQuitting the game... Good bye!");
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
