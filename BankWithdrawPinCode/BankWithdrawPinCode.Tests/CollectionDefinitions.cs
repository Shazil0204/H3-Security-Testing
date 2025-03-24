using Xunit;

namespace BankWithdrawPinCode.BankWithdrawPinCode.Tests
{
	[CollectionDefinition("Account Collection")]
	public class AccountCollection : ICollectionFixture<object> { }

	[CollectionDefinition("PIN Collection")]
	public class PINCollection : ICollectionFixture<object> { }

	[CollectionDefinition("Account Data Collection")]
	public class AccountDataCollection : ICollectionFixture<object> { }

	[CollectionDefinition("Transaction Manager Collection")]
	public class TransactionManagerCollection : ICollectionFixture<object> { }

	[CollectionDefinition("Mock API Gateway Collection")]
	public class MockAPIGatewayCollection : ICollectionFixture<object> { }
}
