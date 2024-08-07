@echo off
setlocal

set "BASE_DIR=D:\Repositories\mag-microservices\mag-microservices"

cd /d "%BASE_DIR%\microservices\interfaces\AdminUI\AdminUI"
docker build -t mag/adminui:latest .

cd /d "%BASE_DIR%\microservices\interfaces\CustomerUI\CustomerUI"
docker build -t mag/customerui:latest .

cd /d "%BASE_DIR%\microservices\services\CartManagement\CartManagementService"
docker build -t mag/cartmanagement:latest .

cd /d "%BASE_DIR%\microservices\services\CatalogManagement\CatalogManagementService"
docker build -t mag/catalogmanagement:latest .

cd /d "%BASE_DIR%\microservices\services\OrderManagement\OrderManagementService"
docker build -t mag/ordermanagement:latest .

cd /d "%BASE_DIR%\microservices\services\UserAuthentication\UserAuthenticationService"
docker build -t mag/userauthentication:latest .

echo Docker images build completed.
