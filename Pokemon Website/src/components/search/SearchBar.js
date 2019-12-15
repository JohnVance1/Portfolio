import React from 'react';
//import axios from 'axios';
import Suggestions from './Suggestions'
//const { API_KEY } = process.env
//const API_URL = 'http://api.musicgraph.com/api/v2/artist/suggest'


class SearchBar extends React.Component {

    state = {
        query: '',
        results: []
    }

    getInfo = () => {
        if(this.state.query === 'bulb')
        {
            this.setState({
                results: ['1']
            })
        }


        // axios.get(`${API_URL}?api_key=${API_KEY}&prefix=${this.state.query}&limit=7`)
        //     .then(({ data }) => {
        //         this.setState({
        //             results: data.data // MusicGraph returns an object named data, 
        //                             // as does axios. So... data.data                             
        //         })
        //     })
      }

    inputChange = () => {
        this.setState({
            query: this.search.value
        }, () => {
            if (this.state.query && this.state.query.length > 1) {
                if (this.state.query.length % 2 === 0) {
                    this.getInfo();
                }
            } 
        })
    }

    render(){
        return(
            <form>
                <input
                    placeholder="Search for..."
                    ref={input => this.search = input}
                    onChange={this.inputChange}
                />
                <Suggestions results={this.state.results} />

            </form>
        )
    }

}

export default SearchBar;
