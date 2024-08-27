import React from "react";
import { NavLink } from 'react-router-dom';
import '../styles/Footer.css'; 
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faGithub, faFacebook, faYoutube} from '@fortawesome/free-brands-svg-icons';
import {faCopyright} from "@fortawesome/free-solid-svg-icons";

const Footer = () => {
    return (
        <footer className="footer">
            <div className= "footer-container">
                <div className = "footer-item">
                    <span style = {{paddingRight: 5}}>Copyright </span>
                    <FontAwesomeIcon icon = {faCopyright} />{" "}
                    <span style={{paddingLeft: 5}}>
                        {new Date().getFullYear()} Pacopolis Online Ballot. All Rights Reserved.
                    </span>
                </div>
                <a
                    href="https://github.com/BClear75/Capstone-Project"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="footer-item"
                >
                    <FontAwesomeIcon icon = {faGithub} className="footer-icon" />
                </a>
                <a
                    href="https://www.facebook.com/profile.php?id=100085104463353"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="footer-item"
                >
                    <FontAwesomeIcon icon = {faFacebook} className="footer-icon" />
                </a>
                <a
                    href="https://www.youtube.com/results?search_query=voting+news"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="footer-item"
                >
                    <FontAwesomeIcon icon={faYoutube} className="footer-icon"  />
                </a>

            </div>
        </footer>
    );
};

export default Footer;
