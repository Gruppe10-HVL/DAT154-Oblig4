import { Routes, Route, BrowserRouter as Router } from 'react-router-dom'

import { NavBar } from 'components/NavBar'
import { Home } from 'components/Home'
import { Bookings } from 'components/Bookings'
import { Login } from 'components/Login'
import { Register } from 'components/Register'

const App = () => {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/bookings" element={<Bookings />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </Router>
  )
}

export default App
