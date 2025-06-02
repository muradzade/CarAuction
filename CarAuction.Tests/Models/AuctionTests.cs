using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;

namespace CarAuction.Tests.Models;

public class AuctionTests
{
    [Fact]
    public void Constructor_ValidVehicleId_ShouldCreateAuction()
    {
        // Act
        var auction = new Auction("ID01");

        // Assert
        Assert.Equal("ID01", auction.VehicleId);
        Assert.Equal(AuctionState.Created, auction.Status);
        Assert.Null(auction.StartDate);
        Assert.Null(auction.EndDate);
        Assert.Empty(auction.Proposals);
        Assert.Null(auction.HighestProposal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidVehicleId_ShouldThrowException(string vehicleId)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Auction(vehicleId));
    }

    [Fact]
    public void Start_FromCreatedState_ShouldStart()
    {
        // Arrange
        var auction = new Auction("ID01");

        // Act
        auction.Start();

        // Assert
        Assert.Equal(AuctionState.Started, auction.Status);
        Assert.NotNull(auction.StartDate);
    }

    [Fact]
    public void Close_FromStartedState_ShouldClose()
    {
        // Arrange
        var auction = new Auction("ID01");
        auction.Start();

        // Act
        auction.Close();

        // Assert
        Assert.Equal(AuctionState.Closed, auction.Status);
        Assert.NotNull(auction.EndDate);
    }

    [Fact]
    public void AddProposal_ValidProposal_ShouldAdd()
    {
        // Arrange
        var auction = new Auction("ID01");
        auction.Start();
        var proposal = new Proposal(1000m, "John");

        // Act
        auction.AddProposal(proposal);

        // Assert
        Assert.Single(auction.Proposals);
        Assert.Equal(proposal, auction.HighestProposal);
    }

    [Fact]
    public void AddProposal_NotStarted_ShouldThrowException()
    {
        // Arrange
        var auction = new Auction("ID01");
        var proposal = new Proposal(1000m, "John");

        // Act & Assert
        Assert.Throws<AuctionNotActiveException>(() => auction.AddProposal(proposal));
    }

    [Fact]
    public void AddProposal_LowerAmount_ShouldThrowException()
    {
        // Arrange
        var auction = new Auction("ID01");
        auction.Start();
        auction.AddProposal(new Proposal(1000m, "John"));
        var lowerProposal = new Proposal(900m, "Jane");

        // Act & Assert
        Assert.Throws<InvalidProposalException>(() => auction.AddProposal(lowerProposal));
    }
}