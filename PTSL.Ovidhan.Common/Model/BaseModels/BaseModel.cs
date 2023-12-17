using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Model.BaseModels
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; } = true;
    }

    public class DropdownVM
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DropdownLongVM
    {
        public string Id { get; set; }
        public string? Name { get; set; } = string.Empty;
    }
}
