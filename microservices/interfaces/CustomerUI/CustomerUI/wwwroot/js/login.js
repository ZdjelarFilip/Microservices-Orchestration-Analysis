import { post } from './utils.js';
import settings from "/script/settings.js";
import UserSession from './userSession.js';

class Login {
    constructor() {
        this.userAuthenticationUrl = settings.UserAuthenticationUrl;
        this.setupEventListeners();
    }

    setupEventListeners() {
        const loginForm = document.getElementById('loginForm');
        loginForm.addEventListener('submit', (event) => {
            event.preventDefault();
            this.loginUser();
        });
    }

    async loginUser() {
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const loginUrl = this.userAuthenticationUrl + 'UserAuthentication/login';

        try {
            const token = await post(loginUrl, { email, password });
            UserSession.setSession(email, token);
            window.location.href = '/store.html';
        } catch (error) {
            alert('Login failed: ' + error.message);
        }
    }
}

document.addEventListener('DOMContentLoaded', () => new Login());