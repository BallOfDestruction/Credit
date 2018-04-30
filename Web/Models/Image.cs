using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Controllers.cms;

namespace Web.Models
{
    [Access(false, false, false, false)]
    public class Image : Entity<Image>
    {
        [DataType(DataType.ImageUrl)]
        [DisplayName("Ссылка на диске")]
        public string Url { get; set; }

        public string ImageType { get; set; }

        public string Extension { get; set; }
    }
}
