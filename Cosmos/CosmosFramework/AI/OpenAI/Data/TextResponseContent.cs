namespace Cosmos.AI.Open_AI
{
	public class TextResponseContent
	{
		public string id { get; set; }
		public string @object { get; set; }
		public long created { get; set; }
		public string model { get; set; }
		public Choice[] choices { get; set; }
		public Usage usage { get; set; }
	}
}