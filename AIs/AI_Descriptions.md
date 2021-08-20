# Monopoly Lads
The AI that we created for this project. Uses Q-Learning, but only for purchasing unowned properties and for deciding whether or not to get out of jail. Otherwise, all of its actions are the default. 
In terms of buying property, the strategy phase means the system will only buy the property if it will leave them with a cash reserve over 500. During the explore phase, it will derive the Max Q from the sets of rewards.
It is important to explain how the sets of rewards are set up - for buying properties, there are 2 sets. One set **(buyreward)** is based on the action of buying the property, and the other one **(nobuyreward)** is based on not buying the property. 
They are set up with 49 indicies - this represents the range of properties that the player owns, which is the deciding factor for this function. It ranges from 0 to 48, all possible configurations of ownership.
They are all set to 0 at the beginning, and some of them will remain that way - the value at an index only gets updated for that state. Some states will never occur naturally, like a state of ownning all 48 properties. 
But the value at the index will get updated to represent a psotive or a negative reward for a certain action within that state. 

The function for getting out of jail works similarly for the Q-Learning, but this one has 4 sets.
Two sets represent the state of the amount of buildings on the board, which is meant to represent exactly how high the rent is on the board. They both go up to 130 because that is the maximum amount of buildings that can be on the board. 
One of these sets **(buildings_stayin)** is for when the system decides to not leave jail, and the other **(buildings_getout)** is for when the system decides to leave jail. Just like the other function, they start at 0, and are updated
when that state occurs within the game. The remaining two sets are for the amount of unowned properties. One set **(properties_getout)** is when the system decides to leave jail, and the other **(properties_stayin)** is for when the system decides to stay within jail.
Just like the others, these start at 0 and update during gameplay. For reasoning, this function will only decide to leave jail if over half the properties are up for sale (14 or more) and will stay in jail otherwise.

Obviously, these functions are much more complex in real life. But unfortunately, that was outside of our scope of possibilites. Our next step would've been to define a function for recieving deals from other players, and then to eventually get all functions based off of Q-Learning.

**None of the rest are really AIs - they do not use any sort of learning. Instead, they are designed around certain personalities and their actions are hard-coded.**
# Lazy Bones
Based entirely off the default actions that are within "/monopyly/game/player_ai_base.py" and this means that it never buys property, always pays fine, never says Happy Birthday, etc. If you look within lazy_bones.py, you will see that it is practically empty.

# Sophie
Designed to play like Sophie - we can only assume this is referring to a Sophie that the original developer of the API knew. She uses default actions only while in jail. She has a specific list of properties that she wants, and this influences both her buying and making deals. She will only buy a property if it is within the list of properties she wants **(check lines 16 through 21)** and will always attempt to make a deal for a property she wants, and it will always be double the MSRP of the card. She will always mortgage is the cash she has is below 500, and will always unmortgage if her cash is above 2000. She will always try to build houses if she has over 1000 in cash.

# Generous Daddy
Designed around the original developer's playstyle when he would play with his children. He will always buy property as long as he is left with at least 500 in cash after the transaction. Will only accept deals if they are for a single property AND the money being given is higher than the original price. He will always try to build a house but only will if his cash after the transcation is above 500. He will make deals of a random amount for other properties with no discretion. 

# Mean Daddy
This one derives directly from Generous Daddy, but is more stingy. It will **always** reject deals, and also has a considerably lower random range for making offers in auctions.

# AI Base
This is given within "/monopyly/game/player_ai_base.py" and are the default actions for any created AIs - this means that an AI does not have to have all actions designed to function. The default actions are the following:
- Never buys property
- Never wishes a player "Happy Birthday" and instead tells them to "Choke on their birthday cake"
- Will always choose to pay a fine over picking a Chance card
- Never bids in auctions
- Never builds houses
- Never mortgages properties
- Never unmortgages properties (but that will never occur naturally anyway)
- Never chooses to leave jail
- Never makes deals
- Always rejects deals
