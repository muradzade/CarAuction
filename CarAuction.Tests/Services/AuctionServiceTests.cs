using CarAuction.Exceptions;
using CarAuction.Services;

namespace CarAuction.Tests.Services;

public class AuctionServiceTests
{
    private readonly AuctionService _service = new();

    [Fact]
    public void Start_ValidId_ShouldStart()
    {
        // Arrange
        _service.Start("ID01");

        // Act & Assert
        var exception = Record.Exception(() => _service.Propose("ID01", 1000m, "John", 500m));
        Assert.Null(exception);
    }

    [Fact]
    public void Start_EmptyId_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.Start(""));
    }

    [Fact]
    public void Start_AlreadyActive_ShouldThrowException()
    {
        // Arrange
        _service.Start("ID01");

        // Act & Assert
        Assert.Throws<AuctionAlreadyActiveException>(() => _service.Start("ID01"));
    }

    [Fact]
    public void Close_ValidAuction_ShouldClose()
    {
        // Arrange
        _service.Start("ID01");

        // Act
        _service.Close("ID01");

        // Assert - Should not be able to propose now
        Assert.Throws<AuctionNotActiveException>(() =>
            _service.Propose("ID01", 1000m, "John", 500m));
    }

    [Fact]
    public void Close_NonExistent_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<AuctionNotFoundException>(() => _service.Close("NONE"));
    }

    [Fact]
    public void Propose_ValidBid_ShouldAccept()
    {
        // Arrange
        _service.Start("ID01");

        // Act
        _service.Propose("ID01", 1000m, "John", 500m);

        // Assert - Next bid must be higher
        Assert.Throws<InvalidProposalException>(() =>
            _service.Propose("ID01", 900m, "Jane", 500m));
    }

    [Fact]
    public void Propose_TooLow_ShouldThrowException()
    {
        // Arrange
        _service.Start("ID01");

        // Act & Assert
        Assert.Throws<InvalidProposalException>(() =>
            _service.Propose("ID01", 400m, "John", 500m));
    }

    [Fact]
    public void Propose_NoAuction_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<AuctionNotActiveException>(() =>
            _service.Propose("NONE", 1000m, "John", 500m));
    }
}