using PooriaTicket2.Models;

namespace PooriaTicket2.ViewModels
{
    public class ResponseListViewModel
    {
        public List<Response> Responses { get; set; }
        public string Title { get; set; }//Ticket Title
        public int TicketId { get; set; }
    }
}
