using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;

namespace CarAuction.Tests.Models;

public class TruckTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateTruck()
    {
        // Act
        var truck = new Truck("ID04", "Chevrolet", "Silverado", 2020, 40000m, 5.5m);

        // Assert
        Assert.Equal("ID04", truck.Id);
        Assert.Equal("Chevrolet", truck.Manufacturer);
        Assert.Equal("Silverado", truck.Model);
        Assert.Equal(2020, truck.Year);
        Assert.Equal(40000m, truck.StartingProposal);
        Assert.Equal(5.5m, truck.LoadCapacity);
        Assert.Equal(VehicleType.Truck, truck.Type);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2.5)]
    [InlineData(10.75)]
    [InlineData(50)]
    public void Constructor_ValidLoadCapacity_ShouldCreateTruck(decimal loadCapacity)
    {
        // Act
        var truck = new Truck("ID04", "Chevrolet", "Silverado", 2020, 40000m, loadCapacity);

        // Assert
        Assert.Equal(loadCapacity, truck.LoadCapacity);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(-1)]
    [InlineData(-10.5)]
    public void Constructor_NegativeLoadCapacity_ShouldThrowException(decimal loadCapacity)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Truck("ID04", "Chevrolet", "Silverado", 2020, 40000m, loadCapacity));
    }
}