using Shared.Database;

namespace Shared.Models
{
    public class RegistrationModel : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Town { get; set; }
    }
}
