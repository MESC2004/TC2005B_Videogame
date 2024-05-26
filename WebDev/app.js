// Miguel Enrique Soria A01028033 22/04/2024

"use strict";

import express from 'express';

import mysql from 'mysql2/promise';

const port = 5000;
const app = express();

// Card template for the wanted parameters of the cards in the game


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

    // The await keyword is used to wait for a Promise. It can only be used inside an async function.
    // The await expression causes async function execution to pause until a Promise is settled (that is, fulfilled or rejected), and to resume execution of the async function after fulfillment. When resumed, the value of the await expression is that of the fulfilled Promise.

    connection = await connectToDB();

    const [results, fields] = await connection.execute("SELECT Card_ID, Type_ID, Name, HP, Speed, Speed_Cost, Atk, Def, Passive FROM card INNER JOIN stats ON card.Card_ID = stats.Stats_ID;");
    
    // FOR DEBUGGING, DO NOT UNCOMMENT
    // const [results, fields] = await connection.execute("SELECT * FROM card;");
    
    // TODO replace the query with a view.

    console.log('Requesting all cards...')
    console.log(`${results.length} rows returned`);
    // uncomment to see the cards in the console
    // console.log(results);
    response.status(200).json({cards: results});
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    // The finally statement lets you execute code, after try and catch, regardless of the result. In this case, it closes the connection to the database.
    // Closing the connection is important to avoid memory leaks and to free up resources.
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await
    // Missing view (SELECT * FROM all_cards_view WHERE Card_ID = ?;, [request.params.id])
    // Currently using full query
    connection.execute("SELECT Card_ID, Type_ID, Name, HP, Speed, Speed_Cost, Atk, Def, Passive FROM card INNER JOIN stats ON card.Card_ID = stats.Stats_ID WHERE Card_ID = ?;", [request.params.id]);

    console.log('Requesting card with ID ' + request.params.id);
    console.log(`${results.length} rows returned`);
    if (results.length === 0) {
      response.status(200).send("Card not found.");
    } else {
      response.status(200).json({cards: results});
    }
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});
 
app.post("/api/add_card", async (request, response) => {
  // Hardcoded INSERT, might replace with a stored procedure in the schema to be called to avoid SQL injection.
  // Must recieve an object named cards: which contains the card to be added.
  // MUST BE A SINGLE OBJECT, NOT A LIST OR IT WILL NOT WORK

  let card = request.body.cards;
  let connection = null; 

  try {
    connection = await connectToDB();

    console.log(card);
    console.log(card.Name, card.Type_ID, card.Description, card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive);

    const [results, fields] = await connection.execute("INSERT INTO card (Type_ID, Name, Description) VALUES (?, ?, ?);", [card.Type_ID, card.Name, card.Description]);
    const [results2, fields2] = await connection.execute("INSERT INTO stats (HP, Speed, Speed_Cost, Atk, Def, Passive) VALUES (?, ?, ?, ?, ?, ?);", [card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive]);

    console.log('Card added succesfully!');
    response.status(200).send("Card added succesfully!");
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});




app.delete("/api/delete_card/:id", async (req, res) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("DELETE FROM card WHERE Card_ID = ?;", [req.params.id]);
    const [results2, fields2] = await connection.execute("DELETE FROM stats WHERE Stats_ID = ?;", [req.params.id]);

    console.log('Card deleted succesfully!');
    res.status(200).send("Card deleted succesfully!");
  }
  catch (error) {
    res.status(500);
    res.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});



app.put("/api/update_card/:id", async (req, res) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const card = req.body.cards;
    console.log(card);
    console.log(card.Name, card.Type_ID, card.Description, card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive);

    const [results, fields] = await connection.execute("UPDATE card SET Type_ID = ?, Name = ?, Description = ? WHERE Card_ID = ?;", [card.Type_ID, card.Name, card.Description, req.params.id]);
    const [results2, fields2] = await connection.execute("UPDATE stats SET HP = ?, Speed = ?, Speed_Cost = ?, Atk = ?, Def = ?, Passive = ? WHERE Stats_ID = ?;", [card.HP, card.Speed, card.Speed_Cost, card.Atk, card.Def, card.Passive, req.params.id]);

    console.log('Card updated succesfully!');
    res.status(200).send("Card updated succesfully!");
  }
  catch (error) {
    res.status(500);
    res.json(error);
    console.log(error);
  }
  finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

// for log in and registration system
app.post("/api/login", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const { username, password } = request.body;

    // Log the incoming request data
    console.log('Login request received:', { username, password });

    // Check for undefined or null values
    if (!username || !password) {
      response.status(400).json({ error: 'Username or password is missing' });
      console.log('Username or password is missing');
      return;
    }

    const [results] = await connection.execute(
      "SELECT Player_ID, Name, Password FROM Player WHERE Name = ?;",
      [username]
    );

    if (results.length === 0) {
      response.status(404).json({ error: 'User not found' });
      console.log('User not found:', username);
      return;
    }

    const user = results[0];

    if (user.Password !== password) {
      response.status(401).json({ error: 'Incorrect password' });
      console.log('Incorrect password for user:', username);
      return;
    }

    console.log('Login successful for user:', username);
    response.status(200).json({ message: "Login successful", userId: user.Player_ID });
  } catch (error) {
    response.status(500).json({ error: error.message });
    console.log(error);
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
