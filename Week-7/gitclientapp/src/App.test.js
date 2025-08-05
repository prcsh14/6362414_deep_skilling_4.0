// src/App.test.js

import { render, screen } from '@testing-library/react';
import App from './App';

// Update the test to check for your application's title.
test('renders the main heading', () => {
  render(<App />);
  const headingElement = screen.getByText(/Git Repositories of User - TechieSyed/i);
  expect(headingElement).toBeInTheDocument();
});