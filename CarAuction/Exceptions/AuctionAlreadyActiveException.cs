namespace CarAuction.Exceptions;

public class AuctionAlreadyActiveException : Exception
{
    public AuctionAlreadyActiveException(string message) : base(message) { }
}