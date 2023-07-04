# UpStorage App

<image align="left" height="100px" alt="[upstorage_logo]" width="100px" src="https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/d12206bb-e3b5-49ba-a573-90458f046cb3"/>

The UpStorage App is a tool that allows users to manage personal infos like social media account passwords, credit card infos, personal notes, addresses and generate secure and strong passwords. The recent upgrades have introduced new functionalities, such as managing addresses and notes, establishing relationships between entities, and implementing a CQRS structure in aspect of Clean Architecture by [jasontaylordev](https://github.com/jasontaylordev/CleanArchitecture).

**The key principles of Clean Architecture include:**

![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/4502b3f7-d99b-46c8-a5ce-f632bcee4282)

- Separation of Concerns: The architecture separates different concerns and responsibilities into distinct layers, each with a specific purpose. This helps in managing complexity and enables changes in one layer without affecting the others.

- Independence of Frameworks: The core business logic and entities should not depend on any specific framework or technology. The use of frameworks or libraries is limited to the outer layers, which interact with the infrastructure or UI.

- Dependency Rule: Dependencies flow inward, with the inner layers having no knowledge of the outer layers. This helps in maintaining high-level modules that are independent and decoupled.

- Domain-centric Design: The architecture focuses on the domain layer, representing the core business concepts, rules, and logic. This layer is isolated and does not depend on external concerns.

- Testability: By isolating the business logic and dependencies, Clean Architecture promotes easy testing. Unit tests can be written for the inner layers without the need for external dependencies.

The architecture typically consists of multiple layers, such as:

- Entities: Represent the core business objects and encapsulate business logic.
- Use Cases: Contain application-specific business rules and orchestrate the flow of data between entities and external systems.
- Interfaces/Adapters: Handle external concerns, such as user interfaces, databases, frameworks, or external services.
- Frameworks/Drivers: Provide the infrastructure and technical details, such as frameworks, databases, or web servers.

Clean Architecture helps in achieving code maintainability, flexibility, and scalability. It allows for easier evolution and adaptation to changing requirements, as the core business logic remains unaffected by external factors. Additionally, it facilitates the use of best practices, such as SOLID principles, dependency injection, and test-driven development.

## Features

The following features have been added to the Password Generator app:

1. **Address Management**
   - Users can now manage their addresses within the app.
   - Implemented a one-to-many relationship between the "User" and "Address" entities to handle multiple addresses per user.
   - Added fields in the "Address" entity to properly configure the database.

2. **Note Management**
   - Introduced the ability to create and manage notes.
   - Implemented a many-to-many relationship between the "Note" and "NoteCategory" entities to allow notes to be assigned to multiple categories and vice versa.
   - Configured the "Note", "NoteCategory," and related entities to handle the relationship in the database.

3. **CQRS Structure**
   - Implemented the CQRS (Command Query Responsibility Segregation) pattern for the "Address" entity.
   - Created commands (`Add`, `Update`, `Delete`, `HardDelete`) to handle operations on addresses.
   - Created queries (`GetById`, `GetAll`) to retrieve address information.
   - These commands and queries follow the CQRS structure, ensuring separation of read and write operations.

Here is WebApi,

![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/8ed58402-00bb-4da6-af13-cfc516855645)


## Database Migration

The upgrades for both the Application and Identity modules were pushed to the MySQL Database. The migrations performed include:

1. **Application Database Migration**
   - Applied migration(s) to update the database schema related to the new features and improvements introduced.
   - Ensured that the database structure aligns with the upgraded Password Generator app.

2. **Identity Database Migration**
   - Applied migration(s) to update the Identity module's database schema, incorporating the changes required for the upgraded app.
   - Ensured that the Identity module integrates smoothly with the updated Password Generator app.

Here is the database diagram;

![image](https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/eee1c960-d660-4c19-b415-97dade634a27)


## Usage

Once the Password Generator app is up and running, users can:

- Generate secure and random passwords.
- Manage their addresses, including adding, updating, and deleting them.
- Create and manage notes, assigning them to relevant categories.
- Retrieve address information using the provided queries.
