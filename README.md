
# eBazaar E-Commerce Platform

A full-stack e-commerce application built with .NET 8.0, featuring product management, shopping cart, and dynamic discounts.

## Tech Stack

- **Frontend**: .NET Core Razor Pages with Tailwind CSS
- **Backend**: ASP.NET Core Web API
- **Database**: MSSQL Server
- **Documentation**: Swagger/OpenAPI
- **Framework**: .NET 8.0

## Features

- Product Management (Add, List, Search)
- Shopping Cart Functionality
- Dynamic Discount System based on Dates
- Pagination
- RESTful API Integration
- Responsive Design

## Project Structure

```
eBazaar/
├── eBazaar.sln                        # Solution file
│
├── src/
│   ├── eBazaar.Web/                   # Razor Frontend Project
│   │   ├── wwwroot/                   # Static files (CSS, JS, images)
│   │   ├── Pages/
│   │   │   ├── Index.cshtml           # Homepage
│   │   │   ├── Products/
│   │   │   │   ├── List.cshtml        # Product listing + search + pagination
│   │   │   │   ├── Detail.cshtml      # Product details page
│   │   │   ├── Cart/
│   │   │   │   ├── Index.cshtml       # Cart view
│   │   ├── Models/                    # Shared models
│   │   ├── Services/                  # API call services
│   │   └── appsettings.json
│   │
│   └── eBazaar.Api/                   # Web API Project
│       ├── Controllers/
│       │   ├── ProductsController.cs
│       │   └── CartController.cs
│       ├── Models/
│       │   ├── Product.cs
│       │   └── Cart.cs
│       ├── Data/
│       │   └── AppDbContext.cs
|       ├── Interfaces/
|       |   ├── IProductRepository.cs
|       |   └── ICartRepository.cs
│       ├── Repositories/
|       |   ├── ProductRepository.cs
|       |   └── CartRepository.cs
│       ├── appsettings.json
│       └── Program.cs      
│
└── README.md
```

## Getting Started

1. Clone the repository
2. Ensure .NET 8.0 SDK is installed
3. Set up the database
4. Run the API project
5. Run the Web project

## Development Setup

```bash
# Restore dependencies
dotnet restore

# Run the API
cd src/eBazaar.API
dotnet run

# Run the Web App
cd src/eBazaar.Web
dotnet run
```

## Database Schemas

### Product
- Id
- Name
- Description
- Price
- DiscountPercentage
- DiscountStartDate
- DiscountEndDate
- ImageUrl
- CreatedAt
- UpdatedAt

### Cart
- Id
- UserId
- ProductId
- Quantity
