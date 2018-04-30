using System;
using Shared.WebService;

namespace Shared.Commands.Registration
{
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Town { get; set; }

        public RegistrationRequest(string name, string secondName, string email, string password, string town)
        {
            Name = name;
            SecondName = secondName;
            Email = email;
            Password = password;
            Town = town;
        }

        public bool IsValid(Action<Error> error)
        {
            if (string.IsNullOrEmpty(Email))
            {
                error?.Invoke(new Error("Ошибка", "Введите email"));
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                error?.Invoke(new Error("Ошибка", "Введите пароль"));
                return false;
            }

            if (string.IsNullOrEmpty(Name))
            {
                error?.Invoke(new Error("Ошибка", "Введите имя"));
                return false;
            }

            if (string.IsNullOrEmpty(SecondName))
            {
                error?.Invoke(new Error("Ошибка", "Введите фамилию"));
                return false;
            }

            if (string.IsNullOrEmpty(Town))
            {
                error?.Invoke(new Error("Ошибка", "Введите город"));
                return false;
            }

            return true;
        }
    }
}
