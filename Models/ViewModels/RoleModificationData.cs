using System.ComponentModel.DataAnnotations;

namespace SODV3201_LibMgtSys.Models.ViewModels
{
    public class RoleModificationData
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}