import React from "react";
import "./App.css";

const offices = [
  {
    Name: "DBS",
    Rent: 50000,
    Address: "Chennai",
    image:
      "https://images.unsplash.com/photo-1587502536263-9298f94a3b3b?auto=format&fit=crop&w=600&q=80",
  },
  {
    Name: "WeWork",
    Rent: 75000,
    Address: "Bangalore",
    image:
      "https://images.unsplash.com/photo-1550581190-9c1c48d21d6c?auto=format&fit=crop&w=600&q=80",
  },
  {
    Name: "Cowork Studio",
    Rent: 60000,
    Address: "Hyderabad",
    image:
      "https://images.unsplash.com/photo-1570129477492-45c003edd2be?auto=format&fit=crop&w=600&q=80",
  },
];

const App = () => {
  return (
    <div style={{ padding: "30px", fontFamily: "Arial" }}>
      <h1>Office Space , at Affordable Range</h1>

      {offices.map((item, index) => {
        const colorClass = item.Rent <= 60000 ? "textRed" : "textGreen";

        return (
          <div key={index} style={{ marginBottom: "30px" }}>
            <img
              src={item.image}
              width="25%"
              height="25%"
              alt="Office Space"
              style={{ border: "1px solid #ccc", borderRadius: "8px" }}
            />
            <h2>
              Name: <strong>{item.Name}</strong>
            </h2>
            <h3 className={colorClass}>Rent: Rs. {item.Rent}</h3>
            <h3>Address: {item.Address}</h3>
          </div>
        );
      })}
    </div>
  );
};

export default App;
