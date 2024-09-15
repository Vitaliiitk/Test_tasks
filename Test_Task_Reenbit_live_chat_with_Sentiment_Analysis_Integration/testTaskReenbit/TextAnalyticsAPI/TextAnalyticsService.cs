using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

namespace testTaskReenbit.TextAnalyticsAPI
{
	public class TextAnalyticsService
	{
		private readonly TextAnalyticsClient _client;

		public TextAnalyticsService(IConfiguration configuration)
		{
			var endpoint = new Uri(configuration["TextAnalytics:Endpoint"]);
			var apiKey = new AzureKeyCredential(configuration["TextAnalytics:ApiKey"]);
			_client = new TextAnalyticsClient(endpoint, apiKey);
		}

		public string AnalyzeSentiment(string text)
		{
			DocumentSentiment documentSentiment = _client.AnalyzeSentiment(text);
			return documentSentiment.Sentiment.ToString();
		}
	}
}
