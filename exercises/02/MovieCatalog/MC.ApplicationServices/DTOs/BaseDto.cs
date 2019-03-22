namespace MC.ApplicationServices.DTOs
{
    public class BaseDto
    {
        public BaseDto() { }
        public BaseDto(int id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
