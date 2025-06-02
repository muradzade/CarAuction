using CarAuction.Exceptions;
using CarAuction.Models.Enums;

namespace CarAuction.Models.Entities;

public class Suv : Vehicle
{
    public int NumberOfSeats { get; }
    public override VehicleType Type => VehicleType.Suv;

    public Suv(string id, string manufacturer, string model, int year, decimal startingProposal, int numberOfSeats)
        : base(id, manufacturer, model, year, startingProposal)
    {
        if (numberOfSeats < 2 || numberOfSeats > 12)
        {
            throw new InvalidVehicleDataException("Number of seats must be between 2 and 12.");
        }

        NumberOfSeats = numberOfSeats;
    }
}