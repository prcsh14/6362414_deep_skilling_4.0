import React from "react";

const oddPlayers = ["Sachin1", "Virat3", "Yuvraj5"];
const evenPlayers = ["Dhoni2", "Rohit4", "Raina6"];

const T20 = [
  "Mr. First Player",
  "Mr. Second Player",
  "Mr. Third Player"
];
const Ranji = [
  "Mr. Fourth Player",
  "Mr. Fifth Player",
  "Mr. Sixth Player"
];
const mergedPlayers = [...T20, ...Ranji];

const IndianPlayers = () => {
  return (
    <div>
      <h3>Odd Players</h3>
      <ul>
        {oddPlayers.map((p, i) => (
          <li key={i}>
            {["First", "Third", "Fifth"][i]} : {p}
          </li>
        ))}
      </ul>

      <h3>Even Players</h3>
      <ul>
        {evenPlayers.map((p, i) => (
          <li key={i}>
            {["Second", "Fourth", "Sixth"][i]} : {p}
          </li>
        ))}
      </ul>

      <h3>List of Indian Players Merged:</h3>
      {mergedPlayers.map((player, i) => (
        <p key={i}>{player}</p>
      ))}
    </div>
  );
};

export default IndianPlayers;
