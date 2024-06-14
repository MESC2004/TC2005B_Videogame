USE submil;

INSERT INTO Type_ (Name) VALUES
('Identity'),
('Attack'),
('Defense'),
('Effect'),
('Draw');

INSERT INTO Deck (Name, Creation_Date, Description, Type, Size) VALUES
('Basic Deck 1', NOW(), 'Faust Attack Deck', 'Attack', 23),
('Basic Deck 2', NOW(), 'Heathcliff Spam Deck', 'Defense', 23);

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

INSERT INTO Player (Deck_ID, Name, Registration_Date, Password, IsNPC) VALUES
(1, 'Fabrizio', NOW(), 'password1', FALSE),
(2, 'Miguel', NOW(), 'password2', FALSE),
(2, 'Cyber', NOW(), 'password3', FALSE),
(2, 'Dante', NOW(), 'password', FALSE),
(2, 'Guest1', NOW(), 'password', FALSE),
(2, 'Guest2', NOW(), 'password', FALSE);

INSERT INTO Match_ (Player1_ID, Player2_ID, Winner_ID, Total_Turns) VALUES
(1, 2, 1, 15),
(2, 1, 2, 15),
(2, 1, 2, 15),
(3, 2, 3, 18),
(4, 3, 4, 17),
(5, 3, 5, 19),
(5, 3, 3, 18),
(6, 1, 6, 18);

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




