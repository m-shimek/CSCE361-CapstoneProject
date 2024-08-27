import React, {useState} from 'react';
import axios from 'axios';
import '../styles/ElectionDetails.css';
import Candidate from './Candidate';
export const API_URL = "http://localhost:3001/api/";

function ElectionDetails({ election, handleCandidateClick, onVoteClick, showVoteButton}) {
    const userId = JSON.parse(localStorage.getItem('user')).userId;
    const [votedId, setVotedId] = useState(0);
    const [cand1Id, setCand1Id] = useState(election.candidates[0].id);
    const [cand2Id, setCand2Id] = useState(election.candidates[1].id);
     
    axios.get(API_URL+"getVoteStatusId",{
        params:{
            userId: userId,
            electionId: election.id
        }
    })
    .then(response =>{
        setVotedId(response.data);
    })
    .catch(error => {
        console.error(error);
        alert(`Failed to submit vote: ${error}`); 
    });

    return (
        <div className = "election-details-container">
            <h2>{election.position}</h2>
            <div className = "candidates">
                {election.candidates.map((candidate) => (
                    <Candidate
                        key = {candidate.id}
                        candidate = {candidate}
                        electionId={election.id}
                        onInfoClick = {handleCandidateClick}
                        onVoteClick = {onVoteClick}
                        showVoteButton = {showVoteButton && (cand1Id !== votedId) && (cand2Id !== votedId)}  
                        votedAlready ={votedId===candidate.id}
                    />
                ))}
            </div>
        </div>
    );
}

export default ElectionDetails;
