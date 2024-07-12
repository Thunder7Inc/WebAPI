# ATM Management System

## Backend

This repository contains the backend implementation of a WebAPI for managing transactions and accounts.

## Overview

The WebAPI provides endpoints to manage bank accounts and transactions. It supports operations such as creating accounts, depositing money, withdrawing money, and fetching transaction histories.

## Live Demo

[Access Swagger UI](https://thunderapi.azurewebsites.net/swagger/index.html)

## Features

- **Account Management**: CRUD operations for bank accounts.
- **Transactions**: Deposit and withdrawal operations.
- **Validation**: Validates input data for account and transaction operations.
- **Logging**: Logs important events and errors for debugging and auditing.

## Technologies Used

- **Backend**: C#, .NET Web API
- **Database**: MS Sql Server, Entity Framework (EF) for data access
- **Authentication**: Basic authentication
- **Logging**: Serilog for logging events

## Getting Started

To run this WebAPI locally:

1. Clone this repository.
2. Set up your database connection string in `appsettings.json`.
3. Build and run the application using Visual Studio or `dotnet run`.

Make sure you have .NET SDK installed on your machine.

## API Endpoints

- **Retrieve Account**: Retrieve details of a specific account.
- **Create Account**: Create a new account with a specified name and PIN.
- **Create Transaction**: Create transactions such as deposits and withdrawals.
- **Retrieve Transactions**: Retrieve transaction details for an account.

Refer to the Swagger UI for detailed information on each endpoint.

## Contributors

- [HarshaVardhan](https://github.com/Thunder7Inc)
- [Kousik Raj](https://github.com/RajKousik)
- [Mridula]()
- [Mani Bharathi]()
- [Raghavendiran]()

## Contributing

Contributions are welcome! Feel free to fork this repository, make changes, and submit a pull request. Please follow the existing code style and conventions.

