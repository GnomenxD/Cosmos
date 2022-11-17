using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CosmosFramework
{
	public static class StringExtension
	{
		public static string Normalize(this StringBuilder sb) => Regex.Replace(sb.ToString(), @"\r\n|\n\r|\n|\r", "\r\n");
		public static string ToDisplayString(this string s)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (i == 0)
				{
					sb.Append(char.ToUpper(s[i]));
					continue;
				}
				else if (char.IsUpper(s[i]) && !char.IsUpper(s[i - 1]))
					sb.Append(" ");
				else if(char.Equals(s[i], '_'))
				{
					sb.Append(" ");
					continue;
				}

				sb.Append(s[i]);

			}
			return sb.ToString();
		}
		public static string ToTitleCase(this string s)
		{
			return s switch
			{
				null => throw new ArgumentNullException(nameof(s)),
				"" => throw new ArgumentException($"{nameof(s)} cannot be empty", nameof(s)),
				_ => string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1))
			};
		}
		public static string ToPascalCase(this string s)
		{
			string final = "";
			string[] split = s.Split(' ');
			foreach (string word in split)
				final += string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1), " ");
			return final;
		}

		public static Vector2 MeasureString(this string s, Font font) => font.MeasureString(s);
		public static Vector2 MeasureString(this string s, Font font, int fontSize) => font.MeasureString(s, fontSize);
	}
}