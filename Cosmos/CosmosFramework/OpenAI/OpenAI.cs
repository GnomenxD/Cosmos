namespace CosmosFramework.OpenAI
{
	public class OpenAI
	{
		private string authKey;

		public string AuthKey => authKey;

		public OpenAI(string authKey)
		{
			this.authKey = authKey;
		}
	}
}