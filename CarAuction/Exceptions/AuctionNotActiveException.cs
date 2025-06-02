namespace CarAuction.Exceptions;

public class AuctionNotActiveException : Exception
{
    public AuctionNotActiveException(string message) : base(message) { }
}