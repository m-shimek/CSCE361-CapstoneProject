import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './components/LoginPage';
import SignUpPage from './components/SignUpPage';
import ConfirmAccount from './components/ConfirmAccount';
import MainPage from './components/MainPage';
import VotePage from './components/VotePage';
import AccountPage from './components/AccountPage';
import ForgotPassword from './components/ForgotPassword';
import PasswordReset from './components/PasswordReset';
import PastPage from './components/PastPage';
import UpcomingPage from './components/UpcomingPage';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/signup" element={<SignUpPage />} />
          <Route path="/confirm-account" element={<ConfirmAccount />} />
          <Route path="/main" element={<MainPage />} />
          <Route path="/vote-page" element={<VotePage />} />
          <Route path="/account-page" element={<AccountPage />} />
          <Route path="/forgot-password" element={<ForgotPassword />} />
          <Route path="/password-reset" element={<PasswordReset />} />
          <Route path="/past" element={<PastPage />} />
          <Route path="/upcoming" element={<UpcomingPage />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
