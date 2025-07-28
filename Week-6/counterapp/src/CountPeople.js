import React, { Component } from 'react';

class CountPeople extends Component {
  constructor(props) {
    super(props);
    this.state = {
      entrycount: 3,   // For demo, set initial counts to match image
      exitcount: 2
    };
  }

  // Handler for Login button
  UpdateEntry = () => {
    this.setState(prevState => ({
      entrycount: prevState.entrycount + 1
    }));
  };

  // Handler for Exit button
  UpdateExit = () => {
    this.setState(prevState => ({
      exitcount: prevState.exitcount + 1
    }));
  };

  render() {
    // Styles as per screenshot
    const buttonStyle = {
      background: '#4CAF50',
      color: 'white',
      border: 'none',
      borderRadius: '5px',
      padding: '2px 12px',
      fontWeight: 'bold',
      marginRight: '8px',
      fontSize: '14px',
      verticalAlign: 'middle'
    };

    // Wrapper to align the two sections on same row and space out
    const containerStyle = {
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      marginTop: '50px',
      gap: '80px'
    };

    return (
      <div style={containerStyle}>
        {/* Login section */}
        <div>
          <button style={buttonStyle} onClick={this.UpdateEntry}>Login</button>
          <span style={{ verticalAlign: 'middle', color: '#444', fontSize: '16px' }}>
            {this.state.entrycount} People Entered!!!
          </span>
        </div>

        {/* Exit section */}
        <div>
          <button style={buttonStyle} onClick={this.UpdateExit}>Exit</button>
          <span style={{ verticalAlign: 'middle', color: '#444', fontSize: '16px' }}>
            {this.state.exitcount} People Left!!!
          </span>
        </div>
      </div>
    );
  }
}

export default CountPeople;
