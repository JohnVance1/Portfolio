import React from 'react';

class Counter extends React.Component {
    state = {
        count: 0
    };

    AddOne = () => {
        this.setState({count: this.state.count + 1})
    }

    MinusOne = () => {
        this.setState({count: this.state.count - 1})
    }

    render(){
        return(
            <React.Fragment>
                <h2>{this.state.count}</h2>
                <button onClick={this.AddOne}>+1</button>
                <button onClick={this.MinusOne}>-1</button>
            
            </React.Fragment>
        
        )
    }

}

export default Counter;