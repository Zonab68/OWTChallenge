using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.API.Models;

public class ContactSkillLevelDTO
{
    public int? ContactId { get; set; }  
    public string? ContactName { get; set; }
    public int? LevelId { get; set; }    
    public string? LevelName { get; set; }   
    public int? SkillId { get; set; }    
    public string? SkillName { get; set; }


    public static ContactSkillLevelDTO ToContactSkillLevelDTO(ContactSkillRel item)
    {
        return new ContactSkillLevelDTO()
        {
            ContactId = item.ContactId,
            ContactName = item.Contact.FullName,
            LevelId = item.LevelId,
            LevelName = item.Level.Name,
            SkillId = item.SkillId,
            SkillName = item.Skill.Name
        };
    }
}
