using CosmosFramework;
using System;

namespace Cosmos.AI
{
	public class Prompt
	{
		private string[] prompts;
		public int Count => prompts.Length;

		public string[] Prompts => prompts;

		public Prompt(string prompts)
		{
			string[] splits = prompts.Split('|');
			this.prompts = splits;
		}

		public Prompt(string[] prompts)
		{
			this.prompts = prompts;
		}

		public static Prompt operator +(Prompt p, string prompts)
		{
			string[] splits = prompts.Split('|');
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
	}
}