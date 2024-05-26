USE submil;

-- Insert initial data into Type table
INSERT INTO Type_ (Name) VALUES
('Identity'),
('Attack'),
('Defense'),
('Effect'),
('Draw');

-- Insert initial data into Deck table
INSERT INTO Deck (Name, Creation_Date, Description, Type, Size) VALUES
('Basic Deck 1', NOW(), 'Description Deck 1', 'Attack', 23),
('Basic Deck 2', NOW(), 'Description Deck 2', 'Defense', 23);

-- Insert initial data into Card table
INSERT INTO Card (Type_ID, Name, Description) VALUES
(1, 'Heathcliff', 'Tank Unit'),
(1, 'Faust', 'Balanced Unit'),
(1, 'Don Quixote', 'Fast Unit'),
(1, 'Ishmael', 'Tank Unit'),
(1, 'Outis', 'Balanced Unit'),
(1, 'Yi Sang', 'Fast Unit'),
(2, 'Opportunistic Slash', 'A strong slash with powerful damage'),
(2, 'Blunt Hit', 'A hard hit that knocks back enemies'),
(2, 'Weak Punch', 'A weak preemptive hit'),
(3, 'Strong Block', 'A strong block that covers vitals'),
(3, 'Shield', 'A shield that can support a medium hit'),
(3, 'Arm Block', 'A desperate struggle to avoid damage'),
(4, 'Ego Armor', 'Powerful EGO gear that allows for greater defense'),
(4, 'Ego Weapon', 'Powerful EGO gear that allows for greater power'),
(4, 'Ego Needie', 'Powerful EGO gear that allows piercing defense'),
(4, 'Healing Ampule', 'Description 1'),
(5, 'Ego Claw', 'A claw that allows for deeper insight');

-- Insert initial data into Player table
INSERT INTO Player (Deck_ID, Name, Registration_Date, Password, IsNPC) VALUES
(1, 'Fabrizio', NOW(), 'password1', FALSE),
(2, 'Miguel', NOW(), 'password2', FALSE);

-- Insert initial data into Match table
INSERT INTO Match_ (Player1_ID, Player2_ID, Winner_ID, Total_Turns) VALUES
(1, 2, 1, 15),
(2, 1, 2, 15);

-- Insert initial data into Stats table
INSERT INTO Stats (HP, Speed, Speed_Cost, Atk, Def, Passive) VALUES
(20, 1, 0, 0, 0, 'Pierce Cards have 50% less effect'),
(15, 2, 0, 0, 0, 'If an attack boost card is used, heal 50% of damage dealt'),
(10, 3, 0, 0, 0, 'If a pierce card is used, pierce the defense 50% more'),
(20, 1, 0, 0, 0, 'Healing cards heal 25% more'),
(15, 2, 0, 0, 0, 'Attack cards cost 1 more speed (Except weak punch)'),
(10, 3, 0, 0, 0, 'Ego weapons increase 50% more damage'),
(0, 0, 3, 7, 0, 'N/A'),
(0, 0, 2, 4, 0, 'N/A'),
(0, 0, 1, 2, 0, 'N/A'),
(0, 0, 3, 0, 10, 'N/A'),
(0, 0, 2, 0, 5, 'N/A'),
(0, 0, 1, 0, 3, 'N/A'),
(0, 0, 0, 0, 3, 'N/A'),
(0, 0, 0, 2, 0, 'N/A'),
(0, 0, 0, 0, -4, 'N/A'),
(4, 0, 0, 0, 0, 'N/A'),
(0, 0, 0, 0, 0, 'N/A');

-- Insert initial data into Deck_Card table
INSERT INTO Deck_Card (Card_ID, Deck_ID) VALUES
(1, 1),
(2, 1),
(1, 2),
(2, 2);

-- Insert initial data into Card_Stats table
INSERT INTO Card_Stats (Card_ID, Stats_ID) VALUES
(1, 1),
(2, 2);
