using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum Cadre
    {
        [Display(Name = "Cadre")]
        Cadre = 1,
        [Display(Name = "Non-Cadre")]
        NonCadre = 2
    }
}
