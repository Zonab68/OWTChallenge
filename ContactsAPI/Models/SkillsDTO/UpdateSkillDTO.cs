namespace OWTChallenge.API.Models
{
    public class UpdateSkillDTO
    {
        public UpdateSkillDTO(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
