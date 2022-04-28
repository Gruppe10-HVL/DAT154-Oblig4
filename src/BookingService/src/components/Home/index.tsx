import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import DateRangePicker from '@wojtekmaj/react-daterange-picker'
import axios from 'axios'
import dayjs from 'dayjs'
import isBetween from 'dayjs/plugin/isBetween'
dayjs.extend(isBetween)

import User from 'interfaces/user.interface'
import Room from 'interfaces/room.interface'
import Booking from 'interfaces/booking.interface'

export const Home = () => {
  //* External Data
  const [rooms, setRooms] = useState<Room[]>([])
  const [bookings, setBookings] = useState<Booking[]>([])
  const [availableRooms, setAvailableRooms] = useState<Room[]>([])

  //* User Choices
  const [dates, onChangeDates] = useState([new Date(), new Date()])
  const [bedCount, setBedCount] = useState(1)
  const [quality, setQuality] = useState(0)

  //* User Logged In
  const [user, setUser] = useState<User>()

  useEffect(() => {
    const storageUser = localStorage.getItem('user')
    if (storageUser) {
      setUser(JSON.parse(storageUser))

      const fetchRooms = async () => {
        await axios
          .get('https://localhost:5001/api/v1/room')
          .then(res => setRooms(res.data))
          .catch(err => console.log(err.message))
      }

      const fetchBookings = async () => {
        await axios
          .get('https://localhost:5001/api/v1/booking')
          .then(res => setBookings(res.data))
          .catch(err => console.log(err.message))
      }

      fetchRooms()
      fetchBookings()
    }
  }, [])

  const handleRoomSearch = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    console.log(dates)
    console.log(bedCount)
    console.log(quality)

    console.log(rooms)
    console.log(bookings)

    const filteredRooms = rooms
      .filter(room => room.bedCount === bedCount && room.quality === quality)
      .filter(room => {
        const relatedBooking = bookings.filter(booking => booking.roomId === room.id)

        const fromDate = dates[0]
        const toDate = dates[1]
        return relatedBooking.some(
          booking =>
            dayjs(fromDate).isSame(booking.bookingStart) ||
            dayjs(fromDate).isBetween(booking.bookingStart, booking.bookingEnd) ||
            dayjs(toDate).isSame(booking.bookingEnd) ||
            dayjs(toDate).isBetween(booking.bookingStart, booking.bookingEnd),
        )
      })

    setAvailableRooms(filteredRooms)
  }

  const handleBooking = async (roomId: number) => {
    const form = {
      customerId: user?.id,
      roomId,
      startDate: dates[0],
      endDate: dates[1],
    }

    await axios
      .post('https://localhost:5001/api/v1/booking', form)
      .then(res => {
        if (res.data?.roomId) {
          setAvailableRooms([...availableRooms.filter(room => room.id !== res.data.roomId)])
          setBookings([...bookings, res.data])
          alert('Successfully booked room')
        }
      })
      .catch(err => alert(`An error occured: ${err.message}`))
  }

  return (
    <div className="container mt-5">
      <div className="text-center">
        {!user ? (
          <>
            <h1 className="h3 mb-4">Booking Service</h1>
            <p>
              Please <Link to="/login">login</Link> or <Link to="/register">register</Link> to use
              the application
            </p>
          </>
        ) : (
          <div>
            <h1 className="h3 mb-4">Book a room</h1>

            <form className="mb-3" onSubmit={handleRoomSearch}>
              <DateRangePicker
                value={dates}
                onChange={onChangeDates}
                className="bg-light text-black-50 mb-4"
              />
              <div className="row mb-3">
                <div className="col mb-3">
                  <label htmlFor="beds" className="form-label">
                    How many beds?
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="beds"
                    value={bedCount}
                    onChange={e => setBedCount(parseInt(e.target.value))}
                  />
                </div>
                <div className="col mb-3">
                  <label htmlFor="quality" className="form-label">
                    Quality
                  </label>
                  <select
                    id="quality"
                    className="form-select"
                    value={quality}
                    onChange={e => setQuality(parseInt(e.target.value))}
                  >
                    <option value="0">3*</option>
                    <option value="1">4*</option>
                    <option value="2">5*</option>
                  </select>
                </div>
              </div>
              <button type="submit" className="btn btn-primary mb-2">
                Search
              </button>
            </form>
            <div className="row">
              {availableRooms.map(room => {
                return (
                  <div key={room.id} className="card bg-dark w-50">
                    <div className="card-body">
                      <h5 className="card-title">Room</h5>
                      <h6 className="card-subtitle mb-2 text-muted">
                        {room.quality + 3} star hotel
                      </h6>
                      <p>
                        {room.bedCount} bed(s) - {room.size} m²
                      </p>
                      <button className="btn btn-primary" onClick={() => handleBooking(room.id)}>
                        Book
                      </button>
                    </div>
                  </div>
                )
              })}
            </div>
          </div>
        )}
      </div>
    </div>
  )
}
