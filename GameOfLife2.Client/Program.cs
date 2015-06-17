using System;
using System.Threading;

namespace GameOfLife2.Client
{
    using Entities;

// ReSharper disable once ClassNeverInstantiated.Global
    internal class Program
    {
        #region Game Description

        /*
        The universe of the Game of Life is an infinite two-dimensional orthogonal grid of square cells, each of which is in one of two
        possible states, alive or dead. Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically,
        or diagonally adjacent. At each step in time, the following transitions occur:
        
        1. Any live cell with fewer than two live neighbours dies, as if caused by under-population.
        2. Any live cell with two or three live neighbours lives on to the next generation.
        3. Any live cell with more than three live neighbours dies, as if by overcrowding.
        4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        
        The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules simultaneously to
        every cell in the seed—births and deaths occur simultaneously
        */

        #endregion

        private static GameGrid _gameGrid;

// ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            string line;
            Console.WriteLine("Is Game Seeded?");
            bool isSeeded = false;
            int seed = 0;
            int columns;
            int rows;
            int iterations;
            do
            {
                line = Console.ReadLine();
            } while (line == null || !Boolean.TryParse(line, out isSeeded));
            if (isSeeded)
            {
                Console.WriteLine("What is the game seed?");
                do
                {
                    line = Console.ReadLine();
                } while (line == null || !Int32.TryParse(line, out seed));
            }
            Console.WriteLine("How many rows?");
            do
            {
                line = Console.ReadLine();
            } while (line == null || !Int32.TryParse(line, out columns));
            Console.WriteLine("How many columns?");
            do
            {
                line = Console.ReadLine();
            } while (line == null || !Int32.TryParse(line, out rows));
            Console.WriteLine("How many iterations?");
            do
            {
                line = Console.ReadLine();
            } while (line == null || !Int32.TryParse(line, out iterations));

            _gameGrid = new GameGrid();
            _gameGrid.Initialise(columns, rows, seed);
            DrawGrid(1);

            for (var iterationNumber = 1; iterationNumber <= iterations - 1; iterationNumber++)
            {
                _gameGrid.Iterate();
                DrawGrid(iterationNumber + 1);
            }

            Console.ReadKey();
        }

        private static void DrawGrid(int iterationNumber)
        {
            Console.Clear();
            Console.WriteLine("Sequence: " + iterationNumber);
            for (var j = 0; j < _gameGrid.Cells.GetLength(0); j++)
            {
                for (var i = 0; i < _gameGrid.Cells.GetLength(1); i++)
                {
                    string cellState = _gameGrid.Cells[j, i].State ? ". " : "  ";
                    Console.Write(cellState);
                }
                Console.WriteLine();
            }
            Thread.Sleep(500);
        }
    }
}