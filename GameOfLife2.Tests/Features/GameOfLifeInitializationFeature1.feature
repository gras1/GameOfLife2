Feature: GameOfLifeInitializationFeature1
	The universe of the Game of Life is an infinite two-dimensional orthogonal grid of square cells, each of which is in
	one of two possible states, alive or dead. Every cell interacts with its eight neighbours, which are the cells that
	are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:
	1. Any live cell with fewer than two live neighbours dies, as if caused by under-population.
	2. Any live cell with two or three live neighbours lives on to the next generation.
	3. Any live cell with more than three live neighbours dies, as if by overcrowding.
	4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
	The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules
	simultaneously to every cell in the seed—births and deaths occur simultaneously, and the discrete moment at which
	this happens is sometimes called a tick (in other words, each generation is a pure function of the preceding one).
	The rules continue to be applied repeatedly to create further generations.

Scenario: Initializing a GameGrid with no configuration
	Given I start the Game Of Life
	When I create a GameGrid 10 cells wide by 10 cells high
	Then the number of cells along the first dimension of the GameGrid will be 10
	And the number of cells along the second dimension of the GameGrid will be 10

Scenario: Initializing a GameGrid with a random seed value of 1
	Given I start the Game Of Life
	When I create a GameGrid 10 cells wide by 10 cells high with a seeded random configuration of 1
	Then the cell in position 0, 3 has a state of true
	And the cell in position 0, 0 has 3 neighbours
	And the cell in position 1, 1 has 8 neighbours
	And the cell in position 9, 1 has 5 neighbours

Scenario: Checking for the first rule after initialising the GameGrid with a specific configuration
	Given I start the Game Of Life with a GameGrid with the following Cell state
	| column0 | column1 | column2 |
	| false   | false   | false   |
	| false   | true    | false   |
	| false   | false   | false   |
	When I apply game rule 1 to the cell in position 1, 1
	Then the cell in position 1, 1 has a state of false

Scenario: Checking for the second rule after initialising the GameGrid with a specific configuration
	Given I start the Game Of Life with a GameGrid with the following Cell state
	| column0 | column1 | column2 |
	| false   | false   | true    |
	| true    | true    | false   |
	| false   | true    | false   |
	When I apply game rule 2 to the cell in position 1, 1
	Then the cell in position 1, 1 has a state of true

Scenario: Checking for the third rule after initialising the GameGrid with a specific configuration
	Given I start the Game Of Life with a GameGrid with the following Cell state
	| column0 | column1 | column2 |
	| false   | false   | true    |
	| true    | true    | true    |
	| false   | true    | false   |
	When I apply game rule 3 to the cell in position 1, 1
	Then the cell in position 1, 1 has a state of false

Scenario: Checking for the fourth rule after initialising the GameGrid with a specific configuration
	Given I start the Game Of Life with a GameGrid with the following Cell state
	| column0 | column1 | column2 |
	| false   | false   | true    |
	| true    | false   | false   |
	| false   | true    | false   |
	When I apply game rule 4 to the cell in position 1, 1
	Then the cell in position 1, 1 has a state of true

Scenario: Iterating the GameGrid after initialising it with a specific configuration
	Given I start the Game Of Life with a GameGrid with the following Cell state
	| column0 | column1 | column2 | column3 | column4 |
	| false   | false   | false   | true    | false   |
	| false   | true    | false   | false   | false   |
	| false   | true    | true    | true    | false   |
	| false   | false   | true    | true    | false   |
	| false   | true    | true    | false   | false   |
	When I iterate the GameGrid with all game rules
	Then the cell in position 1, 1 has a state of true
	And the cell in position 2, 2 has a state of false
	And the cell in position 0, 3 has a state of false
	And the cell in position 1, 2 has a state of false
	And the cell in position 1, 3 has a state of true
