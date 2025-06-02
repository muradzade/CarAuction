using CarAuction.Exceptions;
using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;
using CarAuction.Services;

namespace CarAuction.Tests.Services;

public class CarAuctionFacadeServiceTests
{
    private readonly CarAuctionFacadeService _service = new();

    [Fact]
    public void AddVehicle_Valid_ShouldAdd()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);

        // Act
        _service.AddVehicle(sedan);

        // Assert
        var vehicles = _service.FilterVehicles(null);
        Assert.Contains(sedan, vehicles);
    }

    [Fact]
    public void StartAuction_ExistingVehicle_ShouldStart()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        _service.AddVehicle(sedan);

        // Act
        _service.StartAuction("ID01");

        // Assert - Should be able to propose
        _service.ProposeToAuction("ID01", 27000m, "John");
    }

    [Fact]
    public void StartAuction_NonExistent_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<VehicleNotFoundException>(() => _service.StartAuction("NONE"));
    }

    [Fact]
    public void ProposeToAuction_Valid_ShouldAccept()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        _service.AddVehicle(sedan);
        _service.StartAuction("ID01");

        // Act
        _service.ProposeToAuction("ID01", 27000m, "John");

        // Assert - Next proposal must be higher
        Assert.Throws<InvalidProposalException>(() =>
            _service.ProposeToAuction("ID01", 26000m, "Jane"));
    }

    [Fact]
    public void ProposeToAuction_NonExistentVehicle_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<VehicleNotFoundException>(() =>
            _service.ProposeToAuction("NONE", 1000m, "John"));
    }

    [Fact]
    public void CloseAuction_Valid_ShouldClose()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        _service.AddVehicle(sedan);
        _service.StartAuction("ID01");

        // Act
        _service.CloseAuction("ID01");

        // Assert - Should not be able to propose
        Assert.Throws<AuctionNotActiveException>(() =>
            _service.ProposeToAuction("ID01", 27000m, "John"));
    }

    [Fact]
    public void FilterVehicles_ByType_ShouldReturnMatching()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, 7);
        _service.AddVehicle(sedan);
        _service.AddVehicle(suv);

        // Act
        var result = _service.FilterVehicles(new SearchInput { Type = VehicleType.Sedan });

        // Assert
        Assert.Single(result);
        Assert.Contains(sedan, result);
    }

    [Fact]
    public void FullWorkflow_ShouldWork()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);

        // Act & Assert
        _service.AddVehicle(sedan);
        _service.StartAuction("ID01");
        _service.ProposeToAuction("ID01", 27000m, "John");
        _service.ProposeToAuction("ID01", 28000m, "Jane");
        _service.CloseAuction("ID01");

        // Final check - no more proposals allowed
        Assert.Throws<AuctionNotActiveException>(() =>
            _service.ProposeToAuction("ID01", 30000m, "Bob"));
    }
}