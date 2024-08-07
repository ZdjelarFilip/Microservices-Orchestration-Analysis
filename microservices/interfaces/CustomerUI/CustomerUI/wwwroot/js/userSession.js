import { get, buildURL } from './utils.js';
import settings from "/script/settings.js";

class UserSession {
    static getSession() {
        const sessionData = localStorage.getItem('auth');
        return sessionData ? JSON.parse(sessionData) : null;
    }

    static setSession(email, token) {
        localStorage.setItem('auth', JSON.stringify({ email, token }));
    }

    static isAuthenticated() {
        return !!this.getSession();
    }

    static getSessionToken() {
        const session = this.getSession();
        return session ? session.token : null;
    }

    static getUserId() {
        const userId = localStorage.getItem('userId')
        return userId ? userId : null;
    }

    static clearSession() {
        localStorage.removeItem('auth');
        localStorage.removeItem('userId');
        window.location.href = '/index.html';
    }

    static async checkAuthentication() {
        const session = this.getSession();
        if (!session) {
            alert('No active session. Please log in.');
            window.location.href = '/index.html';
            return false;
        }

        const url = buildURL(`${settings.UserAuthenticationUrl}UserAuthentication/validate`, {
            email: session.email,
            token: session.token
        });

        try {
            var userId = await get(url, session.token);
            localStorage.setItem('userId', userId);
            return true;
        } catch (error) {
            alert('Authentication failed: ' + error.message);
            this.clearSession();
            return false;
        }
    }
}

export default UserSession;