using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Model.EntityViewModels
{
    public class UserRolesVM : BaseModel
    {
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
