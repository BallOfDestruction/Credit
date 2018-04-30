namespace Web.StaticHelpers
{
    public class EmailModel
    {
        public string From { get; set; } = "support@zmk-armada.ru";
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
