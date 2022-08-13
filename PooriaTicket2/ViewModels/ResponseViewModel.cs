using PooriaTicket2.Models;

namespace PooriaTicket2.ViewModels
{
    public class ResponseViewModel
    {
        public int Id { get; set; }
        public string TicketResponse { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
    }
}
