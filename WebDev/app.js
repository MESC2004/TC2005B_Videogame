// Miguel Enrique Soria A01028033 22/04/2024

"use strict";

import express from 'express';

import mysql from 'mysql2/promise';

const port = 5000;
const app = express();

// Card template for the wanted parameters of the cards in the game
let card_template = ["id", "name", "type_id", "hp", "speed", "speed_cost", "atk", "def"];

let card_list = [];

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



app.post("/add_card", (req, res) => {
  let cards_to_add = req.body.cards;

  if (!cards_to_add) {
    res.status(400).send("No cards provided.");
    return;
  }

  if (!Array.isArray(cards_to_add)) {
    cards_to_add = [cards_to_add];
  }

  cards_to_add.forEach((card_to_add) => {
    const cardFields = Object.keys(card_to_add);
    const missingFields = card_template.filter((field) => !cardFields.includes(field));
    if (missingFields.length > 0) {
      res.status(400).send(`Missing fields: ${missingFields.join(", ")}`);
      return;
    }

    const extraFields = cardFields.filter(field => !card_template.includes(field));
    if (extraFields.length > 0) {
      res.status(400).send(`Card with id ${card_to_add.id} has extra fields: ${extraFields.join(", ")}`);
      return;
    }

    const existingCard = card_list.find((card) => card.id === card_to_add.id);
    if (existingCard) {
      res.status(400).send(`Card with id ${card_to_add.id} already exists.`);
      return;
    }

    card_list.push(card_to_add);
  });

  res.status(200).send("Cards added successfully.");
});


app.delete("/delete_card/:id", (req, res) => {
  const id = req.params.id;
  const card = card_list.find((card) => card.id == id);
  if (card) {
    card_list = card_list.filter((card) => card.id != id);
    res.status(200).send("Card deleted successfully."); 
  } else {
    res.status(200).send("Card not found in the card list.");
  }
});

app.put('/update/:id', (req, res) => {
    const card = card_list.find(card => card.id == parseInt(req.params.id));
    if (!card) {
        res.status(404).send('card not found');
    } else {
        const { name, type_id, hp, speed, speed_cost, atk, def } = req.body;
        if (name) card.name = name;
        if (type_id) card.type_id = type_id;
        if (hp) card.hp = hp;
        if (speed) card.speed = speed;
        if (speed_cost) card.speed_cost = speed_cost;
        if (atk) card.atk = atk;
        if (def) card.def = def;
        res.status(201).send('card updated');
    }
});


app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
