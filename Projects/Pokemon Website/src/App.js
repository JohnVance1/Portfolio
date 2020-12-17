import React from 'react';
import {BrowserRouter as Router, Route} from 'react-router-dom';
import Header from './components/layout/Header'
import Todos from './components/Todos';
import AddTodo from './components/AddTodo';
import Counter from './components/pages/Counter';
import DexTracker from './components/pages/DexTracker';
import Dex from './components/pages/Dex';
import Dex2 from './components/pages/Dex2';
import Pokemon from './databases/Pokemon';

import uuid from 'uuid';

import './App.css';

class App extends React.Component {
  state = {
    todos: [
      {
        id: uuid.v4(),
        title: 'Take out the trash',
        completed: false
      },
      {
        id: uuid.v4(),
        title: 'Dinner at home',
        completed: false
      },
      {
        id: uuid.v4(),
        title: 'Meeting with boss',
        completed: false
      }
    ]
  }

  // Toggle Completed
  markComplete = (id) => {
    this.setState({todos: this.state.todos.map(todo => {
      if(todo.id === id)
      {
        todo.completed = !todo.completed;
      }
      return todo;
    })});
  }

  // Delete Todo
  delTodo = (id) => {
    this.setState({ todos: [...this.state.todos.filter(todo => todo.id !== id)] });
  }

  // Adds Todo
  addTodo = (title) =>{
    const newTodo = {
      id: uuid.v4(),
      title: title,
      completed: false
    }

    this.setState({todos: [...this.state.todos, newTodo]})

  }

  render(){
    return (
      <Router>
        <div className="App">
          <div className="container">
            <Header />
            <Route exact path="/" render={props => (
              <React.Fragment>
                <AddTodo addTodo={this.addTodo} />
                <Todos todos={this.state.todos} markComplete={this.markComplete}
                delTodo={this.delTodo} />
              </React.Fragment>
            )} />
            <Route exact path="/dexTracker" component={DexTracker} />
            <Route path="/counter" component={Counter} />    
            <Route path="/dex" component={Dex} />  
            <Route path="/dex2" component={Dex2} />  
            <Route path="/info" component={Pokemon} />  
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
