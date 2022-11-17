using System.Text.RegularExpressions;
using System.Text;

namespace AssetLibraryBuilder
{
	public static class Extensions
	{
		public static string Normalize(this StringBuilder sb) => Regex.Replace(sb.ToString(), @"\r\n|\n\r|\n|\r", "\r\n");
		public static string ToTitleCase(this string s)
		{
			return s switch
			{
				null => throw new ArgumentNullException(nameof(s)),
				"" => string.Empty,
				_ => string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1))
			};
		}
		public static string ToPascalCase(this string s)
		{
			string final = "";
			string[] split = s.Split(' ');
			if (split.Length <= 1)
				return string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1), " ");
			foreach (string word in split)
			{
				if (string.IsNullOrWhiteSpace(word))
					continue;
				if (word.Length == 1)
					final += word;
				final += string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1), " ");
			}
			return final;
		}
	}
}