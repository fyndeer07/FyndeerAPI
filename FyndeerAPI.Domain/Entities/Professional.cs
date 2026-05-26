namespace FyndeerAPI.Domain.Entities;

public class Professional
{
    public string Id { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string CategoryId { get; private set; } = string.Empty;
    public string CategoryName { get; private set; } = string.Empty;
    public int YearsOfExperience { get; private set; }
    public string? LicenseNumber { get; private set; }
    public List<string> Languages { get; private set; } = [];
    public List<string> Specialties { get; private set; } = [];
    public string Phone { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? Website { get; private set; }
    public string ServiceArea { get; private set; } = string.Empty;
    public string Bio { get; private set; } = string.Empty;
    public string? PhotoUrl { get; private set; }
    public bool IsSponsored { get; private set; }
    public bool IsVerified { get; private set; }
    public double Rating { get; private set; }
    public int ReviewCount { get; private set; }
    public string? LicenseState { get; private set; }
    public string? Brokerage { get; private set; }
    public bool IsAcceptingClients { get; private set; }
    public string? WorkingHours { get; private set; }

    public List<Review> Reviews { get; private set; } = [];
    public TrackRecord? TrackRecord { get; private set; }

    private Professional() { }

    public static Professional Create(
        string id,
        string slug,
        string fullName,
        string title,
        string categoryId,
        string categoryName,
        int yearsOfExperience,
        string? licenseNumber,
        List<string> languages,
        List<string> specialties,
        string phone,
        string email,
        string? website,
        string serviceArea,
        string bio,
        string? photoUrl,
        bool isSponsored,
        bool isVerified,
        double rating,
        int reviewCount,
        string? licenseState,
        string? brokerage,
        bool isAcceptingClients,
        string? workingHours)
    {
        return new Professional
        {
            Id = id,
            Slug = slug,
            FullName = fullName,
            Title = title,
            CategoryId = categoryId,
            CategoryName = categoryName,
            YearsOfExperience = yearsOfExperience,
            LicenseNumber = licenseNumber,
            Languages = languages,
            Specialties = specialties,
            Phone = phone,
            Email = email,
            Website = website,
            ServiceArea = serviceArea,
            Bio = bio,
            PhotoUrl = photoUrl,
            IsSponsored = isSponsored,
            IsVerified = isVerified,
            Rating = rating,
            ReviewCount = reviewCount,
            LicenseState = licenseState,
            Brokerage = brokerage,
            IsAcceptingClients = isAcceptingClients,
            WorkingHours = workingHours
        };
    }

    public void Update(
        string slug,
        string fullName,
        string title,
        string categoryId,
        string categoryName,
        int yearsOfExperience,
        string? licenseNumber,
        List<string> languages,
        List<string> specialties,
        string phone,
        string email,
        string? website,
        string serviceArea,
        string bio,
        string? photoUrl,
        bool isSponsored,
        bool isVerified,
        string? licenseState,
        string? brokerage,
        bool isAcceptingClients,
        string? workingHours)
    {
        Slug = slug;
        FullName = fullName;
        Title = title;
        CategoryId = categoryId;
        CategoryName = categoryName;
        YearsOfExperience = yearsOfExperience;
        LicenseNumber = licenseNumber;
        Languages = languages;
        Specialties = specialties;
        Phone = phone;
        Email = email;
        Website = website;
        ServiceArea = serviceArea;
        Bio = bio;
        PhotoUrl = photoUrl;
        IsSponsored = isSponsored;
        IsVerified = isVerified;
        LicenseState = licenseState;
        Brokerage = brokerage;
        IsAcceptingClients = isAcceptingClients;
        WorkingHours = workingHours;
    }
}
