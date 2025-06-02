namespace CarAuction.Exceptions;

public class DuplicateVehicleException : Exception
{
    public DuplicateVehicleException(string message) : base(message) { }
}