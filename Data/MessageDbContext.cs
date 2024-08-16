using Microsoft.EntityFrameworkCore;
using MessageService.Models;

public class MessageDbContext : DbContext
{
	public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
	{
	}

	public DbSet<Message> Messages { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Message>().ToTable("Messages");
		modelBuilder.Entity<Message>().HasKey(m => m.Id);
	}
}

