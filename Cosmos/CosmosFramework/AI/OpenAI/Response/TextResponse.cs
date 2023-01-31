using Cosmos.AI.Open_AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cosmos.AI
{
	public class TextResponse : IEnumerable<TextResponse.Answer>
	{
		private readonly long created;
		private readonly List<Answer> responses;
		private readonly string model;
		private readonly Usage usage;

		public long Created => created;
		public string? Response => (responses.Count > 0 ? responses[0].Response : null);
		public List<Answer> Responses => responses;
		public string Model => model;
		public Usage Usage => usage;

		public TextResponse(long created, List<Answer> responses, string model, Usage usage)
		{
			this.created = created;
			this.responses = responses;
			this.model = model;
			this.usage = usage;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach(var response in this)
			{
				sb.AppendLine(response.ToString());
			}
			return sb.ToString();
		}
		public IEnumerator<Answer> GetEnumerator()
		{
			return responses.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return GetEnumerator();
		}

		public static TextResponse Generate(TextResponseContent content)
		{
			Answer[] responses = new Answer[content.choices.Length];
			foreach(Choice choice in content.choices)
			{
				responses[choice.index] = new Answer(choice.text, choice.finish_reason);
			}
			return new TextResponse(content.created, responses.ToList(), content.model, content.usage);
		}

		public readonly struct Answer
		{
			private readonly string response;
			private readonly string finishReason;

			public string Response => response;
			public string FinishReason => finishReason;

			public string Format() => response.Replace(".", "." + Environment.NewLine);

			public Answer(string response, string finishReason)
			{
				this.response =Regex.Replace(response.Trim(), @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline); ;
				this.finishReason = finishReason.Trim();
			}

			public override string ToString() => response;
		}
	}
}