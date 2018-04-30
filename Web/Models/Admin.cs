using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Controllers.cms;

namespace Web.Models
{
    [Access(false)]
    public class Admin : Entity<Admin>
    {
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Show(false, false, true, true)]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Show(false, false, false, false)]
        public override string Title => $"{Login}";
    }
}
