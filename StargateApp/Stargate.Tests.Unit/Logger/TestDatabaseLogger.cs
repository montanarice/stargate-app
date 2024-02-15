using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using StargateAPI.Business.Data;
using StargateAPI.Business.Logging;

namespace Stargate.Tests.Unit.Logger;

[TestClass]
public class TestDatabaseLogger
{
    [TestMethod]
    public void TestLog()
    {
        /* Arrange */
        // TODO PENDING: Update test to be tied to IDataAccess, not StargateContext, once refactored.
        var mockContext  = new Mock<StargateContext>();

        // TODO: Broken test setup return to later
        mockContext.SetupGet(sc => sc.LogTables)
            .Returns(It.IsAny<DbSet<LogTableEntry>>());
        mockContext.Setup(sc => sc.SaveChanges()).Returns(It.IsAny<int>());

        var databaseLogger = new DatabaseLogger(mockContext.Object);

        /* Act */
        databaseLogger.Log(LogLevel.Information, message: "Message");

        /* Assert */
        mockContext.VerifyGet(sc => sc.LogTables, Times.Once);
        mockContext.Verify(sc => sc.SaveChanges(), Times.Once);
    }
}