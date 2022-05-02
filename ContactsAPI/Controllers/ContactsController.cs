#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OWTChallenge.Core.Entities;
using OWTChallenge.Core.Interfaces;
using OWTChallenge.API.Models;

namespace OWTChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public ContactsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets a list of all Contacts. For each contact, the list of their skill with the level they achieved.
        /// </summary>
        /// <returns>List of extended Contacts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CompleteContactDTO), 200)]
        public async Task<ActionResult<IEnumerable<CompleteContactDTO>>> GetContacts()
        {
            var listContacts = await unitOfWork.ContactRepo.GetContactsWithSkillLevelAsync();

            var listContactDTOs = listContacts.Select(c => CompleteContactDTO.getfromContact(c)).ToList();

            return Ok(listContactDTOs);
        }

        /// <summary>
        ///  Return a contact by its Id.
        /// </summary>
        /// <remarks>The contact is complete : skills and levels are attached.</remarks>
        /// <param name="id">Id of the contact we are getting.</param>   
        /// <response code="200">Contact has been selected successfully!</response>
        /// <response code="404">Contact is not found!</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompleteContactDTO), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await unitOfWork.ContactRepo.GetContactByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            var dto = CompleteContactDTO.getfromContact(contact);
            return Ok(dto);
        }

        /// <summary>
        ///  Update a contact, see specification
        /// </summary>
        /// <remarks>Update the contact and it's skills/levels when given a list of skill-Level objects. If the list is empty
        /// then the Skills are removed, if there is no skill-level list at all then we ignore the skills and update only the contact</remarks>
        /// <param name="contact">The contact to be updated.</param>   
        /// <response code="200">The Contact has been updated successfully!</response>
        /// <response code="500">Oups, an error has occured!</response>
        /// <response code="404">Targeted contact has not been found!</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CompleteContactDTO), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateContact(int id, UpdateContactDTO contact)
        {
            var updatedContact = await unitOfWork.ContactRepo.GetContactByIdAsync(id);
            if (updatedContact == null)
            {
                return NotFound();
            }
            try
            {
                
                updatedContact.FirstName = contact.FirstName;
                updatedContact.LastName = contact.LastName;
                updatedContact.PhoneNumber = contact.PhoneNumber;
                updatedContact.Email = contact.Email;
                updatedContact.Address = contact.Address;

                // IF there is a list of skill/level given in parameters => we clear the old one and replace it by the 
                // new one even if empty (we can delete a relationship here ...)
                // ELSE there is no list of skill/level given in parameters => we don't touch the relationships, only Update the Contact 
                if (contact.SkillLevelsList != null)
                {
                    //TODO: Check if there is no doublon for the skills
                    updatedContact.ContactSkillRels.Clear();
                    var contactSkillRelList = contact.SkillLevelsList.Select(r => new ContactSkillRel { 
                        LevelId = r.LevelId.Value, 
                        SkillId = r.SkillId.Value, 
                        ContactId = id })                                                              
                        .ToList();
                    updatedContact.ContactSkillRels = contactSkillRelList;
                }
                unitOfWork.Save();
                
                
                // We need to upload the whole object with attached relations

                var fullContact = await unitOfWork.ContactRepo.GetContactByIdAsync(id);
                var updatedContactDTO = CompleteContactDTO.getfromContact(fullContact);
                return Ok(updatedContactDTO);
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }

            //_context.Entry(contact).State = EntityState.Modified;

           

            return NoContent();
        }

        /// <summary>
        ///  Create a new Contact.
        /// </summary>
        /// <param name="contact">The contact that will be created.</param>   
        /// <response code="201">The contact has been created successfully!</response>
        /// <response code="400">The contact properties should not be null or empty !</response>
        /// <response code="500">Oups, an error has occured</response>
        [HttpPost]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> PostContact(UpdateContactDTO contact)
        {
            if (contact.IsMandatoryPropNull())
                return BadRequest();

            //TODO : Check for doublon => contact already exists !

            try
            {
                var newContact = new Contact();
                newContact.FirstName = contact.FirstName;
                newContact.LastName = contact.LastName; 
                newContact.PhoneNumber = contact.PhoneNumber;
                newContact.Email = contact.Email;
                newContact.Address = contact.Address;
                   
                var createdContact = await unitOfWork.ContactRepo.AddAsync(newContact);
                unitOfWork.Save();
                return CreatedAtAction("GetContact", new { id = createdContact.Id }, createdContact); ;

            }
            catch (Exception ex)
            {
                return Problem("An error has occured. The Skill could not be updated, see exception :  " + ex.Message);
            }
        }

        /// <summary>
        ///  Delete a Contact by its Id.
        /// </summary>
        /// <param name="id">The id of the contact we would like to delete.</param>   
        /// <response code="204">contact has been deleted successfully!</response>
        /// <response code="404">Targeted contact has not been found!</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await unitOfWork.ContactRepo.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            //Delete first the link between the Contact and the Skills
            if (contact.ContactSkillRels !=null && contact.ContactSkillRels.Any())
            {
                 contact.ContactSkillRels.Clear();
                unitOfWork.Save();
            }
            await unitOfWork.ContactRepo.DeletedAsync(contact);
            
            return NoContent();
        }

        //    private bool ContactExists(int id)
        //    {
        //        return _context.Contacts.Any(e => e.Id == id);
        //    }
    }
}
