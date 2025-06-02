using CarAuction.Exceptions;
using CarAuction.Models.Entities;

namespace CarAuction.Tests.Models;

public class VehicleTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidId_ShouldThrowException(string id)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan(id, "Toyota", "Corolla", 2025, 25000m, 4));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidManufacturer_ShouldThrowException(string manufacturer)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", manufacturer, "Corolla", 2025, 25000m, 4));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidModel_ShouldThrowException(string model)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", "Toyota", model, 2023, 25000m, 4));
    }

    [Theory]
    [InlineData(1884)]
    [InlineData(1800)]
    public void Constructor_YearTooOld_ShouldThrowException(int year)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", "Toyota", "Corolla", year, 25000m, 4));
    }

    [Fact]
    public void Constructor_YearTooNew_ShouldThrowException()
    {
        // Arrange
        var futureYear = DateTime.Now.Year + 2;

        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", "Toyota", "Corolla", futureYear, 25000m, 4));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Constructor_NegativeStartingProposal_ShouldThrowException(decimal startingProposal)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Sedan("ID01", "Toyota", "Corolla", 2025, startingProposal, 4));
    }

    [Fact]
    public void Constructor_ValidBoundaryYears_ShouldCreateVehicle()
    {
        // Arrange & Act
        var vehicle1885 = new Sedan("ID01", "Brand", "Model", 1885, 1000m, 4);
        var vehicleCurrentYear = new Sedan("ID02", "Brand", "Model", DateTime.Now.Year, 1000m, 4);
        var vehicleNextYear = new Sedan("ID03", "Brand", "Model", DateTime.Now.Year + 1, 1000m, 4);

        // Assert
        Assert.Equal(1885, vehicle1885.Year);
        Assert.Equal(DateTime.Now.Year, vehicleCurrentYear.Year);
        Assert.Equal(DateTime.Now.Year + 1, vehicleNextYear.Year);
    }

    [Fact]
    public void Constructor_ZeroStartingProposal_ShouldCreateVehicle()
    {
        // Act
        var vehicle = new Sedan("ID01", "Toyota", "Corolla", 2025, 0m, 4);

        // Assert
        Assert.Equal(0m, vehicle.StartingProposal);
    }
}