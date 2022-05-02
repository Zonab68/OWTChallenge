using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.Core.Entities;

namespace OWTChallenge.API.Models;

public class ContactLevelDTO
{
    public int? ContactId { get; set; }  
    public string? ContactName { get; set; }
    public int? LevelId { get; set; }    
    public string? LevelName { get; set; }   


    public static ContactLevelDTO ToContactLevelDTO(ContactSkillRel item)
    {
        return new ContactLevelDTO()
        {
            ContactId = item.ContactId,
            ContactName = item.Contact.FullName,
            LevelId = item.LevelId,
            LevelName = item.Level.Name
        };
    }
}
