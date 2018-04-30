namespace Web.Controllers.cms
{
    /// <summary>
    /// Показывает нужно ли отображать это свойство в списке\при детализации\редактировании
    /// </summary>
    public class ShowAttribute : System.Attribute
    {
        public bool ShowInList { get; set; }
        public bool ShowInDetails { get; set; }
        public bool ShowInEdit { get; set; }
        public bool ShowInCreate { get; set; }

        public ShowAttribute(bool showInList = true, bool showInDetails = true, bool showInEdit = true, bool showInCreate = true)
        {
            ShowInList = showInList;
            ShowInDetails = showInDetails;
            ShowInEdit = showInEdit;
            ShowInCreate = showInCreate;
        }
    }
}
