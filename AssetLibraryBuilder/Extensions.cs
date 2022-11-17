using System.Text.RegularExpressions;
using System.Text;
using System.IO.Compression;

namespace AssetLibraryBuilder
{
	internal static class Extensions
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

		public static bool CompareAssetLink(this SpriteAssetReference reference, SpriteAssetReference other)
		{
			return reference.Name
				.Replace("_", "")
				.Replace("-", "")
				.Replace(" ", "")
				.Equals(other.Name
				.Replace("-", "")
				.Replace("_", "")
				.Replace(" ", "")
				, StringComparison.CurrentCultureIgnoreCase);
		}

		public static byte[] Compress(this byte[] data, Stream stream)
		{
			data.Compress(stream, CompressionLevel.Optimal);
			byte[] buffer = new byte[data.Length];
			stream.Read(buffer, 0, buffer.Length);
			return buffer;
		}

		public static void Compress(this byte[] data, Stream stream, CompressionLevel compression)
		{
			using (DeflateStream dStream = new DeflateStream(stream, compression))
			{
				dStream.Write(data, 0, data.Length);
			}
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
				else
					final += string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1), " ");
			}
			return final;
		}
	}
}