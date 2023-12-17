using System.ComponentModel.DataAnnotations;

namespace PTSL.Ovidhan.Web.Core.Helper.Enum;

public enum Cadre
{
    //[Display(Name = "Non-Cadre")] NonCadre = 1,
    //[Display(Name = "Food")] Food = 2,
    [Display(Name = "Cadre")]
    Cadre = 1,
    [Display(Name = "Non-Cadre")]
    NonCadre = 2
}
