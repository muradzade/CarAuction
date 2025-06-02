using CarAuction.Exceptions;
using CarAuction.Models.Enums;

namespace CarAuction.Models.Entities;

public abstract class Vehicle
{
    public string Id { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal StartingProposal { get; }
    public abstract VehicleType Type { get; }

    protected Vehicle(string id, string manufacturer, string model, int year, decimal minimumProposal)
    {
        ValidateCommonProperties(id, manufacturer, model, year, minimumProposal);

        Id = id;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingProposal = minimumProposal;
    }

    private static void ValidateCommonProperties(string id, string manufacturer, string model, int year, decimal startingProposal)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new InvalidVehicleDataException("Vehicle ID cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            throw new InvalidVehicleDataException("Manufacturer cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            throw new InvalidVehicleDataException("Model cannot be null or empty.");
        }

        if (year < 1885 || year > DateTime.Now.Year + 1)
        {
            throw new InvalidVehicleDataException($"Year must be between 1885 and {DateTime.Now.Year + 1}.");
        }

        if (startingProposal < 0)
        {
            throw new InvalidVehicleDataException("Starting proposal cannot be negative.");
        }
    }
}