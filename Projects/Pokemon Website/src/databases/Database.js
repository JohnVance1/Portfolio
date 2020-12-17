import React from 'react';
//import console = require('console');
//import console = require('console');
//import {graphql} from "gatsby";
//import Img from "gatsby-image";
//import bulb from '../pokemon/icons/gen7/001.png';
//import ivy from '../pokemon/icons/gen7/002.png';


class Database extends React.Component{    
    
    state = {
        Map: 0,
        gen: 7,
        images: 1,
        pokemon: 4,

    };
    
    Display = (num) => {
        this.setState({Map: 4}, 
            function(){
                this.setState({gen: num},
                    function(){
                        console.log(this.state.Map);
                        console.log(this.state.gen);
                    }
                );
            }
        );
        
    }

    
    render(){
        return(
            <React.Fragment>
            
            <button onClick={() => this.Display(4)}>GEN 4</button>
            <button onClick={() => this.Display(7)}>GEN 7</button>
           

            </React.Fragment>
        )
    }
    


}



export default Database;