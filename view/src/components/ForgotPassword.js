import React, {useState} from 'react';
import '../styles/ForgotPassword.css';
import Header from './Header';
import axios from 'axios';
import {useNavigate} from 'react-router-dom'; 
import { Link } from 'react-router-dom';
import { motion } from 'framer-motion';
export const API_URL = "http://localhost:3001/api/";


function ForgotPassword() {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [error, setError] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post(API_URL + 'forgot-password', { email })
            .then(response => {
                setSuccessMessage('Password reset email sent. Please check your inbox.');
                setError('');
                navigate('/password-reset');
            })
            .catch(error => {
                console.error(error);
                setError('An error occurred. Please try again later.');
                setSuccessMessage('');
            });
    };

    return (
        <div className = "forgot-password-page">
            <Header showIcon = {false} />
            <main className = "main-container">
                <form className = "forgot-password-form" onSubmit={handleSubmit}>
                <motion.h2
                        initial = {{opacity: 0, y: -20}}
                        animate = {{opacity: 1, y: 0}}
                        transition = {{duration: 0.5, type: 'spring', stiffness: 120}}
                    >
                        Forgot Password?
                    </motion.h2>
                    <motion.p
                        style = {{color: 'black'}}
                        initial = {{opacity: 0, y: -20}}
                        animate = {{opacity: 1, y: 0}}
                        transition = {{duration: 0.5, delay: 0.3, type: 'spring', stiffness: 120}}
                    >
                        Please enter your email address below to receive a link to reset your password.
                    </motion.p>
                    {error && <p style = {{color: 'red'}}>{error}</p>}
                    {successMessage && <motion.p
                        style = {{color: 'green'}}
                        initial = {{opacity: 0}}
                        animate = {{opacity: 1}}
                        transition = {{duration: 0.5}}
                    >{successMessage}</motion.p>}
                    <label htmlFor="email"></label>
                    <input
                        id = "email"
                        type = "email"
                        placeholder = "Enter your email"
                        value = {email}
                        onChange = {(e) => setEmail(e.target.value)}
                        required
                    />
                    <button type="submit">Reset Password</button>
                    <p style={{ textAlign: "center",  marginTop: "30px"}}>
                         <Link to="/">Back to Log in</Link>
                    </p>
                </form>
            </main>
        </div>
    );
}

export default ForgotPassword;