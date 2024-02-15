using Microsoft.Extensions.Logging;
using Moq;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Handlers;

namespace Stargate.Tests.Unit.Handlers;

[TestClass]
public class TestCreatePersonHandler
{
    [TestMethod]
    public async Task TestHandle_ACreatePersonResultIsReturned()
    {
        /* Arrange */
        var mockContext = new Mock<IDataAccess>();
        var mockLogger = new Mock<ILogger>();

        var request = new CreatePerson { Name = "name" };
        var person = new Person { Name = request.Name, Id = 42 };
        var token = new CancellationToken();

        mockContext.Setup(context => 
            context.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
                .Returns(new ValueTask<Person>(person));

        mockContext.Setup(context => 
            context.UpdateAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

        var handler = new CreatePersonHandler(mockContext.Object, mockLogger.Object);

        /* Act */
        var result = await handler.Handle(request, token);

        /* Assert */
        // TODO BREAKING: Issue testing this test due to database update reliance
        Assert.AreEqual(person.Id, result.Id);
    }

    public void TestHandle_ANewPersonIsAddedToTheDatabase()
    {
        // TODO PENDING: Waiting on StargateContext to IDatabaseAccess refactor
        throw new NotImplementedException("Return to this once handler is refactored to use IDatabaseAccess");
    }

    public void TestHandle_AnExistingPersonIsUpdatedInTheDatabase()
    {
        // TODO PENDING: Waiting on StargateContext to IDatabaseAccess refactor
        throw new NotImplementedException("Return to this once handler is refactored to use IDatabaseAccess");
    }
}