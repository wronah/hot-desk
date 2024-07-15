using HotDesk.Api.Persistence.HotDesk.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotDesk.Api.Persistence.HotDesk.Entities
{
    [Table("desks", Schema = "public")]
    public class Desk
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("status")]
        public DeskStatusEnum Status { get; set; }
        [Column("add_date")]
        public DateTime AddDate { get; set; }
        [Column("remove_date")]
        public DateTime? RemoveDate { get; set; }
        [Column("start_reservation_date")]
        public DateTime? StartReservationDate { get; set; }
        [Column("end_reservation_date")]
        public DateTime? EndReservationDate { get; set; }
        [Column("location_id")]
        public int? LocationId { get; set; }

        public Location? Location { get; set; }
        public Employee? Employee { get; set; }
    }
}
