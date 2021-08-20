# What is Monopoly Lads? 

The end goal of the project is to utilize the reinforcment learning strategy to train an AI system that can compete with and outperform human opponents in a standard game of Monopoly. This iteration only has updated machine learning capabilites regarding getting out of jail and buying properties.  

# Dependencies 

We used an API called Monopyly (https://github.com/richard-shepherd/monopyly) which provides the functionality of Monopoly along with sample players to play against. It is important to note, this API was created 8 years ago and a lot of time was spent to get the API up and running again. Some things still do not function properly (mainly the GUI).

The sample AIs provided (ie. Generous Daddy, Sophie, Lazy Bones, etc) do not use any deep learning. Essentially each sample AI makes decisions based on their 'play style'. For example, the Daddy class buys as many properties as possible while Sophie only buys properties at certain locations (more specific descriptions available toward the top of each AI file). 

# Set-Up

This part depends on what exactly you're looking for. The API is designed to either play a tournament of a large amount of games OR a single game. Depending on what you want to run, some code requires slight changes, all within the file main.py. 

**Tournament vs. Single Game:**
If you want to play only a single game, change the boolean "play_tournament" on line 5 to False. If you want to play multiple games, set it to True.

**Rounds in a game:**
Usually, a game of Monopoly goes until only one player is not bankrupt. This API is different in that each game lasts a set amount of rounds, and if you want to change that, change the variable "number_of_rounds" on line 25 to your desired amount. 

**Number of games:**
If you want to change the amount of games in a tournament, change the variable "maximum_games" on line 26 to your liking.

**The more games/rounds, the more the system will have to train itself with.**

**See rewards:**
If you want to see the rewards of the agent progress over time, you need to un-comment certain print statements. For the rewards regarding buying properties, un-comment lines 88 and 89. For the rewards regarding getting out of jail, un-comment lines lines 187 through 190.

# Running

It's simple: after you've done your set up, simply run main.py. We recommend diverting it to a txt file for output.

# Members
- Lehar Mogilisetty
- Nick Lutrzykowski
- Tristan Roberts
- Yash Sabarad
