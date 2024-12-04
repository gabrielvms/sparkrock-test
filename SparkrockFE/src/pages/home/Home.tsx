import { useSignal } from "@preact/signals";
import { useCallback, useEffect } from "preact/hooks";
import { CurrencyExchangeRate } from "../../shared/models";
import { fetchLatestRates } from "../../shared/services";
import { FunctionComponent } from "preact";
import { CurrencyCard } from "./components";
import { CURRENCIES_INFOS, DEFAULT_RATES } from "../../shared/constants";
import { Box } from "@mui/material";
import { useGlobalCurrency } from "../../shared/providers";

const Home: FunctionComponent = () => {
    const { currency, setLatestRates } = useGlobalCurrency();
    const rates = useSignal<CurrencyExchangeRate>({
        base: "",
        rates: {},
        date: new Date()
    });

    const fetchData = useCallback(async () => {
        fetchLatestRates(currency)
            .then(res => {
                rates.value = res as CurrencyExchangeRate;
            })
            .catch(err => console.error("Failed to fetch data: ", err));
    }, [currency]);

    useEffect(() => {

        fetchData();
        setLatestRates(DEFAULT_RATES);

        const interval = setInterval(() => {
            setLatestRates(rates.value.rates);
            fetchData();
        }, 10000); // Refresh every 10 seconds

        return () => clearInterval(interval); // Cleanup interval on component unmount
    }, [currency]);

    return (
        <>
            {rates.value.base == "" && <div>Loading...</div>}
            {rates.value.base != "" && <Box
                sx={{
                    display: 'flex',
                    flexWrap: 'wrap',
                    gap: 2,
                    justifyContent: 'center',
                    padding: 2,
                }}
            >
                {CURRENCIES_INFOS.map((currencyInfo) => (
                    <CurrencyCard key={currencyInfo.code} {...currencyInfo} value={rates.value!.rates[currencyInfo.code]} />
                ))}
            </Box>}
        </>
    )
}

export default Home;