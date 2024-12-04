import { Box, Typography } from "@mui/material";
import { FunctionalComponent } from "preact";
import { useEffect, useRef, useState } from "preact/hooks";
import { useGlobalCurrency } from "../../../shared/providers";

const CurrencyValue: FunctionalComponent<{ value: number, code: string }> = ({ value, code }) => {
    const currentValue = useRef<number>(value);
    const [change, setChange] = useState<number>(0)
    const { latestRates, currency } = useGlobalCurrency();

    useEffect(() => {
        setChange(0);
    }, [currency])

    useEffect(() => {
        currentValue.current = value;
        if (latestRates[code] > -1)
            setChange(currentValue.current - latestRates[code]);
    }, [value]);

    return (
        <>
            <Box sx={{ display: "flex", flex: 1, alignItems: "flex-end", justifyContent: "center" }}>
                <div>
                    <div>
                        <Typography
                            variant="body1"
                            sx={{
                                color: `${change > 0 ? "green" : change < 0 ? "red" : "black"}`,
                            }}
                        >
                            {currentValue.current.toFixed(10)}
                        </Typography>
                    </div>
                    <div>
                        <Typography
                            variant="body2"
                            sx={{
                                color: `${change > 0 ? "green" : change < 0 ? "red" : "black"}`,
                            }}
                        >
                            {`${change > 0 ? "+" : change < 0 ? "-" : ""} ${Math.abs(change).toFixed(10)}`}
                        </Typography>
                    </div>
                </div>
            </Box>

        </>
    );
}

export default CurrencyValue;