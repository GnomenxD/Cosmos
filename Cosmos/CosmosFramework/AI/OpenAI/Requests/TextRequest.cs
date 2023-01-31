using Newtonsoft.Json;

namespace Cosmos.AI.Open_AI
{
	public readonly struct TextRequest
	{
		private readonly Model model;
		private readonly Prompt prompts;
		private readonly string suffix;
		private readonly int maxTokens;
		private readonly double temperature;
		private readonly double p;
		private readonly int n;
		private readonly bool echo;
		private readonly string stopSequence;

		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.model"/>
		/// </summary>
		public Model Model => model;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.prompt"/>
		/// </summary>
		public Prompt Prompts => prompts;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.suffix"/>
		/// </summary>
		public string Suffix => suffix;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.max_tokens"/>
		/// </summary>
		public int MaxTokens => maxTokens;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.temperature"/>
		/// </summary>
		public double Temperature => temperature;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.top_p"/>
		/// </summary>
		public double P => p;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.n"/>
		/// </summary>
		public int N => n;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.echo"/>
		/// </summary>
		public bool Echo => echo;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.stop"/>
		/// </summary>
		public string Stop => stopSequence;

		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody"/>
		/// </summary>
		/// <param name="prompts"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.prompt"/></param>
		/// <param name="model"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.model"/></param>
		/// <param name="suffix"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.suffix"/></param>
		/// <param name="maxTokens"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.max_tokens"/></param>
		/// <param name="temperature"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.temperature"/></param>
		/// <param name="p"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.top_p"/></param>
		/// <param name="amount"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.n"/></param>
		/// <param name="echo"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.echo"/></param>
		/// <param name="stopSequence"><inheritdoc cref="Cosmos.AI.Open_AI.TextRequestBody.stop"/></param>
		public TextRequest(Prompt prompts, Model model = default, string suffix = default, int maxTokens = 10, double temperature = 0.7d, double p = 1.0d, int amount = 1, bool echo = default, string stopSequence = default)
		{
			this.prompts = prompts;
			this.model = model;
			this.suffix = suffix;
			this.maxTokens = maxTokens;
			this.temperature = temperature;
			this.p = p;
			this.n = amount;
			this.echo = echo;
			this.stopSequence = stopSequence;
		}

		/// <summary>
		/// Converts <see cref="Cosmos.AI.Open_AI.TextRequest"/> to a <see cref="Cosmos.AI.Open_AI.TextRequestBody"/>.
		/// </summary>
		/// <returns></returns>
		internal TextRequestBody ConstructBody() => new TextRequestBody
		{
			model = this.Model.Convert(),
			prompt = (string[])this.Prompts,
			suffix = this.Suffix,
			max_tokens = this.MaxTokens,
			temperature = this.Temperature,
			top_p = this.P,
			n = this.N,
			echo = this.Echo,
			stop = this.Stop,
		};

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this.ConstructBody());
		}

	}

	/// <summary>
	/// Creates a completion for the provided prompt and parameters
	/// </summary>
	internal class TextRequestBody
	{
		/// <summary>
		/// ID of the model to use. You can use the List models API to see all of your available models, or see our Model overview for descriptions of them.
		/// </summary>
		public string model { get; set; }
		/// <summary>
		/// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays.
		/// <para>Note that <b>endoftext</b> is the document separator that the model sees during training, so if a prompt is not specified the model will generate as if from the beginning of a new document.</para>
		/// </summary>
		public string[]? prompt { get; set; }
		/// <summary>
		/// The suffix that comes after a completion of inserted text.
		/// </summary>
		public string? suffix { get; set; }
		/// <summary>
		/// The maximum number of <see href="https://beta.openai.com/tokenizer">tokens</see> to generate in the completion.
		/// <para>The token count of your prompt plus max_tokens cannot exceed the model's context length. Most models have a context length of 2048 tokens (except for the newest models, which support 4096).</para>
		/// </summary>
		public int? max_tokens { get; set; }
		/// <summary>
		/// What <see href="https://towardsdatascience.com/how-to-sample-from-language-models-682bceb97277">sampling temperature</see> to use. Higher values means the model will take more risks. Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
		/// <para>It's recommend altering this or <see cref="top_p"/> but not both.</para>
		/// </summary>
		public double? temperature { get; set; }
		/// <summary>
		/// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
		/// <para>It's recommend altering this or <see cref="temperature"/> but not both.</para>
		/// </summary>
		public double? top_p { get; set; }
		/// <summary>
		/// How many completions to generate for each prompt.
		/// <para><b>Note</b>: Because this parameter generates many completions, it can quickly consume your token quota. Use carefully and ensure that you have reasonable settings for <see cref="max_tokens"/> and stop.</para>
		/// </summary>
		public int? n { get; set; }
		/// <summary>
		/// Echo back the prompt in addition to the completion
		/// </summary>
		public bool? echo { get; set; }
		/// <summary>
		/// Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
		/// </summary>
		public string? stop { get; set; }
	}
}