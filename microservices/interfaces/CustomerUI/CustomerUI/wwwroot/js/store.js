import { get, post, buildURL } from './utils.js';
import settings from "/script/settings.js";
import UserSession from './userSession.js';

class Store {
    constructor() {
        this.cartManagementUrl = settings.CartManagementUrl;
        this.catalogManagementUrl = settings.CatalogManagementUrl;
        this.userAuthenticationUrl = settings.UserAuthenticationUrl;
        this.setupEventListeners();
        this.initialize();
    }

    setupEventListeners() {
        document.getElementById('logout').addEventListener('click', () => this.logoutUser());
    }

    async initialize() {
        const isAuthenticated = await UserSession.checkAuthentication();
        if (isAuthenticated) {
            this.loadCatalog();
        } else {
            alert('Authentication failed', error.message);
            window.location.href = '/index.html';
        }
    }

    async loadCatalog() {
        const url = buildURL(`${this.catalogManagementUrl}CatalogManagement`, {});
        try {
            const catalogItems = await get(url, UserSession.getSessionToken());
            this.renderCatalog(catalogItems);
        } catch (error) {
            alert('Error while retrieving catalog.');
        }
    }

    renderCatalog(catalogItems) {
        const catalogContainer = document.getElementById('catalogContainer');
        catalogContainer.innerHTML = '';
        catalogItems.forEach(item => {
            const itemRow = this.createCatalogRow(item);
            catalogContainer.appendChild(itemRow);
        });
        document.getElementById('mainContent').classList.remove('hidden');
    }

    createCatalogRow(item) {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${item.name}</td>
            <td>${item.description}</td>
            <td>${item.price} EUR</td>
            <td>
                <button class="btn btn-success btn-sm add-to-cart-btn">Add to Cart</button>
            </td>`;

        row.querySelector('.add-to-cart-btn').addEventListener('click', () => this.addToCart(item.id, item.name, item.price));
        return row;
    }

    async addToCart(catalogItemId, itemName, itemPrice) {
        const newItem = { catalogItemId: catalogItemId, name: itemName, price: itemPrice, quantity: 1 };
        const url = buildURL(`${this.cartManagementUrl}CartManagement`, { userId: UserSession.getUserId() });
        try {
            await post(url, newItem, UserSession.getSessionToken());
            alert('Item added to cart.');
        } catch (error) {
            alert('Failed to add item to cart: ' + error.message);
        }
    }

    logoutUser() {
        localStorage.removeItem('auth');
        window.location.href = '/index.html';
    }
}

document.addEventListener('DOMContentLoaded', () => new Store());