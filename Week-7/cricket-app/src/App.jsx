import React, { useState } from "react";
import ListOfPlayers from "./components/ListOfPlayers";
import IndianPlayers from "./components/IndianPlayers";

const App = () => {
  const [Flag, setFlag] = useState(true);

  return (
    <div style={{ padding: "20px", fontFamily: "sans-serif" }}>
      {Flag ? <ListOfPlayers /> : <IndianPlayers />}
      <p>When Flag={String(Flag)}</p>
      <button onClick={() => setFlag(!Flag)}>Toggle Flag</button>
    </div>
  );
};

export default App;
