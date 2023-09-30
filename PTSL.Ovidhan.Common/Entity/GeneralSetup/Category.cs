using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.Tasks;

namespace PTSL.Ovidhan.Common.Entity.GeneralSetup
{
    public class Category : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
        public string logo { get; set; } = string.Empty;
        public List<Todo> Todos { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
