using Microsoft.Extensions.Logging;
using Moq;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Handlers;
using StargateAPI.Business.Queries;

namespace Stargate.Tests.Unit.Handlers;

[TestClass]
public class TestGetPersonByNameHandler
{
    [TestMethod]
    public async Task TestHandle()
    {
        /* Arrange */
        var mockContext = new Mock<IDataAccess>();
        var mockLogger = new Mock<ILogger>();

        var request = new GetPersonByName("Test Name 01");
        var token = new CancellationToken();

        IEnumerable<PersonAstronaut> queryResult = new List<PersonAstronaut>
        {
            new()
            {
                PersonId = 1, Name = "Test Name 01", CurrentRank = Rank.Captain,
                CurrentDutyTitle = DutyTitle.MoraleBooster, CareerStartDate = DateTime.Today,
                CareerEndDate = DateTime.Now
            }
        };

        mockContext
            .Setup(context => context
                .QueryAsync<PersonAstronaut>(It.IsAny<string>(),It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(queryResult));

        var handler = new GetPersonByNameHandler(mockContext.Object, mockLogger.Object);

        /* Act */
        var result = await handler.Handle(request, token);

        /* Assert */
        Assert.AreEqual(queryResult.First(), result.Person);
    }

    // TODO: Test more logical paths
}