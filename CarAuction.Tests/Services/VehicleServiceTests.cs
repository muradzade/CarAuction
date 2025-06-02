using CarAuction.Exceptions;
using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;
using CarAuction.Services;

namespace CarAuction.Tests.Services;

public class VehicleServiceTests
{
    private readonly VehicleService _service = new();

    [Fact]
    public void Add_ValidVehicle_ShouldAdd()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);

        // Act
        _service.Add(sedan);

        // Assert
        Assert.True(_service.Exists("ID01"));
        Assert.Equal(sedan, _service.Get("ID01"));
    }

    [Fact]
    public void Add_DuplicateId_ShouldThrowException()
    {
        // Arrange
        var sedan1 = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        var sedan2 = new Sedan("ID01", "Honda", "Accord", 2023, 26000m, 4);
        _service.Add(sedan1);

        // Act & Assert
        Assert.Throws<DuplicateVehicleException>(() => _service.Add(sedan2));
    }

    [Fact]
    public void Get_NonExistent_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<VehicleNotFoundException>(() => _service.Get("ID01"));
    }

    [Fact]
    public void Exists_ValidId_ShouldReturnTrue()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        _service.Add(sedan);

        // Act & Assert
        Assert.True(_service.Exists("ID01"));
        Assert.False(_service.Exists("V999"));
    }

    [Fact]
    public void Filter_ByType_ShouldReturnMatching()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, 7);
        _service.Add(sedan);
        _service.Add(suv);

        // Act
        var result = _service.Filter(new SearchInput { Type = VehicleType.Sedan });

        // Assert
        Assert.Single(result);
        Assert.Contains(sedan, result);
    }

    [Fact]
    public void Filter_ByManufacturer_ShouldReturnMatching()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, 7);
        _service.Add(sedan);
        _service.Add(suv);

        // Act
        var result = _service.Filter(new SearchInput { Manufacturer = "Toyota" });

        // Assert
        Assert.Single(result);
        Assert.Contains(sedan, result);
    }

    [Fact]
    public void Filter_Null_ShouldReturnAll()
    {
        // Arrange
        var sedan = new Sedan("ID01", "Toyota", "Corolla", 2025, 25000m, 4);
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, 7);
        _service.Add(sedan);
        _service.Add(suv);

        // Act
        var result = _service.Filter(null);

        // Assert
        Assert.Equal(2, result.Count());
    }
}