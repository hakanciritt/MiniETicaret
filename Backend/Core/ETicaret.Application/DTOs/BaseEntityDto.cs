namespace ETicaret.Application.DTOs
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreateData { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
