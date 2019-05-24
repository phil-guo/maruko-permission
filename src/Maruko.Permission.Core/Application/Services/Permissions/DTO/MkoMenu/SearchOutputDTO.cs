using System.Collections.Generic;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoMenu
{
    public class SearchOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Level { get; set; }

        public Dictionary<int, string> OperatesDictionary { get; set; } = new Dictionary<int, string>();

        public string Operates { get; set; }

        public int ParentId { get; set; }

        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
