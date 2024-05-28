-- Drop the schema if it already exists
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Type`
--

CREATE TABLE Type_ (
    Type_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    PRIMARY KEY (Type_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
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
    FOREIGN KEY (Type_ID) REFERENCES Type_(Type_ID),
    Image_Path VARCHAR(255)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- VIEWS -- 

--
-- View to consult general information of all cards, can be used in API with a WHERE clause to find a unique card's data
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
-- View of the data of every player's active deck, can be filtered with a where clause
--

CREATE VIEW Player_Deck_Info AS
SELECT 
	p.Player_ID, 
    p.Name AS Player_Name, 
    d.Deck_ID, d.Name AS Deck_Name, 
    d.Type, 
    d.Size
FROM 
	Player p
	JOIN Deck d ON p.Deck_ID = d.Deck_ID;

-- Test
-- SELECT * FROM PlayerDeckInfo WHERE Player_ID = 1;

-- 
-- View of a player's deck's cards
--

CREATE VIEW Deck_Card_Stats AS
SELECT 
    d.Deck_ID,
    d.Name AS Deck_Name,
    csc.Card_ID,
    csc.Type_ID,
    csc.Name AS Card_Name,
    csc.HP,
    csc.Speed,
    csc.Speed_Cost,
    csc.Atk,
    csc.Def,
    csc.Passive
FROM 
    Deck d
    INNER JOIN Deck_Card dc ON d.Deck_ID = dc.Deck_ID
    INNER JOIN card_stat_consult csc ON dc.Card_ID = csc.Card_ID;
    
-- Test
-- SELECT * FROM Deck_Card_Stats;
    

--
-- Most Used Cards
-- 

CREATE VIEW Top_Used_Cards AS
SELECT 
    c.Card_ID, 
    c.Name AS Card_Name, 
    COUNT(dc.Deck_ID) AS Deck_Count
FROM 
	Card c
	JOIN Deck_Card dc ON c.Card_ID = dc.Card_ID
	GROUP BY c.Card_ID
	ORDER BY Deck_Count DESC;

-- Test
SELECT * FROM Top_Used_Cards;

--
-- Match Insight View (All cards used in a match, might change to get stats of a match like avg dmg, avg hp, etc)
--

CREATE VIEW Match_Insight AS
SELECT 
	m.Match_ID, 
	c.Card_ID, 
    c.Name AS Card_Name, 
    s.HP, 
    s.Speed, 
    s.Speed_Cost, 
    s.Atk, 
    s.Def
FROM 
	Match_ m
	INNER JOIN Player p ON m.Player1_ID = p.Player_ID OR m.Player2_ID = p.Player_ID
	INNER JOIN Deck d ON p.Deck_ID = d.Deck_ID
	INNER JOIN Deck_Card dc ON d.Deck_ID = dc.Deck_ID
	INNER JOIN Card c ON dc.Card_ID = c.Card_ID
	INNER JOIN Card_Stats cs ON c.Card_ID = cs.Card_ID
	INNER JOIN Stats s ON cs.Stats_ID = s.Stats_ID;

-- Test
SELECT * FROM Match_Insight WHERE Match_ID = 1;

--
-- Average Speed Cost of a Deck View
-- 

CREATE VIEW Avg_Spd_Cost AS
SELECT 
    d.Deck_ID,
    d.Name AS Deck_Name,
    AVG(s.Speed_Cost) AS Avg_Speed_Cost
FROM 
    Deck d
    INNER JOIN Deck_Card dc ON d.Deck_ID = dc.Deck_ID
    INNER JOIN Card c ON dc.Card_ID = c.Card_ID
    INNER JOIN Card_Stats cs ON c.Card_ID = cs.Card_ID
    INNER JOIN Stats s ON cs.Stats_ID = s.Stats_ID
WHERE 
    c.Type_ID IN (2, 3)
GROUP BY 
    d.Deck_ID, d.Name;

-- Test    
SELECT * FROM Avg_Spd_Cost;


--
-- Summary of the stats of all decks, or a selected deck with a WHERE clause
--
CREATE VIEW Deck_Stats_Summary AS
SELECT 
	d.Deck_ID, 
    d.Name AS Deck_Name, 
    AVG(s.HP) AS Avg_HP, 
    AVG(s.Speed) AS Avg_Speed,
	AVG(s.Speed_Cost) AS Avg_Speed_Cost, 
    AVG(s.Atk) AS Avg_Atk, 
    AVG(s.Def) AS Avg_Def
FROM Deck d
	INNER JOIN Deck_Card dc ON d.Deck_ID = dc.Deck_ID
	INNER JOIN Card c ON dc.Card_ID = c.Card_ID
	INNER JOIN Card_Stats cs ON c.Card_ID = cs.Card_ID
	INNER JOIN Stats s ON cs.Stats_ID = s.Stats_ID
GROUP BY 
	d.Deck_ID;

-- Test
SELECT * FROM Deck_Stats_Summary;


--
-- Winrate of a specific deck among different players (if they use the same deck)
--

CREATE VIEW Deck_Performance AS
SELECT 
	d.Deck_ID, 
    d.Name AS Deck_Name, 
    COUNT(m.Match_ID) AS Matches_Won
FROM 
	Deck d
	JOIN Player p ON d.Deck_ID = p.Deck_ID
	JOIN Match_ m ON p.Player_ID = m.Winner_ID
GROUP BY 
	d.Deck_ID;
    
-- Test
SELECT * FROM Deck_Performance;
    

--
-- Player WInrate View;
--

CREATE VIEW Player_Performance AS
SELECT 
	p.Player_ID, 
	p.Name, 
    COUNT(m.Match_ID) AS Matches_Played,
	SUM(CASE WHEN p.Player_ID = m.Winner_ID THEN 1 ELSE 0 END) AS Matches_Won
FROM 
	Player p
	LEFT JOIN Match_ m ON p.Player_ID = m.Player1_ID OR p.Player_ID = m.Player2_ID
GROUP BY 
	p.Player_ID;

-- Test
SELECT * FROM Player_Performance;

--
-- View for looking at the outcomes of matches
--

CREATE VIEW Match_Results AS
SELECT 
	m.Match_ID, 
    p1.Name AS Player1, 
    p2.Name AS Player2, 
    w.Name AS Winner, 
    m.Total_Turns
FROM 
	Match_ m
	INNER JOIN Player p1 ON m.Player1_ID = p1.Player_ID
	INNER JOIN Player p2 ON m.Player2_ID = p2.Player_ID
	INNER JOIN Player w ON m.Winner_ID = w.Player_ID;
    
-- Test
SELECT * FROM Match_Results;



