using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.SharedKernel.Interfaces;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.Core.Interfaces;


/// <summary>
/// Repository to manage Skills
/// </summary>
/// 
public interface ISkillRepository : IGenericRepository<Skill>
{
    /// <summary>
    /// Gets all skills with specification : include Contacts and Levels
    /// </summary>
    /// <returns>List of skills and related contacts and levels</returns>
    Task<IEnumerable<Skill>> GetSkillsWithContactLevelAsync();

    /// <summary>
    /// Gets one skill by its Id. Specification : include Contacts and Levels
    /// </summary>
    /// <param name="id"></param>        
    /// <returns>Skill having Id = id</returns>
    Task<Skill> GetSkillByIdAsync(int id);

    //Task<bool> SkillExists(int id);
}

