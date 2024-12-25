# 📚 HeyBook - Book Exchange Platform

This README file provides a comprehensive overview of the backend architecture for HeyBook, a book exchange platform. The project aims to create a platform where users can exchange books, communicate seamlessly, and organize meetups effectively.


# Technical Architecture

### Backend Technologies
* .NET 8.0: Latest framework features and performance optimizations
* Entity Framework Core: Advanced ORM with optimized database operations
* LINQ: Advanced data querying and manipulation capabilities
* PostgreSQL: Robust and scalable database solution
* Docker & Containerization: Microservices-ready deployment architecture
* SignalR: Real-time communication implementation
* JWT Authentication: Secure token-based authentication system

### Architecture Patterns
* Domain-Driven Design (DDD): Complex domain logic handling
* Modular Monolithic Architecture: Scalable and maintainable codebase


# Core Features

* Location-based Book Listings: Find books available in your area
* Real-time Messaging: Connect with other users through SignalR
* Smart Search: Advanced search and filtering capabilities
* Secure Authentication: JWT-based user authentication
* Meet-up Planning: Organize book exchange meetings

# Project Structure

```plaintext
📂 src/
├── 📂 Core/                          
│   ├── 📂 DataAccess/                  
│   ├── 📂 Entities/                    
│   ├── 📂 Utilities/                   
│   │   ├── 📂 Results/                   
│   │   ├── 📂 IoC/
│   │   └── 📂 Interceptors/
│   ├── 📂 Aspects/                     
│   ├── 📂 CrossCuttingConcerns/       
│   └── 📂 DependencyResolvers/
│ 
├── 📂 Modules/                       
│   ├── 📂 Books/                       
│   │   ├── 📂 Books.Entity/             
│   │   ├── 📂 Books.DataAccess/         
│   │   └── 📂 Books.Business/  
│   │ 
│   ├── 📂 UserManagement/             
│   │   ├── 📂 Users.Entity/
│   │   ├── 📂 Users.DataAccess/
│   │   └── 📂 Users.Business/
│   │ 
│   ├── 📂 Swap/                       
│   │   ├── 📂 Swap.Entity/
│   │   ├── 📂 Swap.DataAccess/
│   │   └── 📂 Swap.Business/
│   │ 
│   ├── 📂 Messaging/                  
│   │   ├── 📂 Messaging.Entity/
│   │   ├── 📂 Messaging.DataAccess/
│   │   └── 📂 Messaging.Business/
│   │ 
│   └── 📂 Notifications/              
│       ├── 📂 Notifications.Entity/
│       ├── 📂 Notifications.DataAccess/
│       └── 📂 Notifications.Business/
│  
└── 📂 API/                          
│     ├── 📂 Controllers/                
│     ├── 📂 Properties/                               
│     ├── 📂 Extensions/
│     └── 📂 DockerFile/   
│ 
└── 📄 docker-compose.yml        
```

# Getting Started

### Prerequisites
- .NET 8.0 SDK
- Docker Desktop
- PostgreSQL

### Installation

1. Clone the repository
   ```
   git clone https://github.com/heyybooks/heybook-backend.git
   ```
3. Start Docker containers
    ```
    docker compose up -d

    ```
4. Apply migrations
   
    ```
    dotnet ef database update
    ```

5. Run the application
   ```
   Navigate to API folder
   cd src/API
   
   dotnet run
   
   Alternative: dotnet watch run
   
    ```
    


