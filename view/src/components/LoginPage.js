import React, {useState} from 'react';
import '../styles/LoginPage.css';
import Header from './Header';
import {motion} from 'framer-motion';
import axios from 'axios';
import {useNavigate, Link} from 'react-router-dom';
export const API_URL = "http://localhost:3001/api/";

function LoginPage() {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');


    const handleSubmit = (event) => {
        event.preventDefault();
        axios.get(API_URL+'signin', {
            params: {
                email: email,
                password: password
            }
        })
        .then(response => {
            const userData = response.data;
            localStorage.setItem('user', JSON.stringify(userData));
            console.log('User:', userData);
            setError('');
            navigate('/main');
        })
        .catch(error => {
            console.error(error);
            if (error.response && error.response.status === 401) {
                setError('Invalid credentials.'); 
            } else {
                setError('An error occurred. Please try again later.'); 
            }
        });
    };

    const imageVariants = {
        hover: {
            y: 20,
            scale: 1.1, // Scale up on hover
            transition: { type: "spring", stiffness: 300 }
        },
        tap: {
            scale: 0.9 // Scale down on click
        }
    };

    return (
        <div className = "login-page">
            <Header showIcon = {false} />
            <main className ="main-container">
            <motion.img
                src="/elephant.png"
                alt="Republican"
                className="icon"
                whileHover="hover"
                whileTap="tap"
                variants={imageVariants}
            />
                <form className = "login-form" onSubmit = {handleSubmit}>
                    <h2>Sign In</h2>
                    {error && <p style={{ color: 'red' }}>{error}</p>}
                    <label htmlFor = "email"></label>
                    <input
                        id = "email"
                        type = "email"
                        placeholder="Email Address"
                        value = {email}
                        onChange={(e) => setEmail(e.target.value)}
                        required 
                    />

                    <label htmlFor = "password"></label> 
                    <input
                        id = "password"
                        type = "password"
                        placeholder="Password"
                        value = {password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                    <Link to="/forgot-password" className="forgot-password">Forgot Password?</Link>
                    <button type = "submit">Sign In</button>
                    <div className="additional-options">
                        <div className="or-text">or</div>
                        <a href="/signup" className="create-account">Create Account</a>
                    </div>
                </form>
                <motion.img
                    src="/donkey.png"
                    alt="Democrat"
                    className="icon"
                    whileHover="hover"
                    whileTap="tap"
                    variants={imageVariants}
            />
            </main>
        </div>
    );
}

export default  LoginPage; 