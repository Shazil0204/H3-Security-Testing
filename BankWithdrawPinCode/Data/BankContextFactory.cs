using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BankWithdrawPinCode.Data
{
	public class BankContextFactory : IDesignTimeDbContextFactory<BankContext>
	{
		public BankContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BankContext>();
			optionsBuilder.UseSqlite("Data Source=bank.db");

			return new BankContext(optionsBuilder.Options);
		}
	}
}
