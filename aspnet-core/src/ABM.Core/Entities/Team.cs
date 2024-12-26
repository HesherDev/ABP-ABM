using Abp.Domain.Entities;
using System.Collections.Generic;


namespace ABM.Entities
{
    public class Team : Entity
    { 
    
        public string Name { get; set; }

        public IList<Player> Players { get; set; } = new List<Player>();
    }
}

