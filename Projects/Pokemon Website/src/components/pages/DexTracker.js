import React from 'react';
import uuid from 'uuid';

import SearchBar from '../search/SearchBar';
//import SearchField from "react-search-field";


class DexTracker extends React.Component {

    state = {
        checklists: [
            {
                id: uuid.v4(),
                list: [
                    {
                        id: 1,
                        name: 'Bulb'
                    }
                ]
            },
            {
                id: uuid.v4(),
                list: [
                    {
                        id: 2,
                        name: 'Char'
                    }
                ]
            }
            
        ]

    }

    addChecklist = (list) =>{
        const newChecklist = {
          id: uuid.v4(),
          list: list
          
        }
    
        this.setState({checklists: [...this.state.checklists, newChecklist]})
    
    }

    delChecklist = (id) => {
        this.setState({ checklists: [...this.state.checklists.filter(todo => todo.id !== id)] });
    }


   
    render(){
        return(
           <SearchBar />
        
        )
    }

}

export default DexTracker;