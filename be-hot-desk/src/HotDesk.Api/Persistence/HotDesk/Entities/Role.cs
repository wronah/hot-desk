using System.ComponentModel.DataAnnotations.Schema;

namespace HotDesk.Api.Persistence.HotDesk.Entities
{
    [Table("roles", Schema = "public")]
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public List<Employee>? Employees { get; set; }
    }
}
