using Newtonsoft.Json;

namespace CosmosFramework.OpenAI
{
	public class Request
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("model")]
		public string Model { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("prompt")]
		public object Prompt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("suffix")]
		public string Suffix { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("max_tokens")]
		public int? MaxTokens { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("temperature")]
		public double? Temperature { get; set; }
	}
}