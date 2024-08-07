import { post, redirectTo } from './utils.js';
import settings from "/script/settings.js";

class Register {
    constructor() {
        this.userAuthenticationUrl = settings.UserAuthenticationUrl;
        this.setupEventListeners();
    }

    setupEventListeners() {
        document.getElementById('registerForm').addEventListener('submit', (event) => {
            event.preventDefault();
            this.registerUser();
        });
    }

    async registerUser() {
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const registerUrl = `${this.userAuthenticationUrl}UserAuthentication/register`;

        try {
            await post(registerUrl, { email, password });
            alert('Registration successful. Please log in.');
            redirectTo('/index.html');
        } catch (error) {
            alert('Registration failed: ' + error.message);
        }
    }
}

document.addEventListener('DOMContentLoaded', () => new Register());