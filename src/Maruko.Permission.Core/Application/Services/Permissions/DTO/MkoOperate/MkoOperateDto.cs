//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：05-23-2019  
//版本1.0
//===================================================================================

using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;

namespace Maruko.Permission.Core.Application.Services.Permissions.DTO.MkoOperate
{
    [AutoMap(typeof(Domain.Permissions.MkoOperate))]
    public class MkoOperateDto : EntityDto
    {
        /// <summary>
        /// 名称不能为空
        /// </summary>
        //[TryValidateNullOrEmptyModel(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        public string Remark { get; set; }

        public int Unique { get; set; }
    }
}