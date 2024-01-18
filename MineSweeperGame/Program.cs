// See https://aka.ms/new-console-template for more information
using MineSweeperGame;

Console.WriteLine("Hello, World!");

Console.WriteLine("Welcome to Minesweeper!\n");

int gridSize;
do
{
    Console.Write("Enter the size of the grid (minimum 2, maximum 10): ");
} while (!int.TryParse(Console.ReadLine(), out gridSize) || gridSize < 2 || gridSize > 10);

int maxMines = (int)(0.35 * gridSize * gridSize);
int numMines;
do
{
    Console.Write($"Enter the number of mines to place on the grid (maximum is {maxMines}): ");
} while (!int.TryParse(Console.ReadLine(), out numMines) || numMines < 1 || numMines > maxMines);

MinesweeperGame game = new MinesweeperGame(gridSize, numMines);
game.PlayGame();

Console.ReadKey();