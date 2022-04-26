import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

import { User } from 'interfaces/user.interface'

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

  return (
    <nav className="navbar navbar-expand-md navbar-light bg-light">
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
              <a className="nav-link active" aria-current="page" href="#">
                Home
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="#">
                Link
              </a>
            </li>
          </ul>
          <form className="d-flex">
            {!user ? (
              <Link to="/login" className="btn btn-outline-primary" type="submit">
                Login
              </Link>
            ) : (
              <p className="mt-3">Logged in as {user.name}</p>
            )}
          </form>
        </div>
      </div>
    </nav>
  )
}
