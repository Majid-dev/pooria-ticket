using PooriTicket.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PooriaTicket2.Models
{
    public class User : BaseEntity
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public DateTime? LastLoginTime { get; set; }

        public string Password { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("RoleId")]

        public virtual Role? Role { get; set; }
        [Display(Name = "گروه کاربری")]
        public int RoleId { get; set; }


        public virtual ICollection<Response>? Responses { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
