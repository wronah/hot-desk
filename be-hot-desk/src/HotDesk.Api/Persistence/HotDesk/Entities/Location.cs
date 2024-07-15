using System.ComponentModel.DataAnnotations.Schema;

namespace HotDesk.Api.Persistence.HotDesk.Entities
{
    [Table("locations", Schema = "public")]
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("add_date")]
        public DateTime AddDate { get; set; }
        [Column("remove_date")]
        public DateTime? RemoveDate { get; set; }

        public List<Desk>? Desks { get; set; }
    }
}
