import React, { Component } from "react";

class Getuser extends Component {
  constructor(props) {
    super(props);
    this.state = {
      person: null,
      loading: true,
    };
  }

  async componentDidMount() {
    const url = "https://api.randomuser.me/";
    const response = await fetch(url);
    const data = await response.json();
    this.setState({ person: data.results[0], loading: false });
    console.log(data.results[0]);
  }

  render() {
    const { person, loading } = this.state;

    if (loading) {
      return <h3>Loading...</h3>;
    }

    return (
      <div style={{ textAlign: "center", fontFamily: "Arial" }}>
        <h2>User Details</h2>
        <p><strong>Title:</strong> {person.name.title}</p>
        <p><strong>First Name:</strong> {person.name.first}</p>
        <img src={person.picture.large} alt="User" style={{ borderRadius: "50%" }} />
      </div>
    );
  }
}

export default Getuser;
