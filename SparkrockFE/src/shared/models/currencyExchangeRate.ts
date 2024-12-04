export interface CurrencyExchangeRate {
    date: Date;
    base: string;
    rates: { [key: string]: number }
}