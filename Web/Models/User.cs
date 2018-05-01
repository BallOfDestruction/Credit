using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Controllers.cms;

namespace Web.Models
{
    [Access(false)]
    public class User : Entity<User>
    {
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string SecondName { get; set; }

        [DisplayName("Town")]
        public string Town { get; set; }

        [Show(false, false, true, true)]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Show(false, false, false, false)]
        public override string Title => $"{Email}";

        [Show(false,false,false,false)]
        public List<TokenModel> TokenModels { get; set; }

        [Show(false, false, false, false)]
        public List<Credit> Credits { get; set; }
    }
}
