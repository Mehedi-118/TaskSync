using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Model.GeneralSetup
{
    public class DegreeVM : BaseModel
    {
        [MaxLength(100)]
        public string DegreeName { get; set; }
    }
}
