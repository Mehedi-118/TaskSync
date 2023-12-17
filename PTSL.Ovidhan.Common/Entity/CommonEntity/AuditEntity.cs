using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PTSL.Ovidhan.Common.Entity.CommonEntity
{
    public abstract class AuditEntity
    {
        public AuditEntity()
        {
            this.CreateDate = DateTime.Now;
            this.IsRemoved = false;
            this.IsActive = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(200)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsActive { get; set; }
    }

    public interface IAuditEntity
    {
        DateTime CreateDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
        bool IsRemoved { get; set; }
        bool IsActive { get; set; }
    }
}
