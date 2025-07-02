## Backend Design Decisions and Notes

- I chose the **Clean Architecture** pattern to organize the application for better scalability and testability.
- I implemented the **CQRS (Command Query Responsibility Segregation)** pattern to ensure clear separation of concerns between read and write operations.
- **EF Core migrations** are configured to run automatically on application startup — no need to execute any manual migration commands.
- The solution targets **.NET 9**, the latest stable version of .NET Core.
- The solution includes basic **unit tests**, as recommended in the assignment.

## Frontend Notes

- The frontend is built using **Angular 20 (latest)** and integrates **ng-bootstrap** as instructed.
- I used the **Angular Router** for client-side navigation, following the requirements.
- The UI is structured using **fine-grained, reusable components** to maintain separation of concerns and support scalability.

## Run instructions
- Update the **connection string** in `GameCatalogue/GameCatalogue.Api/appsettings.json` with your local SQL Server instance details.
- Navigate to `GameCatalogue/GameCatalogue.Api` and run: `dotnet run`, this will start the backend, apply EF Core migrations, and seed the database on first run.
- In `game-catalogue-client/src/environments/environment.ts`, update the baseUrl to match the running URL of the GameCatalogue API.
- Navigate to `game-catalogue-client`, then install dependencies and start the frontend: `npm install` and `npm start`
