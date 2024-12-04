import { FunctionalComponent } from 'preact';
import { route } from 'preact-router';
import { useEffect } from 'preact/hooks';

const NotFoundRedirect: FunctionalComponent = () => {
    useEffect(() => {
        route('/home'); // Redirect to /home
    }, []);

    return "loading"; // Render nothing while redirecting
};

export default NotFoundRedirect;
