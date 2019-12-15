import React from 'react';
//import {Link} from 'react-router-dom';
//import ls from 'local-storage'

//import PropTypes from 'prop-types';

let img7 = require.context('./gen7', false, /\.(png|jpe?g|svg)$/);
let seven = {};
img7.keys().map((item, index) => { seven[item.replace('./', '')] = img7(item); });
    
let img4 = require.context('./gen4/platinum', false, /\.(png|jpe?g|svg)$/);
let four = {};
img4.keys().map((item, index) => { four[item.replace('./', '')] = img4(item); });

let imgSug = require.context('./gen4/platinum', false, /\.(png|jpe?g|svg)$/);
let sug = {};
imgSug.keys().map((item, index) => { sug[item.replace('./', '')] = imgSug(item); });


const gen7 = seven;
const gen4 = four;
const genSug = sug;

let monID;
let pokes;

class Pokemon extends React.Component {
    constructor(props){
        super(props);
        monID = props.monID;
        pokes = props.poke;
    }

    state = {
        Map: [],
        gen: 7,
        images: [],
        pokemon: [],

    };

    ChangeGen4 = () => {
        let that = this;

        let poke = that.Pokemon;

        let setMapState = () => 
        {
            that.setState({Map: 
            
                <div key={pokes[monID - 1].id}>
                    <h3>{pokes[monID - 1].name}</h3>
                    <img src={poke[monID - 1].sprite} alt={poke[monID - 1].name}></img>
                    
                </div>

            
                }
            
            );

        }

        that.setState({images: gen4}, 
            function (){
                poke = poke();
                setMapState();
            }    
        );
        
        console.log("Hello4");
        
    };
    
    // Allows for the creation of the gen 7 dex
    ChangeGen7 = () => {
        let that = this;

        let poke = that.Pokemon;

        let setMapState = () => 
        {
            that.setState({Map: 
            
                <div key={pokes[monID - 1].id}>
                    <h3>{pokes[monID - 1].name}</h3>
                    <img src={poke[monID - 1].sprite} alt={poke[monID - 1].name}></img>
                    
                </div>

            
                }
            
            );

        }


        that.setState({images: gen7},
            function (){
                poke = poke();
                setMapState();

            }    
        );        
        // let poke = [];
        // console.log(poke);

        // // Sets the structure to something that I can output
        // let setMapState = (list) =>{
        //     that.setState({Map: list});

        // };


        // // Creates the basic structure for the dex
        // let createListAndSet = (setMap) =>{
        //     const list = poke.map((mon) =>
        //     <div key={mon.id}>
        //         <h3>{mon.name}</h3>
        //         <img src={mon.sprite} alt={mon.name}></img>
                    
        //     </div>
        //     )
            
        //     setMap(list);
        // };

        // // Calls and sets the 'database' array
        // let pokemonFunction = that.Pokemon;


        // // The actual method that runs everything
        // that.setState({images: gen7},
        //     function () {
        //         poke = pokemonFunction();
        //         createListAndSet(setMapState);
        //     }
        // );

        console.log("Hello7");

    };

    SelectGen = (gen) => {

        switch(gen){
            case 1: console.log(1);//this.setState({images: gen1});
            break;
            case 2: console.log(2);
            break;
            case 3: console.log(3);
            break;
            case 4: this.ChangeGen4();
            break;
            case 5: console.log(5);
            break;
            case 6: console.log(6);
            break;
            case 7: this.ChangeGen7();
            break;
            default: console.log(0);

        }

        //this.Display();
        

    };

    Display = () => {
        console.log(pokes);
        console.log(monID);

        // have to call the method before seting state
        let poke = this.Pokemon();

        let setMapState = () => 
        {
            this.setState({Map: 
            
                <div key={pokes[monID - 1].id}>
                    <h3>{pokes[monID - 1].name}</h3>
                    <img src={poke[monID - 1].sprite} alt={poke[monID - 1].name}></img>
                    
                </div>

            
            }
            
        );

        }

        this.setState()


        
        //this.setState({Map: disp});
        console.log(this.state.Map);

    };

    Pokemon = () => {
        let poke = [
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

    
    render(){
        return(
        <React.Fragment>
            
            <button onMouseUp={() => {this.SelectGen(4)}}>GEN 4</button>
            <button onMouseUp={() => {this.SelectGen(7)}}>GEN 7</button>

                
            <div>{this.state.Map}</div>
            {/* <Pokemon /> */}


        </React.Fragment>

        )
    }

}

export default Pokemon;