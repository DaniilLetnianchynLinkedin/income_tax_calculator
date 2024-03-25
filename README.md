# INCOME TAX CALCULATOR

## Prerequisites

Before running the project, ensure you have the following installed:

- **.NET SDK**: The .NET Software Development Kit (SDK) is required for running the backend API. You can download it from the [official .NET download page](https://dotnet.microsoft.com/download).

- **Node.js**: This is necessary to run the Angular CLI and the frontend application. Download Node.js from the [official Node.js website](https://nodejs.org/).

- **Angular CLI**: With Node.js installed, install the Angular CLI globally by running `npm install -g @angular/cli` in your terminal.


## Getting Started

Follow these steps to set up and run the project.

### Step 1: Run the Backend API

From the root folder, execute the following commands to start the backend API:

```bash
cd backend/Host.Application
dotnet dev-certs https --trust
set ASPNETCORE_ENVIRONMENT=Test # remove this if You want to configure database in Step 3
dotnet run 
```

### Step 2: Run the Frontend Application

From the root folder, execute the following commands to start Angular application:

```bash
cd frontend/income-tax-calculator
ng serve
```

This will serve the frontend application on http://localhost:4200 navigate to this address from browser.

### [OPTIONAL] Step 3: Configure Database

By default inmemory database used. To change it modify appsettings.json

```json
"LocalDbConnectionString": "" //Put Your connection string here
```

### [OPTIONAL] Step 4: Run unit tests on angular project

```bash
ng test
```