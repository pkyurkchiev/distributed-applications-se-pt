namespace MC.Data.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}
