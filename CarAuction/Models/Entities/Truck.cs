using CarAuction.Exceptions;
using CarAuction.Models.Enums;

namespace CarAuction.Models.Entities;

public class Truck : Vehicle
{
    public decimal LoadCapacity { get; }
    public override VehicleType Type => VehicleType.Truck;

    public Truck(string id, string manufacturer, string model, int year, decimal startingProposal, decimal loadCapacity)
        : base(id, manufacturer, model, year, startingProposal)
    {
        if (loadCapacity < 0)
        {
            throw new InvalidVehicleDataException("Load capacity cannot be negative.");
        }

        LoadCapacity = loadCapacity;
    }
}