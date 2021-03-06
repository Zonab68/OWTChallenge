using OWTChallenge.Core.Entities;
using System;
using System.Collections.Generic;


namespace OWTChallenge.API.Models;

public class CompleteContactDTO : CreateContactDTO
{
    public CompleteContactDTO(int id, string firstName, string lastName, string address, string email, string phoneNumber, List<SkillLevelDTO> skillLevels) : base(firstName, lastName, address, email, phoneNumber)
    {
        Id = id;
        SkillLevels = skillLevels ?? new List<SkillLevelDTO>();
        FullName = firstName + " " + lastName;
    }

    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public List<SkillLevelDTO> SkillLevels { get; set; }



    public static CompleteContactDTO getfromContact(Contact contact) 
    {
        var result = new CompleteContactDTO(
                id: contact.Id,
                firstName: contact.FirstName,
                lastName: contact.LastName,
                address: contact.Address,
                email: contact.Email,
                phoneNumber: contact.PhoneNumber,
                skillLevels: new List<SkillLevelDTO>
                                    (
                                        contact.ContactSkillRels.Select(i => SkillLevelDTO.ToSkillLevelDTO(i)).ToList()
                                    )
                );
        return result;
    }
}

// Creation DTOs should not include an ID if the ID will be generated by the back end
public abstract class CreateContactDTO
{
    protected CreateContactDTO(string firstName, string lastName, string address, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

}