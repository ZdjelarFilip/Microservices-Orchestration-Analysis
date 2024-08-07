import { get } from './utils.js';
import settings from "/script/settings.js";
import UserSession from './userSession.js';

class Order {
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
    }

    async initialize() {
        const isAuthenticated = await UserSession.checkAuthentication();
        if (isAuthenticated) {
            this.loadOrders();
        } else {
            alert('Authentication failed', error.message);
            window.location.href = '/index.html';
        }
    }

    async loadOrders() {
        const url = `${this.orderManagementUrl}OrderManagement`;
        try {
            const orders = await get(url, UserSession.getSessionToken());
            this.renderOrders(orders);
        } catch (error) {
            alert('Error while retrieving orders.');
        }
    }

    renderOrders(orders) {
        const ordersContainer = document.getElementById('ordersContainer');
        ordersContainer.innerHTML = '';
        if (Array.isArray(orders)) {
            orders.forEach(order => {
                const orderRow = this.createOrderRow(order);
                ordersContainer.appendChild(orderRow);
            });
        }
        document.getElementById('mainContent').classList.remove('hidden');
    }

    createOrderRow(order) {
        const row = document.createElement('tr');
        const itemsDetail = order.items.map(item => `${item.name} x ${item.quantity}`).join(", ");

        row.innerHTML = `
        <td>${order.id}</td>
        <td>${order.customerId}</td>
        <td>${order.totalPrice} EUR</td>
        <td>${new Date(order.processingDate).toLocaleString()}</td>
        <td>${order.status}</td>
        <td>${itemsDetail}</td>`;
        return row;
    }

    logoutUser() {
        UserSession.clearSession();
    }
}

document.addEventListener('DOMContentLoaded', () => new Order());