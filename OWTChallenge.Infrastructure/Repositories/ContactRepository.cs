using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.Core.Entities;
using OWTChallenge.Core.Interfaces;
using OWTChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OWTChallenge.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(OWTChallengeContext context) : base(context) { }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var result = await context.Set<Contact>()
                .Include(o => o.ContactSkillRels).ThenInclude(o=> o.Skill)
                .Include(o=>o.ContactSkillRels).ThenInclude(o=>o.Level)
                .FirstOrDefaultAsync(o => o.Id == id);

            return result;
        }

        public async Task<IEnumerable<Contact>> GetContactsWithSkillLevelAsync()
        {
            var result = await context.Set<Contact>()
                .Include(o => o.ContactSkillRels).ThenInclude(o => o.Skill)
                .Include(o => o.ContactSkillRels).ThenInclude(o => o.Level)
                .ToListAsync();

            return result;
        }


        //public async Task<IEnumerable<Contact>> SetSkillLevelToContact(int skillId, int levelId, Contact contact) 
        //{
        //    contact.
            
        //}
    }
}