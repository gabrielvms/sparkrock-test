import { signal } from "@preact/signals";
import { useEffect } from "preact/hooks";
import './app.css'
import { CurrencyExchangeRate } from "../shared/models";
import { fetchLatestRates } from "../shared/services";

export function App() {
  const currency = signal("USD");
  const rates = signal<CurrencyExchangeRate | void>();

  useEffect(() => {
    const fetchData = async () => {
      try {
        rates.value = await fetchLatestRates(currency.value);
        console.log(rates.value);
      } catch (error) {
        console.error("Failed to fetch rates:", error);
      }
    };

    fetchData();
    const interval = setInterval(fetchData, 10000); // Refresh every 10 seconds

    return () => clearInterval(interval); // Cleanup interval on component unmount
  }, []);

  return (
    <>
      <div>{currency}</div>
      <div>{rates.value?.base}</div>
    </>
  )
}
