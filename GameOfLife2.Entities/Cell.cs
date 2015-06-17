using System;
using System.Runtime.Remoting;

namespace GameOfLife2.Entities
{
    [Serializable]
    public class Cell
    {
        public enum CalculatedCellState
        {
            NotSet = 0,
            Dead = 1,
            Alive = 2
        }
        public bool State { get; set; }
        public bool HasLeftNeighbour { get; set; }
        public bool HasTopLeftNeighbour { get; set; }
        public bool HasTopNeighbour { get; set; }
        public bool HasTopRightNeighbour { get; set; }
        public bool HasRightNeighbour { get; set; }
        public bool HasBottomRightNeighbour { get; set; }
        public bool HasBottomNeighbour { get; set; }
        public bool HasBottomLeftNeighbour { get; set; }

        public int NumberOfNeighbours
        {
            get
            {
                int count = 0;
                if (HasLeftNeighbour)
                {
                    count++;
                }
                if (HasTopLeftNeighbour)
                {
                    count++;
                }
                if (HasTopNeighbour)
                {
                    count++;
                }
                if (HasTopRightNeighbour)
                {
                    count++;
                }
                if (HasRightNeighbour)
                {
                    count++;
                }
                if (HasBottomRightNeighbour)
                {
                    count++;
                }
                if (HasBottomNeighbour)
                {
                    count++;
                }
                if (HasBottomLeftNeighbour)
                {
                    count++;
                }
                return count;
            }
        }

        public bool GetNextState(int numberOfLiveNeighbours)
        {
            CalculatedCellState cellState = CheckRule1(numberOfLiveNeighbours, State);
            if (cellState == CalculatedCellState.NotSet)
            {
                cellState = CheckRule2(numberOfLiveNeighbours, State);
                if (cellState == CalculatedCellState.NotSet)
                {
                    cellState = CheckRule3(numberOfLiveNeighbours, State);
                    if (cellState == CalculatedCellState.NotSet)
                    {
                        cellState = CheckRule4(numberOfLiveNeighbours, State);
                    }
                }
            }

            switch (cellState)
            {
                case CalculatedCellState.Dead:
                    return false;
                case CalculatedCellState.Alive:
                    return true;
                default:
                    return State;
            }
        }

        /*
        4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        */
        public static CalculatedCellState CheckRule4(int numberOfLiveNeighbours, bool alive)
        {
            CalculatedCellState cellState = CalculatedCellState.NotSet;
            if (!alive)
            {
                cellState = numberOfLiveNeighbours == 3 ? CalculatedCellState.Alive : CalculatedCellState.Dead;
            }
            return cellState;
        }

        /*
        3. Any live cell with more than three live neighbours dies, as if by overcrowding.
        */
        public static CalculatedCellState CheckRule3(int numberOfLiveNeighbours, bool alive)
        {
            CalculatedCellState cellState = CalculatedCellState.NotSet;
            if (numberOfLiveNeighbours > 3 && alive)
            {
                cellState = CalculatedCellState.Dead;
            }
            return cellState;
        }

        /*
        2. Any live cell with two or three live neighbours lives on to the next generation.
        */
        public static CalculatedCellState CheckRule2(int numberOfLiveNeighbours, bool alive)
        {
            CalculatedCellState cellState = CalculatedCellState.NotSet;
            if ((numberOfLiveNeighbours == 2 || numberOfLiveNeighbours == 3) && alive)
            {
                cellState = CalculatedCellState.Alive;
            }
            return cellState;
        }

        /*
        1. Any live cell with fewer than two live neighbours dies, as if caused by under-population.
        */
        public static CalculatedCellState CheckRule1(int numberOfLiveNeighbours, bool alive)
        {
            CalculatedCellState cellState = CalculatedCellState.NotSet;
            if (numberOfLiveNeighbours < 2 && alive)
            {
                cellState = CalculatedCellState.Dead;
            }
            return cellState;
        }
    }
}