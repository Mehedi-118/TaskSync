using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup
{
    public class CategoryVM : BaseModel
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
        public string logo { get; set; } = string.Empty;
        public List<TodoVM> TodoVMs { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
