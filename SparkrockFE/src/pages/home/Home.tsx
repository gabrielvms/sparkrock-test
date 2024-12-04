import { useSignal } from "@preact/signals";
import { useEffect } from "preact/hooks";
import { CurrencyExchangeRate } from "../../shared/models";
import { fetchLatestRates } from "../../shared/services";
import { CURRENCIES } from "../../shared/constants";
import { FunctionComponent } from "preact";

const Home: FunctionComponent = () => {
    const currency = useSignal("USD");
    const rates = useSignal<CurrencyExchangeRate | void>();

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
            {CURRENCIES.map((currency) => (<div>{rates.value?.rates[currency]}</div>))}
        </>
    )
}

export default Home;