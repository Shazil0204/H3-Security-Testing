namespace BankWithdrawPinCode.Interfaces
{
	public interface IAPIGateway
	{
		/// <summary>
		/// Fetches data from an external API.
		/// </summary>
		/// <param name="endpoint">The API endpoint to fetch data from.</param>
		/// <returns>A task representing the asynchronous operation, with the data as a string.</returns>
		Task<string> FetchDataAsync(string endpoint);

		/// <summary>
		/// Sends data to an external API.
		/// </summary>
		/// <param name="endpoint">The API endpoint to send data to.</param>
		/// <param name="payload">The data payload to send.</param>
		/// <returns>A task representing the asynchronous operation, with a boolean indicating success.</returns>
		Task<bool> SendDataAsync(string endpoint, string payload);
	}
}
