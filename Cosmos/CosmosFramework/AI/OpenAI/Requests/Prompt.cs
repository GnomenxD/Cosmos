using CosmosFramework;
using System;
using System.Text.RegularExpressions;

namespace Cosmos.AI
{
	/// <summary>
	/// A collection of one or more prompts used for the AI tools.
	/// </summary>
	public class Prompt
	{
		public const string Split = "<prompt_split>";
		private string[] prompts;
		public int Count => prompts.Length;

		public string[] Prompts => prompts;

		/// <summary>
		/// Mulitple prompts can be created in a single string by splitting them using the <see cref="Cosmos.AI.Prompt.Split"/> token between each prompt.
		/// </summary>
		/// <param name="prompts"></param>
		public Prompt(string prompts)
		{
			this.prompts = SplitByToken(prompts);
		}

		public Prompt(string[] prompts)
		{
			this.prompts = prompts;
		}

		private static string[] SplitByToken(string prompts)
		{
			string[] splits = prompts.Split(Split);
			for(int i = 0; i < splits.Length; i++)
			{
				splits[i] = Regex.Replace(splits[i], @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
			}
			return splits;
		}

		public static Prompt operator +(Prompt p, string prompts)
		{
			string[] splits = SplitByToken(prompts);
			int indexing = p.Count;
			p.prompts.EnsureCapacity(p.prompts.Length + splits.Length);
			for(int i = 0; i < splits.Length; i++)
			{
				p.prompts[i + indexing] = prompts;
			}
			return p;
		}

		public static implicit operator Prompt (string prompt) => new Prompt(prompt);

		public static explicit operator string[](Prompt prompt) => prompt.prompts;
		public static explicit operator string(Prompt prompt) => prompt.prompts.Length > 0 ? prompt.prompts[0] : string.Empty;
	}
}