# ArcheryGame
Small archery game mechanic as a practicing console app

The task is to implement a turn-based strategy game. The strategy game is a two-player archery game. A player has:
		
- Each player has a name; this must be specified at the beginning of the game
-	Each player has a life point (we start with 3)
-	Each player has arrows (we start with 0)
-	There is some distance between the two players (initially 5).
		
In each round, players can choose from:
		
1.Create an arrow (max number of arrows 12)
2.Move closer (this reduces the distance between the two players; min distance: 1)
3.Move further afield (increasing the distance between the two players)
4.Shooting; This reduces the number of arrows and, if the opponent is hit, his points are reduced. The probability of shooting success is inversely proportional to the distance.

Here's how to play:
The two players choose something from each round.
Implement a human player who can choose from a range of options on the console
Implement an AI player who chooses an option on their own
Let it be possible to choose between human-human, human-machine, machine-machine modes
At the beginning of the game, you can specify how many rounds you want to play, after which a game goes down in that number of rounds
At the end of the game, we display the winner
		
		
