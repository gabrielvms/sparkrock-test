import { FunctionComponent } from 'preact';
import { Router, route } from 'preact-router';
import { AppBar, Toolbar, Typography, Button, Box, InputLabel, MenuItem, Select, SelectChangeEvent, FormControl } from '@mui/material';
import Home from '../pages/home/Home';
import Charts from '../pages/charts/Charts';
import NotFoundRedirect from './components/NotFoundRedirect';
import { CURRENCIES_INFOS } from '../shared/constants';
import { CurrencyProvider, useGlobalCurrency } from '../shared/providers';

const App: FunctionComponent = () => {
    return (
        <CurrencyProvider>
            <AppWithCurrencyProvider />
        </CurrencyProvider>
    )
}

const AppWithCurrencyProvider: FunctionComponent = () => {
    const { currency, setCurrency } = useGlobalCurrency();

    const handleChange = ({ target }: SelectChangeEvent) => {
        setCurrency((target as EventTarget & { value: string }).value as string);
    };
    const handleNavigation = (_route: string) => {
        route(_route)
    }
    return (
        <>
            <AppBar position="static" color='transparent'>
                <Toolbar>
                    <Box component="img"
                        src="sparkrock-logo.png"
                        alt="Logo"
                        sx={{ height: 40, marginRight: 2 }} />
                    <Typography
                        variant="h6"
                        noWrap
                        component="a"
                        href="/home"
                        sx={{
                            mr: 7,
                            display: { xs: 'none', md: 'flex' },
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            fontSize: 23,
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        Sparkrock Test
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        <Button
                            key="home"
                            onClick={() => handleNavigation("home")}
                            sx={{ my: 2, color: 'inherit', display: 'block', fontFamily: 'monospace', fontSize: 19 }}
                        >
                            Home
                        </Button>
                        <Button
                            key="charts"
                            onClick={() => handleNavigation("charts")}
                            sx={{ my: 2, color: 'inherit', display: 'block', fontFamily: 'monospace', fontSize: 19 }}
                        >
                            Charts
                        </Button>
                    </Box>
                    <FormControl sx={{ m: 1, minWidth: 150 }}>
                        <InputLabel id="currency-dropdown-label">Currency</InputLabel>
                        <Select
                            labelId="currency-dropdown-label"
                            id="currency-dropdown"
                            value={currency}
                            onChange={handleChange}
                            sx={{ width: 150 }}
                            label="Currency"
                        >
                            {CURRENCIES_INFOS.map((currency) => (
                                <MenuItem value={currency.code}>{`${currency.code} (${currency.symbol})`}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                </Toolbar>
            </AppBar>
            <Router>
                <Home path="/home" />
                <Charts path="/charts" />
                <NotFoundRedirect default />
            </Router>
        </>
    )
};
export default App;
