using CarAuction.Exceptions;
using CarAuction.Models.Enums;

namespace CarAuction.Models.Entities;

public class Sedan : Vehicle
{
    public int NumberOfDoors { get; }
    public override VehicleType Type => VehicleType.Sedan;

    public Sedan(string id, string manufacturer, string model, int year, decimal startingProposal, int numberOfDoors)
        : base(id, manufacturer, model, year, startingProposal)
    {
        if (numberOfDoors < 2 || numberOfDoors > 6)
        {
            throw new InvalidVehicleDataException("Number of doors must be between 2 and 6.");
        }

        NumberOfDoors = numberOfDoors;
    }
}