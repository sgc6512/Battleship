using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Battleship
{
    public class Game
    {
        static void Main(string[] args)
        {
            // Define the ships
            List<string> shipCoords = new List<string>
            {
                "3:2,3:5", // 4-cell ship
                "6:1,6:2", // 2-cell ship
                "7:3,9:3" // 3-cell ship
            };

            // Play the game with some guesses
            List<string> guesses = new List<string>
            {
                "7:0",
                "3:3",
                "6:1",
                "6:2",
                "7:3",
                "8:3",
                "9:3"
            };

            // Create the board
            bool[,] board = CreateBoard(shipCoords);

            // Find the number of ships sunk
            int numSunk = Play(shipCoords, guesses, board);

            // Display the game board and the number of ships sunk
            Console.WriteLine($"Number of ships sunk: {numSunk}");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for(int t = 0; t < board.GetLength(1); t++)
                {
                    if (board[i, t])
                        Console.Write("O ");
                    else
                        Console.Write("X ");
                }
                Console.WriteLine("");
            }
        }

        public static bool[,] CreateBoard(List<string> ships)
        {
            bool[,] board = new bool[10, 10]; // Initialize the game board to all false (no hits)

            // Place the ships on the board
            foreach (string coord in ships)
            {
                // Parse the ship coordinates
                string[] parts = coord.Split(',');
                int x1 = int.Parse(parts[0].Split(':')[0]);
                int y1 = int.Parse(parts[0].Split(':')[1]);
                int x2 = int.Parse(parts[1].Split(':')[0]);
                int y2 = int.Parse(parts[1].Split(':')[1]);

                // Place the ship on the board
                for (int i = 0; i < x2 - x1 + 1; i++)
                {
                    for (int t = 0; t < y2 - y1 + 1; t++)
                    {
                        int x = x1 + i;
                        int y = y1 + t;
                        board[x, y] = true;
                    }
                }
            }
            return board;
        }

        public static int Play(List<string> ships, List<string> guesses, bool[,] board)
        {
            int numSunk = 0;

            // Process the guesses
            foreach (string guess in guesses)
            {
                // Parse the guess coordinates
                int x = int.Parse(guess.Split(':')[0]);
                int y = int.Parse(guess.Split(':')[1]);

                // Check if the guess hits a ship
                if (board[x, y])
                {
                    // Mark the cell as hit
                    board[x, y] = false;

                    // Check if the ship has been sunk
                    string sunkCheck = IsShipSunk(board, ships, x, y);
                    if (sunkCheck != "Not Sunk")
                    {
                        numSunk++;
                        ships.Remove(sunkCheck);
                    }
                }
            }

            return numSunk;
        }

        public static string IsShipSunk(bool[,] board, List<string> ships, int x, int y)
        {
            // Find the ship that was hit
            string hitShip = "";
            foreach (string coord in ships)
            {
                // Parse the ship coordinates
                string[] parts = coord.Split(',');
                int x1 = int.Parse(parts[0].Split(':')[0]);
                int y1 = int.Parse(parts[0].Split(':')[1]);
                int x2 = int.Parse(parts[1].Split(':')[0]);
                int y2 = int.Parse(parts[1].Split(':')[1]);

                // Determine the orientation of the ship (horizontal or vertical)
                bool horizontal = (y1 == y2);

                // Check if the hit cell is part of this ship
                if (horizontal)
                {
                    if (x >= x1 && x <= x2 && y == y1)
                    {
                        hitShip = coord;
                        break;
                    }
                }
                else
                {
                    if (y >= y1 && y <= y2 && x == x1)
                    {
                        hitShip = coord;
                        break;
                    }
                }
            }

            // Check if the ship has been sunk
            if (hitShip != "")
            {
                string[] parts = hitShip.Split(',');
                int x1 = int.Parse(parts[0].Split(':')[0]);
                int y1 = int.Parse(parts[0].Split(':')[1]);
                int x2 = int.Parse(parts[1].Split(':')[0]);
                int y2 = int.Parse(parts[1].Split(':')[1]);

                for (int i = 0; i < x2 - x1 + 1; i++)
                {
                    for (int t = 0; t < y2 - y1 + 1; t++)
                    {
                        int xCoord = x1 + i;
                        int yCoord = y1 + t;
                        if (board[xCoord, yCoord])
                        {
                            return "Not Sunk"; // ship not sunk yet
                        }
                    }
                }
                return $"{x1}:{y1},{x2}:{y2}"; // ship has been sunk
            }

            return "Not Sunk"; // no ship was hit
        }
    }
}
