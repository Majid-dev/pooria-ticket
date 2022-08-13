using PooriTicket.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PooriaTicket2.Models
{
    public class Response:BaseEntity
    {
        public string TicketResponse { get; set; }
        public string Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
        [Display(Name = "تیکت")]
        public int TicketId { get; set; }

    }
}
