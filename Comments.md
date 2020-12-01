# Comments

I will firstly list my assumptions based on the readme of the project:

1. Maintaining the single service taking a single application approach and improving the how the application is then passed to the other existing external services.
2. The structure of the seller application cannot be changed, the wording of "hosting in this project" also implies that the `SubmitApplicationFor` method is invoked by an external service (potentially via a REST request), keeping the setters (mutability) of these classes are needed for mapping of the request parameters/body to the properties of the sellerapplication class
3. Although the `ProductApplication` project is a class library, the primary service uses dependency injection. I assume that the container framework is the default container supplied in .NET Core.
4. -1 is an error code.

After having a scan of the project, it immediately stood out to me that the `ProductApplicationService` does a lot of switching logic based on the type of the `Product`. This can be improved by seperating out the business logic here into seperate classes. At this point in time, the `ProductApplicationService` is violating the single responsibility principle.

The first thing to tackle would be the current code coverage, there appears to be only a single test for a single product, where there are already three products available. This would allow more confidence when re-factoring.

It is key to ensure that the `Products`, `Applications` and `SellerCompanyData` remain as they are for backwards compatibility. Changing names, namespaces or any method signature of the `ProductApplicationService` would be considered a breaking change.

There also appears to be few pre-condition checking in the code, this could result to bugs if say the company name cannot be null. It's difficult to justify putting this into the code as I am unaware if a null company name is allowed or not. This would be something to clarify with a BA for clarification (depending on where the external project implementations reside, could be a third party company for instance).

## Improvements

With the `ProductServiceFactory`, the command type needs to be known (hence the use of the dynamic keyword). This isn't ideal and cascaded to the tests where `FluentAssertions` couldn't be used. Another option is to have registered implementations of `IService<IProduct>` so the product type can be used resolve a valid handler. To be honest here I wanted to see what CQS (command query seperation pattern) would look like in the scenario given. I have learned that although I like CQS is nice and I like it, it's not always the best option.

I would like to add tests to check that all products have registered handlers, but time constraints is an issue here.