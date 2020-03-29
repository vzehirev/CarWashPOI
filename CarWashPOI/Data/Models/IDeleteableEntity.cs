namespace CarWashPOI.Data.Models
{
    public interface IDeleteableEntity
    {
        public bool IsDeleted { get; set; }
    }
}
