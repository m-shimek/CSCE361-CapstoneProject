import React, { useEffect, useState } from 'react';
import '../styles/Modal.css';

function Modal({ candidate, closeModal }) {
    const [isVisible, setIsVisible] = useState(false);

    useEffect(() => {
        if (candidate) {
            // show modal after a short delay for transition
            setTimeout(() => setIsVisible(true), 30);
        }
    }, [candidate]);

    if (!candidate) return null;

    const closeWithAnimation = () => {
        setIsVisible(false);
        setTimeout(() => closeModal(), 200); // wait for the animation to complete
    };

    return (
        <div className={`modal-backdrop ${isVisible ? 'show' : ''}`} onClick = {closeWithAnimation} aria-labelledby="candidate-details">
            <div className="modal-content" onClick = {e => e.stopPropagation()}>
                <button className="close-button" onClick = {closeWithAnimation}>×</button>
                <div className = "candidate-details" id = "candidate-details">
                    <div className = "candidate-header">
                        <img
                            src = {"/candidate" + candidate.id + ".jpg"}
                            alt = {`${candidate.name}`}
                            className="modal-headshot"
                        />
                        <div className = "modal-info">
                            <h2>{candidate.name}</h2>
                            <p>Party: {candidate.party}</p>
                            <p>Age: {candidate.age}</p>
                            <p>{candidate.description}</p>
                        </div>
                    </div>
                    <div className = "candidate-policies">
                        <h3>Priority Policies</h3>
                        <ul>
                            {candidate.priorityPolicies && candidate.priorityPolicies.length ? (
                                candidate.priorityPolicies.map((policy, index) => (
                                    <li key = {index}>{policy}</li>
                                ))) : (
                                <li>No policies available</li>
                            )}
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Modal;