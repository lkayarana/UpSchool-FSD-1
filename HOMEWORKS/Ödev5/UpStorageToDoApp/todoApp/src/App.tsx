import { useState } from "react";
import { Button, Dropdown, Grid, Header, Input, List } from "semantic-ui-react";
import './App.css';

interface Todo {
  id: number;
  task: string;
  isCompleted: boolean;
  createdDate: Date;
}

function App() {

  const [todos, setTodos] = useState<Todo[]>([]);
  const [inputText, setInputText] = useState('');
  const [sortDirection, setSortDirection] = useState<'ascending' | 'descending'>('ascending');
  const [currentPage, setCurrentPage] = useState(1);
  const [tasksPerPage] = useState(5);

  const addTodo = (): void => {
    if (inputText.trim() === '') return;
    const newTodo: Todo = {
      id: Date.now(),
      task: inputText.trim(),
      isCompleted: false,
      createdDate: new Date(),
    };
    setTodos([...todos, newTodo]);
    setInputText('');
  };

  const removeTodo = (id: number): void => {
    const updatedTodos = todos.filter(todo => todo.id !== id);
    setTodos(updatedTodos);
  };

  const isButtonDisabled = inputText.trim() === '';

  const handleTaskDoubleClick = (id: number): void => {
    const updatedTodos = todos.map((todo) => {
      if (todo.id === id) {
        return {
          ...todo,
          isCompleted: !todo.isCompleted,
        };
      }
      return todo;
    });
    setTodos(updatedTodos);
  };

  const handleSortChange = (value: string): void => {
    const direction = value === 'ascending' ? 'ascending' : 'descending';
    setSortDirection(direction);
  };

  const indexOfLastTask = currentPage * tasksPerPage;
  const indexOfFirstTask = indexOfLastTask - tasksPerPage;
  const currentTasks = todos.slice(indexOfFirstTask, indexOfLastTask);

  const paginate = (pageNumber: number): void => {
    setCurrentPage(pageNumber);
  };

  todos.sort((a, b) => {
    if (sortDirection === 'ascending') {
      return a.createdDate.getTime() - b.createdDate.getTime();
    } else {
      return b.createdDate.getTime() - a.createdDate.getTime();
    }
  });

  return (
    <div className="centered" style={{ display: "flex", alignItems: "center", justifyContent: "center", minHeight: "100vh" }}>
      <Grid columns={2} stackable>
        <Grid.Row>
          <Grid.Column width={16}>
            <Header size="huge" style={{ textAlign: "center", fontSize: '55px', fontWeight: 'bold' }}>Todos</Header>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={16}>
            <Input
              placeholder="Enter a new todo..."
              value={inputText}
              onChange={(x) => setInputText(x.target.value)}
              fluid
              action={
                <Button color="green" onClick={addTodo} disabled={isButtonDisabled}>
                  Add
                </Button>
              }
            />
            <Grid.Column width={16} textAlign="right">
              <div className="sort-section">
                Sort By Created Date:{' '}
                <Dropdown
                  inline
                  options={[
                    { key: 'ascending', text: 'Ascending', value: 'ascending' },
                    { key: 'descending', text: 'Descending', value: 'descending' },
                  ]}
                  value={sortDirection}
                  onChange={(_, { value }) => handleSortChange(value as string)}
                />
              </div>
            </Grid.Column>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={16}>
            <List divided relaxed>
              {currentTasks.map((todo) => (
                <List.Item
                  key={todo.id}
                  style={{
                    textDecoration: todo.isCompleted ? 'line-through' : 'none',
                    cursor: 'pointer',
                    textWeight: 'bold',
                  }}
                  onDoubleClick={() => handleTaskDoubleClick(todo.id)}
                >
                  <List.Content floated="right">
                    <Button
                      size="tiny"
                      color="red"
                      icon="trash"
                      onClick={() => removeTodo(todo.id)}
                    />
                  </List.Content>
                  <List.Content>
                    {todo.task}
                    <div style={{ fontSize: '0.8rem', color: 'black', fontWeight: 'bold'}}>
                      Created: {todo.createdDate.toLocaleString()}
                    </div>
                  </List.Content>
                </List.Item>
              ))}
            </List>
            <div style={{ textAlign: 'center' }}>
              {Array.from({ length: Math.ceil(todos.length / tasksPerPage) }).map((_, index) => (
                <Button
                  key={index + 1}
                  style={{ margin: '5px' }}
                  secondary={currentPage === index + 1}
                  onClick={() => paginate(index + 1)}
                >
                  {index + 1}
                </Button>
              ))}
            </div>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </div>
  );
}

export default App