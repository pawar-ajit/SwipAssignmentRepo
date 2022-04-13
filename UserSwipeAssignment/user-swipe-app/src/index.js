import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {BrowserRouter,Routes, Route} from 'react-router-dom';
import LoginComponent from './Components/LoginComponent';
import AboutComponent from './Components/AboutComponent'
import HomeComponent from './Components/HomeComponent'
import configureStore from './Store/store';
import { Provider } from 'react-redux';

const rootElement = document.getElementById("root");
ReactDOM.render(
  
  <Provider store={configureStore()}>
  <React.StrictMode>
        <BrowserRouter>
          
        <Routes>
            <Route path="/" element={<App/>} />
            <Route path="/login" element={<LoginComponent/>} />
            <Route path="/home" element={<HomeComponent/>} />
            <Route path="/about" element={<AboutComponent/>} />
        </Routes>
        </BrowserRouter>
  
    {/* <App /> */}
  </React.StrictMode>
  </Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
