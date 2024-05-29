# Project Description

This project is a simple REST API with two endpoints: one for retrieving a list of users and another for creating new users. The project is designed to demonstrate handling user roles and authorization using the Open Policy Agent (OPA).

## Endpoints

### 1. GET `/api/users`

- **Description**: Retrieves a list of users.
- **Authorization**: Accessible by any authenticated user with a valid JWT.
- **Response**: A JSON array containing user objects with `name`, `email` and `role` attributes.
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
- **Authorization**: Restricted to users with the "admin" role, validated by a JWT.
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

- **JWT**: Used for authorizing users.
- **OPA**: Enforces policies to ensure:
  - Only users with the "admin" role can create new users.
  - Only authenticated users can read the list of users.
  - Unauthorized users have no access.

## Project Structure

- `OpaAuth/`: Contains the source code for the API.
- `k8s/`: Contains Kubernetes deployment and service configuration files.
- `opa/`: Contains OPA policies and configuration files.

## Deployment

### Prerequisites

- [Docker](https://www.docker.com/)
- [Kubernetes](https://kubernetes.io/)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Minikube](https://minikube.sigs.k8s.io/docs/)

### Setup Instructions

1. **Clone the repository**:
    ```sh
    git clone https://github.com/sajadmirz4ei/OPA.git
    cd OPA
    ```

2. **Start Minikube**:
    ```sh
    minikube start
    ```

3. **Deploy OPA**:
    ```sh
    kubectl apply -f deployment/kubernetes/opa-deployment.yml
    ```

4. **Deploy the API service**:
    ```sh
    kubectl apply -f deployment/kubernetes/opaauth-deployment.yml
    ```

5. **Access the API and Swagger UI**:
    ```sh
    minikube service opaauth-api-svc
    ```

This command will open the Swagger UI in your default web browser. Use the returned URL to interact with and test the API endpoints.
