DROP SCHEMA IF EXISTS submil;
CREATE SCHEMA submil;
USE submil;

-- TABLES -- 

--
-- Table structure for table `Deck`
--

CREATE TABLE Deck (
    Deck_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Creation_Date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Description TEXT,
    Type VARCHAR(100) NOT NULL,
    Size INT UNSIGNED NOT NULL,
    PRIMARY KEY (Deck_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Player`
--

CREATE TABLE Player (
    Player_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Deck_ID INT UNSIGNED NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Registration_Date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Password VARCHAR(255) NOT NULL,
    IsNPC BOOLEAN,
    PRIMARY KEY (Player_ID),
    FOREIGN KEY (Deck_ID) REFERENCES Deck(Deck_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Match`
--

CREATE TABLE Match_ (
    Match_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Player1_ID INT UNSIGNED NOT NULL,
    Player2_ID INT UNSIGNED NOT NULL,
    Winner_ID INT UNSIGNED NOT NULL,
    Total_Turns INT UNSIGNED NOT NULL,
    PRIMARY KEY (Match_ID),
    FOREIGN KEY (Player1_ID) REFERENCES Player(Player_ID),
    FOREIGN KEY (Player2_ID) REFERENCES Player(Player_ID),
    FOREIGN KEY (Winner_ID) REFERENCES Player(Player_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Type`
--

CREATE TABLE Type_ (
    Type_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    PRIMARY KEY (Type_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Card`
--

CREATE TABLE Card (
    Card_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Type_ID INT UNSIGNED NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    PRIMARY KEY (Card_ID),
    FOREIGN KEY (Type_ID) REFERENCES Type_(Type_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Deck_Card`
--

CREATE TABLE Deck_Card (
    Deck_Card_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Card_ID INT UNSIGNED NOT NULL,
    Deck_ID INT UNSIGNED NOT NULL,
    PRIMARY KEY (Deck_Card_ID),
    FOREIGN KEY (Card_ID) REFERENCES Card(Card_ID),
    FOREIGN KEY (Deck_ID) REFERENCES Deck(Deck_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Stats`
--

CREATE TABLE Stats (
    Stats_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    HP INT NOT NULL,
    Speed INT NOT NULL,
    Speed_Cost INT NOT NULL,
    Atk INT NOT NULL,
    Def INT NOT NULL,
    Passive VARCHAR(255),
    PRIMARY KEY (Stats_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Card_Stats`
--

CREATE TABLE Card_Stats (
    Card_Stat_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Card_ID INT UNSIGNED NOT NULL,
    Stats_ID INT UNSIGNED NOT NULL,
    PRIMARY KEY (Card_Stat_ID),
    FOREIGN KEY (Card_ID) REFERENCES Card(Card_ID),
    FOREIGN KEY (Stats_ID) REFERENCES Stats(Stats_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- VIEWS -- 

--
-- View to consult general information of all cards 
--

USE submil;

CREATE VIEW card_stat_consult AS
SELECT 
	Card_ID, 
    Type_ID, 
    Name, 
    HP, 
    Speed, 
    Speed_Cost, 
    Atk, 
    Def, 
    Passive 
FROM card 
INNER JOIN stats
ON card.Card_ID = stats.Stats_ID;

SELECT * FROM card_stat_consult;

--
-- View for consulting a card through ID
--

USE submil;

CREATE VIEW card_through_ID AS
SELECT 
    card.Card_ID, 
    card.Type_ID, 
    card.Name, 
    stats.HP, 
    stats.Speed, 
    stats.Speed_Cost, 
    stats.Atk, 
    stats.Def, 
    stats.Passive 
FROM card 
INNER JOIN stats 
ON card.Card_ID = stats.Stats_ID;
    
SELECT * FROM card_through_ID WHERE Card_ID = 2;
