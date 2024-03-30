# Prisoner's dilemma (PD) simulation
PD is a two player variable-sum game, where the sum of the payoffs of the two players is not constant.
Originally formulated by the American mathematician Albert W. Tucker. Two prisoners, A and B, suspected of
committing a robbery together, are isolated and urged to confess. Each is concerned only with getting the
shortest possible prison sentence for himself; each must decide whether to confess without knowing his
partner’s decision. Both prisoners, however, know the consequences of their decisions: 
- (1) if both confess, both go to jail for five years; 
- (2) if neither confesses, both go to jail for one year (for carrying concealed weapons); and 
- (3) if one confesses while the other does not, the confessor goes free (for turning state’s evidence)
and the silent one goes to jail for 20 years. 

## PD simulation variant
This variant of PD is using the following payoffs:
- if both A nd B cooperate, both get 3 points,
- if A cooperates and B defects, A gets 0 points and B gets 5 points,
- if A defects and B cooperates, A gets 5 points and B gets 0 points,
- if both defect, both get 1 point.

## Simulation rules
- The game is played for a random number of rounds. Therefore it resembles the infinitely repeated PD.
- The player decission algorythms are called startegies. Each strategy is playing against all other 
strategies and against itself.
- No communication between the strategies is allowed.
- The PD and this simulation is a perfect information game meaning that all information is available to 
players. Each strategy knows all previous turns of the current game.
- On each turn both strategies present their decission without knowing the opponents current turn decission. 
The strategy can choose to either defect or cooperate
- The strategies are evaluated based on the total points they have accumulated during the simulation and are 
- ranked based on the total points they have accumulated.


## Strategy traits
One of the main characteristics of a strategy is being `nice` or `nasty`. 
Nice strategies never defect first, while nasty strategies always defect first when playing against nice strategies.
The strategy's character is calculated by the simulator, based on the strategy's decisions. Nice strategies are 
colored green, while nasty strategies are colored red. 


