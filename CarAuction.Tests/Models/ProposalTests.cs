using CarAuction.Exceptions;
using CarAuction.Models.Entities;

namespace CarAuction.Tests.Models;

public class ProposalTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateProposal()
    {
        // Act
        var proposal = new Proposal(1000m, "John Doe");

        // Assert
        Assert.Equal(1000m, proposal.Amount);
        Assert.Equal("John Doe", proposal.ProposerName);
        Assert.True(proposal.ProposeDate <= DateTime.Now);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_InvalidAmount_ShouldThrowException(decimal amount)
    {
        // Act & Assert
        Assert.Throws<InvalidProposalException>(() => new Proposal(amount, "John"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidProposerName_ShouldThrowException(string proposerName)
    {
        // Act & Assert
        Assert.Throws<InvalidProposalException>(() => new Proposal(1000m, proposerName));
    }

    [Fact]
    public void Constructor_ProposerNameWithSpaces_ShouldTrimSpaces()
    {
        // Act
        var proposal = new Proposal(1000m, "  John Doe  ");

        // Assert
        Assert.Equal("John Doe", proposal.ProposerName);
    }
}