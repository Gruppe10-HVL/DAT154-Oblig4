import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

import User from 'interfaces/user.interface'
import { NavLink } from 'react-router-dom'

export const NavBar = () => {
  const [user, setUser] = useState<User>()

  useEffect(() => {
    const getUser = () => {
      const storageUser = localStorage.getItem('user')
      if (storageUser) setUser(JSON.parse(storageUser))
    }

    getUser()

    window.addEventListener('storage', getUser)

    return () => window.removeEventListener('storage', getUser)
  }, [])

  const handleLogout = () => localStorage.removeItem('user')

  return (
    <nav className="navbar navbar-expand-md navbar-dark bg-dark">
      <div className="container-fluid">
        <Link to="/" className="navbar-brand">
          Booking Service
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink to="/" className={isActive => 'nav-link' + (isActive ? ' active' : '')}>
                Home
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink
                to="/bookings"
                className={isActive => 'nav-link' + (isActive ? ' active' : '')}
              >
                My bookings
              </NavLink>
            </li>
          </ul>
          <form className="d-flex">
            {!user ? (
              <Link to="/login" className="btn btn-outline-primary" type="submit">
                Login
              </Link>
            ) : (
              <>
                <p className="mt-3">Logged in as {user.name}</p>
                <button className="btn btn-link" onClick={handleLogout}>
                  Log out
                </button>
              </>
            )}
          </form>
        </div>
      </div>
    </nav>
  )
}
