import React, { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { motion } from 'framer-motion';
import '../styles/Navbar.css';

function Navbar() {
    const location = useLocation();
    const [activeTab, setActiveTab] = useState(location.pathname);
    const tapStyle = {scale: 1.5, color: ["#ff0040", "#004cff", "#ff0040"],transition: {yoyo: Infinity, duration: 0.5}};

    return (
        <nav className = "navbar">
            <ul className =  "nav-links">
                <li>
                    <Link to = "/past" className = {activeTab === '/past' ? 'active' : ''} onClick = {() => setActiveTab('/past')}>
                        <motion.div whileTap = {tapStyle}>Past Elections</motion.div>
                    </Link>
                </li>
                <li>
                    <Link to = "/main" className = {activeTab === '/main' ? 'actiive' : ''} onClick = {() => setActiveTab('/main')}>
                        <motion.div whileTap = {tapStyle}>Current Elections</motion.div>
                    </Link>
                </li>
                <li>
                    <Link to = "/upcoming" className = {activeTab === '/upcoming' ? 'active' : ''} onClick = {() => setActiveTab('/upcoming')}>
                        <motion.div whileTap = {tapStyle}> Upcoming Elections</motion.div>
                    </Link>
                </li>
            </ul>
        </nav>
    );
}

export default Navbar;