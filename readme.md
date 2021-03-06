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

[ServiceFixture.cs](/App.Tests/Fixtures/ServiceFixture.cs) is responsible for initializing / disposing an internal `DbManager` instance that is used to create a public, readonly `IService<EntityBase>` instance based on the generic types assigned to `ServiceFixture<T, S>`. Additionally, it executes the `IService<EntityBase>.SeedTest()` method to initialize any starting test data.

[PriorityOrderer.cs](./App.Tests/PriorityOrderer.cs) allows test methods to be decorated with a [TestPriorityAttribute](./App.Tests/Attributes/TestPriorityAttribute.cs) so that the order of test execution can be controlled. All test methods default to a priority of `0`.  The higher the test priority, the later it will be executed in the testing pipeline. Negative values are allowed. See [TestBase.Remove](./App.Tests/Tests/TestBase.cs#L38) for use case.

[TestBase.cs](./App.Tests/Tests/TestBase.cs) is an abstract generic class that is intended to be used with any service of `IService<EntityBase>`. The service is accessed via the `ServiceFixture` class. It is responsible for defining tests for the `QueryAll`, `Find`, and `Remove` methods of `IService`.

[CategoryTest.cs](./App.Tests/Tests/CategoryTest.cs) demonstrates an implementation of `TestBase`. It is responsible for defining tests for its `Save` method and any other unique functionality provided by `CategoryService`.

> Note the use of test data via `[MemberData(nameof(Data))]` as a simple way of providing multiple objects for executing the same test.