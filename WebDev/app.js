// Miguel Enrique Soria A01028033 22/04/2024

"use strict";

import express from 'express';
const port = 5000;
const app = express();

// Card template for the wanted parameters of the cards in the game
let card_template = ["id", "name", "type_id", "hp", "speed", "speed_cost", "atk", "def"];

let card_list = [];

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
    card_list = card_list.filter((card) => card.id !== id);
    res.json(card_list);
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
