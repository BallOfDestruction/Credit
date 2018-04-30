namespace Web.ViewModels.Registration
{
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Town { get; set; }

        public RegistrationRequest()
        {
            
        }

        public RegistrationRequest(string name, string secondName, string email, string password, string town)
        {
            Name = name;
            SecondName = secondName;
            Email = email;
            Password = password;
            Town = town;
        }
    }
}
