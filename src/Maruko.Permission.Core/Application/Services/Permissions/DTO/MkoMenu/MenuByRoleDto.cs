using System.Collections.Generic;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu
{
    public class MenuByRoleDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public string Path { get; set; }

        public List<MenuByRoleDto> Children { get; set; } = new List<MenuByRoleDto>();

        public string Operates { get; set; }
    }
}
