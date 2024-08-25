# Product Management Web Application and API

## Overview

This project is a web application developed using ASP.NET MVC Framework, .NET Web API, and ADO.NET. It allows users to manage products with operations including viewing details, creating new products, editing existing products, and deleting products.

## Architecture

The architecture of the application is designed to separate concerns clearly between the data layer, business logic, and user interface.

### Architecture Diagram

![Architecture Diagram](https://github.com/user-attachments/assets/271db0f9-7a0c-4bbc-8a54-1c61b6311f36) <br>
*Diagram showing the relationship between ASP.NET Framework Web App MVC, .NET Web API, and ADO_EXAMPLE Database.*

### Technical Architecture

![Technical Architecture](https://github.com/user-attachments/assets/7de5412d-41fa-4428-905b-84537569122a)
*Diagram illustrating the technical architecture of the application, including Data Layer, Model, View, and Controller. It also showcases the Web (Frontend), API (Backend), and Main Route Controllers.*

### Component Interaction

![Component Interaction](https://github.com/user-attachments/assets/310ea4d4-fc4f-4ae6-a106-23f9790813e6)
*Diagram showing the interaction between Products (Model), Product_DAL, ProductsWebController, and ProductController. This image details how the ProductController handles frontend operations (Details, Create, Edit, Delete) and how ProductsWebController manages API requests (GET, POST, PUT, DELETE).*

## Features

- **View Product Details**: Retrieve and display detailed information about a product.
- **Create New Product**: Add new products to the database via a form submission.
- **Edit Product**: Update existing product details.
- **Delete Product**: Remove products from the database with a confirmation step.

## API Endpoints

- **GET /api/ProductsWeb**: Retrieves a list of all products.
- **GET /api/ProductsWeb/{id}**: Retrieves a specific product by ID.
- **POST /api/ProductsWeb**: Creates a new product.
- **PUT /api/ProductsWeb/{id}**: Updates an existing product.
- **DELETE /api/ProductsWeb/{id}**: Deletes a product by ID.
