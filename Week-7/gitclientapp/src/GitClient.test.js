// src/GitClient.test.js

import axios from 'axios';
import GitClient from './GitClient';

jest.mock('axios');

describe("Git Client Tests", () => {
  test("should return repository names for techiesyed", async () => {
    // 1. Arrange
    const dummyRepos = {
      data: [
        { name: 'Repo1' },
        { name: 'Repo2' }
      ]
    };
    axios.get.mockResolvedValue(dummyRepos);

    // 2. Act
    await GitClient.getRepositories('techiesyed');

    // 3. Assert
    // This is the line that needs to be fixed.
    // We now check for the headers object as the second argument.
    expect(axios.get).toHaveBeenCalledWith(
      'https://api.github.com/users/techiesyed/repos',
      {
        headers: {
          Authorization: `token ${process.env.REACT_APP_GITHUB_TOKEN}`,
        },
      }
    );
  });
});