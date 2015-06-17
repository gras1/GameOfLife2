using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameOfLife2.Entities
{
    [Serializable]
    public class GameGrid
    {
        public Cell[,] Cells { get; set; }

        public void Initialise(int numberOfCellsLength, int numberOfCellsHeight, int randomSeedValue)
        {
            Random cellStateRandomiser = randomSeedValue == 0 ? new Random() : new Random(randomSeedValue);

            var cells = new Cell[numberOfCellsHeight, numberOfCellsLength];
            for (var positionY = 0; positionY < numberOfCellsHeight; positionY++)
            {
                for (var positionX = 0; positionX < numberOfCellsLength; positionX++)
                {
                    int nextRandomNumber = cellStateRandomiser.Next(0, 2);
                    cells[positionY, positionX] = CreateCell(nextRandomNumber == 1, positionY, positionX, numberOfCellsLength, numberOfCellsHeight);
                }
            }

            Cells = cells;
        }

        public void Initialise(List<bool[]> initialCellStates)
        {
            int numberOfCellsHeight = initialCellStates.Count;
            int numberOfCellsLength = initialCellStates[0].GetLength(0);
            var cells = new Cell[numberOfCellsHeight, numberOfCellsLength];
            for (var positionY = 0; positionY < numberOfCellsHeight; positionY++)
            {
                for (var positionX = 0; positionX < numberOfCellsLength; positionX++)
                {
                    cells[positionY, positionX] = CreateCell(initialCellStates[positionY][positionX], positionY, positionX, numberOfCellsLength, numberOfCellsHeight);
                }
            }

            Cells = cells;
        }

        private static Cell CreateCell(bool state, int positionY, int positionX, int width, int length)
        {
            var cell = new Cell
            {
                State = state,
                HasLeftNeighbour = (positionX - 1 >= 0),
                HasTopLeftNeighbour = ((positionX - 1 >= 0) && (positionY - 1 >= 0)),
                HasTopNeighbour = (positionY - 1 >= 0),
                HasTopRightNeighbour = ((positionY - 1 >= 0) && (positionX + 1 < width)),
                HasRightNeighbour = (positionX + 1 < width),
                HasBottomRightNeighbour = ((positionY + 1 < length) && (positionX + 1 < width)),
                HasBottomNeighbour = (positionY + 1 < length),
                HasBottomLeftNeighbour = ((positionX - 1 >= 0) && (positionY + 1 < length))
            };
            return cell;
        }

        public void Iterate()
        {
            int numberOfCellsLength = Cells.GetLength(1);
            int numberOfCellsHeight = Cells.GetLength(0);
            GameGrid nextGameGrid = Clone(this);
            for (var positionY = 0; positionY < numberOfCellsHeight; positionY++)
            {
                for (var positionX = 0; positionX < numberOfCellsLength; positionX++)
                {
                    Cell cellToCheck = Cells[positionY, positionX];
                    nextGameGrid.Cells[positionY, positionX].State = cellToCheck.GetNextState(CalculateNumberOfLiveNeighbours(cellToCheck, positionX, positionY));
                }
            }

            Cells = nextGameGrid.Cells;
        }

        public int CalculateNumberOfLiveNeighbours(Cell cellToCheck, int positionX, int positionY)
        {
            int numberOfLiveNeighbours = 0;
            if (cellToCheck.HasLeftNeighbour)
            {
                if (Cells[positionY, positionX - 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasTopLeftNeighbour)
            {
                if (Cells[positionY - 1, positionX - 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasTopNeighbour)
            {
                if (Cells[positionY - 1, positionX].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasTopRightNeighbour)
            {
                if (Cells[positionY - 1, positionX + 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasRightNeighbour)
            {
                if (Cells[positionY, positionX + 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasBottomRightNeighbour)
            {
                if (Cells[positionY + 1, positionX + 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasBottomNeighbour)
            {
                if (Cells[positionY + 1, positionX].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            if (cellToCheck.HasBottomLeftNeighbour)
            {
                if (Cells[positionY + 1, positionX - 1].State)
                {
                    numberOfLiveNeighbours++;
                }
            }
            return numberOfLiveNeighbours;
        }

        private static GameGrid Clone(GameGrid source)
        {
            if (ReferenceEquals(source, null))
            {
                return default(GameGrid);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (GameGrid)formatter.Deserialize(stream);
            }
        }
    }
}