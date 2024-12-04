export interface CurrencyExchangeRate {
    date: Date;
    base: string;
    rates: RatesDictionary
}

interface RatesDictionary {
    [key: string]: number
}