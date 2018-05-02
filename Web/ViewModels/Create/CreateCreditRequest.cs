using System;

namespace Web.ViewModels.Create
{
    public class CreateCreditRequest
    {
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Type { get; set; }
        public float Procent { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public float Amount { get; set; }
    }
}
