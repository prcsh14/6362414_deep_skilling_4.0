// src/GitClient.js

import axios from "axios";

// Access the token from environment variables
const token = process.env.REACT_APP_GITHUB_TOKEN;

class GitClient {
  static getRepositories(userName) {
    const url = `https://api.github.com/users/${userName}/repos`;

    // Send the token in the Authorization header
    return axios.get(url, {
      headers: {
        Authorization: `token ${token}`,
      },
    });
  }
}

export default GitClient;