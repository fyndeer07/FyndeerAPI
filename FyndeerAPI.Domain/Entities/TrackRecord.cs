namespace FyndeerAPI.Domain.Entities;

public class TrackRecord
{
    public string ProfessionalId { get; private set; } = string.Empty;
    public int HomesSold { get; private set; }
    public int AvgDaysOnMarket { get; private set; }
    public double SaleToListRatio { get; private set; }
    public decimal PriceRangeMin { get; private set; }
    public decimal PriceRangeMax { get; private set; }
    public List<string> PrimaryCounties { get; private set; } = [];
    public string CommunityClients { get; private set; } = string.Empty;

    private TrackRecord() { }

    public static TrackRecord Create(
        string professionalId,
        int homesSold,
        int avgDaysOnMarket,
        double saleToListRatio,
        decimal priceRangeMin,
        decimal priceRangeMax,
        List<string> primaryCounties,
        string communityClients)
    {
        return new TrackRecord
        {
            ProfessionalId = professionalId,
            HomesSold = homesSold,
            AvgDaysOnMarket = avgDaysOnMarket,
            SaleToListRatio = saleToListRatio,
            PriceRangeMin = priceRangeMin,
            PriceRangeMax = priceRangeMax,
            PrimaryCounties = primaryCounties,
            CommunityClients = communityClients
        };
    }

    public void Update(
        int homesSold,
        int avgDaysOnMarket,
        double saleToListRatio,
        decimal priceRangeMin,
        decimal priceRangeMax,
        List<string> primaryCounties,
        string communityClients)
    {
        HomesSold = homesSold;
        AvgDaysOnMarket = avgDaysOnMarket;
        SaleToListRatio = saleToListRatio;
        PriceRangeMin = priceRangeMin;
        PriceRangeMax = priceRangeMax;
        PrimaryCounties = primaryCounties;
        CommunityClients = communityClients;
    }
}
