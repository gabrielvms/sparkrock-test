import axios from "axios"
import { CurrencyExchangeRate } from "../models/currencyExchangeRate";

const BASE_URL = "http://localhost:5218/api/currencyExchange";

export const fetchLatestRates = async (currency: string) => {
    return axios
        .get<CurrencyExchangeRate>(`${BASE_URL}/latest`, {
            params: {
                baseCurrency: currency
            }
        })
        .then(res => res.data);
}