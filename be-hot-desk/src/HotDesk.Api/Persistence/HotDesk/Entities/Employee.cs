using System.ComponentModel.DataAnnotations.Schema;

namespace HotDesk.Api.Persistence.HotDesk.Entities
{
    [Table("employees", Schema = "public")]
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("desk_id")]
        public int? DeskId { get; set; }

        public Desk? Desk { get; set; }
        public List<Role>? Roles { get; set; }
    }
}
