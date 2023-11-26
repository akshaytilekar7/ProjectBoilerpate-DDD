# sample-cqrs-mediatr
CQRS with MediatR in .NET 7.0

# ProjectName
  - **src**
    - **Application**
      - CQRS classes
    - **Domain**
      - Entities, Aggregates, IRepository etc.
    - **Infrastructure/Persistance**
      - DbContext
      - Migrations
	  -	Repository
      - **Middleware**
        - YourMiddleware.cs (Cross-cutting concerns middleware)
      - Other database-related components
    - **Presentation**
      - Controllers, Views, etc.
      - **Middleware**
        - YourMiddleware.cs (HTTP request/response handling middleware)
      - Other presentation-related components
	  
- **Project Dependencies**

  - **Application**
    - Depends on: Domain, Infrastructure
  
  - **Infrastructure/Persistance**
    - Depends on: Domain
  
  - **Presentation/API**
    - Depends on: Application, Domain
  
  - **Domain**
    - No direct dependencies on other layers
  

Application Layer:

    -   implement the use cases and application-specific business logic. 
    -   Bridge between Domain and Infrastructure Layers:

Domain Layer:

    -   Model your core business concepts such as entities, aggregates, value objects, and domain services. 
    -   It encapsulates the fundamental rules and logic that govern the behavior of your application.

Infrastructure Layer

    -   Technical concerns such as data access, external integrations.
    -   Components like databases, DbContext and 
    


### 
- [x] .NET 7.0
- [x] MediatR 
- [x] Swagger 
- [x] Serilog 
- [x] Middlewares

https://henriquemauri.net/mediatr-no-net-6-0/
