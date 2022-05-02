using System;
using System.Collections.Generic;

namespace OWTChallenge.Core.Entities
{
    public partial class ContactSkillRel
    {
        public int ContactId { get; set; }
        public int SkillId { get; set; }
        public int LevelId { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual Level Level { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
