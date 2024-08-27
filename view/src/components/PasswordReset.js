import React, {useState} from 'react';
import '../styles/PasswordReset.css';
import Header from './Header';
import axios from 'axios';
import {useNavigate} from 'react-router-dom'; 
import { Link } from 'react-router-dom';
export const API_URL = "http://localhost:3001/api/";

function PasswordReset() {
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post(API_URL + 'password-reset', { password })

            .then(response => {
                setSuccessMessage('Password reset successfuly!');
                setError('');
            })
            .catch(error => {
                console.error(error);
                setError('An error occurred. Please try again later.');
                setSuccessMessage('');
            });
    };

    return (
        <div className="password-reset-page">
            <Header showIcon = {false} />
            <main className = "main-container">
                <form className = "password-reset-form" onSubmit={handleSubmit}>
                    <h2>Forgot Password?</h2>
                    {error && <p style = {{ color: 'red' }}>{error}</p>}
                    {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                    <div className="form-row">
                        <label htmlFor = "password"></label> 
                        <input
                            id = "password"
                            type = "password"
                            placeholder="Password"
                            minLength = "4" 
                            maxLength = "128"
                            pattern = "(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                            title = "Password must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters."
                            value = {password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-row">
                        <label htmlFor = "confirm-password"></label> 
                        <input
                            id = "confirm-password"
                            type = "password"
                            placeholder="Confirm Password"
                            pattern= "(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                            title = "Password must match."
                            value = {confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type="submit">Confirm Password</button>
                    <p style={{ textAlign: "center",  marginTop: "30px"}}>
                         <Link to="/">Back to Log in</Link>
                    </p>
                </form>
            </main>
        </div>
    );
}

export default PasswordReset;