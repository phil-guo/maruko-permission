using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoRole
{
    public class RolePermissionDto
    {
        public int MenuId { get; set; }

        public List<int> Operates { get; set; } = new List<int>();
    }
}
