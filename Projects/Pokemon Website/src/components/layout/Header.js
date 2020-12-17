import React from 'react';
import {Link} from 'react-router-dom';

function Header() {
    return (
        <header style={headerStyle}>
            {/* <h1>TodoList</h1> */}
            <Link style={linkStyle} to="/">Home</Link> | 
            <Link style={linkStyle} to="/dexTracker"> Dex Tracker </Link> |
            <Link style={linkStyle} to="/counter"> Counter </Link> |
            <Link style={linkStyle} to="/dex"> Dex</Link> |
            <Link style={linkStyle} to="/dex2"> Dex2</Link>

        </header>

    )

}

const headerStyle = {
    background: '#333',
    color: '#fff',
    textAlign: 'center',
    padding: '10px'
    
}

const linkStyle = {
    color: '#fff',
    textDecoration: 'none'
    
}

export default Header