namespace NewsApi.Models
{
    public class NewsItem
    {
         public int Id { get; set; }

        
        public string? ReportName { get; set; }

        
        public string? Description { get; set; }

        public string? Body { get; set; }

        public Guid CreatedBy { get; set; }

        
        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
        public string? Category { get; set; }        
        public string? Secret { get; set; }

        public bool IsComplete { get; set; }
    }
}