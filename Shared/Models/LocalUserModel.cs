using System;
using Shared.Commands.Login;
using Shared.Database;

namespace Shared.Models
{
    public class LocalUserModel : Entity
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Town { get; set; }
        public string Token { get; set; }
        public DateTime AutorizationTime { get; set; }

        public LocalUserModel()
        {
            
        }

        public LocalUserModel(LoginResponceViewModel model)
        {
            Name = model.Name;
            Email = model.Email;
            Town = model.Town;
            Token = model.Token;
            SecondName = model.SecondName;
            AutorizationTime = DateTime.Now;
        }
    }
}
