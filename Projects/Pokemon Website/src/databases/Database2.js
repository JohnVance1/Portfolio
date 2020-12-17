import React from 'react';
import { checkPropTypes } from 'prop-types';
import Pokemon from './Pokemon';
import {Link} from 'react-router-dom';
import ls from 'local-storage';

//import console = require('console');
//import {graphql} from "gatsby";
//import Img from "gatsby-image";
//import bulb from '../pokemon/icons/gen7/001.png';
//import ivy from '../pokemon/icons/gen7/002.png';
//const list = <div></div>;
//let gen = 7;

// Imports the gen 7 sprites
let img7 = require.context('./gen7', false, /\.(png|jpe?g|svg)$/);
let seven = {};
img7.keys().map((item, index) => { seven[item.replace('./', '')] = img7(item); });
    
// Imports the gen4 sprites
let img4 = require.context('./gen4/platinum', false, /\.(png|jpe?g|svg)$/);
let four = {};
img4.keys().map((item, index) => { four[item.replace('./', '')] = img4(item); });


const gen7 = seven;
const gen4 = four;
let check = false;
let list3 = [];
//let poke2 = <Pokemon gen="4"/>;



class Database extends React.Component{    
    constructor(props){
        super(props);
        

    }   

    state = {
        Map: [],
        ID: 0,
        images: [],
        pokemon: [],        

    };

    
    // Allows for the creation of the gen 7 dex
    ShowImgs = () => {
        let that = this;
        let poke = [];
        console.log(poke);

        // Sets the structure to something that I can output
        let setMapState = (list2) =>{

            that.setState({Map: list2});            

        };

        // Possible transfering between pages
        let Clicked = (mon, setMap) =>{
            console.log("Clicked");
            check = false;
            that.setState({ID: mon.id});
            
            list3 = (() => (

                <Pokemon monID={mon.id} poke={poke}></Pokemon>

                //<React.Fragment>
                    // <div key={poke[mon.id - 1].id}>
                    //     <h3>{poke[mon.id - 1].name}</h3>
                    //     <div>
                    //         <img src={poke[mon.id - 1].sprite} alt={poke[mon.id - 1].name}></img>
                    //     </div>
                    // </div>

                //</React.Fragment>
            ))

            setMap(list3());


        };

        // Creates the basic structure for the dex
        let createListAndSet = (setMap) =>{
            list3 = poke.map((mon) =>
            <div key={mon.id}>
                <h3>{mon.name}</h3>
                <div onClick={() => Clicked(mon, setMap)}>
                    {/* cant create a new page and I will have to do a dropdown */}
                    {/* <Pokemon gen = {this.state.gen} id= {mon.id}> */}
                    <img src={mon.sprite} alt={mon.name}></img>
                    
                    {/* </Pokemon> */}
                </div>
            </div>
            )
            
            setMap(list3);
        };

        // Calls and sets the 'database' array
        let pokemonFunction = that.Pokemon;


        // The actual method that runs everything
        that.setState({images: gen7},
            function () {
                poke = pokemonFunction();
                createListAndSet(setMapState);
            }
        );

        console.log("Hello7");

    };

    // Creates the array that will be used 
    Pokemon = () => {
        const poke = [
            {   id: 1, 
                name: 'Bulbasaur',
                sprite: this.state.images['001.png']
            },
        
            {   id: 2,
                name: 'Ivysaur',
                sprite: this.state.images['002.png']
            },
        
            {   id: 3,
                name: 'Veanusaur',
                sprite: this.state.images['003.png']
            },
        
            {   id: 4,
                name: 'Charmander',
                sprite: this.state.images['004.png']
            },

            {   id: 5,
                name: 'Charmeleon',
                sprite: this.state.images['005.png']
            },
        
        ];


        //this.setState({pokemon: poke}); 
        return poke;    

    };


    SetMap = () => {
        

    };

   
    render(){
        return(
            <React.Fragment>
            
                {/* <button onMouseUp={() => {this.Func(4)}}>GEN 4</button>
                <button onMouseUp={() => {this.Func(7)}}>GEN 7</button> */}

                <button onMouseUp={() => {this.ShowImgs()}}>Display</button>


                <div>{this.state.Map}</div>
                {/* <Pokemon /> */}


            </React.Fragment>
        )
    }
    


}



export default Database;