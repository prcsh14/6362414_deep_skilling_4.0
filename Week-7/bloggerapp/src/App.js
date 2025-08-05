import React, { useState } from 'react';
import BookDetails from './components/BookDetails';
import BlogDetails from './components/BlogDetails';
import CourseDetails from './components/CourseDetails';
import './index.css'; // Make sure index.css contains proper styles

function App() {
  const [view, setView] = useState('course');

  const books = [
    { id: 1, bname: 'Master React', price: 670 },
    { id: 2, bname: 'Deep Dive into Angular 11', price: 800 },
    { id: 3, bname: 'Mongo Essentials', price: 450 },
  ];

  return (
    <div className="container">
      <div className="button-group">
        <button onClick={() => setView('course')}>Course Details</button>
        <button onClick={() => setView('book')}>Book Details</button>
        <button onClick={() => setView('blog')}>Blog Details</button>
      </div>

      <div className="details-view">
        {view === 'course' && <CourseDetails />}
        {view === 'book' && <BookDetails books={books} />}
        {view === 'blog' && <BlogDetails />}
      </div>
    </div>
  );
}

export default App;
