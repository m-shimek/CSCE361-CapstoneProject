import React from 'react';
import '../styles/Candidate.css'; 
import {motion} from 'framer-motion';

function Candidate({ candidate, onInfoClick, onVoteClick, showVoteButton, showWinnerStatus, electionId, votedAlready}) {
    const cardVariants = {
        initial: {
            scale: 1,
            boxShadow: "0px 0px 0px rgba(0,0,0,0)",
            },
            hover: {
                scale: 1.02,
                boxShadow: "5px 5px 15px rgba(0,0,0,0.4)",
                
            }
    };

    const voteButtonVariants = {
        hover: {
            scale: 1.1,
            border: "2px solid",
            borderColor: ["#ff0040", "#004cff", "#ff0040"],
            color: "#0056b3",
            transition: {
                borderColor: {
                    repeat: Infinity, 
                    duration: 1, 
                    ease: "linear" 
                }
            }
        },
        tap: {
            scale: 0.9
        }
    };

    return (
        <motion.div
            className = {votedAlready ? "grey-candidate-card" : "candidate-card"}
            variants = {cardVariants}
            onClick={() => onInfoClick(candidate)}
            initial = "initial"
            whileHover = "hover"
            transition = {{type: "spring", stiffness: 300, damping: 20}}
        >
            <img src = {"/candidate" + candidate.id + ".jpg"} alt = {`${candidate.name}`} className = "candidate-card-headshot" />
            <div className = "candidate-info">
                <h2>{candidate.name}</h2>
                <p>Party: {candidate.party}</p>
                <p>Votes: {candidate.votes}</p>
                {showWinnerStatus && <p>{candidate.isWinner ? "Winner" : "Not a Winner"}</p>}
            </div>
            {showVoteButton && !votedAlready && (<motion.button 
                className = "vote-now-button" 
                variants = {voteButtonVariants}
                whileHover = "hover"
                style = {{border: 'none', background: 'transparent'}}
                onClick={(event) => {
                    event.stopPropagation();  
                    onVoteClick(candidate,electionId);  
                }}
            > 
                Vote Now
            </motion.button>)}
        </motion.div>
    );
}

export default Candidate;
