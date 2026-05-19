```markdown
# Cloud Native Inventory API

This project is a containerized microservice built using .NET 9 for managing a product inventory. The application is deployed on Azure with a strong focus on security, automation, and cloud-native best practices.

## Azure Services Used
* **Azure Container Apps:** Used to host and run the API in a serverless, scalable, and isolated environment.
* **Azure Container Registry (ACR):** Stores and manages the application's Docker images.
* **Azure Key Vault:** Securely manages secrets and sensitive configuration data centrally.
* **Managed Identity:** Enables passwordless and secure authentication between the Azure Container App and Azure Key Vault.

---

## Running the API Locally
To run the application locally without risking checking in secrets, a local **In-Memory database** is utilized.

1. Clone the repository:
   ```bash
   git clone <YOUR-REPO-URL>

```

2. Navigate to the project directory:
```bash
cd CloudNativeInventory.Api

```


3. Run the application:
```bash
dotnet run

```



*The application automatically seeds initial test data into the memory database upon startup.*

---

## CI/CD Pipeline (GitHub Actions)

The automated workflow is configured in `.github/workflows/main.yml` and handles automated testing and verification.

* **Trigger:** The pipeline is automatically triggered on every `push` or `pull_request` to the `master` branch.
* **Workflow Steps:**
1. **Checkout Code:** Fetches the latest source code from GitHub.
2. **Setup .NET:** Configures the .NET 9 environment.
3. **Restore & Build:** Restores NuGet packages (including EntityFrameworkCore) and builds the project to ensure no compilation errors.
4. **Run Tests:** Automatically executes all unit tests in the `CloudNativeInventory.Tests` project to verify code health before any potential deployment.



---

## Deployment and Verification

### Deployment

Deployment is performed in Azure by building a new Docker image from the application's `Dockerfile`, pushing it to Azure Container Registry (ACR), and updating the Azure Container App.

### Verification (Integration Test)

To verify that the application is correctly deployed and successfully interacting with its dependencies (such as Azure Key Vault via Managed Identity), hit the following endpoint:

* **Endpoint:** `https://<your-container-app-url>/system/verify-integration`
* **Expected Response:** An HTTP 200 OK status code confirming that all integrations are functioning flawlessly.

---

## ADR (Architecture Decision Record)

### Title: Choice of Infrastructure and Security Architecture for Inventory API

#### Context

The application required a modern, secure, and automated cloud environment that minimizes the use of hardcoded connection strings or passwords, while offering effortless horizontal scaling.

#### Decisions & Justifications

1. **Azure Container Apps over AKS or VMs:** We chose Azure Container Apps because it delivers the full benefits of containers (Docker) and Kubernetes-style scaling (KEDA) without the operational overhead. This severely reduces infrastructure management and keeps costs to a minimum.
2. **Azure Key Vault & Managed Identity:** For strict security compliance, all application secrets are stored in Key Vault. Instead of injecting credentials into the source code, a **System-assigned Managed Identity** was enabled on the Container App, which was granted the `Key Vault Secrets User` role via Azure RBAC. This guarantees completely passwordless authentication.
3. **In-Memory Database for Local Development:** To simplify the developer onboarding experience, an Entity Framework Core In-Memory provider is used. This allows developers to run and debug the project instantly without spinning up local database engines or external dependencies.

```

---
