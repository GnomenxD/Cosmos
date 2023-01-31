
using CosmosFramework.Editor;
using CosmosFramework.Rendering;
using System;
using System.Collections;
using System.Text;

namespace CosmosFramework.Diagnostics
{
	public class LogMessage : IEquatable<LogMessage>
	{
		private string message;
		private readonly LogFormat format;
		private readonly LogOption option;
		private readonly string stackTrace;
		private readonly int stackTraceLine;
		private readonly string initialStackCall;
		private IEnumerable table;
		private string collectionTable;
		private bool expanded;
		private int count;
		private Rect rect;

		public string Message { get => message; internal set => message = value; }
		public LogFormat Format => format;
		public LogOption Option => option;
		public string StackTrace => stackTrace;
		public string InitialCall => initialStackCall;
		internal IEnumerable Table 
		{ 
			get => table;
			set
			{
				table = value;
				if (table != null)
				{
					int index = 0;
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					foreach (var item in table)
					{
						sb.Append($"<stacktrace>[{index}] ");
						sb.AppendLine((item == null) ? "null" : item.ToString());
						index++;
					}
					collectionTable = sb.ToString().Trim();
				}
			}
		}
		internal bool Expanded { get => expanded; set => expanded = value; }
		internal int Count { get => count; set => count = value; }
		internal Rect Rect { get => rect; set => rect = value; }

		public string print
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				string[] log = SplitLines();
				for (int i = 0; i < log.Length; i++)
				{
					if (log[i].Contains("<stacktrace>"))
					{
						continue;
					}
						if (i > 0)
						sb.Append($"\t");
					sb.Append(log[i]);
				}
				return sb.ToString();
			}
		}

		public Sprite Icon
		{
			get
			{
				return format switch
				{
					LogFormat.Message => EditorContent.LogMessage,
					LogFormat.Warning => EditorContent.LogWarning,
					LogFormat.Error => EditorContent.LogError,
					LogFormat.Complete => EditorContent.LogComplete,
					_ => null,
				};
			}
		}

		public LogMessage(string message, LogFormat format, LogOption option, string stackTrace, int stackTraceLine, string initialStackCall)
		{
			this.message = message;
			this.format = format;
			this.option = option;
			this.stackTrace = stackTrace;
			this.stackTraceLine = stackTraceLine;
			this.initialStackCall = initialStackCall;
			this.count = 1;
			this.Rect = new Rect(0, 0, 20, 20);
		}

		public string[] SplitLines()
		{
			if(Option.HasFlag(LogOption.Collection))
			{
				return ($"{Message}{(string.IsNullOrEmpty(collectionTable) ? "\nempty" : $"\n{collectionTable}")}").Split('\n');
			}
			return ($"{Message}{(string.IsNullOrEmpty(StackTrace) ? "" : $"\n{stackTrace}")}").Split('\n');
		}

		public bool Equals(LogMessage other)
		{
			return Message.Equals(other.Message) && initialStackCall.Equals(other.InitialCall);
		}

		public bool Compare(LogMessage other) => Compare(other, false);

		public bool Compare(LogMessage other, bool ignoreMessage)
		{
			if (other == null)
				return false;
			if (ignoreMessage)
			{
				return InitialCall.Equals(other.InitialCall) && stackTraceLine.Equals(other.stackTraceLine);
			}
			else
				return Equals(other);
		}
	}
}