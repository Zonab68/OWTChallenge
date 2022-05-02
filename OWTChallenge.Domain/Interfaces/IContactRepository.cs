using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.Core.Entities;
using OWTChallenge.SharedKernel.Interfaces;

namespace OWTChallenge.Core.Interfaces;

public interface IContactRepository : IGenericRepository<Contact>
{
    Task<IEnumerable<Contact>> GetContactsWithSkillLevelAsync();
    Task<Contact> GetContactByIdAsync(int id);
}
