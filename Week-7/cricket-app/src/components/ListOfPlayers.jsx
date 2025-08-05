import React from "react";

const players = [
  { name: "Mr. Jack", score: 50 },
  { name: "Mr. Michael", score: 70 },
  { name: "Mr. John", score: 40 },
  { name: "Mr. Alin", score: 61 },
  { name: "Mr. Elisabeth", score: 61 },
  { name: "Mr. Sachin", score: 96 },
  { name: "Mr. Dhoni", score: 100 },
  { name: "Mr. Virat", score: 84 },
  { name: "Mr. Jadeja", score: 64 },
  { name: "Mr. Raina", score: 75 },
  { name: "Mr. Rohit", score: 80 },
];

const ListOfPlayers = () => {
  const below70 = players.filter(player => player.score < 70);

  return (
    <div>
      <h2>List of Players</h2>
      {players.map((player, idx) => (
        <p key={idx}>
          {player.name} {player.score}
        </p>
      ))}

      <h3>List of Players having Scores Less than 70</h3>
      {below70.map((player, idx) => (
        <p key={idx}>
          {player.name} {player.score}
        </p>
      ))}
    </div>
  );
};

export default ListOfPlayers;
