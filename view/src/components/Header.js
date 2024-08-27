import React from 'react';
import { NavLink } from 'react-router-dom';
import '../styles/Header.css'; 
import {motion} from 'framer-motion';


function Header({ showIcon }) {
    const iconVariants = {
        initial: { scale: 1 },
        hover: { scale: 1.2 }, 
        tap: { scale: 0.8 }     
    };

    return (
        <header className="header">
            <h1>Pacopolis Online Ballot</h1>
            {showIcon && ( 
                <NavLink to = "/account-page">
                    <motion.img
                        src="account.png"
                        alt="Account Icon"
                        className="header-icon"
                        variants={iconVariants}  
                        initial="initial"        
                        whileHover="hover"      
                        whileTap="tap"
                    />
                </NavLink>
            )}
        </header>
    );
}

export default Header;
