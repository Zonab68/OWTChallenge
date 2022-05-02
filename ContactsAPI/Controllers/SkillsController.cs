#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OWTChallenge.SharedKernel.Interfaces;
using OWTChallenge.Core.Entities;
using OWTChallenge.Core.Interfaces;
using OWTChallenge.API.Models;

namespace OWTChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;


        public SkillsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets a list of all Skills. For each skill, the list of contact having them and their level.
        /// </summary>
        /// <returns>List of extended Skills</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CompleteSkillDTO), 200)]
        public async Task<ActionResult<IEnumerable<CompleteSkillDTO>>> GetSkill()
        {
            var listSkills = await unitOfWork.SkillRepo.GetSkillsWithContactLevelAsync(); 

            var listSkillDTOs = listSkills.Select(s => new CompleteSkillDTO
            (
                id:s.Id, 
                name:s.Name,
                contactlevels: new List<ContactLevelDTO>
                                        (
                                            s.ContactSkillRels.Select(i => ContactLevelDTO.ToContactLevelDTO(i)).ToList()
                                        )
            ))
            .ToList();

            return Ok(listSkillDTOs);
        }

        /// <summary>
        ///  Return a skill by its id.
        /// </summary>
        /// <remarks>The skill is complete => contacts and levels are attached</remarks>
        /// <param name="id">id of the skill we are getting</param>   
        /// <response code="200">skill has been selected successfully</response>
        /// <response code="404">skill is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompleteSkillDTO), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<CompleteSkillDTO>> GetSkill(int id)
        {
            var skill = await unitOfWork.SkillRepo.GetSkillByIdAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            var result =  new CompleteSkillDTO(
                id: skill.Id,
                name: skill.Name,
                contactlevels: new List<ContactLevelDTO>
                                        (
                                            skill.ContactSkillRels.Select(i => ContactLevelDTO.ToContactLevelDTO(i)).ToList()
                                        )
                ); 

            return Ok(result);
        }

        /// <summary>
        ///  Update a skill name.
        /// </summary>
        /// <remarks>We need to update only the name</remarks>
        /// <param name="skill">A simple skill object</param>   
        /// <response code="200">skill has been updated successfully</response>
        /// <response code="500">Oups, an error has occured</response>
        /// <response code="404">Targeted skill has not been found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SimpleSkillDTO), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSkill(int id, UpdateSkillDTO skill)
        {
            var updatedSkill = await unitOfWork.SkillRepo.GetSkillByIdAsync(id);
            if (updatedSkill == null)
                return NotFound();
            try
            {
                
                updatedSkill.Name = skill.Name;
                unitOfWork.Save();

                var result = new SimpleSkillDTO(
                    id: updatedSkill.Id, 
                    name: updatedSkill.Name
                    );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("An error has occured. The Skill could not be updated, see exception :  " + ex.Message);
            }

           
        }

        /// <summary>
        ///  Create a new Skill.
        /// </summary>
        /// <param name="skill">A simple skill object</param>   
        /// <response code="201">skill has been created successfully</response>
        /// <response code="400">Oups, an error has occured</response>
        [HttpPost]
        public async Task<ActionResult<Skill>>  PostSkill(UpdateSkillDTO skill)
        {
            if (skill.Name == null)
                return BadRequest();

            try
            {
                var newSkill = new Skill();
                newSkill.Name = skill.Name;

                var createdSkill = await unitOfWork.SkillRepo.AddAsync(newSkill);

                unitOfWork.Save();

                return CreatedAtAction("GetSkill", new { id = createdSkill.Id }, createdSkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        /// <summary>
        ///  Delete a Skill by its Id.
        /// </summary>
        /// <param name="id">The id of the skill we would like to delete</param>   
        /// <response code="204">skill has been deleted successfully</response>
        /// <response code="404">Targeted skill has not been found</response>
        /// <response code="409">Targeted skill is already used and canot be deleted!</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await unitOfWork.SkillRepo.GetSkillByIdAsync(id);
            if (skill == null)
                return NotFound();
            else if (skill.ContactSkillRels.Any()) 
            {
                return this.Conflict(new Exception("The Skill Id : " + skill.Id + " Name:"+ skill.Name +" is used by a contact and cannot be deleted."));
            }

            await unitOfWork.SkillRepo.DeletedAsync(skill);

            return NoContent();
        }

    }
}
