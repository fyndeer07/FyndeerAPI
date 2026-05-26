namespace FyndeerAPI.Infrastructure.Persistence;

public static class DatabaseSchema
{
    public static class Tables
    {
        public const string Categories = "Categories";
        public const string Professionals = "Professionals";
        public const string Reviews = "Reviews";
        public const string ProfessionalTrackRecords = "ProfessionalTrackRecords";
    }

    public static class Columns
    {
        public static class General
        {
            public const string Id = "Id";
            public const string CreatedAt = "CreatedAt";
        }

        public static class Categories
        {
            public const string Slug = "Slug";
            public const string Name = "Name";
        }

        public static class Professionals
        {
            public const string Slug = "Slug";
            public const string FullName = "FullName";
            public const string Title = "Title";
            public const string CategoryId = "CategoryId";
            public const string CategoryName = "CategoryName";
            public const string YearsOfExperience = "YearsOfExperience";
            public const string LicenseNumber = "LicenseNumber";
            public const string Languages = "Languages";
            public const string Specialties = "Specialties";
            public const string Phone = "Phone";
            public const string Email = "Email";
            public const string Website = "Website";
            public const string ServiceArea = "ServiceArea";
            public const string Bio = "Bio";
            public const string PhotoUrl = "PhotoUrl";
            public const string IsSponsored = "IsSponsored";
            public const string IsVerified = "IsVerified";
            public const string Rating = "Rating";
            public const string ReviewCount = "ReviewCount";
            public const string LicenseState = "LicenseState";
            public const string Brokerage = "Brokerage";
            public const string IsAcceptingClients = "IsAcceptingClients";
            public const string WorkingHours = "WorkingHours";
        }

        public static class Reviews
        {
            public const string ProfessionalId = "ProfessionalId";
            public const string ReviewerName = "ReviewerName";
            public const string IsVerified = "IsVerified";
            public const string Rating = "Rating";
            public const string Comment = "Comment";
        }

        public static class ProfessionalTrackRecords
        {
            public const string ProfessionalId = "ProfessionalId";
            public const string HomesSold = "HomesSold";
            public const string AvgDaysOnMarket = "AvgDaysOnMarket";
            public const string SaleToListRatio = "SaleToListRatio";
            public const string PriceRangeMin = "PriceRangeMin";
            public const string PriceRangeMax = "PriceRangeMax";
            public const string PrimaryCounties = "PrimaryCounties";
            public const string CommunityClients = "CommunityClients";
        }
    }

    public static class Indexes
    {
        public static class Categories
        {
            public const string SlugUnique = "IX_Categories_Slug";
        }

        public static class Professionals
        {
            public const string SlugUnique = "IX_Professionals_Slug";
            public const string EmailUnique = "IX_Professionals_Email";
            public const string CategoryId = "IX_Professionals_CategoryId";
        }

        public static class Reviews
        {
            public const string ProfessionalId = "IX_Reviews_ProfessionalId";
        }
    }
}
