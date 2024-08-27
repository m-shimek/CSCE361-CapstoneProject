import React, { useState, useEffect } from 'react';
import '../styles/MainPage.css';
import Header from './Header';
import Navbar from './Navbar';
import axios from 'axios';
import Modal from './Modal';
import Ballot from './Ballot';
import VoteModal from './VoteModal';

export const API_URL = "http://localhost:3001/api/";

function MainPage() {
    const [ballot, setBallot] = useState(null);  
    const [requestCompleted, setRequestCompleted] = useState(false); 
    const [selectedCandidate, setSelectedCandidate] = useState(null);
    const [votingCandidate, setVotingCandidate] = useState(null);
    const [votingElectionId, setElectionId] = useState(null);

      useEffect(() => {
        axios.get(`${API_URL}getCurrentBallot`)
          .then(response => {
            console.log(JSON.stringify(response.data))
            setBallot(response.data);
            setRequestCompleted(true);
          })
          .catch(error => {
            console.error(error);
            
          });
      }, []);
  

    const handleCandidateClick = (candidate) => {
        setSelectedCandidate(candidate);// when candidate card's info icon is clicked
    };
    
    const handleVoteClick = (candidate,electionId) => {
      setVotingCandidate(candidate);
      setElectionId(electionId);
    } ;


    const closeModal = () => { //resets selectedCandidate to "null" which hides the modal
        setSelectedCandidate(null);
        setVotingCandidate(null);
    };

    return (
        <div>
            <Header showIcon={true} />
            <Navbar />
        <div>
        {requestCompleted ?  
          (ballot.year ?  
            <Ballot ballot={ballot} handleCandidateClick={handleCandidateClick} onVoteClick={handleVoteClick} showVoteButton={true} /> 
            :(<h3>No Current Election</h3>))
          :(<h3>Loading... :(</h3>)} 
        </div>
            {selectedCandidate && <Modal candidate={selectedCandidate} closeModal={closeModal} />}
            {votingCandidate && <VoteModal candidate={votingCandidate} electionId={votingElectionId} closeModal={closeModal} showVoteButton={true}/>}
        </div>
        
  );
}

export default MainPage;
