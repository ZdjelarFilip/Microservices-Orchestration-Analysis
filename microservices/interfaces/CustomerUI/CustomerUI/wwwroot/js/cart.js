import { get, post, remove, buildURL } from './utils.js';
import settings from "/script/settings.js";
import UserSession from './userSession.js';

class Cart {
    constructor() {
        this.cartManagementUrl = settings.CartManagementUrl;
        this.catalogManagementUrl = settings.CatalogManagementUrl;
        this.orderManagementUrl = settings.OrderManagementUrl;
        this.userAuthenticationUrl = settings.UserAuthenticationUrl;
        this.setupEventListeners();
        this.initialize();
    }

    setupEventListeners() {
        document.getElementById('logout').addEventListener('click', () => this.logoutUser());
        document.getElementById('order').addEventListener('click', () => this.createOrder());
    }

    async initialize() {
        const isAuthenticated = await UserSession.checkAuthentication();
        if (isAuthenticated) {
            this.loadCart();
        } else {
            alert('Authentication failed', error.message);
            window.location.href = '/index.html';
        }
    }

    async loadCart() {
        const url = buildURL(`${this.cartManagementUrl}CartManagement`, { userId: (UserSession.getUserId()) });
        try {
            const cartItems = await get(url, UserSession.getSessionToken());
            this.renderCart(cartItems);
        } catch (error) {
            alert('Error while retrieving cart.');
        }
    }

    renderCart(cartItems) {
        const cartContainer = document.getElementById('cartContainer');
        if (cartContainer) {
            cartContainer.innerHTML = '';
            cartItems.forEach(item => {
                const itemRow = this.createCartRow(item);
                cartContainer.appendChild(itemRow);
            });
        document.getElementById('mainContent').classList.remove('hidden');
        } 
    }

    createCartRow(item) {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${item.catalogItemId}</td>
            <td>${item.name}</td>
            <td>${item.price} EUR</td>
            <td>${item.quantity}</td>
            <td>
                <button class="btn btn-danger btn-sm remove-from-cart-btn">Remove from Cart</button>
            </td>
        `;

        row.querySelector('.remove-from-cart-btn').addEventListener('click', () => this.removeItem(row));
        return row;
    }

    initEventListeners() {
        const rows = document.querySelectorAll(".cart tbody tr");
        rows.forEach(row => {
            const removeButton = row.querySelector(".remove");
            removeButton.addEventListener('click', () => {
                this.removeItem(row);
            });
        });
    }

    async createOrder() {
        const url = `${this.orderManagementUrl}OrderManagement?userId=${UserSession.getUserId()}`;
        try {
            await post(url, "", UserSession.getSessionToken())
            alert('Order created successfully!');
            this.loadCart();
        } catch (error) {
            alert(`Failed to create order: ${error.message}`);
        }
    }

    async removeItem(row) {
        const firstTdElement = row.querySelector("td:first-child");
        if (!firstTdElement) {
            alert('No first <td> element found in the row:', row);
            return;
        }
        const catalogItemId = firstTdElement.textContent.trim();
        const url = `${this.cartManagementUrl}cartManagement?userId=${UserSession.getUserId()}&cartItemId=${catalogItemId}`;
        try {
            await remove(url, UserSession.getSessionToken())
            row.remove();
        } catch (error) {
            alert(`Error while removing item from cart: ${error.message}`);
        }
    }

    logoutUser() {
        localStorage.removeItem('auth');
        window.location.href = '/index.html';
    }
}

document.addEventListener('DOMContentLoaded', () => new Cart());