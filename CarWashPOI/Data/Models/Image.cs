namespace CarWashPOI.Data.Models
{
    public class Image : IDeleteableEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
    }
}
