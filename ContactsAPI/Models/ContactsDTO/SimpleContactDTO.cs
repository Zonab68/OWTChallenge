namespace OWTChallenge.API.Models
{
    public class SimpleContactDTO : CreateContactDTO
    {
        public SimpleContactDTO(int id, string firstName, string lastName, string address, string email, string phoneNumber) : base(firstName, lastName, address, email, phoneNumber)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
