import axios from "axios"
import { CurrencyExchangeRate } from "../models/currencyExchangeRate";

const BASE_URL = "https://api.fxratesapi.com";

export const fetchLatestRates = async (currency: string) => {
    return axios
        .get<CurrencyExchangeRate>(`${BASE_URL}/latest`, {
            params: {
                base: currency
            }
        })
        .then(res => res.data)
        .catch(err => console.error(err));
}