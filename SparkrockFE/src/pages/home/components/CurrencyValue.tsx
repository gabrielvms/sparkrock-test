import { Box } from "@mui/material";
import { FunctionalComponent } from "preact";
import { useEffect, useRef } from "preact/hooks";
import { useGlobalCurrency } from "../../../shared/providers";

const CurrencyValue: FunctionalComponent<{ value: number, code: string }> = ({ value, code }) => {
    const currentValue = useRef<number>(value);
    const change = useRef<number>(0)
    const { latestRates } = useGlobalCurrency();


    useEffect(() => {
        change.current = latestRates[code] > -1 ? currentValue.current - latestRates[code] : 0;
        console.log(code, change.current)
        currentValue.current = value;
    }, [value]);

    return (
        <>
            <Box sx={{ display: "flex", flex: 1, alignItems: "flex-end", justifyContent: "center" }}>
                {currentValue.current.toFixed(8)}
            </Box>
        </>
    );
}

export default CurrencyValue;