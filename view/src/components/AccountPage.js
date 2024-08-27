import React, {useState, useEffect} from 'react';
import '../styles/Account.css';
import Header from './Header';
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';
import '../styles/LogoutModal.css';
import '../styles/DeleteAccountModal.css';

export const API_URL = "http://localhost:3001/api/";

function AccountPage() {

    const [email, setEmail] = useState('');
    const [partyAffiliation, setpartyAffiliation] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [error, setError] = useState('');
    const [isLogoutModalOpen, setIsLogoutModalOpen] = useState(false);
    const [isDeleteAccountModalOpen, setIsDeleteAccountModalOpen] = useState(false);


    useEffect(() => {
        const storedUserData = localStorage.getItem('user');
        if (storedUserData) {
          const userData = JSON.parse(storedUserData);
          setEmail(userData.email);
          setpartyAffiliation(userData.partyAffiliation);
          setFirstName(userData.firstName);
          setLastName(userData.lastName);
          let tPosition = userData.dateOfBirth.indexOf("T");
          let newDOB = userData.dateOfBirth.slice(0, tPosition);
          setDateOfBirth(newDOB);
        }
    }, []);

    useEffect(() => {
        console.log('Modal state updated:', isLogoutModalOpen, isDeleteAccountModalOpen);
    }, [isLogoutModalOpen, isDeleteAccountModalOpen]);

    const toggleLogoutModal = () => {
        setIsLogoutModalOpen(!isLogoutModalOpen);
    };
      
    const toggleDeleteAccountModal = () => {
        setIsDeleteAccountModalOpen(!isDeleteAccountModalOpen);
    };
    
    const navigate = useNavigate();
    const handleEditInformation = async (event) => {
        event.preventDefault();
            axios.post(API_URL+'updateaccount', {
                params: {
                    partyAffiliation: partyAffiliation,
                    firstName: firstName,
                    lastName: lastName,
                    dateOfBirth: dateOfBirth,
                    email: email
                }
            })
        
            .then(response => {
                const userData = response.data;
                localStorage.setItem('user', JSON.stringify(userData));
                console.log('User:', userData);
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
                    }
            });
    };

    const handleResetPassword = async () => {
        navigate('/forgot-password');
    };

    const handleLogout = async () => {
        
        try {
            const response = await axios.post(`${API_URL}logout`, {}, { 
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });
      
            if (response.status === 200) {
                localStorage.removeItem('user'); 
                navigate('/login'); 
            } else {
                console.error('Error logging out:', response.data);
                setError('An error occurred while logging out. Please try again later.');
            }
        } catch (error) {
            console.error('Error logging out:', error);
            setError('An error occurred while logging out. Please try again later.');
        } finally {
            setIsLogoutModalOpen(false); 
        }
    }

    const handleDeleteAccount = async () => { 
        try {
            const response = await axios.delete(`${API_URL}account-page`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });
        
            if (response.status === 200) {
                localStorage.removeItem('user'); 
                navigate('/login'); 
            } else {
                console.error('Error deleting account:', response.data);
                setError('An error occurred while deleting your account. Please try again later.');
            }
        } catch (error) {
            console.error('Error deleting account:', error);
            setError('An error occurred while deleting your account. Please try again later.');
        }


    };
    
   
    return (
        <div className = "account-page">
            <Header showIcon={false} />
            <main className="main-container">
                <form className = "account-form" onSubmit = {handleEditInformation}>
                    <h2>User Settings</h2>
                    <div className="form-row" id = "top-box">
                        <p>Email:{email}</p>
                    </div>
                    
                    <div className="form-row">
                        <label htmlFor = "party-affiliation">Party Affiliation:</label> 
                        <select id="party-affiliation" value={partyAffiliation} onChange={(e) => setpartyAffiliation(e.target.value)} required>
                        <option value="democrat">Democrat</option>
                            <option value="republican">Republican</option>
                            <option value="communist">Communist</option>
                            <option value="socialist">Socialist</option>
                            <option value="libertarian">Libertarian</option>
                            <option value="green party">Green Party</option>
                        </select>
                    </div>


                    <div className="form-row">
                        <label htmlFor = "first-name">First Name:</label>
                        <input
                            id = "first-name"
                            type = "text"
                            placeholder="First Name"
                            title = "Please enter your first name."
                            value = {firstName}
                            onChange={(e) => setFirstName(e.target.value)} 
                        />
                    </div>

                    <div className="form-row">
                        <label htmlFor = "last-name">Last Name:</label>
                        <input
                            id = "last-name"
                            type = "text"
                            placeholder="Last Name"
                            value = {lastName}
                            onChange={(e) => setLastName(e.target.value)} 
                            />  
                    </div>

                    <div className="form-row">
                        <label htmlFor = "date-of-birth">Date of Birth:</label>
                        <input
                            id = "date-of-birth"
                            type = "text"
                            placeholder="Date of Birth"
                            title = "Please enter your date of birth."
                            min = "1900-01-01"
                            max = "2006-04-24"
                            value = {dateOfBirth}
                            onChange={(e) => setDateOfBirth(e.target.value)}
                        />
                    </div>

                    <button className = "button" id = "submit" onClick={handleEditInformation}>Edit Information</button>
                    
                    
                    <button className = "button" id = "reset" onClick={handleResetPassword}>Reset Password</button>
                    
                    <button className = "button" id = "logout" onClick={toggleLogoutModal}>Log-Out</button>
                
                
                    <button className = "button" id = "delete" onClick={handleDeleteAccount}>Delete Account</button>
                    
                    

                </form>

            {isLogoutModalOpen && (
            <div id="logoutModal" className="modal">
                <div className="modal-content">
                <span className="close" onClick={toggleLogoutModal}>
                    &times;
                </span>
                <p>Are you sure you want to log out?</p>
                <button id="confirmLogout" onClick={handleLogout}>
                    Yes, Log Out
                </button>
                </div>
            </div>
            )}

            {isDeleteAccountModalOpen && (
            <div id="deleteAccountModal" className="modal">
                <div className="modal-content">
                <span className="close" onClick={toggleDeleteAccountModal}>
                    &times;
                </span>
                <p>Are you sure you want to delete your account?</p>
                <button id="confirmDelete" onClick={handleDeleteAccount}>
                    Yes, Delete Account
                </button>
                </div>
            </div>
            )}
                
            </main>
        </div>
    );
}

export default  AccountPage;