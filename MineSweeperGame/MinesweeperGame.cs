using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperGame
{
    public class MinesweeperGame
    {
        private char[,] grid;
        private char[,] mineField;
        private int gridSize;
        private int squaresToUncover;

        public MinesweeperGame(int size, int mines)
        {
            gridSize = size;
            squaresToUncover = size * size - mines;
            grid = new char[size, size];
            mineField = new char[size, size];

            InitializeGrid();
            PlaceMines(mines);
            CalculateAdjacentMines();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = '_';
                    mineField[i, j] = '_';
                }
            }
        }

        private void PlaceMines(int mines)
        {
            Random random = new Random();

            for (int i = 0; i < mines; i++)
            {
                int row, col;
                do
                {
                    row = random.Next(0, gridSize);
                    col = random.Next(0, gridSize);
                } while (mineField[row, col] == '*');

                mineField[row, col] = '*';
            }
        }

        private void CalculateAdjacentMines()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (mineField[i, j] != '*')
                    {
                        int count = CountAdjacentMines(i, j);
                        mineField[i, j] = count.ToString()[0];
                    }
                }
            }
        }

        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize && mineField[i, j] == '*')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void DisplayGrid()
        {
            Console.WriteLine("  " + string.Join(" ", new string[gridSize]));
            for (int i = 0; i < gridSize; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private bool IsValidInput(string input)
        {
            if (input.Length != 2)
                return false;

            int row = input[0] - 'A';
            int col = input[1] - '1';

            return row >= 0 && row < gridSize && col >= 0 && col < gridSize && grid[row, col] == '_';
        }

        private void UncoverSquare(int row, int col)
        {
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize || grid[row, col] != '_')
                return;

            grid[row, col] = mineField[row, col];

            if (grid[row, col] == '0')
            {
                for (int r = -1; r <= 1; r++)
                {
                    for (int c = -1; c <= 1; c++)
                    {
                        int x = row + r;
                        int y = col + c;

                        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize && (r != 0 || c != 0))
                            UncoverSquare(x, y);
                    }
                }
            }
        }

        public void PlayGame()
        {
            Console.WriteLine("Welcome to Minesweeper!\n");

            while (true)
            {
                int size, mines;
                do
                {
                    Console.Write("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
                } while (!int.TryParse(Console.ReadLine(), out size) || size < 2 || size > 10);

                do
                {
                    Console.Write("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                } while (!int.TryParse(Console.ReadLine(), out mines) || mines < 1 || mines > (size * size * 35) / 100);

                MinesweeperGame game = new MinesweeperGame(size, mines);

                Console.WriteLine("\nHere is your minefield:");
                game.DisplayGrid();

                while (true)
                {
                    string userInput;
                    do
                    {
                        Console.Write("\nSelect a square to reveal (e.g. A1): ");
                        userInput = Console.ReadLine().Trim().ToUpper();
                    } while (!game.IsValidInput(userInput));

                    int row = userInput[0] - 'A';
                    int col = userInput[1] - '1';

                    if (game.mineField[row, col] == '*')
                    {
                        Console.WriteLine("\nOh no, you detonated a mine! Game over.\n");
                        break;
                    }
                    else
                    {
                        game.UncoverSquare(row, col);
                        game.DisplayGrid();

                        if (game.squaresToUncover == 0)
                        {
                            Console.WriteLine("\nCongratulations, you have won the game!\n");
                            break;
                        }
                    }
                }

                Console.WriteLine("Press any key to play again or 'q' to quit.");
                if (Console.ReadLine().Trim().ToLower() == "q")
                    break;

                Console.WriteLine();
            }
        }

    }
}
