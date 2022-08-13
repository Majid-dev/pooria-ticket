using PooriTicket.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PooriaTicket2.Models
{
    public class Ticket : BaseEntity
    {
        [Required]
        [Display(Name = "عنوان تیکت")]
        public string Title { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;


        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        public virtual ICollection<Response>? Responses { get; set; }


    }
}
