namespace ETicaret.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateData { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
