import { SyntheticEvent, useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

export const Login = () => {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const navigate = useNavigate()

  const handleLogin = async (event: SyntheticEvent) => {
    event.preventDefault()

    if (username !== '' && password !== '') {
      const form = {
        username,
        password,
      }

      await axios
        .post('https://localhost:5001/api/v1/customer/login', form)
        .then(res => {
          if (res.data?.jwt) {
            localStorage.setItem(
              'user',
              JSON.stringify({
                name: res.data.customer.name,
                jwt: res.data.jwt,
              }),
            )

            window.dispatchEvent(new Event('storage'))

            navigate('/')
          }
        })
        .catch(err => setError(err.message))
    } else {
      setError('Fields cannot be empty')
    }
  }

  return (
    <main className="form-signin">
      <form onSubmit={handleLogin}>
        <h1 className="h3 mb-3 fw-normal">Login</h1>

        {error && <p className="text-danger">Incorrect username or password</p>}

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
          Login
        </button>
        <p className="mt-2">
          Don't have an account? <Link to="/register">Register</Link>
        </p>
      </form>
    </main>
  )
}
