using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.SharedKernel.Interfaces;
using OWTChallenge.Infrastructure.Data;
using OWTChallenge.Core.Entities;
using OWTChallenge.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OWTChallenge.Infrastructure.Repositories;

class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(OWTChallengeContext context) : base(context) { }

    public async Task<IEnumerable<Skill>> GetSkillsWithContactLevelAsync()
    {
        var result = await context.Set<Skill>()
            .Include(o => o.ContactSkillRels).ThenInclude(C => C.Contact)
            .Include(o => o.ContactSkillRels).ThenInclude(l => l.Level)
            .ToListAsync();
        return result;
    }

    public async Task<Skill> GetSkillByIdAsync(int id)
    {
        var result = await context.Set<Skill>()
                .Include(o => o.ContactSkillRels).ThenInclude(o => o.Contact)
                .Include(o => o.ContactSkillRels).ThenInclude(o => o.Level)
                .FirstOrDefaultAsync(o => o.Id == id);

        return result;
    }




}



