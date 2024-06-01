# Instructions for running

## Cloning the repository
```
git clone https://github.com/MESC2004/TC2005B_Videogame
```
After cloning, you will be able to run the unity project by opening the project with the route:
```
clone_path/MESC2004/TC2005B_Videogame/SUBMIL_TCG
```

## Running the game
To experience all of the features the game has, it must be run from the title scene. You will be able to register a user and account by clicking "Register", then typing the credentials you wish to use. Currently this is done locally. Logging in with the credentials will take you to the deck creation scene. Credentials are saved locally on a txt file named credentials.txt.
From the title screen, you can also access the deck screen directly, counting as a guest, and allowing to modify the predefined deck that is given. You may add up to 3 identity cards, and as many of the other cards as you wish to add. In the future, draw cards will also limited.
Upon saving the deck, the data will be sent over for  when you choose to play a match against AI. This can be done by clicking the "Play" button.

## Combat instructions
Upon entering the combat phase, you will be able to play against an AI opponent. Each turn is divided in 4 phases: Swap, Draw, Play and End phases.

- Swap Phase
At the beginning of oyur turn, you are given the chance to swap the card that is on the top of your triangle for one of the bottom cards. If you wish to leave the top card as is, click the top card. If not, click the card you wish to swap it with. Swapping cards will reset the car's Speed to its original value, so long as it is alive (More than 0 HP).

- Draw Phase
In this phase, the Deck on the left of your screen will be interactable, and you must click it to draw 3 cards from your deck. If your deck has run out of cards, it will be refilled and reshuiffled with the cards from the discard pile. Decks are always randomized at the beginning of the match, and when the p[reviously mentioned situation arises.

- Play phase
In this phase, you will be able to click the cards in your hand to play them. The possible options are:
  - Attack cards: When these are played, damage is applied invisibly to the card in combat. This card advances you to the end phase when played.
  - Defense cards: When these are played, a certain amount of defense points are assigned to the card on top of the triangle (card in combat), and is now able to absorb an amount of damage less than or equal to its defense value. This card also etakes you to the end phase.
  - Effect cards: When these are played, the effect specified in the card will be applied to the card in combat. These cards will not end your turn, so feel free to use as many as you want for your strategy.
  - Draw cards: When these are played, you will draw 2 cards from your deck and the card will be discarded. These cards also will not end your turn.

- End phase
In this phase, all necessary changes are made, including damage to the opposing card.

- Enemy Turn
The enemy follows the same phases as the player, currently too fast to follow due to a bug. This, however, does not affect the start of the p[layer's next turn, and stat chnages and interactions are made as intended.

- Win or Lose condition
You will win the match if you manage to lower two of the enemy's cards to 0 HP (Sometimes it takes 3, bug is being worked on)
You will lose the match if 2 of your cards reach 0 HP (As intended, you lose if 2 cards die)

Most interactions are made through left mouse clicks, except the text input boxes and scroll inputs in some scenes such as the scrollers in the deck scene and combat scene, or the text inputs on the register and title screens.

Game logic is fully finished, only the exceptional win condition of having to kill 3 instead of 2 enemy cards needs fixing.

Some designs still need to be implemented, suuch as button designs or different fonts for styling the game.

Passives of identity cards still not implemented, might remove from project alltogether.

# THIS CHANGE IS POST DELIVERY
Noticed that even though debug message of player win appears, panel for game win does not activate. Not too sure why.
