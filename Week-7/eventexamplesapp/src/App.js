import React, { useState } from 'react';
import './index.css';

function App() {
  const [count, setCount] = useState(0);
  const [amount, setAmount] = useState('');
  const [converted, setConverted] = useState('');

  const handleIncrement = () => {
    setCount(prev => prev + 1);
    alert("Hello Member1"); // Static message
  };

  const handleDecrement = () => {
    setCount(prev => prev - 1);
  };

  const sayWelcome = (msg) => {
    alert(`Hello ${msg}`);
  };

  const handleClick = () => {
    alert("I was clicked");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const euro = parseFloat(amount) * 0.8;
    setConverted(`Euro Amount is ${euro}`);
    alert(`Converting to Euro Amount is ${euro}`);
  };

  return (
    <div className="container">
      <h1>{count}</h1>
      <button onClick={handleIncrement}>Increment</button>
      <button onClick={handleDecrement}>Decrement</button>
      <button onClick={() => sayWelcome("Member1")}>Say welcome</button>
      <button onClick={handleClick}>Click on me</button>

      <h2 className="title">Currency Convertor!!!</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Amount:</label>
          <input
            type="number"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
          />
        </div>
        <div>
          <label>Currency:</label>
          <input type="text" value="Euro" disabled />
        </div>
        <button type="submit">Submit</button>
      </form>

      {converted && <p>{converted}</p>}
    </div>
  );
}

export default App;
