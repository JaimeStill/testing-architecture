# Unit Testing

Before executing any tests, be sure that the connection strings defined in [appsettings.json](./App.DbCli/appsettings.json) are adjusted to match your environment.

In order to execute the tests, simply run `dotnet test` in a terminal from the root of this repository.

![image](https://user-images.githubusercontent.com/14102723/177339934-88f68f69-8245-4bf5-bb90-35e4ce3218bb.png)

## [Services](./App.Services/)
Testing is centered around [Services](./App.Services/). In addition to providing functionality, services also provide a [`SeedTest`](./App.Services/CategoryService.cs#L43) method for establishing an initial data set when running tests.

## [DbCli](./App.DbCli/)
[DbManager.cs](./App.DbCli/DbManager.cs) enables the generation, initialization, migration, and optional destruction of an EF database. Connection is established via [appsettings.json](./App.DbCli/appsettings.json) using [.NET Configuration](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration).

The `App.DbCli` CLI project allows a database to be seeded as follows:

```bash
# connectionString
# - string
# - Optional
# - Default: "Dev"
# - A value of ConnectionStrings in appsettings.json
#
# destroy
# - boolean
# - Optional
# - Default: false
# - Whether to destroy the database when DbManager is disposed
App.DbCli> dotnet run -- {connectionString} {destroy}

#examples
# Create, Migrate, and Seed the Dev database
App.DbCli> dotnet run -- "Dev"

# Create, Migrate, Seed, and Destroy the Test database
App.DbCli> dotnet run -- "Test" true
```

## [Tests](./App.Tests/)

[TestBase.cs](./App.Tests/Tests/TestBase.cs) is an abstract generic class that is intended to be used with any service of `IService<EntityBase>`. It is responsible for initializing / disposing the `DbManager` instance and defining tests for the `QueryAll`, `Find`, and `Remove` methods of `IService`.

[CategoryTest.cs](./App.Tests/Tests/CategoryTest.cs) demonstrates an implementation of `TestBase`. It is responsible for initializing its service instance and seeding the database, as well as defining tests for its `Save` method and any other unique functionality provided by `CategoryService`.

Note the use of test data via `[MemberData(nameof(Data))]` as a simple way of providing multiple objects for executing the same test.