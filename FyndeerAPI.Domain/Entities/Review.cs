namespace FyndeerAPI.Domain.Entities;

public class Review
{
    public string Id { get; private set; } = string.Empty;
    public string ProfessionalId { get; private set; } = string.Empty;
    public string ReviewerName { get; private set; } = string.Empty;
    public bool IsVerified { get; private set; }
    public double Rating { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Review() { }

    public static Review Create(
        string id,
        string professionalId,
        string reviewerName,
        bool isVerified,
        double rating,
        string comment,
        DateTime createdAt)
    {
        return new Review
        {
            Id = id,
            ProfessionalId = professionalId,
            ReviewerName = reviewerName,
            IsVerified = isVerified,
            Rating = rating,
            Comment = comment,
            CreatedAt = createdAt
        };
    }
}
