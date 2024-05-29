# Project Description

This project is a simple REST API with two endpoints: one for retrieving a list of users and another for creating new users. The project is designed to demonstrate handling user roles and authorization using the Open Policy Agent (OPA).

## Endpoints

### 1. GET `/api/users`

- **Description**: Retrieves a list of users.
- **Authorization**: Accessible by any authenticated user.
- **Response**: A JSON array containing user objects with `name`, `email` and `role`  attributes.
  ```json
  [
    {
      "name": "John Doe",
      "email": "john.doe@example.com",
      "role": "admin"
    },
    {
      "name": "Jane Smith",
      "email": "jane.smith@example.com",
      "role": "user"
    }
  ]

### 2. POST `/api/users`

- **Description**: Creates a new user.
- **Authorization**: Restricted to users with the "admin" role.
- **Request Body**: A JSON object with `name`, `email` and `role` attributes.
  ```json
  [
    {
      "name": "John Doe",
      "email": "john.doe@example.com",
      "role": "admin"
    },
    {
      "name": "Jane Smith",
      "email": "jane.smith@example.com",
      "role": "user"
    }
  ]

## Authorization and Security

- **Authorization Service**: All authorization decisions are handled by the Open Policy Agent (OPA) service.
- **Roles**:
  - **Admin**: Can create new users.
  - **Authenticated User**: Can read the list of users.
  - **Unauthorized Users**: No access.

## Deployment

The API service and OPA run within a Kubernetes cluster (e.g., Minikube). Each request's authorization is validated and logged for security and auditing purposes.

## Project Structure

- `OpaAuth/`: Contains the source code for the API.
- `k8s/`: Contains Kubernetes deployment and service configuration files.
- `opa/`: Contains OPA policies and configuration files.

## Directory Structure

OPA/
├── OpaAuth/
│ ├── Contracts/
│ │ ├── IUserRepository.cs
│ ├── Controllers/
│ │ ├── TokenController.cs
│ │ ├── UsersController.cs
│ ├── DataAccess/
│ | ├── Repositories/
│ │ | ├── UsersRepository.cs
│ ├── MiddleWares/
│ │ ├── CustomLoggingMiddleware.cs
│ │ ├── GlobalExceptionHandler.cs
│ │ ├── OpaAuthorizationMiddleware.cs
│ ├── Models/
│ │ ├── OpaResponse.cs
│ │ ├── User.cs
│ ├── OpaAuth.csproj
│ ├── Program.cs
│ ├── Startup.cs
│ ├── appsettings.json
│ ├── appsettings.Development.json
│ └── Dockerfile
├── k8s/
│ ├── opa-deployment.yml
│ └── opaauth-deployment.yml
├── opa/
│ ├── policies/
│ │ ├── Policy.rego
├── .gitignore
├── OpaAuth.sln
└── README.md