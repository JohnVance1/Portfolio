import React from 'react';
import PropTypes from 'prop-types';

export class TodoItem extends React.Component{
    getStyle = () => {
        return{
            background: '#f4f4f4',
            padding: '10px',
            borderBottom: '1px #ccc dotted',
            textDecoration: this.props.todo.completed ? 'line-through' : 'none'
        }
    }



    render(){
        const{ id, title } = this.props.todo;
        return(
            // <div style={this.getStyle()}>
            <div>
                <p>
                    <input type="checkbox" onChange={this.props.markComplete.bind(this, id)} /> {' '}
                { title }
                <button onClick={this.props.delTodo.bind(this, id)} /*style={btnStyle}*/>x</button>
                </p>
            </div>
        )
    }
}

// PropTypes
TodoItem.propTypes = {
     todo: PropTypes.object.isRequired
}



export default TodoItem
