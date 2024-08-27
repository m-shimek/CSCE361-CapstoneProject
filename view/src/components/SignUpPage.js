import React, {useState} from 'react';
import '../styles/SignUpPage.css';
import Header from './Header';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

export const API_URL = "http://localhost:3001/api/";

function SignUpPage() {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [partyAffiliation, setpartyAffiliation] = useState('');
    const [error, setError] = useState('');
    
    const navigate = useNavigate();
    const handleSubmit = (event) => {
        event.preventDefault();
        if (password !== confirmPassword) {
            document.getElementById('warning').innerHTML = "Passwords don't match.";
            return;
        }

        axios.get(API_URL+'signup', {params:
            {
                firstName: firstName,
                lastName: lastName,
                dateOfBirth: dateOfBirth,
                email: email,
                password: password,
                partyAffiliation: partyAffiliation
            }}
        )
        .then(response => {
            const userData = response.data;
            localStorage.setItem('user', JSON.stringify(userData));
            console.log('User:', userData);
            navigate('/confirm-account', { state: { email } });
        })
        .catch(error => {
            console.error(error);
                if (error.response && error.response.status === 401) {
                    setError('Invalid credentials.'); 
                } else if (error.response.status === 409) {
                    setError('This email address is already in use. Please try a different email.');
                } else if (error.response.status === 500) {
                    setError('Internal server error. Please try again later.');
                } else {
                    setError('An error occurred. Please try again later.'); 
                    navigate('/confirm-account', { state: { email } });
                }
        });
    };
    
   
    return (
        <div className = "create-account">
            <Header showIcon={false} />
            <main className="main-container">
                <form className = "signup-form" onSubmit = {handleSubmit}>
                    <h2>Sign Up</h2>
                    <p id="warning" style={{textAlign: "center", fontSize: 45, color: 'red'}}></p>
                    <div className = "form-row">
                        <label htmlFor = "first-name"></label>
                        <input
                            id = "first-name"
                            type = "text"
                            placeholder="First Name"
                            title = "Please enter your first name."
                            value = {firstName}
                            onChange={(e) => setFirstName(e.target.value)} 
                            required
                        />
                        <label htmlFor = "last-name"></label>
                            <input
                                id = "last-name"
                                type = "text"
                                placeholder="Last Name"
                                value = {lastName}
                                onChange={(e) => setLastName(e.target.value)} 
                                required
                            />  
                        
                    </div>
                    <input type="hidden" id="hiddenEmail" name="hiddenEmail" value={email} />
                    <div className="form-row">
                        <label htmlFor = "email"></label>
                        <input
                            id = "email"
                            type = "email"
                            placeholder="Email Address"
                            title = "Please enter your email address."
                            value = {email}
                            onChange={(e) => setEmail(e.target.value)} 
                            required
                        />

                        <label htmlFor = "date-of-birth"></label>
                        <input
                            id = "date-of-birth"
                            type = "Date"
                            placeholder="Date of Birth"
                            title = "Please enter your date of birth."
                            min = "1900-01-01"
                            max = "2006-04-24"
                            value = {dateOfBirth}
                            onChange={(e) => setDateOfBirth(e.target.value)}
                            required 
                        />
                    </div>
                    <div className="form-row">
                        <label htmlFor = "password"></label> 
                        <input
                            id = "password"
                            type = "password"s
                            placeholder="Password"
                            minLength = "4" 
                            maxLength = "128"
                            pattern = "(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                            title = "Password must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters."
                            value = {password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />

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

                    <div className="form-row">
                        <label htmlFor = "party-affiliation"></label> 
                        <select id="party-affiliation" value={partyAffiliation} onChange={(e) => setpartyAffiliation(e.target.value)} required>
                        <option class="placeholder1" selected disabled value ="">Party Affiliation</option>
                            <option value="democrat">Democrat</option>
                            <option value="republican">Republican</option>
                            <option value="communist">Communist</option>
                            <option value="socialist">Socialist</option>
                            <option value="libertarian">Libertarian</option>
                            <option value="green party">Green Party</option>
                        </select>
                    </div>

                    <button type = "submit">Sign Up</button>
                    <p style={{ textAlign: "center",  marginTop: "30px"}}>
                        Already have an account? <Link to="/">Back to Log in</Link>
                    </p>
                </form>
                <p id="warning" style={{textAlign: "center", fontSize: 45, color: 'red'}}></p>
            </main>
        </div>
    );
}

export default  SignUpPage;