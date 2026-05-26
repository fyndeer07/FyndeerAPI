using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260007, "Seed Reviews")]
public class Migration202604260007_SeedReviews : Migration
{
    public override void Up()
    {
        // Professional 1 — Rajesh Acharya
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r1", ProfessionalId = "1", ReviewerName = "Anita Shrestha", IsVerified = true, Rating = 5.0, Comment = "Rajesh helped my entire family get their green cards. He was patient, thorough, and always available to answer our questions.", CreatedAt = new DateTime(2025, 11, 10) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r2", ProfessionalId = "1", ReviewerName = "Bikram Thapa", IsVerified = true, Rating = 5.0, Comment = "Excellent attorney. Got my citizenship application approved on the first try. Highly recommended.", CreatedAt = new DateTime(2025, 9, 4) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r3", ProfessionalId = "1", ReviewerName = "Manisha Gurung", IsVerified = false, Rating = 4.5, Comment = "Very knowledgeable and responsive. Helped us with our H-1B extension with no issues.", CreatedAt = new DateTime(2025, 7, 20) });

        // Professional 2 — Priya Sharma
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r4", ProfessionalId = "2", ReviewerName = "Suman Rai", IsVerified = true, Rating = 5.0, Comment = "Priya understood exactly what we needed and made the whole process smooth. She speaks Nepali which was a huge relief.", CreatedAt = new DateTime(2025, 12, 1) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r5", ProfessionalId = "2", ReviewerName = "Deepa Karki", IsVerified = true, Rating = 4.5, Comment = "Professional and compassionate. Helped my husband with his work permit renewal quickly.", CreatedAt = new DateTime(2025, 10, 15) });

        // Professional 3 — Arun Karmacharya
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r6", ProfessionalId = "3", ReviewerName = "Geeta Lama", IsVerified = true, Rating = 5.0, Comment = "Arun has been our family CPA for 8 years. He is meticulous, honest, and always gets us the best return.", CreatedAt = new DateTime(2025, 4, 5) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r7", ProfessionalId = "3", ReviewerName = "Nabin Pradhan", IsVerified = true, Rating = 5.0, Comment = "Set up my LLC and handled all the accounting. Made it very easy for me as a first-time business owner.", CreatedAt = new DateTime(2025, 3, 18) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r8", ProfessionalId = "3", ReviewerName = "Rina Basnet", IsVerified = false, Rating = 4.5, Comment = "Very professional. Has been doing our taxes for years and we always leave satisfied.", CreatedAt = new DateTime(2025, 2, 28) });

        // Professional 4 — Sushma Pradhan
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r9", ProfessionalId = "4", ReviewerName = "Kamal Thapa", IsVerified = true, Rating = 5.0, Comment = "Sushma helped me file taxes for the first time after moving to the US. She explained everything clearly in Nepali.", CreatedAt = new DateTime(2025, 5, 10) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r10", ProfessionalId = "4", ReviewerName = "Nirmala KC", IsVerified = false, Rating = 4.5, Comment = "Great service, fair pricing, and always responds quickly.", CreatedAt = new DateTime(2025, 4, 22) });

        // Professional 5 — Bikas Shrestha
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r11", ProfessionalId = "5", ReviewerName = "Sita Tamang", IsVerified = true, Rating = 5.0, Comment = "Bikas found us our dream home in Burke within 3 weeks. He knew exactly what we needed and never pressured us.", CreatedAt = new DateTime(2025, 8, 12) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r12", ProfessionalId = "5", ReviewerName = "Raju Adhikari", IsVerified = true, Rating = 5.0, Comment = "Sold our townhouse above asking price in 2 days. Bikas is the best realtor in the Nepali community.", CreatedAt = new DateTime(2025, 6, 30) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r13", ProfessionalId = "5", ReviewerName = "Kamala Subedi", IsVerified = true, Rating = 5.0, Comment = "Excellent communicator, always available, and truly cares about his clients.", CreatedAt = new DateTime(2025, 5, 14) });

        // Professional 6 — Nisha Lama
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r14", ProfessionalId = "6", ReviewerName = "Hari Bhandari", IsVerified = true, Rating = 5.0, Comment = "Nisha was incredibly patient with us as first-time buyers. She explained everything and made sure we understood every step.", CreatedAt = new DateTime(2025, 9, 25) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r15", ProfessionalId = "6", ReviewerName = "Puja Gautam", IsVerified = false, Rating = 4.5, Comment = "Very responsive and professional. Found us a great rental within our budget.", CreatedAt = new DateTime(2025, 7, 8) });

        // Professional 7 — Dr. Arun Shrestha
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r16", ProfessionalId = "7", ReviewerName = "Maya Gurung", IsVerified = true, Rating = 5.0, Comment = "Dr. Shrestha is the most thorough doctor I have seen. He takes his time and never makes you feel rushed.", CreatedAt = new DateTime(2025, 11, 2) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r17", ProfessionalId = "7", ReviewerName = "Bishal Poudel", IsVerified = true, Rating = 5.0, Comment = "Has been my family doctor for years. Wonderful bedside manner and very knowledgeable.", CreatedAt = new DateTime(2025, 9, 11) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r18", ProfessionalId = "7", ReviewerName = "Sarita Magar", IsVerified = false, Rating = 4.5, Comment = "Great doctor. It means a lot to speak to someone in Nepali about my health.", CreatedAt = new DateTime(2025, 7, 3) });

        // Professional 8 — Dr. Sunita Rai
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r19", ProfessionalId = "8", ReviewerName = "Indira Tamang", IsVerified = true, Rating = 5.0, Comment = "Dr. Rai is amazing with kids. My children actually look forward to their checkups.", CreatedAt = new DateTime(2025, 10, 20) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r20", ProfessionalId = "8", ReviewerName = "Ganesh Koirala", IsVerified = true, Rating = 4.5, Comment = "Very thorough and caring. She took the time to understand my full history before making any recommendations.", CreatedAt = new DateTime(2025, 8, 6) });

        // Professional 9 — Deepak Gurung
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r21", ProfessionalId = "9", ReviewerName = "Laxmi Shrestha", IsVerified = true, Rating = 5.0, Comment = "Deepak installed our EV charger and upgraded our panel in one day. Clean work, fair price, very professional.", CreatedAt = new DateTime(2025, 12, 5) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r22", ProfessionalId = "9", ReviewerName = "Prakash Ale", IsVerified = true, Rating = 4.5, Comment = "Diagnosed and fixed our electrical issue quickly. Very honest about what needed to be done.", CreatedAt = new DateTime(2025, 10, 28) });

        // Professional 10 — Binod Tamang
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r23", ProfessionalId = "10", ReviewerName = "Tulsi Rai", IsVerified = true, Rating = 5.0, Comment = "Binod replaced our water heater the same day we called. Very fair price and great work.", CreatedAt = new DateTime(2025, 11, 18) });
        Insert.IntoTable(Tables.Reviews).Row(new { Id = "r24", ProfessionalId = "10", ReviewerName = "Dawa Sherpa", IsVerified = true, Rating = 4.5, Comment = "Fixed our HVAC before a big snowstorm. Very grateful for the quick response and quality work.", CreatedAt = new DateTime(2025, 1, 14) });
    }

    public override void Down()
    {
        for (var i = 1; i <= 24; i++)
            Delete.FromTable(Tables.Reviews).Row(new { Id = $"r{i}" });
    }
}
