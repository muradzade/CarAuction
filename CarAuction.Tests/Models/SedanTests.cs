using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;

namespace CarAuction.Tests.Models;

public class SedanTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateSedan()
    {
        // Act
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);

        // Assert
        Assert.Equal("ID01", sedan.Id);
        Assert.Equal("Toyota", sedan.Manufacturer);
        Assert.Equal("Corolla", sedan.Model);
        Assert.Equal(2025, sedan.Year);
        Assert.Equal(25000m, sedan.StartingProposal);
        Assert.Equal(4, sedan.NumberOfDoors);
        Assert.Equal(VehicleType.Sedan, sedan.Type);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void Constructor_ValidNumberOfDoors_ShouldCreateSedan(int doors)
    {
        // Act
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, doors);

        // Assert
        Assert.Equal(doors, sedan.NumberOfDoors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(10)]
    public void Constructor_InvalidNumberOfDoors_ShouldThrowException(int doors)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, doors));
    }
}