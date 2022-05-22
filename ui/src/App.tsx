import React from 'react';
import './App.css';
import MainPageContainer from './components/MainPageContainer';
import Nav from './components/Nav';

function App() {
  return (
    <div className="App">
        <header className="App-header">
            <h1>WordCount.com</h1>
            <Nav />    
            <MainPageContainer />
        </header>
    </div>
  );
}

export default App;
