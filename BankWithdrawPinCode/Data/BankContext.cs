using Microsoft.EntityFrameworkCore;
using BankWithdrawPinCode.Models;

namespace BankWithdrawPinCode.Data
{
	public class BankContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }

		public BankContext(DbContextOptions<BankContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Account>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.PIN).IsRequired();
				entity.Property(e => e.Balance).IsRequired();
				entity.Property(e => e.OwnerName).IsRequired();
			});
		}
	}
}
