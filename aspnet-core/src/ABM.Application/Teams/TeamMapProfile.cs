using ABM.Entities;
using ABM.Teams.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABM.Teams
{
    internal class TeamMapProfile : Profile
    {
        public TeamMapProfile ()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
        }
    }
}
