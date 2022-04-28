import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import dayjs from 'dayjs'

import User from 'interfaces/user.interface'
import Booking from 'interfaces/booking.interface'
import Room from 'interfaces/room.interface'
import { api } from 'utils/api'

export const Bookings = () => {
  const navigate = useNavigate()

  //* External Data
  const [bookings, setBookings] = useState<Booking[]>([])
  const [rooms, setRooms] = useState<Room[]>([])

  //* User Logged In
  const [user, setUser] = useState<User>()

  useEffect(() => {
    const storageUser = localStorage.getItem('user')
    if (storageUser) {
      setUser(JSON.parse(storageUser))

      const jwt = JSON.parse(storageUser).jwt
      if (!jwt) {
        alert('Please relog')
        return
      }

      const fetchBookings = async () => {
        await api
          .get('https://localhost:5001/api/v1/booking/customer')
          .then(res => setBookings(res.data))
          .catch(err => console.log(err.message))
      }

      const fetchRooms = async () => {
        await api
          .get('https://localhost:5001/api/v1/room')
          .then(res => setRooms(res.data))
          .catch(err => console.log(err.message))
      }

      fetchBookings()
      fetchRooms()
    } else navigate('/login')
  }, [])

  const getBookingStatus = (status: number): string => {
    switch (status) {
      case 0:
        return 'Not Started'
      case 1:
        return 'Checked In'
      case 2:
        return 'Checked Out'
      case 3:
        return 'Cancelled'

      default:
        return 'Unknown Status'
    }
  }

  return (
    <div className="container mt-5">
      <div className="text-center">
        <h1 className="h3 mb-4">My Bookings</h1>
        {bookings.map(booking => {
          const room = rooms.find(room => room.id === booking.roomId)
          return (
            <div key={booking.id} className="card bg-dark mb-3">
              <div className="card-body">
                <h5 className="card-title">Booking</h5>
                <h6 className="card-subtitle mb-2 text-muted">
                  {dayjs(booking.bookingStart).format('DD/MM/YYYY')} -{' '}
                  {dayjs(booking.bookingEnd).format('DD/MM/YYYY')}
                </h6>
                <p>
                  {room?.bedCount} bed(s) - {room?.size} m²
                </p>
                <b>{getBookingStatus(booking.status)}</b>
              </div>
            </div>
          )
        })}
      </div>
    </div>
  )
}
