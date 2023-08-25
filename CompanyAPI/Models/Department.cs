namespace CompanyAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Leader { get; set; } 
        public int ProjectId { get; set; }
    }
}
