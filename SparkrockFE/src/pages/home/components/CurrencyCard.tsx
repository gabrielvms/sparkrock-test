
import { Card, CardContent, Box, Typography } from '@mui/material';
import { FunctionComponent } from 'preact';
import { CurrencyInfo } from '../../../shared/models';


const CurrencyCard: FunctionComponent<CurrencyInfo> = ({ code, name, symbol }) => {

    const getColor = (change: number) => {
        return change > 0 ? 'green' : change < 0 ? 'red' : 'gray';
    };

    return (
        <Card sx={{ width: 170 }}>
            <CardContent>
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: 1 }}>
                    {/* Currency Code */}
                    <Typography variant="h6" component="div" fontWeight="bold" align='center' >
                        {code} ({symbol})
                    </Typography>

                    {/* Currency Name */}
                    <Typography variant="body1" color="text.secondary" align='center' >
                        {name}
                    </Typography>
                </Box>
            </CardContent>
        </Card>
    );
};

export default CurrencyCard;
