import { useEffect, useState } from 'react'
import axios from 'axios'

import User from 'interfaces/user.interface'
import Booking from 'interfaces/booking.interface'

export const Bookings = () => {
  //* External Data
  const [bookings, setBookings] = useState<Booking[]>([])

  //* User Logged In
  const [user, setUser] = useState<User>()

  useEffect(() => {
    const storageUser = localStorage.getItem('user')
    if (storageUser) {
      setUser(JSON.parse(storageUser))

      console.log(JSON.parse(storageUser).jwt)

      const fetchBookings = async () => {
        await axios
          .get('https://localhost:5001/api/v1/booking/customer', {
            headers: {
              Authorization: JSON.parse(storageUser).jwt,
            },
          })
          .then(res => console.log(res.data))
          .catch(err => console.log(err.message))
      }

      fetchBookings()
    }
  }, [])

  return (
    <div className="container mt-5">
      <div className="text-center">
        <h1 className="h3 mb-4">My Bookings</h1>
      </div>
    </div>
  )
}
