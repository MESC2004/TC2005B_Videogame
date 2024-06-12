# COLOR CODES

${\texttt{\color{red}[High]}}$\
${\texttt{\color{orange}[Mid]}}$\
${\texttt{\color{green}[Low]}}$\
${\texttt{\color{magenta}[All Sprints]}}$

# User Stories

## Winning and Losing
- **Win Condition ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to beat my opponent and win the match.
    - **Acceptance Criteria:**  Given how the match starts with all the board cards with full health, when all of the opponent's board cards have no health left then the match must end.

- **Lose Condition ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to lose the match.
    - **Acceptance Criteria:**  Given how the match starts with all the board cards with full health, when all of the player's board cards have no health left then the match must end.

## Login and Saving

- **User Account Creation and Access ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to create and login to my account.
    - **Acceptance Criteria:** Given that the user wishes to create and log into their account, when the user accesses the registration option, a form should be displayed to create a new account, requesting essential information. After registration, credentials should allow the user to log in.
      
- **Saving Progress and Decks in Each Account ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to save my progress and decks to my account.
    - **Acceptance Criteria:** Whenever the user wants to save their progress and deck configuration, our system must automatically save these changes.
      
- **Loading Progress and Decks in Each Account ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Sotry:** As a user, I want to be able to load my progress and decks from my account.
    - **Acceptance Criteria:** When the user wishes to loan their progress and decks (from their account), the system must allow access to all saved data regarding progress and decks.
            
- **Displaying Statistics on the Video Game Page ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to see the stats on the webpage the videogame lives in to keep track of my progress.
    - **Acceptance Criteria:** When the user accesses the statistics section of the page, it must detailedly reflect the user's personal statistics, including games played, victories, defeats, overall score table, and other relevant characteristics.
## Deck Creation and Default Decks

- **The Game ${\texttt{\color{red}[High]}}$ ${\texttt{\color{magenta}[5-7 sprints]}}$**
    - **User Story:** As a user, I want to play an interactive card game that involves strategy and deckbuilding to overcome the challenges of the game.
    - **Acceptance Criteria:** Given the initial game development, when the game development has ended then a finished deckbuilding game is expected.
- **The Deck ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to create and modify a 23 card deck to manage the strategy I want to use in my gameplay.
    - **Acceptance Criteria:** There is a menu that allows for deck creation with appropriate limits to cards.
- **Pre-set Deck Generator ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to have the option to use a default deck to play the game without having to create a deck from scratch.
    - **Acceptance Criteria:** If the user does not create a deck in the deck scene, or has no available deck, a default deck is used.

## Gameplay 

- **Custom Match Against AI ${\texttt{\color{red}[High]}}$ ${\texttt{\color{magenta}[3-4 sprints]}}$**
    - **User Story:** As a user, I want to be able to use my deck to play against an AI opponent.
    - **Acceptance Criteria:** Given that the  user wishes to use their own deck to play, when they choose to face an artificial inteligence opponent, the system must allow them to select  their personal deck and start the game.
      
- As a user, I want to be able to draw cards in my deck and plan according to the available options using strategy.

- **Card Interaction on the field ${\texttt{\color{orange}[Mid]}}$ ${\texttt{\color{magenta}[2-3 sprints]}}$**
    - **User Story:** As a user, I want to be able to play cards from my hand to the field to interact with the game.
    - **Acceptance Criteria:** When the user selects a card to play, the system must allow the placement of the card on the playing field and ensure that the interaction with other cards in play conforms to the established rules.
      
- **Game Control through Attack,Defense and Effects ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to use a mix of attack, defense and effects to beat my opponent.
    - **Acceptance Criteria:** The system must allow the user to select and apply strategies of attack, defense, and special effect in such a way that the effectively interact according to the game rules to defeat the opponent.
      
- **Card Selection Confirmation ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to press a button to confirm my choice when deciding what card to play.
    - **Acceptance Criteria:** The user wishes to confirm their card choice when playing, the system must provide a confirmation button that, once pressed, finalizes the selection and proceeds with the play according to the game rules.
      
## Controls and UI

- **Game Menu Interaction ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to interact with a menu screen to initiate a game.
    - **Acceptance Criteria:** When accessing the menu screen, clear  and  accesible  options must be presented to start a game, including  game, including game mode selection, game settings, and game start.
      
- **Card Interaction via Mouse Clicks ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to interact with the cards with mouse clicks.
    - **Acceptance Criteria:** At the moment a card is clicked, the game must display the details of the card execute the action associated with thw card according to the context of the game.
- **Card Display in Hand and on the Field ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to see the cards in my hand and the cards on the field.
    - **Acceptance Criteria:** The system must display all the cards that the user has in their hand and those that are played on the field, allowing complete and real-time visibility of both sets of cards.
      
- **Card Values and Effects Display ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:**  As a user, I want to be able to understand the card's values and effects at a glance.
    - **Acceptance Criteria:** When tha user looks at a card, the information about the card´s values and effects should be clearly reflected in a comprehensible manner without the need for additional actions.
      
- **Detailed Card Information Display ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to see detailed information of a card in a match.
    - **Acceptance Criteria:** At the moment when a card is selected or the mouse hovers over it, the system must clearly display all the relevant details of the card, such as attributes, abilities, an effects, allowing the user to make decisions based on these data.
        
- **Inmersive Game Board Design ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to see a gameboard following the theme of the game to have a better immersive experience.
    - **Acceptance Criteria:** When accessing the game, a game board that reflects the gaming enviroment we are proposing should be displayed, with visual details and elements that contribute to the narrative of the game.
    
- **Active Cards Display in Match ${\texttt{\color{green}[Low]}}$ ${\texttt{\color{magenta}[1-2 sprints]}}$**
    - **User Story:** As a user, I want to be able to tell who is winning the match.
    - **Acceptance Criteria:** When the user wants to see the number of active cards in the game, the system must clearly display the number of active cards both the user and the opponent have, giving the user a clearer overview of how the match is progressing.

