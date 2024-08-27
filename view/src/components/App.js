import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './LoginPage';
import SignUpPage from './SignUpPage';
import ConfirmAccount from './ConfirmAccount';
import MainPage from './MainPage';
import AccountPage from './AccountPage';
import ForgotPassword from './ForgotPassword';
import PasswordReset from './PasswordReset';
import PastPage from './PastPage';
import UpcomingPage from './UpcomingPage';
import Footer from './Footer'; 
import '../styles/App.css';


function App() {
  return (
    <div className = "App">
      <Router>
        <div className = "main-content">
          <Routes>
            <Route path = "/" element = {<LoginPage />} />
            <Route path = "/signup" element = {<SignUpPage />} />
            <Route path = "/confirm-account" element={<ConfirmAccount />} />
            <Route path = "/main" element = {<MainPage />} /> 
            <Route path = "/account-page" element = {<AccountPage />} /> 
            <Route path = "/forgot-password" element = {<ForgotPassword />} />
            <Route path = "/password-reset" element = {<PasswordReset />} />
            <Route path = "/past" element = {<PastPage />} />
            <Route path = "/upcoming" element = {<UpcomingPage />} />
          </Routes>
        </div>
        <Footer />
      </Router>
    </div>
  );
}


export default App;

