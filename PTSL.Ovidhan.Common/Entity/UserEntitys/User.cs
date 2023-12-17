using System;

using Microsoft.AspNetCore.Identity;

using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;

namespace PTSL.Ovidhan.Common.Entity
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        //public string? UserName { get; set; }
        //public string? Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public List<Todo>? Tasks { get; set; } = new List<Todo>();
        public List<Category>? Categories { get; set; } = new List<Category>();

    }
}
