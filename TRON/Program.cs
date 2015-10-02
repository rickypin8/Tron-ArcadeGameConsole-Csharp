using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;//add this namespace to use the Thread.Sleep() forthe time
namespace Snake
{
    class Program
    {
        #region variables
        static int[,] array = new int[80, 40];//[50,20]//this is my array is the base of the game
        static string[] opti = { "<  Easy  >", "< Medium >", "<HARD lml>" };

        /// <summary>
        /// this are the variables for validate directions
        /// </summary>
        static int left = 0;
        static int right = 1;
        static int up = 2;
        static int down = 3;
        static int op = 1;
        /// <summary>
        /// this are the variables for the spawn of the lines,score and direction
        /// </summary>
        /// Player one variables
        static int PlayerScore = 0;
        static int PlayerDirection = right;
        static int PlayerColumn = 1; // column
        static int PlayerRow = 20; // row  
        //player two variables
        static int Player2Score = 0;
        static int Player2Direction = left;
        static int Player2Column = 79; // column
        static int Player2Row = 20; // row 
        #endregion
        #region methods
        static void putWalls()
        {
            for (int Y = 1; Y < 40; Y++)//este ciclo hace que todos los datos de mi array se hagan 0 (MENOS LAS ORILLAS)
            {
                for (int X = 1; X < 80; X++)
                {
                    array[X, Y] = 0;
                }
            }
            for (int X = 0; X < 80; X++)
            {
                //arriba
                Console.SetCursorPosition(X, 0);
                if (X == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("╔");
                    array[X, 0] = 1;
                }
                else if (X > 0 & X < 80)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("═");
                    array[X, 0] = 1;
                }
                else if (X == 80)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("╗");
                    array[X, 0] = 1;
                }

                //abajo
                Console.SetCursorPosition(X, 39);
                if (X == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("╚");
                    array[X, 39] = 1;
                }
                else if (X > 0 & X <= 79)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("═");
                    array[X, 39] = 1;
                }
                else if (X == 80)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("╝");
                    array[X, 39] = 1;
                }
                //dibujar los lados de el marco

                if (X >= 1 & X < 40)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, X);
                    Console.WriteLine("║");
                    array[0, X] = 1;
                }
                if (X > 0 & X < 40)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(79, X);
                    Console.WriteLine("║");
                    array[79, X] = 1;
                }
            }
        }//This function make the walls
        static void ChangePlayerDirection(ConsoleKeyInfo key)//this function isto change directions
        {
            //player 1
            if (key.Key == ConsoleKey.UpArrow && PlayerDirection != down)//playerdirection != down because only you can go up if you are moving in the right or left way.
            {
                PlayerDirection = up;
            }
            if (key.Key == ConsoleKey.LeftArrow && PlayerDirection != right)
            {
                PlayerDirection = left;
            }
            if (key.Key == ConsoleKey.RightArrow && PlayerDirection != left)
            {
                PlayerDirection = right;
            }
            if (key.Key == ConsoleKey.DownArrow && PlayerDirection != up)
            {
                PlayerDirection = down;
            }
            //player 2
            if (key.Key == ConsoleKey.W && Player2Direction != down)
            {
                Player2Direction = up;
            }
            if (key.Key == ConsoleKey.A && Player2Direction != right)
            {
                Player2Direction = left;
            }
            if (key.Key == ConsoleKey.D && Player2Direction != left)
            {
                Player2Direction = right;
            }
            if (key.Key == ConsoleKey.S && Player2Direction != up)
            {
                Player2Direction = down;
            }
        }//cambiar dirección
        static void movePlayer()//move players
        {
            if (PlayerDirection == right)
            {
                PlayerColumn++;
            }
            if (PlayerDirection == left)
            {
                PlayerColumn--;
            }
            if (PlayerDirection == up)
            {
                PlayerRow--;
            }
            if (PlayerDirection == down)
            {
                PlayerRow++;
            }
            //player2
            if (Player2Direction == right)
            {
                Player2Column++;
            }
            if (Player2Direction == left)
            {
                Player2Column--;
            }
            if (Player2Direction == up)
            {
                Player2Row--;
            }
            if (Player2Direction == down)
            {
                Player2Row++;
            }

        }
        static void GeneratePlayers(int speed)//function to generate players
        {
            if (!(array[Player2Column, Player2Row] == 1))//here i check that the player can't cross the wall
            {
                Console.SetCursorPosition(Player2Column, Player2Row);//initialposition
                Console.ForegroundColor = ConsoleColor.Yellow;//color of the second player
                Console.Write("*");//body of the second player
                Player2Score++;//add points
                array[Player2Column, Player2Row] = 1;//convert the 0 in the matrix to 1, to create a collider
                Thread.Sleep(speed);//velocity of movement of the players
            }
            else
            {
                //This is the final menu
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Player pink wins");
                Console.WriteLine("Score: " + PlayerScore + 2 + " points");
                Console.WriteLine("1-.Again");
                Console.WriteLine("2-.Exit");
                Console.Write("What?: ");
                Console.CursorVisible = true;
                op = int.Parse(Console.ReadLine());
                //reset2
                Random y2 = new Random();
                Player2Column = 79;
                Player2Row = y2.Next(3, 35);
                Player2Direction = left;
                //reset1 line spawn
                Random y1 = new Random();
                PlayerColumn = 1;
                PlayerRow = y1.Next(3, 35);
                PlayerDirection = right;
                Console.Clear();
                putWalls();//restart the matrix
            }
            if (!(array[PlayerColumn, PlayerRow] == 1))
            {
                Console.SetCursorPosition(PlayerColumn, PlayerRow);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("*");
                PlayerScore++;
                array[PlayerColumn, PlayerRow] = 1;
                Thread.Sleep(speed);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Player Yellow wins");
                Console.WriteLine("Score: " + Player2Score + 2 + " points");
                Console.WriteLine("1-.Again");
                Console.WriteLine("2-.Exit");
                Console.Write("What?: ");
                Console.CursorVisible = true;
                op = int.Parse(Console.ReadLine());
                //reset2 line spawn
                Random y2 = new Random();
                Player2Column = 79;
                Player2Row = y2.Next(3, 35);
                Player2Direction = left;
                //reset1 line spawn
                Random y1 = new Random();
                PlayerColumn = 1;
                PlayerRow = y1.Next(3, 35);
                PlayerDirection = right;
                Console.Clear();
                putWalls();
            }
        }
        #endregion
        static void Main(string[] args)
        {
            Console.WindowHeight = 41;//height ofthe window
            Console.CursorVisible = false;//make no visible the cursor 
            Console.SetCursorPosition(30, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
                ___           ___           ___           ___     
               /\  \         /\  \         /\  \         /\__\    
               \:\  \       /::\  \       /::\  \       /::|  |   
                \:\  \     /:/\:\  \     /:/\:\  \     /:|:|  |   
                /::\  \   /::\~\:\  \   /:/  \:\  \   /:/|:|  |__ 
               /:/\:\__\ /:/\:\ \:\__\ /:/__/ \:\__\ /:/ |:| /\__\
              /:/  \/__/ \/_|::\/:/  / \:\  \ /:/  / \/__|:|/:/  /
             /:/  /         |:|::/  /   \:\  /:/  /      |:/:/  / 
             \/__/          |:|\/__/     \:\/:/  /       |::/  /  
                            |:|  |        \::/  /        /:/  /   made by:
                             \|__|         \/__/         \/__/    Victor Ricardo.
                ___           ___           ___           ___     
               /\  \         /\  \         /\__\         /\  \    
              /::\  \       /::\  \       /::|  |       /::\  \   
             /:/\:\  \     /:/\:\  \     /:|:|  |      /:/\:\  \  
            /:/  \:\  \   /::\~\:\  \   /:/|:|__|__   /::\~\:\  \ 
           /:/__/_\:\__\ /:/\:\ \:\__\ /:/ |::::\__\ /:/\:\ \:\__\
           \:\  /\ \/__/ \/__\:\/:/  / \/__/~~/:/  / \:\~\:\ \/__/
            \:\ \:\__\        \::/  /        /:/  /   \:\ \:\__\  
             \:\/:/  /        /:/  /        /:/  /     \:\ \/__/  
              \::/  /        /:/  /        /:/  /       \:\__\    
               \/__/         \/__/         \/__/         \/__/ ");
            int ops = 0;
            int Flag = 0;
            int selected = 0;
            Console.SetCursorPosition(30, 28);
            Console.Write("Dificultad");
            while (Flag == 0)
            {
                int[] optinum = { 60, 30, 15 };
                Console.SetCursorPosition(30, 30);
                ConsoleKeyInfo sel;

                if (ops == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (ops == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else { Console.ForegroundColor = ConsoleColor.Red; }
                Console.Write(opti[ops]);
                sel = Console.ReadKey();
                selected = optinum[ops];
                switch (sel.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (ops < 2)
                            ops++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (ops > 0)
                            ops--;
                        break;
                    case ConsoleKey.Enter:
                        Flag = 1;
                        Console.Clear();
                        break;
                }
            }
            putWalls();//map start
            while (op == 1)//variable op used to verify if we want play again or exit
            {
                if (Console.KeyAvailable)//verify if press keys
                {
                    ConsoleKeyInfo ki = Console.ReadKey(true);//here the key is saved in ki variable
                    ChangePlayerDirection(ki);//Here i send the value to the function
                }
                movePlayer();//call the function movePlayer(); (This move the 2 players)
                GeneratePlayers(selected);//Call the function GeneratePlayers(); (thisgenerate players)
            }//cycle to movement
            Console.Clear();//clear window content
            Console.SetCursorPosition(10, 10);//position to the next line
            Console.WriteLine("Come again!");
            Console.ReadKey();


        }//close main
    }//class program
}//namespace
