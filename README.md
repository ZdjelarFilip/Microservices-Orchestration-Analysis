# Microservices-Orchestration-Analysis

## Overview

This project is a demonstration of a microservices-based architecture for an online store management system. 
It showcases several backend services responsible for different functionalities such as user authentication, catalog management, cart servicing, and order processing, each utilizing MongoDB for data storage.
The frontend services provide user interfaces for customers to browse and purchase products, and for administrators to manage the catalog.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Microservices](#microservices)
  - [User Authentication Service](#user-authentication-service)
  - [Catalog Management Service](#catalog-management-service)
  - [Cart Management Service](#cart-management-service)
  - [Order Management Service](#order-management-service)
- [Frontend Interfaces](#frontend-interfaces)
- [Technology Stack](#technology-stack)
- [Setup and Deployment](#setup-and-deployment)
- [Screenshots](#screenshots)
- [License](#license)

## Architecture

The application follows a microservices architecture deployed using **Kubernetes**, **OpenShift**, and **Docker Swarm**. Each microservice operates within its own container and communicates with others via REST APIs over HTTPS. The architecture diagram below illustrates the services and their respective databases:

<img src="https://i.imgur.com/cc8Mzfg.png" alt="Alt Text" width="750"/>

## Microservices

### User Authentication Service

This service handles user registration, login, and token validation for user sessions. It ensures secure access to the application by validating user credentials and maintaining user session states.

### Catalog Management Service

Responsible for managing the product catalog. It provides functionalities to retrieve product information, update inventory, and manage product listings. Administrators can use this service to add new products to the store.

### Cart Management Service

Handles the operations related to the shopping cart. Users can add or remove items from their cart, and update the quantity of products. This service maintains the state of the cart throughout the shopping session.

### Order Management Service

Processes orders placed by users. It simulates payment processing, validates order details, and updates the order status. The service also integrates with external payment gateways for real-world transactions.

## Frontend Interfaces

The project includes two main frontend interfaces:

- **Customer UI**: Allows customers to browse the product catalog, add items to their cart, and place orders.


<img src="user-interface screenshots/AdminUI/1.png" alt="1" width="600"/>
<img src="user-interface screenshots/AdminUI/2.png" alt="2" width="600"/>
<img src="user-interface screenshots/AdminUI/3.png" alt="3" width="600"/>

- **Admin UI**: Enables administrators to manage the product catalog by adding, updating, or removing products.

<img src="user-interface screenshots/CustomerUI/1.png" alt="1" width="600"/>
<img src="user-interface screenshots/CustomerUI/2.png" alt="1" width="600"/>
<img src="user-interface screenshots/CustomerUI/3.png" alt="1" width="600"/>
<img src="user-interface screenshots/CustomerUI/4.png" alt="1" width="600"/>
<img src="user-interface screenshots/CustomerUI/5.png" alt="1" width="600"/>

## Technology Stack

- **Backend**: C#, .NET 8, MongoDB
- **Frontend**: JavaScript, HTML, CSS
- **Containerization**: Docker Swarm, Kubernetes, OpenShift

## Setup and Deployment

To deploy the microservices, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/ZdjelarFilip/mag-microservices.git
   cd mag-microservices

2. **Build and run Docker containers**: Ensure you have Docker installed and running on your machine. Use the following command to build and run the Docker containers:  
   ```bash
   docker-compose up --build

4. **Run MongoDB image**: If you don't have MongoDB image running, use the following commands:
   ```bash
   docker pull mongo:latest
   docker run -d --name mongodb -p 27017:27017 mongo:latest

6. **Run the project**: If you prefer deploying on Kubernetes, apply the deployment configurations available in the deployments/kubernetes directory:
   ```bash
   kubectl apply -f deployments/kubernetes/
