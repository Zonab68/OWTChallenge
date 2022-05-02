namespace OWTChallenge.API.Models
{
    public class SimpleSkillDTO : CreateSkillDTO
    {
        public SimpleSkillDTO(int id, string name) : base(name) 
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
