// src/App.js

import './App.css';
import GitClient from './GitClient';
import { useEffect, useState } from 'react';

function App() {
  const [repositories, setRepositories] = useState([]);

  useEffect(() => {
    // Fetch repositories for the user 'techiesyed'
    GitClient.getRepositories('techiesyed').then(response => {
      setRepositories(response.data);
    });
  }, []);

  return (
    <div className="App">
      <h1>Git Repositories of User - TechieSyed</h1>
      {repositories.map(repo => (
        <p key={repo.name}>{repo.name}</p>
      ))}
    </div>
  );
}

export default App;