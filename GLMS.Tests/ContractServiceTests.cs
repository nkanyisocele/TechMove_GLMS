using GLMS.Web.Interfaces;
using GLMS.Web.Models;
using GLMS.Web.Services;
using Moq;
using Xunit;

namespace GLMS.Tests;

public class ContractServiceTests
{
    private readonly Mock<IContractRepository> _mockRepo;
    private readonly ContractService _service;

    public ContractServiceTests()
    {
        _mockRepo = new Mock<IContractRepository>();
        _service = new ContractService(_mockRepo.Object);
    }

    [Fact]
    public async Task IsContractActive_ShouldReturnFalse_WhenContractIsExpired()
    {
        // Arrange: Create a contract that ended yesterday
        var expiredContract = new Contract
        {
            ContractId = 1,
            Status = "Active",
            StartDate = DateTime.Now.AddMonths(-2),
            EndDate = DateTime.Now.AddDays(-1)
        };

        _mockRepo.Setup(repo => repo.GetContractByIdAsync(1))
                 .ReturnsAsync(expiredContract);

        // Act
        var result = await _service.IsContractActiveAsync(1);

        // Assert: Logic should catch that the date has passed
        Assert.False(result);
    }

    [Fact]
    public async Task IsContractActive_ShouldReturnFalse_WhenStatusIsOnHold()
    {
        // Arrange: Contract is within dates but marked "On Hold"
        var onHoldContract = new Contract
        {
            ContractId = 2,
            Status = "On Hold",
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(10)
        };

        _mockRepo.Setup(repo => repo.GetContractByIdAsync(2))
                 .ReturnsAsync(onHoldContract);

        // Act
        var result = await _service.IsContractActiveAsync(2);

        // Assert
        Assert.False(result);
    }
}
