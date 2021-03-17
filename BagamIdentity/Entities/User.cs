using System.Text.Json.Serialization;

namespace BagamIdentity.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
