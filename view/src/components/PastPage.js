import React, { useState, useEffect } from 'react';
import '../styles/MainPage.css'; // Adjust with proper styles
import Header from './Header';
import Navbar from './Navbar';
import axios from 'axios';
import Modal from './Modal';
import Ballot from './Ballot';
import FilterDropdown from './FilterDropdown';
export const API_URL = "http://localhost:3001/api/";

function PastPage() {
  const [ballots, setBallots] = useState([]);
  const [requestCompleted, setRequestCompleted] =useState(false);
  const [filterYear, setFilterYear] = useState(null);
  const [selectedCandidate, setSelectedCandidate] = useState(null);

useEffect(() => {
  axios.get(`${API_URL}getPastBallots`)
    .then(response => {
      setBallots(response.data);
      setRequestCompleted(true);
    })
    .catch(error => {
      console.error(error);
    });
}, []);

  const handleFilterYearChange = (year) => setFilterYear(year);
  const handleCandidateClick = (candidate) => setSelectedCandidate(candidate);
  const closeModal = () => setSelectedCandidate(null);
  const filteredBallots = ballots.filter(election => !filterYear || election.year === filterYear);
  const years = [...new Set(ballots.map(election => election.year))].sort().reverse();

  return (
    <div>
      <Header showIcon={true} />
      <Navbar />
      <FilterDropdown years={years} onChange={handleFilterYearChange} />
    <div>
      {requestCompleted ? 
        (filteredBallots.length ? 
          filteredBallots.map((ballot)=>(<Ballot ballot={ballot} handleCandidateClick={handleCandidateClick} showVoteButton={false} showWinnerStatus={true} />))
          : (<p>No Past Elections :(</p>))
        : (<p>Loading</p>)}
    </div>
      {selectedCandidate && <Modal candidate={selectedCandidate} closeModal={closeModal} />}
    </div>
  );
}

export default PastPage;
