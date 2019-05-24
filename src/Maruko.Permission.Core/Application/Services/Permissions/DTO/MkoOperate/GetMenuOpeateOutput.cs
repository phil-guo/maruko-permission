using Maruko.AutoMapper.AutoMapper;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate
{
    [AutoMap(typeof(Domain.Permissions.MkoOperate))]
    public class GetMenuOpeateOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
