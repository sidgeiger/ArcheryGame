using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArcheyGame
{
    //TODO Random osztályt megvizsgálni, hogy csak egsyzer legyen példányosítás
    class Program
    {
        //const Random rnd = new Random();
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
            Console.WriteLine("\n1. Creating an arrow(max number of arrows 12)\n2. Moving closer(this reduces the distance between the two players; min distance: 1)\n3. Moving further away a unit(increasing the distance between the two players)\n4. Shooting; This reduces the number of arrows and, if the opponent is hit, his points are reduced.\nThe probability of shooting success is inversely proportional to the distance.");
            Console.WriteLine("\nHere's how to play:\n");
            Console.WriteLine("\nThe two players choose from the options above, taking turns. A game round is over when all player took his/her turn. After they determined the number of turns,\n\nthey play until the game is over");
            Console.WriteLine("\nOR\n\neither player is defeated (their life points are reduced to 0).");
            Console.WriteLine("\nPress a key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        internal static void PvPSetup()
        {
            Player p1 = new Player();
            Player p2 = new Player();
            bool namesAreValid;
            bool gameRoundIsValid;
            byte gameRounds = 3;
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
                    gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (3 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (gameRounds < 3)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("You cannot play for 2 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
            Battle_PvP(gameRounds, p1, p2);
        }
        static void Battle_PvP(byte _gameRounds, Player _p1, Player _p2)
        {
            Console.Clear();
            //a két játékos távolságának távolság beállítása
            byte distance = 5;
            bool pvpMenuSelection = false;
            int menuItem = 1;
            for (int i = 1; i <= _gameRounds; i++)
            {
                Player1Turn(_p1, _p2, ref distance, pvpMenuSelection, menuItem, i);
                if (_p1.Winner)
                {
                    break;
                }
                Player2Turn(_p1, _p2, ref distance, pvpMenuSelection, menuItem, i);
                if (_p2.Winner)
                {
                    break;
                }
            }
            if (_p1.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _p1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_p2.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _p2.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_p1.PlayerLifePoints > _p2.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _p1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_p1.PlayerLifePoints < _p2.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _p2.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else
            {
                Console.WriteLine("\nIt's a draw...\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
        }
        static void Player1Turn(Player _p1, Player _p2,  ref byte _distance, bool _pvpMenuSelection, int _menuItem, int _i)
        {
            Console.Clear();
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _p1.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\nLifepoints: " + _p1.PlayerLifePoints + "\nArrows: " + _p1.Arrows  + "\nWhat will you do?:");
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
                    if (0 < _menuItem && _menuItem < 5)
                    {
                        Console.WriteLine("\nOk...\n");
                    }
                    else
                    {
                        _pvpMenuSelection = false;
                        Console.WriteLine("\nThis is not a valid option (1/2/3/4)!\n");
                    }
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
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("You have created an arrow.");
                            _p1.Arrows++;
                            Thread.Sleep(3600);
                        }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine("You are to close to your enemy. You simply missed you turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("You have moved closer to your enemy.");
                            _distance--;
                            Thread.Sleep(3600);
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("You have moved away from enemy.");
                        _distance++;
                        Thread.Sleep(3600);
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
                Thread.Sleep(2500);
                _p1.Winner = true;
            }
            Console.Clear();
        }
        static void Player1Shot(Player _p1, Player _p2, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (_p1.Arrows == 0)
            {
                Console.WriteLine("\n" + _p1.Name + " tries to fire, but has no arrow... Missed the turn...");
                Thread.Sleep(3500);
            }
            else if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _p1.Arrows--;
                _p2.PlayerLifePoints--;
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("\nIt's a miss... :(");
                _p1.Arrows--;
                Thread.Sleep(2000);
            }
        }
        static void Player2Turn(Player _p1, Player _p2, ref byte _distance, bool _pvpMenuSelection, int _menuItem, int _i)
        {
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _p2.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\nLifepoints: " + _p2.PlayerLifePoints + "\nArrows: " + _p2.Arrows + "\nWhat will you do?:");
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
                    if (0 < _menuItem && _menuItem < 5)
                    {
                        Console.WriteLine("\nOk...\n");
                    }
                    else
                    {
                        _pvpMenuSelection = false;
                        Console.WriteLine("\nThis is not a valid option (1/2/3/4)!\n");
                    }
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
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("You have created an arrow.");
                            _p2.Arrows++;
                            Thread.Sleep(3600);
                         }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine("You are to close to your enemy. You simply missed you turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("You have moved closer to the enemy.");
                            _distance--;
                            Thread.Sleep(3600);
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("You have moved away from your enemy.");
                        _distance++;
                        Thread.Sleep(3600);
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
                Thread.Sleep(2500);
                _p2.Winner = true;
            }
            Console.Clear();
        }
        static void Player2Shot(Player _p1, Player _p2, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (_p2.Arrows == 0)
            {
                Console.WriteLine("\n" + _p2.Name + " tries to fire, but has no arrow... Missed the turn...");
                Thread.Sleep(3500);
            }
            else if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _p2.Arrows--;
                _p1.PlayerLifePoints--;
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("\nIt's a miss... :(");
                _p2.Arrows--;
                Thread.Sleep(2000);
            }
        }
        static void PvESetup()
        {
            Player p1 = new Player();
            AI a1 = new AI();
            byte gameRounds = 3;
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
                    gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (3 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (gameRounds < 3)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("Cannot play for 2 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
            Battle_PvE(gameRounds, p1, a1);
        }
        static void AITurn(Player _p1, AI _a1, ref byte _distance, int _menuItem, int _i)
        {
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _a1.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\nLifepoints: " + _a1.PlayerLifePoints + "\nArrows: " + _a1.Arrows + "\nWhat will you do?:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\n1) Create an arrow (max. 12)");
            Console.WriteLine("2) Move 1 Unit closer to enemy (Cannot go closer than 1 Unit)");
            Console.WriteLine("3) Move 1 Unit further from enemy");
            Console.WriteLine("4) Shoot\n");
            Console.WriteLine("--> * --> * --> * --> * --> * --> * -->");
            Random rnd = new Random();
            _menuItem = rnd.Next(1, 5);
            switch (_menuItem)
            {
                case 1:
                    {
                        if (_p1.Arrows == 12)
                        {
                            Console.WriteLine("The quiver is full already. " + _a1.Name +  " simply missed its turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("\n" + _a1.Name + " has created an arrow!");
                            _a1.Arrows++;
                            Thread.Sleep(3500);
                        }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine(_a1.Name + " is too close to you. It has simply missed its turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("\n" + _a1.Name + " is moving closer!");
                            _distance--;
                            Thread.Sleep(3500);
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("\n" + _a1.Name + " is moving away!");
                        _distance++;
                        Thread.Sleep(3500);
                    }
                    break;
                case 4:
                    {
                        AIShot(_p1, _a1, _distance);
                    }
                    break;
            }
            if (_p1.PlayerLifePoints == 0)
            {
                Console.WriteLine(_p1.Name + "\n is killed... Game is over!");
                Thread.Sleep(2500);
                _a1.Winner = true;
            }
            Console.Clear();
        }
        static void AIShot(Player _p1, AI _a1, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (_a1.Arrows == 0)
            {
                Console.WriteLine("\n" + _a1.Name + " tries to fire, but has no arrow... Missed the turn...");
                Thread.Sleep(3500);
            }
            else if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _a1.Arrows--;
                _p1.PlayerLifePoints--;
                Thread.Sleep(3500);
            }
            else
            {
                Console.WriteLine("\nIt's a miss... :(");
                _a1.Arrows--;
                Thread.Sleep(3500);
            }
        }
        static void Battle_PvE(byte _gameRounds, Player _p1, AI _a1)
        {
            Console.Clear();
            //a két játékos távolságának távolság beállítása
            byte distance = 5;
            bool pvpMenuSelection = false;
            int menuItem = 1;
            for (int i = 1; i <= _gameRounds; i++)
            {
                Player1Turn(_p1, _a1, ref distance, pvpMenuSelection, menuItem, i);
                if (_p1.Winner)
                {
                    break;
                }
                AITurn(_p1, _a1, ref distance, menuItem, i);
                if (_a1.Winner)
                {
                    break;
                }
            }
            if (_p1.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _p1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_a1.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _a1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_p1.PlayerLifePoints > _a1.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _p1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_p1.PlayerLifePoints < _a1.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _a1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else
            {
                Console.WriteLine("\nIt's a draw...\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
        }
        static void EvESetup()
        {
            AI a1 = new AI();
            AI a2 = new AI();
            byte gameRounds = 3;
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
                    gameRounds = Convert.ToByte(Console.ReadLine());
                    gameRoundIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease, enter a valid number (3 - 255) for your game rounds! . . .  ");
                    gameRoundIsValid = false;
                }
                if (gameRounds < 3)
                {
                    gameRoundIsValid = false;
                    Console.WriteLine("Cannot play for 0 or less rounds... Enter the number again:");
                }
            } while (!gameRoundIsValid);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Harc kezdete , Battle függvény...
            Battle_EvE(gameRounds, a2, a1);
        }
        internal static void Battle_EvE(byte _gameRounds, AI _a2, AI _a1)
        {
            Console.Clear();
            //a két játékos távolságának távolság beállítása
            byte distance = 5;
            int menuItem = 1;
            for (int i = 1; i <= _gameRounds; i++)
            {
                AI2Turn(_a2, _a1, ref distance, menuItem, i);
                if (_a2.Winner)
                {
                    break;
                }
                AITurn(_a2, _a1, ref distance, menuItem, i);
                if (_a1.Winner)
                {
                    break;
                }
            }
            if (_a2.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _a2.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_a1.Winner)
            {
                Console.WriteLine("\nThe winner is: " + _a1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_a2.PlayerLifePoints > _a1.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _a2.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else if (_a2.PlayerLifePoints < _a1.PlayerLifePoints)
            {
                Console.WriteLine("\nThe winner is: " + _a1.Name + ". CONGRATULATIONS ! ! !\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
            else
            {
                Console.WriteLine("\nIt's a draw...\nThis battle is over. Back to Main Menu...");
                Thread.Sleep(3600);
            }
        }
        internal static void AI2Turn(AI _a2, AI _a1, ref byte _distance, int _menuItem, int _i)
        {
            Console.WriteLine("<-- * <-- * <-- * <-- * <-- * <-- * <--");
            Console.WriteLine(_i + ". ROUND:\n" + "\nPrepare yourself, " + _a2.Name + ", it is your turn!");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\nDistance from enemy: " + _distance + "\nLifepoints: " + _a2.PlayerLifePoints + "\nArrows: " + _a2.Arrows + "\nWhat will you do?:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("\n1) Create an arrow (max. 12)");
            Console.WriteLine("2) Move 1 Unit closer to enemy (Cannot go closer than 1 Unit)");
            Console.WriteLine("3) Move 1 Unit further from enemy");
            Console.WriteLine("4) Shoot\n");
            Console.WriteLine("--> * --> * --> * --> * --> * --> * -->");
            Random rnd = new Random();
            _menuItem = rnd.Next(1, 5);
            switch (_menuItem)
            {
                case 1:
                    {
                        if (_a2.Arrows == 12)
                        {
                            Console.WriteLine("The quiver is full already. " + _a2.Name + " simply missed its turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("\n" + _a2.Name + " has created an arrow!");
                            _a2.Arrows++;
                            Thread.Sleep(3500);
                        }
                    }
                    break;
                case 2:
                    {
                        if (_distance == 1)
                        {
                            Console.WriteLine(_a1.Name + " is too close. It has simply missed its turn...");
                            Thread.Sleep(3600);
                        }
                        else
                        {
                            Console.WriteLine("\n" + _a2.Name + " is moving closer!");
                            _distance--;
                            Thread.Sleep(3500);
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("\n" + _a2.Name + " is moving away!");
                        _distance++;
                        Thread.Sleep(3500);
                    }
                    break;
                case 4:
                    {
                        AI2Shot(_a2, _a1, _distance);
                    }
                    break;
            }
            if (_a1.PlayerLifePoints == 0)
            {
                Console.WriteLine(_a1.Name + "\n is killed... Game is over!");
                Thread.Sleep(2500);
                _a2.Winner = true;
            }
            Console.Clear();
        }
        internal static void AI2Shot(AI _a2, AI _a1, byte _distance)
        {
            //lövés kiszámolása: minél nagyobb a távolság annál többet vonunk le a random számból,
            //ha még így is egy határérték fölé esik az eredmény (75%), akkor a lövés talál
            Random rnd = new Random();
            double hit = (rnd.Next(70, 100)) - (1.7 * _distance);
            if (_a1.Arrows == 0)
            {
                Console.WriteLine("\n" + _a2.Name + " tries to fire, but has no arrow... Missed the turn...");
                Thread.Sleep(3500);
            }
            else if (hit >= 75)
            {
                Console.WriteLine("\nTarget is HIT!");
                _a2.Arrows--;
                _a1.PlayerLifePoints--;
                Thread.Sleep(3500);
            }
            else
            {
                Console.WriteLine("\nIt's a miss... :(");
                _a1.Arrows--;
                Thread.Sleep(3500);
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Archery Game";
            Console.WriteLine("WELCOME TO THE ARCHERY GAME!");
            bool isGameOn = true;
            bool validGameOption = false;
            string menuSelection = string.Empty;
            //byte gameRounds = 3;
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
                        PvPSetup();
                        Console.Clear();
                    }
                    else if (menuSelection == "2")
                    {
                        validGameOption = true;
                        ///New game : players must input their names and a battle commences
                        Console.Clear();
                        Console.WriteLine("\nNew game PvE Mode!");
                        PvESetup();
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else if (menuSelection == "3")
                    {
                        validGameOption = true;
                        //writing a the rules on screen
                        Console.Clear();
                        Console.WriteLine("\nNew game EvE Mode!");
                        EvESetup();
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
