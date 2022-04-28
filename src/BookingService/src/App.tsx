import './App.css'

import { Routes, Route, BrowserRouter as Router } from 'react-router-dom'

import { NavBar } from 'components/NavBar'
import { Home } from 'components/Home'
import { Login } from 'components/Login'
import { Register } from 'components/Register'

const App = () => {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </Router>
  )
}

export default App
