namespace Web.Controllers.cms
{
    /// <summary>
    /// Показывает нужно ли отображать это свойство в списке\при детализации\редактировании
    /// </summary>
    public class AccessAttribute : System.Attribute
    {
        public bool OnDetails { get; set; }
        public bool OnCreate { get; set; }
        public bool OnEdit { get; set; }
        public bool OnDelete { get; set; }

        public AccessAttribute(bool onDetails = true, bool onCreate = true, bool onEdit = true, bool onDelete = true)
        {
            OnDetails = onDetails;
            OnCreate = onCreate;
            OnEdit = onEdit;
            OnDelete = onDelete;
        }
    }
}
