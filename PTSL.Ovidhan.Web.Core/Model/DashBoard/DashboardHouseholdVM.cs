namespace PTSL.Ovidhan.Web.Core.Model.DashBoard;

public class DashboardHouseholdVM
{
    public int TotalHouseholdMember { get; set; }
    public List<HouseConditionPercentage> HouseConditionPercentages { get; set; } = new List<HouseConditionPercentage>();
}

public class HouseConditionPercentage
{
    public string HouseTypeName { get; set; } = string.Empty;
    public double Percentage { get; set; }
}