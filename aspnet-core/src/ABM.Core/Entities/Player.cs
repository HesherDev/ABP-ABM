using Abp.Domain.Entities;

namespace ABM.Entities
{
    public class Player : Entity
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

    }
}
