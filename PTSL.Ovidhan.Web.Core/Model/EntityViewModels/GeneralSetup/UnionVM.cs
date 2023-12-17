using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Model.GeneralSetup
{
    public class UnionVM : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string NameBn { get; set; }
        public long UpazillaId { get; set; }
        public UpazillaVM? Upazilla { get; set; }

    }
}
