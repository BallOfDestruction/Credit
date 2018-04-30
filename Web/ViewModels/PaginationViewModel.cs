namespace Web.ViewModels
{
    public class PaginationViewModel
    {
        public string Url { get; set; }
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }

        public PaginationViewModel(string url, int currentPage, int maxPage)
        {
            Url = url;
            MaxPage = maxPage;
            CurrentPage = currentPage;
        }
    }
}
