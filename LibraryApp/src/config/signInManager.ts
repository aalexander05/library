import axios from 'axios'
import { state } from './signInConfig'

export function signInManager() {


  const login = async (loginRequest: any) => {
    try {
        const result = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/login/`, loginRequest);
        localStorage.setItem("accessToken", result.data.accessToken);
        localStorage.setItem("refreshToken", result.data.refreshToken);
        state.isAuthenticated = true

        const rolesResult = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/Role`,  { headers: {Authorization: `Bearer ${result.data.accessToken}`} });
        state.roles = rolesResult.data;
    } catch (error) {
        console.error('Login error:', error)
    }
  }

  const logout = () => {
        state.isAuthenticated = false
        localStorage.setItem("accessToken", "");
        localStorage.setItem("refreshToken", "");
  }

  const register = async (loginRequest: any) => {
    try {
        const result = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/register/`, loginRequest);
    } catch (error) {
        console.error('Register error:', error)
    }
  }

  const getToken = async (getTokenRequest: any) => {
    
    try {
      const result = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/refresh/`, getTokenRequest);
      return result.data.accessToken

    } catch (error) {
      console.error('Silent token acquisition error:', error)
    }
  }

  const registerAuthorizationHeaderInterceptor = () => {
    axios.interceptors.request.use(async (config) => {
        const accessToken = localStorage.getItem("accessToken");
        if (accessToken) {
            config.headers.Authorization = `Bearer ${accessToken}`
        }
        return config
    })
  }
  return {
    login,
    logout,
    register,
    getToken,
    registerAuthorizationHeaderInterceptor
  }
}
