using EntityFrameworkCore.Triggers;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PTSL.Ovidhan.Common.Entity.CommonEntity
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 0, TypeName = "bigint")]
        public long Id { get; set; }

        [Column("CreatedAt", Order = 1, TypeName = "datetime2 (3)")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt", Order = 2, TypeName = "datetime2 (3)")]
        public DateTime? UpdatedAt { get; set; }

        [Column("DeletedAt", Order = 3, TypeName = "datetime2 (3)")]
        public DateTime? DeletedAt { get; set; }

        [Column("IsDeleted", Order = 4, TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Column("IsActive", Order = 5, TypeName = "bit")]
        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }

        static BaseEntity() // Must be static for the triggers to be registered only once
        {
            Triggers<BaseEntity>.Inserting += entry =>
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = false;
                entry.Entity.IsActive = true;
            };

            Triggers<BaseEntity>.Updating += entry =>
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            };


            Triggers<BaseEntity>.Deleting += entry =>
            {
                entry.Entity.DeletedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
                entry.Cancel = true;
            };
        }
    }
}
