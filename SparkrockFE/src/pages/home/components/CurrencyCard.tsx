
import { Card, CardContent, Box, Typography } from '@mui/material';
import { FunctionComponent } from 'preact';
import { CurrencyInfo } from '../../../shared/models';
import CurrencyValue from './CurrencyValue';

type CurrencyCardProps = {
    value: number;
} & CurrencyInfo;

const CurrencyCard: FunctionComponent<CurrencyCardProps> = ({ code, name, symbol, value }) => {

    return (
        <Card sx={{ width: 170 }}>
            <CardContent>
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: 1, minHeight: "7.21rem" }}>
                    {/* Currency Code */}
                    <Typography variant="h6" component="div" fontWeight="bold" align='center' >
                        {code} ({symbol})
                    </Typography>

                    {/* Currency Name */}
                    <Typography variant="body1" color="text.secondary" align='center' >
                        {name}
                    </Typography>
                    <CurrencyValue value={value} code={code} />
                </Box>
            </CardContent>
        </Card>
    );
};

export default CurrencyCard;
