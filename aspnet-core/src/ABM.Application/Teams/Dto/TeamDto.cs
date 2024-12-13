using ABM.Entities;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;



namespace ABM.Teams.Dto
{
    [AutoMap(typeof(Team))]
    public class TeamDto : EntityDto
    {
        public string Name { get; set; }

    }
}
