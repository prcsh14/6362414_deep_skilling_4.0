// src/Register.js
import React, { Component } from 'react';

export default class Register extends Component {
  constructor() {
    super();
    this.state = {
      name: '',
      email: '',
      password: ''
    };
  }

  handleChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  handleSubmit = (e) => {
    e.preventDefault();
    const { name, email, password } = this.state;

    if (name.length < 5) {
      alert("Full Name must be 5 characters long!");
      return;
    }

    if (!email.includes("@") || !email.includes(".")) {
      alert("Email is not valid!");
      return;
    }

    if (password.length < 8) {
      alert("Password must be at least 8 characters long!");
      return;
    }

    alert("Registration Successful!");
  };

  render() {
    return (
      <div style={{ textAlign: "center", marginTop: "40px" }}>
        <h2 style={{ color: "red" }}>Register Here!!!</h2>
        <form onSubmit={this.handleSubmit}>
          <div>
            Name: <input type="text" name="name" value={this.state.name} onChange={this.handleChange} />
          </div>
          <br />
          <div>
            Email: <input type="text" name="email" value={this.state.email} onChange={this.handleChange} />
          </div>
          <br />
          <div>
            Password: <input type="password" name="password" value={this.state.password} onChange={this.handleChange} />
          </div>
          <br />
          <button type="submit">Submit</button>
        </form>
      </div>
    );
  }
}
