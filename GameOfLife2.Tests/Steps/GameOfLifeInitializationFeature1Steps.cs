using System;
using System.Collections.Generic;

using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GameOfLife2.Tests.Steps
{
    using Entities;

    [Binding]
    public class GameOfLifeInitializationFeature1Steps
    {
        private GameGrid _gameGrid;
        private const int LENGTHDIMENSIONINDEX = 0;
        private const int HEIGHTDIMENSIONINDEX = 1;

        [Given(@"I start the Game Of Life")]
        public void GivenIStartTheGameOfLife()
        {
            _gameGrid = new GameGrid();
        }
        
        [When(@"I create a GameGrid (.*) cells wide by (.*) cells high")]
        public void WhenICreateAGameGridCellsWideByCellsHigh(int numberOfCellsLength, int numberOfCellsHeight)
        {
            _gameGrid.Cells = new Cell[numberOfCellsHeight, numberOfCellsLength];
        }
        
        [Then(@"the number of cells along the first dimension of the GameGrid will be (.*)")]
        public void ThenTheNumberOfCellsAlongTheFirstDimensionOfTheGameGridWillBe(int numberOfCellsLength)
        {
            Assert.IsTrue(_gameGrid.Cells.GetLength(LENGTHDIMENSIONINDEX) == numberOfCellsLength);
        }
        
        [Then(@"the number of cells along the second dimension of the GameGrid will be (.*)")]
        public void ThenTheNumberOfCellsAlongTheSecondDimensionOfTheGameGridWillBe(int numberOfCellsHeight)
        {
            Assert.IsTrue(_gameGrid.Cells.GetLength(HEIGHTDIMENSIONINDEX) == numberOfCellsHeight);
        }

        [When(@"I create a GameGrid (.*) cells wide by (.*) cells high with a seeded random configuration of (.*)")]
        public void WhenICreateAGameGridCellsWideByCellsHighWithASeededRandomConfigurationOf(int numberOfCellsLength, int numberOfCellsHeight, int randomSeedValue)
        {
            _gameGrid.Initialise(numberOfCellsLength, numberOfCellsHeight, randomSeedValue);
        }

        [Then(@"the cell in position (.*), (.*) has a state of true")]
        public void ThenTheCellInPositionHasAStateOfTrue(int positionY, int positionX)
        {
            Assert.IsTrue(_gameGrid.Cells[positionY, positionX].State);
        }

        [Then(@"the cell in position (.*), (.*) has a state of false")]
        public void ThenTheCellInPositionHasAStateOfFalse(int positionY, int positionX)
        {
            Assert.IsTrue(!_gameGrid.Cells[positionY, positionX].State);
        }

        [Then(@"the cell in position (.*), (.*) has (.*) neighbours")]
        public void ThenTheCellInPositionHasNeighbours(int positionY, int positionX, int numberOfNeighbours)
        {
            Assert.IsTrue(_gameGrid.Cells[positionY, positionX].NumberOfNeighbours == numberOfNeighbours);
        }

        [Given(@"I start the Game Of Life with a GameGrid with the following Cell state")]
        public void GivenIStartTheGameOfLifeWithAGameGridWithTheFollowingCellState(Table cellStates)
        {
            _gameGrid = new GameGrid();
            int columnCount = cellStates.Rows[0].Keys.Count;

            List<bool[]> initialCellStates = new List<bool[]>();
            foreach (var row in cellStates.Rows)
            {
                bool[] initialCellRowStates = new bool[columnCount];
                for (var i = 0; i < columnCount; i++)
                {
                    initialCellRowStates[i] = Convert.ToBoolean(row[i]);
                }
                initialCellStates.Add(initialCellRowStates);
            }

            _gameGrid.Initialise(initialCellStates);
        }

        [When(@"I apply game rule (.*) to the cell in position (.*), (.*)")]
        public void WhenIApplyGameRuleToTheCellInPosition(int ruleNumber, int positionX, int positionY)
        {
            Cell currentCell = _gameGrid.Cells[positionY, positionX];
            int numberOfLiveNeighbours = _gameGrid.CalculateNumberOfLiveNeighbours(currentCell, positionX, positionY);
            Cell.CalculatedCellState calculatedCellState = Cell.CalculatedCellState.NotSet;
            switch (ruleNumber)
            {
                case 1:
                    calculatedCellState = Cell.CheckRule1(numberOfLiveNeighbours, currentCell.State);
                    break;
                case 2:
                    calculatedCellState = Cell.CheckRule2(numberOfLiveNeighbours, currentCell.State);
                    break;
                case 3:
                    calculatedCellState = Cell.CheckRule3(numberOfLiveNeighbours, currentCell.State);
                    break;
                case 4:
                    calculatedCellState = Cell.CheckRule4(numberOfLiveNeighbours, currentCell.State);
                    break;
            }
            _gameGrid.Cells[positionY, positionX].State = calculatedCellState == Cell.CalculatedCellState.Alive;
        }


        [When(@"I iterate the GameGrid with all game rules")]
        public void WhenIIterateTheGameGridWithAllGameRules()
        {
            _gameGrid.Iterate();
        }

    }
}
