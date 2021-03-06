using System;
using System.Collections.Generic;

namespace OWTChallenge.Core.Entities
{
    public partial class Level
    {
        public Level()
        {
            ContactSkillRels = new HashSet<ContactSkillRel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ContactSkillRel> ContactSkillRels { get; set; }
    }
}
