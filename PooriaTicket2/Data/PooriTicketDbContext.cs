using Microsoft.EntityFrameworkCore;
using PooriaTicket2.Models;

namespace PooriTicket.Data
{
    public class PooriTicketDbContext : DbContext
    {
        public PooriTicketDbContext(DbContextOptions<PooriTicketDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys());

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Response> Responses { get; set; }



    }
}
