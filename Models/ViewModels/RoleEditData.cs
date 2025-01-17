using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SODV3201_LibMgtSys.Models.ViewModels
{
    public class RoleEditData
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }

    }
}