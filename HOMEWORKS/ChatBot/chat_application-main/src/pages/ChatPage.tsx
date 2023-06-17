import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, RouteProps } from 'react-router-dom';
import App from './App';
import { ToastContainer } from 'react-toastify';
import 'semantic-ui-css/semantic.min.css';
import './index.css';

ReactDOM.render(
    <Router>
        <React.StrictMode>
            <ToastContainer />
            <App />
        </React.StrictMode>
    </Router>,
    document.getElementById('root')
);
