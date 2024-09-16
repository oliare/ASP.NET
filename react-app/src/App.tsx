import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Categories from './components/categories/index';
import CategoryCreate from './components/categories/create';


function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<Categories />} />
        <Route path="/create" element={<CategoryCreate />} />
      </Routes>
    </Router>
  );
}

export default App;
