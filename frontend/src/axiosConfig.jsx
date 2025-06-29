import axios from "axios";

const instance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  //   withCredentials: true,
});

// const token = localStorage.getItem("token");

// if (token) {
//   instance.defaults.headers.common["Authorization"] = `Bearer ${token}`;
// }

// instance.interceptors.request.use(
//   (config) => {
//     const updatedToken = localStorage.getItem("token");
//     if (updatedToken) {
//       config.headers["Authorization"] = `Bearer ${updatedToken}`;
//     }
//     return config;
//   },
//   (error) => Promise.reject(error)
// );

export default instance;
