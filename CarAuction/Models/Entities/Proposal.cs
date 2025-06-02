using CarAuction.Exceptions;

namespace CarAuction.Models.Entities;

public class Proposal
{
    public decimal Amount { get; }
    public string ProposerName { get; }
    public DateTime ProposeDate { get; }

    public Proposal(decimal amount, string proposerName)
    {
        if (amount <= 0)
        {
            throw new InvalidProposalException("Proposal amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(proposerName))
        {
            throw new InvalidProposalException("Proposer name cannot be null or empty.");
        }

        Amount = amount;
        ProposerName = proposerName.Trim();
        ProposeDate = DateTime.Now;
    }
}