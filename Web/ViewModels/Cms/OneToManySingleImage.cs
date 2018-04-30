namespace Web.ViewModels.Cms
{
    public class OneToManySingleImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string LinkPropertyName { get; set; }

        public OneToManySingleImage()
        {
            
        }
        public OneToManySingleImage(int id, string url, string linkPropertyName)
        {
            Id = id;
            Url = url;
            LinkPropertyName = linkPropertyName;
        }
    }
}
