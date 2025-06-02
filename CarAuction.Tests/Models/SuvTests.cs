using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;

namespace CarAuction.Tests.Models;

public class SuvTests
{
    [Fact]
    public void Constructor_ValidData_ShouldCreateSuv()
    {
        // Act
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, 7);

        // Assert
        Assert.Equal("ID02", suv.Id);
        Assert.Equal("Ford", suv.Manufacturer);
        Assert.Equal("Explorer", suv.Model);
        Assert.Equal(2022, suv.Year);
        Assert.Equal(35000m, suv.StartingProposal);
        Assert.Equal(7, suv.NumberOfSeats);
        Assert.Equal(VehicleType.Suv, suv.Type);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(12)]
    public void Constructor_ValidNumberOfSeats_ShouldCreateSuv(int seats)
    {
        // Act
        var suv = new Suv("ID02", "Ford", "Explorer", 2022, 35000m, seats);

        // Assert
        Assert.Equal(seats, suv.NumberOfSeats);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(13)]
    [InlineData(20)]
    public void Constructor_InvalidNumberOfSeats_ShouldThrowException(int seats)
    {
        // Act & Assert
        Assert.Throws<InvalidVehicleDataException>(() =>
            new Suv("ID02", "Ford", "Explorer", 2022, 35000m, seats));
    }
}