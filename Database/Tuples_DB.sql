USE submil;

-- Insert initial data into Type table
INSERT INTO Type_ (Name) VALUES
('Identity'),
('Attack'),
('Defense'),
('Effect'),
('Draw');

-- for testing purposes only
SELECT * FROM Type_; 

-- Insert initial data into Deck table
INSERT INTO Deck (Name, Creation_Date, Description, Type, Size) VALUES
('Basic Deck 1', NOW(), 'Faust Attack Deck', 'Attack', 23),
('Basic Deck 2', NOW(), 'Heathcliff Spam Deck', 'Defense', 23);

-- Insert initial data into Card table
INSERT INTO Card (Type_ID, Name, Description, Image_Path) VALUES
(1, 'Heathcliff', 'Tank Unit', '../Card_Data/Card_Visuals/Heathcliff.png'),
(1, 'Faust', 'Balanced Unit', '../Card_Data/Card_Visuals/Faust.png'),
(1, 'Don Quixote', 'Fast Unit', '../Card_Data/Card_Visuals/Quixote.png'),
(1, 'Ishmael', 'Tank Unit', '../Card_Data/Card_Visuals/Ishmael.png'),
(1, 'Outis', 'Balanced Unit', '../Card_Data/Card_Visuals/Outis.png'),
(1, 'Yi Sang', 'Fast Unit', '../Card_Data/Card_Visuals/Yi_Sang.png'),
/*(2, 'Opportunistic Slash', 'A strong slash with powerful damage', 'path/to/image_opportunisticslash.png'),
(2, 'Blunt Hit', 'A hard hit that knocks back enemies', 'path/to/image_blunthit.png'),
(2, 'Weak Punch', 'A weak preemptive hit', 'path/to/image_weakpunch.png'),
(3, 'Strong Block', 'A strong block that covers vitals', 'path/to/image_strongblock.png'),
(3, 'Shield', 'A shield that can support a medium hit', 'path/to/image_shield.png'),
(3, 'Arm Block', 'A desperate struggle to avoid damage', 'path/to/image_armblock.png'),*/
(4, 'Ego Armor', 'Powerful EGO gear that allows for greater defense', '../Card_Data/Card_Visuals/Ego_Armor.png'),
(4, 'Ego Weapon', 'Powerful EGO gear that allows for greater power', '../Card_Data/Card_Visuals/Ego_Weapon.png'),
(4, 'Ego Needie', 'Powerful EGO gear that allows piercing defense', '../Card_Data/Card_Visuals/Ego_Needle.png'),
(4, 'Healing Ampule', 'Description 1', '../Card_Data/Card_Visuals/Healing_Ampule.png'),
(5, 'Ego Claw', 'A claw that allows for deeper insight', '../Card_Data/Card_Visuals/Ego_Claw.png');

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

INSERT INTO Deck_Card (Card_ID, Deck_ID) VALUES
(1, 1),
(2, 1),
(3, 1),
(7, 1),
(7, 1),
(7, 1),
(7, 1),
(7, 1),
(8, 1),
(8, 1),
(8, 1),
(8, 1),
(9, 1),
(9, 1),
(9, 1),
(9, 1),
(9, 1),
(9, 1),
(9, 1),
(17, 1),
(17, 1),
(17, 1),
(11, 1),
(1, 2),
(2, 2),
(3, 2),
(8, 2),
(8, 2),
(8, 2),
(9, 2),
(9, 2),
(9, 2),
(9, 2),
(9, 2),
(9, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2),
(10, 2);

INSERT INTO Card_Stats (Card_ID, Stats_ID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17);





(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17);