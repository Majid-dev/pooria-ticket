using System.ComponentModel.DataAnnotations;

namespace PooriTicket.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
