import axios from "axios";
import { baseUrl } from "../constants/urlConstants";

const httpModule = axios.create({
    baseURL: baseUrl
})

export default httpModule;