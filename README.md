# TC2005B_Videogame
Repository for development of the game project for TC2005B
Developed by Pixel Pioneers

A01784901 - Fabrizio Barrios Blanco

A01784217 - Nicole Dávila Hernández

A01028033 - Miguel Enrique Soria

# **SUBMIL TCG**

## _Game Design Document_

---

##### **Copyright notice / author information / boring legal stuff nobody likes**

##
## _Index_

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## _Game Design_

### Elevator Pitch / Concept Statement

Step into the strategic battleground of Submil, where your wit is your greatest weapon. In this turn-based deckbuilding game, you’ll dive into a world where every card and every move is a step in a high-stakes dance of strategy. With a deck of 23 unique cards, including three identity cards that define your playstyle, you’ll engage in cerebral combat that lasts an intense 14 turns.

Each character is not just a scientist, but a rebel with a cause. They’re fighting not just for victory, but for their beliefs, challenging society itself. In Submil, managing your card’s energy is as crucial as the attacks you launch. Every action, from drawing a card to unleashing an effect, is a calculated risk that could lead to triumph or defeat.

Are you ready to challenge the status quo? To outthink and outmaneuver your opponent in a game where intellect is mightier than the sword? Submil isn’t just a game; it’s a revolution one turn at a time. Join the rebellion, craft your strategy, and emerge victorious in the world of Submil.

### Characters

Sublim is considered the first and only part of its arc. It is set in a universe where scientific and technological advances have taken a turn for the worst. They ended up in the wrong hands, where only the elites of society have the possibility of acquiring and benefiting from them.

This is how *The Crisis Unit* was born; a group of the most powerful minds whose goal is to decentralize the domain of power that the authorities have taken control over.

*The Crisis Unit* is made up of Faust, Don Quixote, Heathcliff, Gregor, Ismael and Outis; Each of them has unique passive abilities, which will help the player defeat their opponent.

The player, in this case, could be considered a new member of this unit, who has been personally recruited by this group.

### Setting

Similar to the setting of the universe of the *Arcane* series based off of *League of Legends*, the place where the events of this world take place and where the player's games will take place is somewhat murky.

The city, Vreumcaster, where they are located, is in ruins. The population takes drastic measures to survive, while the aristocrats enjoy all the luxuries that are only imaginable for those below.

However, *The Crisis Unit*, despite living in the ruins of Vreumcaster, being pursued by the government, must hide among the rubble to develop their plans and artifacts that they will use to defeat those who have declared themselves their enemies. They work inside a laboratory, camouflaged under a bridge to go unnoticed. They sometimes find it necessary to change their hiding place, although they have never been captured.

### **Summary**

A turn based Deckbuilding game where you must choose when and how your cards interact with the opponent's cards one at a time, using strategy to manage each card's speed (energy) which is used with most actions through the cards in your hand.

### **Gameplay**

The game's decks are made of 23 cards. 3 of them are the identity cards, which are the ones in play at all times in the match, and the rest are a mix of attack, defense and miscellaneous cards (such as draw cards and effect cards). Matches are intended to last around 14 turns, 7 from each player. The main mechanic revolves around switching your active combat card, which is the card directly facing the opponent's card at the top of the card 'triangle', managing your speed and available cards in your hand to win the match.

### **Mindset**

The intention is for the player to have to manage their speed while also considering what the opponent could do to their front facing card, attempting to predict posibilities and choosing the best course of action, wether this is defending, attacking or using effect cards to increase their stats.

## _Technical_

It was established that for the development of Sublim, the Unity game engine will be mainly used, accompanied by its respective C# scripts to bring the game to life. Likewise, the use of existing assets will be considered to facilitate the development of the final product.

The possibility of using previously made artwork has also been explored to avoid spending time and resources on this part. An artist has been personally contacted and asked for permission in advance to use their art for our game on the condition that it is not distributed for commercial purposes.

### **Screens**

1. Title Screen
    1. Login
    2. Play
    3. Options
    4. Credits

<p align="center">
    <img src="Images/Menu Screen.png" width="500">
</p>

2. Level Select
   1. Tutorial
   2. Challenge
4. Game
    1. Hand
    2. Board

_(example)_

### **Controls**

- All interactions will happen through mouse clicks, except for the login section where keyboard inputs will be accepted for credential registration.
- **Menu Interactions:**
	- Clicking buttons in the menus will take you to the respective screen. E.G. Clicking login will show the login options, clicking credits will take you to the credits screen.
- **Deck Interactions:**
	- Right clicking a card in either the available cards or deck cards will add/remove the card from the deck respectively.
	- Left clicking will show a card's information by zooming it into view.
	- The deck can be saved by clicking a button in the menu.
 - **Game Interactions:**
   	- At the beginning of the turn, clicking a card other than the current card in combat will swap both cards. You must then click confirm to continue the turn.
   	- Drawing cards after swap phase will happen automatically.
   	- Drag clicking a card into the board will play the card and do its respective action or activate its effect.


### **Mechanics**

The game's decks are made of 23 cards. 3 of them are the identity cards, which are the ones in play at all times in the match, and the rest are a mix of attack, defense andmiscellaneous cards (such as draw cards and effect cards). Matches are intended to last around 14 turns, 7 from each player.
#### - **Card Types**

**Identity Cards:** The cards that are in the board interacting with each other. Three of these cards can be in a deck. They have two stats, the left top of the card shows the available speed of it, and the bottom right shows its health. They also have a description of their passive ability in the middle of the card.

The card would resemble something similar to this once it is implemented in-game:

<p align="center">
    <img src="Images/identity_card.png" width="500">
</p>

**Attack Cards:** Attack cards can be played for the front card to do an attack. They also have two stats, damage and speed cost. For now, the damages are 3, 4, and 5 damage costing 1, 2, and 3 speed respectively. The damage of the card, however, can be further increased with effect cards. In the deck, they will appear as follows:

<p align="center">
    <img src="Images/attack_card.png" width="500">
</p>

**Defense Cards:** Similar to attack cards, there are three defense cards available to add to a deck. They absorb 3, 5, and 10 damage, costing 1, 2, and 3 speed respectively. This absorption can be increased or decreased with effect cards as well. In-game, these cards would, tentatively, look as such:

<p align="center">
    <img src="Images/defense_card.png" width="500">
</p>

**Miscellaneous Cards:** Two general types exist:

- Draw cards: Allow you to draw 2 cards from your pile. A maximum of 3 of these cards can be in a deck.
- Effect cards: These cards have caried effects that can apply to identities, attack or defense cards. Some allow you to pierce an opponent's defense card, increase the defense value of a defense card, increase the damage dealt by an attack card, prevent a swap, heal an identity, etc. These cards are used at the beginning of a turn after the swap selection.

An example of an effect card would be:

<p align="center">
    <img src="Images/effect_card.png" width="500">
</p>

Which grants +2 attack damage on the player's next attack card this turn. If an attack card is not used, the effect is discarded, and therefore lost.

Additionally, this would be the view of the back of each card on the card deck:

<p align="center">
    <img src="Images/back_of_card.png" width="500">
</p>


### **Match Gameplay**
### -Game Start:
- Identity cards are placed on the board.
<p align="center">
    <img src="Images/GameEx1.jpeg" width="500">
</p>

### -Player Turn:
- Player is given the option to swap the front card for one of the two back cards or leave the front card in combat. An effect of swapping cards is regenerating the card's original speed value. If a card that had 3 speed and used 2 goes back to having 3 speed when swapping. This is intended to help avoid defense card spamming.
<p align="center">
    <img src="Images/GameEx2.jpeg" width="500">
</p>


- If the player swaps the card the card placement changes, if not, it stays the same. The player then draws 2 cards. In this case, an attack and a defense card from the pile.
<p align="center">
    <img src="Images/GameEx3.jpeg" width="500">
</p>

- The player can use 1 action card (atk, def) per turn and as many item cards as the conditions allow in the card description. In this case, the attack card is played, reducing the front card's speed by 1.
<p align="center">
    <img src="Images/GameEx4.jpeg" width="500">
</p>

- The card performs the action (attack) and the action card gets discarded. The opposing card loses 3 HP
<p align="center">
    <img src="Images/GameEx5.jpeg" width="500">
</p>

- Player turn ends, opponent's turn starts. The opponent uses the same gameloop as the player.
<p align="center">
    <img src="Images/GameEx6.jpeg" width="600">
</p>

#### **This loop repeats until either the player's or the opponent's cards all reach 0 HP.**

### **Edge Cases:** 
- If two of the player's identities have no HP left, other than using effect cards, players can still play by entering a 'panic' mode, where the aactive combat identity will gain a random value of speed, ranging from 1-3.
- If the deck runs out of cards, the deck is reshuffled and the player can take ards again. This can be made by using an invisible discard pile that keeps track of which cards the player does not have in their hand or in their deck.

---

_(Note : These sections can safely be skipped if they&#39;re not relevant, or you&#39;d rather go about it another way. For most games, at least one of them should be useful. But I&#39;ll understand if you don&#39;t want to use them. It&#39;ll only hurt my feelings a little bit.)_

### **Themes**

1. Forest
    1. Mood
        1. Dark, calm, foreboding
    2. Objects
        1. _Ambient_
            1. Fireflies
            2. Beams of moonlight
            3. Tall grass
        2. _Interactive_
            1. Wolves
            2. Goblins
            3. Rocks
2. Castle
    1. Mood
        1. Dangerous, tense, active
    2. Objects
        1. _Ambient_
            1. Rodents
            2. Torches
            3. Suits of armor
        2. _Interactive_
            1. Guards
            2. Giant rats
            3. Chests

_(example)_

### **Game Flow**

1. From the title screen, the player can login or register new credentials.
2. From the title screen, can click the 'Play' button to access stage selection (Tutorial, Challenge).
3. Initially, only the tutorial is available. After clearing it, challenge mode becomes available.
4. Challenge mode rotates the decks that the AI opponent uses at random from a deck pool.
5. The user can play challenge mode as many times as they want to increase their winrate.

_(example)_

## _Development_

---


### **Classes**

1. BaseCard
    1. IdentityCard
    2. AttackCard
    3. DefenseCard
    4. MiscCard
2. BasePlayer
    1. User
    2. Opponent (AI)
3. Deck
    1. UserDeck
    2. OpponentDeck
4. GameManager

## _Graphics_

---

### **Style Attributes**

What kinds of colors will you be using? Do you have a limited palette to work with? A post-processed HSV map/image? Consistency is key for immersion.

What kind of graphic style are you going for? Cartoony? Pixel-y? Cute? How, specifically? Solid, thick outlines with flat hues? Non-black outlines with limited tints/shades? Emphasize smooth curvatures over sharp angles? Describe a set of general rules depicting your style here.

Well-designed feedback, both good (e.g. leveling up) and bad (e.g. being hit), are great for teaching the player how to play through trial and error, instead of scripting a lengthy tutorial. What kind of visual feedback are you going to use to let the player know they&#39;re interacting with something? That they \*can\* interact with something?

### **Graphics Needed**

1. Characters
    1. Human-like
        1. Goblin (idle, walking, throwing)
        2. Guard (idle, walking, stabbing)
        3. Prisoner (walking, running)
    2. Other
        1. Wolf (idle, walking, running)
        2. Giant Rat (idle, scurrying)
2. Blocks
    1. Dirt
    2. Dirt/Grass
    3. Stone Block
    4. Stone Bricks
    5. Tiled Floor
    6. Weathered Stone Block
    7. Weathered Stone Bricks
3. Ambient
    1. Tall Grass
    2. Rodent (idle, scurrying)
    3. Torch
    4. Armored Suit
    5. Chains (matching Weathered Stone Bricks)
    6. Blood stains (matching Weathered Stone Bricks)
4. Other
    1. Chest
    2. Door (matching Stone Bricks)
    3. Gate
    4. Button (matching Weathered Stone Bricks)

_(example)_


## _Sounds/Music_

---

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (dirt floor)
    2. Sharper Footsteps (stone floor)
    3. Soft Landing (low vertical velocity)
    4. Hard Landing (high vertical velocity)
    5. Glass Breaking
    6. Chest Opening
    7. Door Opening
2. Feedback
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_

### Progress Reports

04-24-2024

User stories are finalized and compiled into a .md file in the GitHub repository for review. They include five sections:

Deck Creation and Default Decks
Gameplay
Controls and UI
Login and Saving
Win condition(a)

Deck Creation and Default Decks breaks down how the player's decks are intended to work.

Gameplay focuses more on the game mechanics, such as who the player will face, which in our case is an AI, when they can take cards to play, etc.

Controls and UI is the part that has been developed the most. The ways in which the player interacts with the video game are specified, what are their options, what are the buttons and the interface with which they can interact. etc Likewise, topics related to the art and aesthetics of the game itself are touched upon once the player is in a real game.

In Login and Saving, only the information that the player will have access to is broken down. In our case it is an account protected by username and password, and access to a leaderboard with the highest scores.

In Win Condition, for the moment, is the only way to win our game. However, if more ideas arise about how to win, they will be implemented into GDD and user stories.

The issues corresponding to each user story remain pending.

Likewise, some JSON files were made to experiment with how these would look once they are implemented in the API that will communicate to the client and the database.

Those were some points we were given to fix for the next review, which will include additional progress relevant to Sprint 0:

- Need an image to show basic game mechanics of the game; summarize the readme
- Add Sser Stories format, add priorities (1 - non functional, 10 - functional)
- Remove technicalities (terms like TCG,change to "card came")
- Add basic user stories, simplify them, don't have to be super specific
- We can ask teachers for their area-specific user stories
- Order user stories
- Add tags to issues: functional / non-functional + etc., issues can be 100% technical, must have a description
- Issues will be 70% functional / 30% non functional (game speed, music, etc.)
- Wvery sprint should take care of 5-11 ish issues
- 3 case of use diagrams (web dev, database, videogames)
- Don't make a branch per issue
- Create a branch for each team member
- Distribute issues evenly (everyone must work on web dev, video game, database, etc)

## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_


