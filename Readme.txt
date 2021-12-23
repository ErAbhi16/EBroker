
Clone repo in local machine by SSH or Https. (https://github.com/ErAbhi16/EBroker)

Open & Build solution in VS.

To Check application execution :-

To Run application install postregs db and update connection string accordingly . Run migration and update databas to get schema and seed data initialization.

Swagger is added to test endpoints.

To Check test cases execution :-

Run All Tests in Test Explorer.

or run below commands in cmd at unit test project path to generate test case coverage files .

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=.\TestResults\Coverage\EBroker.coverage.xml


if reportgenerator not installed :- dotnet tool install -g dotnet-reportgenerator-globaltool

reportgenerator -reports:"TestResults\Coverage\EBroker.coverage.xml" -targetdir:"coveragereport" -reporttypes:Html


Assumptions & Key Points :-

a)DateTime is kept taken as DateTime.now to check transaction at real time rather than taking as input
b)Single Repository is created for simplicity purpose instead of following best practices like Repository + unit of work pattern
c)Seed Data is added to avoid entering data via end point for equity and trader so no endpoint is created to create trader/equity 




