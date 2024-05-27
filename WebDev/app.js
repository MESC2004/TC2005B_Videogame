"use strict";

import express from 'express';
import mysql from 'mysql2/promise';
import fs from 'fs';
import path from 'path';

const port = 5000;
const app = express();
app.use(express.json());

async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "TC2005B",
    password: "Password123",
    database: "submil",
  });
}

app.get("/api/cards", async (request, response) => {
  let connection = null;
  try {
    connection = await connectToDB();
    const [results] = await connection.execute(
      `SELECT c.Card_ID, c.Type_ID, c.Name, c.Description, s.HP, s.Speed, s.Speed_Cost, s.Atk, s.Def, s.Passive, c.Image_Path 
       FROM card c
       INNER JOIN card_stats cs ON c.Card_ID = cs.Card_ID
       INNER JOIN stats s ON cs.Stats_ID = s.Stats_ID`
    );

    const cards = results.map(card => {
      if (card.Image_Path) {
        const imagePath = path.join(__dirname, card.Image_Path);
        const imageBuffer = fs.readFileSync(imagePath);
        card.Image = imageBuffer.toString('base64');
      }
      return card;
    });

    response.status(200).json({ cards });
  } catch (error) {
    response.status(500).json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.get("/api/cards/:id", async (request, response) => {
  let connection = null;
  try {
    connection = await connectToDB();
    const [results] = await connection.execute(
      `SELECT c.Card_ID, c.Type_ID, c.Name, c.Description, s.HP, s.Speed, s.Speed_Cost, s.Atk, s.Def, s.Passive, c.Image_Path 
       FROM card c
       INNER JOIN card_stats cs ON c.Card_ID = cs.Card_ID
       INNER JOIN stats s ON cs.Stats_ID = s.Stats_ID
       WHERE c.Card_ID = ?`, [request.params.id]
    );

    if (results.length === 0) {
      response.status(200).send("Card not found.");
    } else {
      const card = results[0];
      if (card.Image_Path) {
        const imagePath = path.join(__dirname, card.Image_Path);
        const imageBuffer = fs.readFileSync(imagePath);
        card.Image = imageBuffer.toString('base64');
      }
      response.status(200).json({ card });
    }
  } catch (error) {
    response.status(500).json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.post("/api/add_card", async (request, response) => {
  let card = request.body.cards;
  let connection = null;
  try {
    connection = await connectToDB();
    await connection.execute("INSERT INTO card (Type_ID, Name, Description, Image_Path) VALUES (?, ?, ?, ?);",
      [card.Type_ID, card.Name, card.Description, card.Image_Path]);
    const [results] = await connection.execute("SELECT LAST_INSERT_ID() as Card_ID;");
    const cardId = results[0].Card_ID;
    await connection.execute("INSERT INTO stats (HP, Speed, Speed_Cost, Atk, Def, Passive) VALUES (?, ?, ?, ?, ?, ?);",
      [card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive]);
    await connection.execute("INSERT INTO card_stats (Card_ID, Stats_ID) VALUES (?, LAST_INSERT_ID());", [cardId]);

    response.status(200).send("Card added successfully!");
  } catch (error) {
    response.status(500).json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.delete("/api/delete_card/:id", async (req, res) => {
  let connection = null;
  try {
    connection = await connectToDB();
    await connection.execute("DELETE FROM card_stats WHERE Card_ID = ?;", [req.params.id]);
    await connection.execute("DELETE FROM card WHERE Card_ID = ?;", [req.params.id]);
    await connection.execute("DELETE FROM stats WHERE Stats_ID = ?;", [req.params.id]);

    res.status(200).send("Card deleted successfully!");
  } catch (error) {
    res.status(500).json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.put("/api/update_card/:id", async (req, res) => {
  let connection = null;
  try {
    connection = await connectToDB();
    const card = req.body.cards;

    await connection.execute("UPDATE card SET Type_ID = ?, Name = ?, Description = ?, Image_Path = ? WHERE Card_ID = ?;",
      [card.Type_ID, card.Name, card.Description, card.Image_Path, req.params.id]);
    await connection.execute("UPDATE stats SET HP = ?, Speed = ?, Speed_Cost = ?, Atk = ?, Def = ?, Passive = ? WHERE Stats_ID = ?;",
      [card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive, req.params.id]);

    res.status(200).send("Card updated successfully!");
  } catch (error) {
    res.status(500).json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.post('/api/login', async (req, res) => {
  const { username, password } = req.body;
  console.log("Login request received:", req.body);

  if (!username || !password) {
    return res.status(400).send('Username or password is missing');
  }

  let connection = null;
  try {
    connection = await connectToDB();
    const [rows] = await connection.execute('SELECT * FROM Player WHERE Name = ? AND Password = ?', [username, password]);
    if (rows.length === 0) {
      console.log("User not found or incorrect password:", username);
      return res.status(404).send('User not found or incorrect password');
    }

    console.log("Login successful for user:", username);
    res.status(200).send('Login successful');
  } catch (err) {
    console.error("Error during login:", err);
    res.status(500).send('Internal server error');
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.post('/api/register', async (req, res) => {
  const { username, password } = req.body;
  console.log("Register request received:", req.body);

  if (!username || !password) {
    return res.status(400).send('Username or password is missing');
  }

  let connection = null;
  try {
    connection = await connectToDB();
    const [rows] = await connection.execute('SELECT * FROM Player WHERE Name = ?', [username]);
    if (rows.length > 0) {
      console.log("Username already taken:", username);
      return res.status(400).send('Username already taken');
    }

    await connection.execute('INSERT INTO Player (Name, Password, Deck_ID) VALUES (?, ?, 1)', [username, password]);
    console.log("User registered successfully:", username);
    res.status(201).send('User registered successfully');
  } catch (err) {
    console.error("Error during user registration:", err);
    res.status(500).send('Internal server error');
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});