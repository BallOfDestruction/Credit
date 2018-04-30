using System;
using Shared.WebService;

namespace Shared.Commands.Login
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public bool IsValid(Action<Error> error)
        {
            if (string.IsNullOrEmpty(Email))
            {
                error?.Invoke(new Error("Ошибка","Введите email"));
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                error?.Invoke(new Error("Ошибка", "Введите пароль"));
                return false;
            }

            return true;
        }
    }
}
