import React from 'react';
import { Outlet } from 'react-router-dom';
import './App.css';
import Nav from './components/Nav/Nav';

function App() {
  return (
    <div className="App">
        <header className="App-header">
            <h1>WordCount.com</h1>
            <Nav />    
        </header>
        <Outlet />
    </div>
  );
}

export default App;
