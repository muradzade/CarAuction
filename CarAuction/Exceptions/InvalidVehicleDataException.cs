namespace CarAuction.Exceptions;

public class InvalidVehicleDataException : Exception
{
    public InvalidVehicleDataException(string message) : base(message) { }
}