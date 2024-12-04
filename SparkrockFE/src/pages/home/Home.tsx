import { useSignal } from "@preact/signals";
import { useEffect } from "preact/hooks";
import { CurrencyExchangeRate } from "../../shared/models";
import { fetchLatestRates } from "../../shared/services";
import { FunctionComponent } from "preact";
import { CurrencyCard } from "./components";
import { CURRENCIES_INFOS } from "../../shared/constants";
import { Box } from "@mui/material";

const Home: FunctionComponent = () => {
    const currency = useSignal("USD");
    const rates = useSignal<CurrencyExchangeRate | void>();

    useEffect(() => {
        const fetchData = async () => {
            try {
                rates.value = await fetchLatestRates(currency.value);
            } catch (error) {
                console.error("Failed to fetch rates:", error);
            }
        };

        fetchData();
        const interval = setInterval(fetchData, 60000); // Refresh every 10 seconds

        return () => clearInterval(interval); // Cleanup interval on component unmount
    }, []);

    return (
        <>
            <Box
                sx={{
                    display: 'flex',
                    flexWrap: 'wrap',
                    gap: 2,
                    justifyContent: 'center',
                    padding: 2,
                }}
            >
                {CURRENCIES_INFOS.map((currencyInfo) => (
                    <CurrencyCard key={currencyInfo.code} {...currencyInfo} />
                ))}
            </Box>
        </>
    )
}

export default Home;