namespace OWTChallenge.API.Models
{
    public class UpdateContactDTO
    {
        public UpdateContactDTO(string firstName, string lastName, string address, string email, string phoneNumber, List<SkillLevelDTO>? skillLevelsList)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            SkillLevelsList = skillLevelsList;
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public List<SkillLevelDTO>? SkillLevelsList { get; set; } = null!;


        public bool IsMandatoryPropNull()
        {
            bool result = false;

            if (string.IsNullOrEmpty(this.FirstName) ||
                string.IsNullOrEmpty(this.LastName) ||
                string.IsNullOrEmpty(this.Address) ||
                string.IsNullOrEmpty(this.Email) ||
                string.IsNullOrEmpty(this.PhoneNumber))
                result = true;


            return result;
        }


    }
}
