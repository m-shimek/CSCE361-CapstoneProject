import React, {useState, useEffect} from 'react';
import '../styles/ConfirmAccount.css';
import Header from './Header';
import axios from 'axios';
import { useNavigate, useLocation } from 'react-router-dom'; 
import { Link } from 'react-router-dom';
export const API_URL = "http://localhost:3001/api/";

function ConfirmAccount() {
    const navigate = useNavigate();
    const location = useLocation();
    const { email } = location.state || {};
    
    const [confirmationCode, setConfirmationCode] = useState('');
    const [error, setError] = useState('');
    const [successMessage, setSuccessMessage] = useState('');
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
      return () => {
        setShowModal(false);
      };
    }, []);

    useEffect(() => {
      //replace error with just successMessage after getting api working
      if (error || successMessage) {
        setShowModal(true);
      }
    }, [error, successMessage]);

    const handleSendEmail = () => {
        if (!email) {
            setError('Please provide an email address.');
            return;
        }

        axios.get(API_URL + 'confirmAccount', { params: { email} })
          .then(response => {
            setSuccessMessage('Account confirmation email sent. Please check your inbox.');
            setError('');
          })
          .catch(error => {
            console.error(error);
            setError('An error occurred. Please try again later.');
            setSuccessMessage('');
          });
      };

    const handleSubmitPasscode = (event) => {
        event.preventDefault();
        
        // Simple Validation
        if (!confirmationCode) {
            setError('Please enter the confirmation code.');
            return;
        }

        if (!email) {
          setError('Please provide an email address.');
          return;
        } 
      
        axios.get(API_URL + 'verifyConfirmationCode', {params:{
          email,
          confirmationCode}
        })
          .then(response => {
            setSuccessMessage('Account confirmed successfully!');
            setError('');
            navigate('/main');
            setShowModal(false);
            // Can redirect to a successful confirmation page here instead of main
          })
          .catch(error => {
            console.error(error);
            setError('Invalid confirmation code. Please try again.');
            setSuccessMessage('');
            setShowModal(false);
          });
      };

    return (
        <div className="confirm-account">
            <Header showIcon = {false} />
            <main className = "main-container">
                <form className = "confirm-account-form" onSubmit={handleSubmitPasscode}>
                    <h2>Confirm Your Account</h2>
                    <h4>Please confirm {email} is your email address by entering your confirmation code.</h4>
                    <button type="button" class="button1" onClick={handleSendEmail}>Confirm Email</button> 

                    <label htmlFor = "confirmationCode"></label> 
                    <input
                        id = "confirmation-code"
                        type = "text"
                        placeholder="Confirmation Code"
                        value = {confirmationCode}
                        onChange={(e) => setConfirmationCode(e.target.value)}
                        required
                    />
                    <button type="submit" class="button2" onClick={handleSubmitPasscode}>Enter Passcode</button>

                    {error && <p style={{ color: 'red' }}>{error}</p>}
                    {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                    
                    <p style={{ textAlign: "center",  marginTop: "30px"}}>
                         <Link to="/signup">Back to sign up</Link>
                    </p>
                </form>
            </main>
              <div className="overlay">
                {showModal && (
                  <div className="modal">
                    <div className="modal-content">
                      <h2>Your Account Has been Confirmed!</h2>
                      <p>{successMessage}</p>
                      <a href = "/" >
                        <button class="close-link"> Back to Sign Up</button>
                      </a>
                    </div>
                  </div>
                )}
              </div>
        </div>
    );
}

export default ConfirmAccount;