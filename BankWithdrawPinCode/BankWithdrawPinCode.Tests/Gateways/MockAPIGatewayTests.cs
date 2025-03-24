using System;
using BankWithdrawPinCode.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using Xunit.Abstractions;

namespace BankWithdrawPinCode.Tests.Gateways
{
	[Collection("Mock API Gateway Collection")]
	public class MockAPIGatewayTests
	{
		private readonly ITestOutputHelper _output;

		public MockAPIGatewayTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public async Task FetchDataAsync_ShouldReturnMockedData_WhenCalledWithValidEndpoint()
		{
			// Arrange
			var mockAPIGateway = Substitute.For<IAPIGateway>();
			var endpoint = "test-endpoint";
			var expectedResponse = "Mocked Response Data";

			_output.WriteLine($"[{DateTime.Now}] SETUP: Mocking FetchDataAsync");
			_output.WriteLine($"Endpoint: {endpoint}");
			_output.WriteLine($"Expected Response: {expectedResponse}");

			// Mock the behavior of FetchDataAsync
			mockAPIGateway.FetchDataAsync(endpoint).Returns(Task.FromResult(expectedResponse));

			// Act
			_output.WriteLine($"[{DateTime.Now}] ACTION: Calling FetchDataAsync...");
			var result = await mockAPIGateway.FetchDataAsync(endpoint);

			// Log the results
			_output.WriteLine($"[{DateTime.Now}] RESULT: Received Response: '{result}'");

			// Assert
			_output.WriteLine($"[{DateTime.Now}] ASSERT: Validating that the result matches the expected response...");
			Assert.Equal(expectedResponse, result);
		}

		[Fact]
		public async Task FetchDataAsync_ShouldThrowException_WhenGatewayFails()
		{
			// Arrange
			var mockAPIGateway = Substitute.For<IAPIGateway>();
			var endpoint = "test-endpoint";

			_output.WriteLine($"[{DateTime.Now}] SETUP: Mocking FetchDataAsync to throw an exception");
			_output.WriteLine($"Endpoint: {endpoint}");

			// Mock the behavior of FetchDataAsync to throw an exception
			mockAPIGateway.FetchDataAsync(endpoint).Throws(new Exception("Mocked Exception"));

			// Act
			_output.WriteLine($"[{DateTime.Now}] ACTION: Calling FetchDataAsync and expecting an exception...");
			var exception = await Assert.ThrowsAsync<Exception>(() => mockAPIGateway.FetchDataAsync(endpoint));

			// Log the results
			_output.WriteLine($"[{DateTime.Now}] RESULT: Exception thrown with message: {exception.Message}");

			// Assert
			_output.WriteLine($"[{DateTime.Now}] ASSERT: Validating the exception was thrown as expected...");
		}

		[Fact]
		public async Task SendDataAsync_ShouldReturnTrue_WhenDataIsSentSuccessfully()
		{
			// Arrange
			var mockAPIGateway = Substitute.For<IAPIGateway>();
			var endpoint = "test-endpoint";
			var payload = "{ \"key\": \"value\" }";

			_output.WriteLine($"[{DateTime.Now}] SETUP: Mocking SendDataAsync for success");
			_output.WriteLine($"Endpoint: {endpoint}");
			_output.WriteLine($"Payload: {payload}");
			_output.WriteLine("Mocked Behavior: Return TRUE to simulate success.");

			// Mock the behavior of SendDataAsync
			mockAPIGateway.SendDataAsync(endpoint, payload).Returns(Task.FromResult(true));

			// Act
			_output.WriteLine($"[{DateTime.Now}] ACTION: Calling SendDataAsync...");
			var result = await mockAPIGateway.SendDataAsync(endpoint, payload);

			// Log the results
			_output.WriteLine($"[{DateTime.Now}] RESULT: Received Response: {result}");

			// Assert
			_output.WriteLine($"[{DateTime.Now}] ASSERT: Validating that SendDataAsync returned TRUE...");
			Assert.True(result, "The mocked gateway should return true for successful data sending.");
		}

		[Fact]
		public async Task SendDataAsync_ShouldReturnFalse_WhenDataSendingFails()
		{
			// Arrange
			var mockAPIGateway = Substitute.For<IAPIGateway>();
			var endpoint = "test-endpoint";
			var payload = "{ \"key\": \"value\" }";

			_output.WriteLine($"[{DateTime.Now}] SETUP: Mocking SendDataAsync for failure");
			_output.WriteLine($"Endpoint: {endpoint}");
			_output.WriteLine($"Payload: {payload}");
			_output.WriteLine("Mocked Behavior: Return FALSE to simulate failure.");

			// Mock the behavior of SendDataAsync to return false
			mockAPIGateway.SendDataAsync(endpoint, payload).Returns(Task.FromResult(false));

			// Act
			_output.WriteLine($"[{DateTime.Now}] ACTION: Calling SendDataAsync...");
			var result = await mockAPIGateway.SendDataAsync(endpoint, payload);

			// Log the results
			_output.WriteLine($"[{DateTime.Now}] RESULT: Received Response: {result}");

			// Assert
			_output.WriteLine($"[{DateTime.Now}] ASSERT: Validating that SendDataAsync returned FALSE...");
			Assert.False(result, "The mocked gateway should return false for data sending failure.");
		}
	}
}
