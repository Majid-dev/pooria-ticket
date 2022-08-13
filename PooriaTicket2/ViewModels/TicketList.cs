using PooriaTicket2.Models;
using PooriTicket.Data;

namespace PooriaTicket2.ViewModels
{
    public class TicketList
    {
       
        public List<Ticket> Tickets { get; set; }
        public string UserName { get; set; }
    }
}
