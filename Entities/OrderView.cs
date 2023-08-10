namespace Store.Entities
{
    public class OrderView
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
