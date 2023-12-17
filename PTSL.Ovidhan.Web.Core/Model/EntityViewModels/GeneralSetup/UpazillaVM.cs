namespace PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

public class UpazillaVM : BaseModel
{
    public string Name { get; set; }
    public string NameBn { get; set; }

    public long DivisionId { get; set; }
    public DivisionVM? Division { get; set; }

    public long DistrictId { get; set; }
    public DistrictVM? District { get; set; }
    public List<UnionVM>? Unions { get; set; }
}
