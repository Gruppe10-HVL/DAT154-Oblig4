import axios from 'axios'

export const api = axios.create({
  baseURL: `${process.env.REACT_APP_STATISTICS_API_URL}/api/v1/`,
})

api.interceptors.request.use(req => {
  const storageUser = localStorage.getItem('user')
  if (storageUser) {
    const accessToken = JSON.parse(storageUser).jwt
    if (req.headers) req.headers.Authorization = `Bearer ${accessToken}`
  }

  return req
})
