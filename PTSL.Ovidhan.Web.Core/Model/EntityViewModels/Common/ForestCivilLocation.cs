namespace PTSL.Ovidhan.Web.Core.Model.EntityViewModels.Common
{
    public class ForestCivilLocationVM
    {
        // Forest Administration
        public long? ForestCircleId { get; set; }
        public long? ForestDivisionId { get; set; }
        public long? ForestRangeId { get; set; }
        public long? ForestBeatId { get; set; }
        public long? ForestFcvVcfId { get; set; }

        // Civil Administration
        public long? PresentDivisionId { get; set; }
        public long? PresentDistrictId { get; set; }
        public long? PresentUpazillaId { get; set; }
        public long? PresentUnionId { get; set; }

        public int? Take { get; set; }
    }
}
