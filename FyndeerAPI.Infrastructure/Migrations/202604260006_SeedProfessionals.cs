using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260006, "Seed Professionals")]
public class Migration202604260006_SeedProfessionals : Migration
{
    public override void Up()
    {
        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "1", Slug = "rajesh-acharya", FullName = "Rajesh Acharya",
            Title = "Immigration Attorney", CategoryId = "immigration-attorney", CategoryName = "Immigration Attorney",
            YearsOfExperience = 12, LicenseNumber = "VA-78432",
            Languages = """["Nepali","English"]""",
            Specialties = """["Family-based immigration","Work visas","Naturalization"]""",
            Phone = "(703) 555-0120", Email = "rajesh@acharyalaw.com", Website = "https://acharyalaw.com",
            ServiceArea = "Fairfax, Burke, Centreville",
            Bio = "I have been helping Nepali families navigate the U.S. immigration system for over 12 years. From green cards and citizenship to employment visas and DACA renewals, I provide clear, honest guidance at every step. I understand the unique challenges our community faces and am committed to making the process as stress-free as possible.",
            PhotoUrl = (string?)null, IsSponsored = true, IsVerified = true, Rating = 4.9, ReviewCount = 34,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Fri 9am – 6pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "2", Slug = "priya-sharma-esq", FullName = "Priya Sharma",
            Title = "Immigration Attorney", CategoryId = "immigration-attorney", CategoryName = "Immigration Attorney",
            YearsOfExperience = 8, LicenseNumber = "VA-62105",
            Languages = """["Nepali","English","Hindi"]""",
            Specialties = """["Student visas","DACA","Family immigration"]""",
            Phone = "(571) 555-0187", Email = "priya@sharmaimmigration.com", Website = (string?)null,
            ServiceArea = "Fairfax, Reston, Herndon",
            Bio = "As an immigration attorney who came to the U.S. on a student visa myself, I deeply understand the fears and uncertainties that come with navigating immigration law. I specialize in family-based immigration, student visas, and naturalization proceedings. My goal is to be the attorney I wish I had when I first arrived.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.7, ReviewCount = 19,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Fri 9am – 5pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "3", Slug = "arun-karmacharya-cpa", FullName = "Arun Karmacharya",
            Title = "Certified Public Accountant", CategoryId = "cpa-accountant", CategoryName = "CPA / Accountant",
            YearsOfExperience = 15, LicenseNumber = "VA-39821",
            Languages = """["Nepali","English"]""",
            Specialties = """["Business accounting","Tax planning","Small business"]""",
            Phone = "(703) 555-0245", Email = "arun@karmacharya-cpa.com", Website = "https://karmacharya-cpa.com",
            ServiceArea = "Fairfax, Springfield, Alexandria",
            Bio = "I have been serving the Nepali community's financial needs for 15 years. Whether you are filing personal taxes, setting up a small business, or planning for retirement, I bring deep expertise and a community-first approach. I work with many small business owners, healthcare professionals, and families in the DMV area.",
            PhotoUrl = (string?)null, IsSponsored = true, IsVerified = true, Rating = 4.8, ReviewCount = 47,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Sat 9am – 6pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "4", Slug = "sushma-pradhan-cpa", FullName = "Sushma Pradhan",
            Title = "Tax Consultant & CPA", CategoryId = "cpa-accountant", CategoryName = "CPA / Accountant",
            YearsOfExperience = 6, LicenseNumber = "VA-51204",
            Languages = """["Nepali","English","Hindi"]""",
            Specialties = """["Personal taxes","ITIN applications","Bookkeeping"]""",
            Phone = "(571) 555-0312", Email = "sushma@pradhan-tax.com", Website = (string?)null,
            ServiceArea = "Fairfax, Chantilly, Manassas",
            Bio = "I am passionate about helping Nepali families and small businesses stay financially healthy. I specialize in individual tax returns, ITIN applications, and small business bookkeeping. With clear communication in Nepali and English, I make complex tax matters easy to understand.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.6, ReviewCount = 12,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Fri 10am – 6pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "5", Slug = "bikas-shrestha-realtor", FullName = "Bikas Shrestha",
            Title = "Licensed Realtor", CategoryId = "realtor", CategoryName = "Realtor",
            YearsOfExperience = 10, LicenseNumber = "VA-230154",
            Languages = """["Nepali","English"]""",
            Specialties = """["Home buying","Home selling","Investment properties"]""",
            Phone = "(703) 555-0398", Email = "bikas@bikasrealty.com", Website = "https://bikasrealty.com",
            ServiceArea = "Fairfax County · Burke · Lorton",
            Bio = "Buying or selling a home is one of the biggest decisions of your life. As a realtor who has helped over 100 Nepali families find their dream homes in Northern Virginia, I understand the local market inside and out. I am fluent in Nepali and will be with you every step of the way — from the first showing to closing day.",
            PhotoUrl = (string?)null, IsSponsored = true, IsVerified = true, Rating = 5.0, ReviewCount = 61,
            LicenseState = "Virginia (active)", Brokerage = "Keller Williams Realty, Fairfax", IsAcceptingClients = true, WorkingHours = "Mon – Sat 8am – 7pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "6", Slug = "nisha-lama-realtor", FullName = "Nisha Lama",
            Title = "Real Estate Agent", CategoryId = "realtor", CategoryName = "Realtor",
            YearsOfExperience = 5, LicenseNumber = "VA-198762",
            Languages = """["Nepali","English","Maithili"]""",
            Specialties = """["First-time buyers","Rental properties","Condos"]""",
            Phone = "(571) 555-0421", Email = "nisha@nishalama.homes", Website = (string?)null,
            ServiceArea = "Centreville · Chantilly · Sterling",
            Bio = "I became a realtor because I saw how hard it was for Nepali families to navigate the homebuying process without a trusted guide. I specialize in first-time homebuyers and rental properties in Centreville and the surrounding areas. Let me help you make Northern Virginia your home.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.7, ReviewCount = 14,
            LicenseState = "Virginia (active)", Brokerage = "RE/MAX Gateway", IsAcceptingClients = true, WorkingHours = "Mon – Sun 9am – 7pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "7", Slug = "dr-arun-shrestha-md", FullName = "Dr. Arun Shrestha",
            Title = "Primary Care Physician", CategoryId = "primary-care-doctor", CategoryName = "Primary Care Doctor",
            YearsOfExperience = 20, LicenseNumber = "VA-062144",
            Languages = """["Nepali","English"]""",
            Specialties = """["Preventive care","Chronic disease","Diabetes care"]""",
            Phone = "(703) 555-0510", Email = "dr.shrestha@fairfaxcare.com", Website = "https://fairfaxcare.com",
            ServiceArea = "Fairfax · Falls Church · Annandale",
            Bio = "I am a board-certified primary care physician serving patients in Northern Virginia for 20 years. My practice welcomes all patients, but I take particular pride in serving the Nepali community with culturally informed, compassionate care. I offer annual physicals, chronic disease management, preventive care, and same-day sick visits.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.9, ReviewCount = 88,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Fri 8am – 5pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "8", Slug = "dr-sunita-rai-md", FullName = "Dr. Sunita Rai",
            Title = "Family Medicine Physician", CategoryId = "primary-care-doctor", CategoryName = "Primary Care Doctor",
            YearsOfExperience = 14, LicenseNumber = "VA-054891",
            Languages = """["Nepali","English","Hindi"]""",
            Specialties = """["Family medicine","Pediatric care","Women's health"]""",
            Phone = "(571) 555-0578", Email = "dr.rai@suncareclinic.com", Website = "https://suncareclinic.com",
            ServiceArea = "Reston · Herndon · Sterling",
            Bio = "As a family medicine physician, I care for patients of all ages — from newborns to seniors. I believe that good health care starts with trust and clear communication. Speaking Nepali, English, and Hindi allows me to connect meaningfully with a wide range of patients. I am currently accepting new patients at my Reston clinic.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.8, ReviewCount = 42,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Fri 8am – 5pm, Sat 9am – 1pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "9", Slug = "deepak-gurung-electric", FullName = "Deepak Gurung",
            Title = "Licensed Electrician", CategoryId = "home-services", CategoryName = "Home Services",
            YearsOfExperience = 9, LicenseNumber = "VA-43219",
            Languages = """["Nepali","English"]""",
            Specialties = """["Electrical repairs","Panel upgrades","EV chargers"]""",
            Phone = "(703) 555-0634", Email = "deepak@gurungelectric.com", Website = (string?)null,
            ServiceArea = "Fairfax · Burke · Springfield",
            Bio = "I am a licensed electrician with 9 years of experience in residential and light commercial electrical work. From panel upgrades and EV charger installations to troubleshooting and repairs, I do it right the first time. I am fully insured, licensed in Virginia, and proud to serve my Nepali community with honest, reliable service.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.8, ReviewCount = 27,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = true, WorkingHours = "Mon – Sat 7am – 6pm"
        });

        Insert.IntoTable(Tables.Professionals).Row(new
        {
            Id = "10", Slug = "binod-tamang-plumbing", FullName = "Binod Tamang",
            Title = "Plumber & HVAC Technician", CategoryId = "home-services", CategoryName = "Home Services",
            YearsOfExperience = 11, LicenseNumber = "VA-28417",
            Languages = """["Nepali","English"]""",
            Specialties = """["Plumbing","HVAC","Water heaters"]""",
            Phone = "(571) 555-0689", Email = "binod@tamangplumbing.com", Website = "https://tamangplumbing.com",
            ServiceArea = "Fairfax · Burke · Springfield · Lorton",
            Bio = "With 11 years in plumbing and HVAC, I have seen and fixed it all — from a leaky faucet to a full HVAC replacement. I am licensed, bonded, and insured in Virginia. I offer free estimates and emergency service. I take pride in transparent pricing and clean, lasting work. Many of my customers have been with me for years.",
            PhotoUrl = (string?)null, IsSponsored = false, IsVerified = true, Rating = 4.7, ReviewCount = 33,
            LicenseState = "Virginia (active)", Brokerage = (string?)null, IsAcceptingClients = false, WorkingHours = "Mon – Fri 8am – 5pm"
        });
    }

    public override void Down()
    {
        for (var i = 1; i <= 10; i++)
            Delete.FromTable(Tables.Professionals).Row(new { Id = i.ToString() });
    }
}
