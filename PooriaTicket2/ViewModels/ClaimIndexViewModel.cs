using PooriaTicket2.Models;
using PooriTicket.Data;

namespace PooriaTicket2.ViewModels
{
    public class ClaimIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<System.Security.Claims.Claim> Claims { get; set; }
    }
}
