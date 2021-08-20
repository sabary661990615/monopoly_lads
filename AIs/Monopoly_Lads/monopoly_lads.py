from monopyly import *
import random

'''
In Each function need to create AI that decides whether or not to buy. 
Can start off by just adding this, without considering the position of other AIs.
'''

'''
state = # of properties
actions = buy or not
reward = +1 for buying, -1 for not buying
'''

buyreward = [0]*29
nobuyreward = [0]*29

properties_getout = [0]*29
properties_stayin = [0]*29
buildings_getout = [0]*130
buildings_stayin = [0]*130

epsilon = 0.3
alpha = 0.5
gamma = 0.9

class MonopolyLadsAI(PlayerAIBase):
    def __init__(self):
        '''
        The 'constructor
        '''
        self.cash_reserve = 500

    def get_name(self):
        '''
        Returns the name shown for this AI.
        '''
        return "Monopoly Lads"
    
    def landed_on_unowned_property(self, game_state, player, property):
        '''
        Called when the AI lands on an unowned property. Only the active
        player receives this notification.
        Must return either the BUY or DO_NOT_BUY action from the
        PlayerAIBase.Action enum.
        '''

        #print(buyreward)
        #print(nobuyreward)

        index = len(player.state.properties)
        price = property.price
        my_money = player.state.cash
        action = 0

        # Exploration
        if random.random() <= epsilon:
            x = random.randint(0,1)
            if x == 0:
                #print("random buy")
                action = 0
            else:
                #print("random don't buy")
                action = 1 # Do not buy
        # Strategy
        else:
            if player.state.cash > (self.cash_reserve + property.price):
                #print("not random buy")
                action = 0
            else:
                #print("not random don't buy")
                action = 1

        # Calculate max between Q(S', buy) and Q(S', don't buy)
        maxq = -100
        #print(index+1)
        if (buyreward[index+1] > maxq):
            maxq = buyreward[index+1]
        if (nobuyreward[index+1] > maxq):
            maxq = nobuyreward[index+1]

        # Update reward (Q Learning)
        if action == 0:
            buyreward[index] = buyreward[index] + alpha*(1 + gamma*maxq - buyreward[index])
        else:
            nobuyreward[index] = nobuyreward[index] + alpha*(-1 + gamma*maxq - nobuyreward[index])

        #print(buyreward)
        #print(nobuyreward)

        # Return Correct Val
        if (action == 0):
            return PlayerAIBase.Action.BUY
        else:
            return PlayerAIBase.Action.DO_NOT_BUY
        
        
    def get_out_of_jail(self, game_state, player):
        '''
        Called in the player's turn, before the dice are rolled, if the player
        is in jail.
        There are three possible return values:
        PlayerAIBase.Action.BUY_WAY_OUT_OF_JAIL
        PlayerAIBase.Action.PLAY_GET_OUT_OF_JAIL_FREE_CARD
        PlayerAIBase.Action.STAY_IN_JAIL
        Buying your way out of jail will cost £50.
        '''

        my_money = player.state.cash # we need to check if we actually have 50 dollars
        action = 6 # default, STAY_IN_JAIL

        which_action = False 
        # False for staying in, true for getting out
        # this will then decide whether or not we pay to get out / use card to get out

        # this variable will hold how many properties are owned
        num_properties = 0
        for temp_player in game_state.players:
            num_properties += len(temp_player.state.properties)

        # this variable will hold how many buildings are on the board
        num_buildings = 0
        for temp_player in game_state.players:
            num_buildings += temp_player.state.get_number_of_houses_and_hotels(game_state.board)[0] + temp_player.state.get_number_of_houses_and_hotels(game_state.board)[1]

        holds_goojf_card = True # see if we actually have a "Get Out of Jail Free" card
        if len(player.state.get_out_of_jail_free_cards) == 0:
            holds_goojf_card = False
        
        # Exploration
        if random.random() <= epsilon:
            x = random.randint(0,1)
            if x == 0:
                # get out of jail
                if holds_goojf_card:
                    # use card to get out
                    action = 5
                else:
                    # pay to get out
                    action = 4
            else:
                # stay in jail
                action = 6
        # Strategy
        else:
            if 28 - num_properties > 13:
                # there are 14 or more properties available, get out
                if holds_goojf_card:
                    # use card to get out
                    action = 5
                else:
                    # pay to get out
                    action = 4
            else:
                # there are less than 14 properties available, stay in
                action = 6 

        # ---------------- REWARD STUFF ----------------

        maxq_buildings = -100
        if (buildings_getout[num_buildings] > maxq_buildings):
            maxq_buildings = buyreward[num_buildings]
        if (buildings_stayin[num_buildings] > maxq_buildings):
            maxq_buildings = buyreward[num_buildings]

        maxq_properties = -100
        if (properties_getout[num_properties] > maxq_properties):
            maxq_properties = buyreward[num_properties]
        if (properties_stayin[num_properties] > maxq_properties):
            maxq_properties = buyreward[num_properties]

        # MAX Qs CALCULATED

        if action == 5 or action == 4:
            # we are leaving
            properties_getout[num_properties] = properties_getout[num_properties] + alpha*(1 + gamma*maxq_properties - properties_getout[num_properties])
            buildings_getout[num_buildings] = buildings_getout[num_buildings] + alpha*(1 + gamma*maxq_buildings - buildings_getout[num_buildings])
        elif action == 6:
            # we are staying
            properties_stayin[num_properties] = properties_stayin[num_properties] + alpha*(1 + gamma*maxq_properties - properties_stayin[num_properties])
            buildings_stayin[num_buildings] = buildings_stayin[num_buildings] + alpha*(1 + gamma*maxq_buildings - buildings_stayin[num_buildings])
        else:
            print("Action is not one of the expected. PROBLEM!")
            print("Action = " + action)
            return None

        #print(properties_stayin)
        #print(properties_getout)
        #print(buildings_stayin)
        #print(buildings_getout)

        if action == 6:
            #print("Lads AI stayed in jail")
            return PlayerAIBase.Action.STAY_IN_JAIL
        elif action == 5:
            #print("Lads AI used a card to get out of jail")
            return PlayerAIBase.Action.PLAY_GET_OUT_OF_JAIL_FREE_CARD
        elif action == 4:
            #print("Lads AI paid to get out of jail")
            return PlayerAIBase.Action.BUY_WAY_OUT_OF_JAIL
        else:
            print("Action is not one of the expected. PROBLEM!")
            print("Action = " + action)
            return None
