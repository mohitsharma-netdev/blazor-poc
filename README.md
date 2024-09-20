# Blazor Web Application

## Overview
This project is a Blazor web application with a RESTful API backend. It includes components with data-binding, routing, middleware setup, dependency injections, and service configurations. The backend API is secured with JWT authentication and uses Entity Framework for data access and migrations. Unit tests are written for both the web API and Blazor components.

## Architecture

### Blazor App
- **Components**: Designed with data-binding and routing.
- **Middleware**: Setup for handling requests and responses.
- **Dependency Injection**: Configured for services.

### Web API Development
- **RESTful APIs**: Implemented with Entity Framework for data access and migrations.
- **JWT Authentication**: Secured APIs with JWT tokens.

### Unit Testing
- **Test Cases**: Written for both web API and Blazor components.

## Code Flow Diagram
Below is a block diagram representing the code flow of the application.

```mermaid
graph TD
    A[Blazor App] -->|Data-binding & Routing| B[Components]
    A -->|Middleware Setup| C[Middleware]
    A -->|Dependency Injection| D[Services]
    B --> E[Web API]
    E -->|Entity Framework| F[Database]
    E -->|JWT Authentication| G[Security]
    H[Unit Testing] -->|Test Cases| B
    H -->|Test Cases| E
