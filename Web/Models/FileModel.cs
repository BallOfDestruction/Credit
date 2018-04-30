namespace Web.Models
{
    public class FileModel : Entity<FileModel>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
    }
}
