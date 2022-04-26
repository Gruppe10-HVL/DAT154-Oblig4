import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

interface User {
  name: string
  jwt: string
}

export const Home = () => {
  const [user, setUser] = useState<User>()

  useEffect(() => {
    const storageUser = localStorage.getItem('user')
    if (storageUser) setUser(JSON.parse(storageUser))
    console.log(storageUser)
    console.log(user)
  }, [])

  return (
    <div className="container mt-5">
      <div className="text-center">
        {!user ? (
          <>
            <h1 className="h3">Booking Service</h1>
            <p className="mt-4">
              Please <Link to="/login">login</Link> or <Link to="/register">register</Link> to use
              the application
            </p>
          </>
        ) : (
          <p>Welcome</p>
        )}
      </div>
    </div>
  )
}
