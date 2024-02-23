namespace pinewood_crud_app.Models
{
    public class Customer
    {
        public int Id { get; set; } // Primary key
        public string?  Name { get; set; } // Customer name
        public string? Email { get; set; } // Customer email

        public string? Address { get; set; }
    }
}
