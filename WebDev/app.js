// Miguel Enrique Soria A01028033 22/04/2024

"use strict";

import express from 'express';
const port = 5000;
const app = express();
// Card template for the wanted parameters of the cards in the game
let card_template = ["id", "name", "type_id", "type_name", "hp", "speed", "speed_cost", "atk", "def"];

let card_list = [
{
  "id": 1, 
  "name": "Faust", 
  "type_id": 1, 
  "type_name": "Identity", 
  "hp": 15, 
  "speed": 3, 
  "speed_cost": "NULL", 
  "atk": "NULL", 
  "def": "NULL"
},
{
  "id": 2,
  "name": "Mephistopheles",
  "type_id": 2,
  "type_name": "Attack Card",
  "hp": "NULL",
  "speed": "NULL",
  "speed_cost": 2,
  "atk": 4,
  "def": "NULL"
}
];

app.use(express.json());

app.get("/available_cards", (req, res) => {
  if (card_list.length > 0) {
    res.json(card_list);
  } else {
    res.status(200).send("No cards available.");
  }
});

app.get("/lookup/:id", (req, res) => {
  const id = req.params.id;
  const card = card_list.find((card) => card.id === id);
  if (card) {
    res.json(card);
  } else {
    res.status(200).send("Card not found in the card list.");
  }
});  


app.post("/add_card", (req, res) => {
  const cards_to_add = req.body;
  let inserted = [];

  if (!Array.isArray(cards_to_add)) {
    cards_to_add = [cards_to_add];
  }

  cards_to_add.forEach((cards_to_add) => {
    const missingFields = card_template.filter((field) => !cards_to_add.hasOwnProperty(field));
    if (missingFields.length > 0) {
      res.status(200).send(`Missing fields: ${missingFields.join(", ")}`,);
      return;
    }

    const existingCard = card_list.find((card) => card.id === cards_to_add.id);
    if (existingCard) {
      res.status(200).send(`Card with id ${cards_to_add.id} already exists.`);
      return;
    }

    card_list.push(cards_to_add);
    inserted.push(cards_to_add);
  });

  res.status(200).send("Cards added successfully.");
});


app.delete("/delete_card/:id", (req, res) => {
  const id = req.params.id;
  const card = card_list.find((card) => card.id == id);
  if (card) {
    card_list = card_list.filter((card) => card.id !== id);
    res.json(card_list);
  } else {
    res.status(200).send("Card not found in the card list.");
  }
});

app.put("/update_card/:id", (req, res) => {
  const id = req.params.id;
  // Get the fields to update
  const updatedFields = req.body;

  const cardIndex = card_list.findIndex((card) => card.id == id);
  if (!cardIndex) {
    res.status(200).send("Card not found in the card list.");
    return;
  }

  if (updatedFields.id) {
    // ID protection
    res.status(200).send("Cannot update card id.");
    return;
  }

  const existingCard = card_list[cardIndex];
  for (const field in updatedFields) {
    // Update only the fields that are present in the card template
    existingCard[field] = updatedFields[field];
  }
  res.status(200).send("Card updated successfully.");
});


app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
