import { get, post, put, remove, buildURL } from './utils.js';
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
        document.getElementById('add').addEventListener('click', () => this.addItem());
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
                <button class="btn btn-info btn-sm update-btn">Update</button>
                <button class="btn btn-danger btn-sm remove-btn">Remove</button>
            </td>
        `;

        row.querySelector('.update-btn').addEventListener('click', () => this.updateItem(item.id));
        row.querySelector('.remove-btn').addEventListener('click', () => this.removeItem(item.id));

        return row;
    }

    async addItem() {
        const name = prompt('Enter item name:');
        const description = prompt('Enter item description:');
        const price = parseFloat(prompt('Enter item price:'));

        if (isNaN(price)) {
            alert('Please enter a valid price.');
            return;
        }

        const newItem = { name, description, price };
        const url = buildURL(`${this.catalogManagementUrl}CatalogManagement`, {});
        try {
            await post(url, newItem, UserSession.getSessionToken());
            this.loadCatalog();
        } catch (error) {
            alert('Failed to add item: ' + error.message);
        }
    }

    async removeItem(itemId) {
        const url = buildURL(`${this.catalogManagementUrl}CatalogManagement/${itemId}`, {});
        try {
            await remove(url, UserSession.getSessionToken());
            this.loadCatalog();
        } catch (error) {
            alert('Failed to remove item: ' + error.message);
        }
    }

    async updateItem(itemId) {
        const name = prompt('Enter new name:');
        const description = prompt('Enter new description:');
        const priceInput = prompt('Enter new price:');

        const price = parseFloat(priceInput);
        if (isNaN(price)) {
            alert('Please enter a valid price.');
            return;
        }

        const item = { id: itemId, name, description, price };
        const url = `${this.catalogManagementUrl}CatalogManagement`;

        try {
            await put(url, item, UserSession.getSessionToken());
            this.loadCatalog();
        } catch (error) {
            alert('Failed to update item: ' + error.message);
        }
    }

    logoutUser() {
        UserSession.clearSession();
    }
}

document.addEventListener('DOMContentLoaded', () => new Store());