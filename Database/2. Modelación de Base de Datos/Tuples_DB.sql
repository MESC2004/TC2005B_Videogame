USE submil;

INSERT INTO Type_ (Name) VALUES
('Faust'),
('Mephistopheles');

INSERT INTO Deck (Name, Creation_Date, Description, Type, Size) VALUES
('Basic Deck 1', NOW(), 'Description Deck 1', 'Type 1', 23),
('Basic Deck 2', NOW(), 'Description Deck 2', 'Type 2', 23);

INSERT INTO Card (Type_ID, Name, Description) VALUES
(1, 'Faust', 'Description Card 1'),
(2, 'Mephistopheles', 'Description Card 2');

INSERT INTO Player (Deck_ID, Name, Registration_Date, Password, IsNPC) VALUES
(1, 'Fabrizio', NOW(), 'password1', FALSE),
(2, 'Miguel', NOW(), 'password2', FALSE);

INSERT INTO Match_ (Player1_ID, Player2_ID, Winner_ID, Total_Turns) VALUES
(1, 2, 1, 15),
(2, 1, 2, 15);

INSERT INTO Stats (HP, Speed, Speed_Cost, Atk, Def, Passive) VALUES
(15, 3, 0, 0, 0, 'Passive2'),
(120, 0, 2, 4, 0, 'Passive1');

INSERT INTO Deck_Card (Card_ID, Deck_ID) VALUES
(1, 1),
(2, 1),
(1, 2),
(2, 2);

INSERT INTO Card_Stats (Card_ID, Stats_ID) VALUES
(1, 1),
(2, 2);

SELECT * FROM Match_;



