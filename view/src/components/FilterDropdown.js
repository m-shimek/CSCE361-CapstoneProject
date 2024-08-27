import React from 'react';
import { motion } from 'framer-motion';
import '../styles/FilterDropdown.css'

function FilterDropdown({ years, onChange }) {
    const handleYearChange = (event) => {
        onChange(event.target.value);
    };

    const containerVariants = {
        hidden: { opacity: 0, y: -20 },
        visible: { 
            opacity: 1, 
            y: 0,
            transition: {
                delay: 0.1,
                when: "beforeChildren",
                staggerChildren: 0.1
            }
        }
    };

    const itemVariants = {
        hidden: { opacity: 0 },
        visible: { 
            opacity: 1,
            initial: 0,
            transition: { duration: 0.5 }
        }
    };

    return (
        <motion.div 
            className = "filter-dropdown"
            variants = {containerVariants}
            initial = "hidden"
            animate = "visible"
        >
            <label htmlFor="year-select">Filter by Year: </label>
            <motion.select 
                id="year-select" 
                onChange={handleYearChange}
                variants={itemVariants}
            >
                <option value = "">All Years</option>
                {years.length > 0 ? (
                    years.map(year => (
                        <motion.option key={year} value={year} variants={itemVariants}>
                            {year}
                        </motion.option>
                    ))) : (
                        <option value = ""> No Data Available</option>
                )}
            </motion.select>
        </motion.div>
    );
}

export default FilterDropdown;