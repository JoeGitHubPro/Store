<h1 align="center">Welcome to Store 👋</h1>
<p>
  <p>
  <img alt="Version" src="https://img.shields.io/badge/version-1.0.0-blue.svg?cacheSeconds=2592000" />
  <img src="https://img.shields.io/badge/SQL%20Server-2019-yellow" />
  <img src="https://img.shields.io/badge/ASP.Net.Core-7.0-%23790c91" />
  <a href="https://github.com/JoeGitHubPro/Store/blob/master/API.xlsx" target="_blank">
    <img alt="Documentation" src="https://img.shields.io/badge/documentation-yes-brightgreen.svg" />
  </a>
  <a href="https://github.com/JoeGitHubPro/Store](https://github.com/JoeGitHubPro/Store)" target="_blank">
    <img alt="Maintenance" src="https://img.shields.io/badge/Maintained%3F-yes-green.svg" />
  </a>
  <a href="https://github.com/JoeGitHubPro/Store" target="_blank">
    <img alt="License:MIT" src="https://img.shields.io/github/license/JoeGitHubPro/Store" />
  </a>
</p>



> This repository contains a Store API project built using .NET Web API Core 7.0. The API includes JWT-based authentication and utilizes the repository pattern and Entity Framework Core's code-first approach. AutoMapper and DTOs are used for presentation requests.

### 🏠 [Homepage](https://github.com/JoeGitHubPro/Store)

## Documentation
> This documentation provides the API endpoints using Postman.

<h1>
<p align="left">
 <a href="https://documenter.getpostman.com/view/17590370/2s9XxwwEMh" target="blank">
  <img src="https://img.shields.io/badge/Decomntation-DC143C?style=for-the-badge&logo=medium&logoColor=white" alt="alsiam" />
 </a>
</p>
</h1>


## Prerequisites

Before running the API project, ensure you have the following software installed:

- .NET 7.0 SDK
- Visual Studio or Visual Studio Code (optional)
- SQL Server
  
## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in your preferred development environment (such as Visual Studio or Visual Studio Code).
3. Build the project to restore NuGet packages and compile the source code.
   
## Configuration

The API project requires some configuration settings to run properly. Open the `appsettings.json` file and update the following settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
  },
  "JwtSettings": {
    "SecretKey": "YourSecretKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience"
  }
}
```

- `DefaultConnection`: Replace this with your own database connection string.
- `SecretKey`: Replace this with a secure secret key for JWT token generation.
- `Issuer`: Replace this with the issuer name for JWT tokens.
- `Audience`: Replace this with the audience for JWT tokens.
  
## Deploy DataBase

```sh
After edit appSettings to target database , database created automatically such project run 
```


## Repository Pattern

The project follows the repository pattern to separate data access logic from the API controllers. The `Repositories` folder contains repository classes for each entity, responsible for querying and modifying the data.

## AutoMapper and DTOs

AutoMapper and DTOs (Data Transfer Objects) are used for mapping between the entities and the API presentation layer. The `AutoMapperProfiles.cs` file contains the mapping configurations for different entities and DTOs.
## Author

👤 **Youssef Mohamed Ali**

* Website: https://joegithubpro.github.io/Profile/
* Twitter: [@https:\/\/twitter.com\/Y\_mohamed\_Ali?t=uW04TUW-iDrdq0u9GFRm9g&s=09](https://twitter.com/https:\/\/twitter.com\/Y\_mohamed\_Ali?t=uW04TUW-iDrdq0u9GFRm9g&s=09)
* Github: [@JoeGitHubPro](https://github.com/JoeGitHubPro)
* LinkedIn: [@https:\/\/www.linkedin.com\/in\/youssef-mohamed-71a368217](https://linkedin.com/in/https:\/\/www.linkedin.com\/in\/youssef-mohamed-71a368217)

## 🤝 Contributing

Contributions, issues and feature requests are welcome!

If you would like to contribute to this project, please follow these steps:

1. Fork the repository.
1. Create a new branch for your feature or bug fix.
1. Make your changes and commit them.
1. Push your changes to your forked repository.
1. Submit a pull request to the main repository.

## Show your support

Give a ⭐️ if this project helped you!

## 📝 License

Copyright © 2023 [Youssef Mohamed Ali](https://github.com/JoeGitHubPro).<br />
This project is [MIT](MIT) licensed.



