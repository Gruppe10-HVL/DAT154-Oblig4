import { SyntheticEvent, useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

export const Register = () => {
  const [name, setName] = useState('')
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const navigate = useNavigate()

  const handleRegister = async (event: SyntheticEvent) => {
    event.preventDefault()

    const form = {
      name,
      username,
      password,
    }

    await axios
      .post(`${process.env.REACT_APP_API_BASE_URL}/api/v1/customer`, form)
      .then(res => {
        if (res.data?.jwt) {
          localStorage.setItem(
            'user',
            JSON.stringify({
              name: res.data.customer.name,
              jwt: res.data.jwt,
            }),
          )

          navigate('/')
        }
      })
      .catch(err => setError(err.message))
  }

  return (
    <main className="form-signin">
      <form onSubmit={handleRegister}>
        <h1 className="h3 mb-3 fw-normal">Register</h1>

        {error && <p className="text-danger">An error occured: {error}</p>}

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            id="floatingName"
            onChange={e => setName(e.target.value)}
          />
          <label htmlFor="floatingName">Name</label>
        </div>
        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            id="floatingUsername"
            onChange={e => setUsername(e.target.value)}
          />
          <label htmlFor="floatingUsername">Username</label>
        </div>
        <div className="form-floating">
          <input
            type="password"
            className="form-control"
            id="floatingPassword"
            onChange={e => setPassword(e.target.value)}
          />
          <label htmlFor="floatingPassword">Password</label>
        </div>

        <button className="w-100 btn btn-lg btn-primary" type="submit">
          Register
        </button>
        <p className="mt-2">
          Already have an account? <Link to="/login">Login</Link>
        </p>
      </form>
    </main>
  )
}
