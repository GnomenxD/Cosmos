namespace Cosmos.AI.Open_AI
{
	public enum Model
	{
		/// <summary>
		/// Capable of very simple tasks, usually the fastest model in the GPT-3 series, and lowest cost.
		/// <para>Max request: 2,048 tokens.</para>
		/// </summary>
		Ada,
		/// <summary>
		/// Capable of straightforward tasks, very fast, and lower cost.
		/// <para>Max request: 2,048 tokens.</para>
		/// </summary>
		Babbage,
		/// <summary>
		/// Very capable, but faster and lower cost than Davinci.
		/// <para>Max request: 2,048 tokens.</para>
		/// </summary>
		Curie,
		/// <summary>
		/// Most capable GPT-3 model. Can do any task the other models can do, often with higher quality, longer output and better instruction-following. Also supports inserting completions within text.
		/// <para>Max request: 4.000 tokens.</para>
		/// </summary>
		Davinci,
	}

	internal static class ModelExtension
	{
		public static string Convert(this Model model) => model switch
		{
			Model.Ada => "text-ada-001",
			Model.Babbage => "text-babbage-001",
			Model.Curie => "text-curie-001",
			Model.Davinci => "text-davinci-003",
			_ => "text-ada-001",
		};
	}
}