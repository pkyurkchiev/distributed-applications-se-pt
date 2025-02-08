namespace MC.Data.Entities
{
    public class Movie : BaseEntity
    {
        required public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
