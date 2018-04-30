using System.Collections.Generic;

namespace Web.ViewModels.Cms
{
    public class OneToMayImageViewModel
    {
        /// <summary>
        /// Список уже сущевствующих картинок
        /// </summary>
        public List<(int Id, string Url)> Images { get; set; }

        /// <summary>
        /// Id объекта к которому привязывается картинка
        /// </summary>
        public int IdObject { get; set; }

        /// <summary>
        /// Имя связанного свойства
        /// </summary>
        public string LinkPropertyName { get; set; }

        public OneToMayImageViewModel(List<(int Id, string Url)> images, int idObject, string linkPropertyName)
        {
            Images = images;
            IdObject = idObject;
            LinkPropertyName = linkPropertyName;
        }
    }
}
