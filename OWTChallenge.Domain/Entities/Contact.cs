using System;
using System.Collections.Generic;

namespace OWTChallenge.Core.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            ContactSkillRels = new HashSet<ContactSkillRel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<ContactSkillRel> ContactSkillRels { get; set; }

        public string FullName
        {
            get
            {
                return (this.FirstName + " " + this.LastName);
            }
           
        }
    }
}
