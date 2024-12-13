using ABM.Entities;
using ABM.Teams.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABM.Teams
{
    public class TeamAppService : CrudAppService<Team, TeamDto>, ITeamAppService
    {
        public TeamAppService(IRepository<Team, int> repository) : base(repository)
        {
        }
    }
}
