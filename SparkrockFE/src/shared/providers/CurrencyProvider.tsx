import { useSignal } from "@preact/signals";
import { createContext, FunctionComponent } from "preact";
import { useContext } from "preact/hooks";
import { DEFAULT_RATES } from "../constants";

interface CurrencyContextProps {
    currency: string;
    setCurrency: (newCurrency: string) => void;
    latestRates: { [key: string]: number };
    setLatestRates: (newLatests: { [key: string]: number }) => void;
}

const CurrencyContext = createContext<CurrencyContextProps>({
    currency: "",
    setCurrency: () => { },
    latestRates: {},
    setLatestRates: () => { }
});

const CurrencyProvider: FunctionComponent = ({ children }) => {
    const currency = useSignal<string>("USD");

    const setCurrency = (newCurrency: string) => {
        currency.value = newCurrency;
    };

    const latestRates = useSignal<{ [key: string]: number }>(DEFAULT_RATES);

    const setLatestRates = (newLatests: { [key: string]: number }) => {
        latestRates.value = newLatests;
    }

    return (
        <CurrencyContext.Provider value={{ currency: currency.value, setCurrency, latestRates: latestRates.value, setLatestRates }}>
            {children}
        </CurrencyContext.Provider>
    );
};

export default CurrencyProvider;


export const useGlobalCurrency = () => {
    return useContext(CurrencyContext);
};
