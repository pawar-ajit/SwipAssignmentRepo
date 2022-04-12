import logo from './logo.svg';
import './App.css';
import React from 'react';
import {Link} from 'react-router-dom';

class App extends React.Component {
  render (){

    return(
    <div className="App">
      <nav
        style={{
          borderBottom: "solid 1px",
          paddingBottom: "1rem",
        }}
      >
        <Link to="/home">Home</Link>
          <Link to="/login">Login</Link>
          <Link to="/about">About</Link>
      </nav>

        
    </div>
    )
}
}

export default App;
