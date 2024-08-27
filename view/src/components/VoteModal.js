import React from 'react';
import axios from 'axios';
import '../styles/VotingModal.css';
export const API_URL = "http://localhost:3001/api/";

function VoteModal({ candidate, electionId, closeModal }) {
    const handleConfirmVote = async () => {
        const user = JSON.parse(localStorage.getItem('user'));

        if (!user || !user.userId) {
            console.error("No user data found. Please log in.");
            return; 
        }

        axios.post(API_URL+'submitVoteById',{
            userId: user.userId,
            electionId: electionId,
            candidateId: candidate.id
        }).then(response=>{
            console.log("Vote successfully submitted!");
            closeModal();
        }).catch(error=>{
            console.error("Failed to submit vote:", error);
        })
    }; 


    return (
        <div className = "vote-backdrop">
            <div className = "vote-content">
                <h2>Confirm Vote</h2>
                <p>Are you sure you want to vote for {candidate.name}?</p>
                <button onClick = {handleConfirmVote}>Confirm Vote</button>
                <button onClick = {closeModal}>Cancel</button>
            </div>
        </div>
    );
}

export default VoteModal;