import axios from 'axios';

const BASE_URL: string = import.meta.env.VITE_API_URL as string;

const httpService = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-type": "application/json"
  }
});

export default httpService;
