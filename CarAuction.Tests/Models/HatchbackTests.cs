using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;

namespace CarAuction.Tests.Models;

public class HatchbackTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateHatchback()
    {
        // Act
        var hatchback = new Hatchback("ID03", "Volkswagen", "Golf", 2021, 22000m, 5);

        // Assert
        Assert.Equal("ID03", hatchback.Id);
        Assert.Equal("Volkswagen", hatchback.Manufacturer);
        Assert.Equal("Golf", hatchback.Model);
        Assert.Equal(2021, hatchback.Year);
        Assert.Equal(22000m, hatchback.StartingProposal);
        Assert.Equal(5, hatchback.NumberOfDoors);
        Assert.Equal(VehicleType.Hatchback, hatchback.Type);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void Constructor_ValidNumberOfDoors_ShouldCreateHatchback(int doors)
    {
        // Act
        var hatchback = new Hatchback("ID03", "Volkswagen", "Golf", 2021, 22000m, doors);

        // Assert
        Assert.Equal(doors, hatchback.NumberOfDoors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(10)]
    public void Constructor_InvalidNumberOfDoors_ShouldThrowException(int doors)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Hatchback("ID03", "Volkswagen", "Golf", 2021, 22000m, doors));
    }
}