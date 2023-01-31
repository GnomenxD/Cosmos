using Cosmos.AI.Open_AI;
using CosmosFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.AI
{
	public class ImageResponse : IEnumerable<Sprite>
	{
		private bool fetched;
		private readonly long created;
		private readonly string[] urls;
		private List<Sprite> images;

		public long Created => created;
		public string? Url => urls.Length > 0 ? urls[0] : null;
		public string[] Urls => urls;
		public Sprite? Image => Images.Count > 0 ? Images[0] : null;
		public List<Sprite> Images
		{
			get
			{
				if(!fetched && images == null)
				{
					new Task(async () => await Fetch()).Start();
					fetched = true;
				}
				return images;
			}
		}
		public int Count => Images.Count;

		public ImageResponse(long created, string[] urls)
		{
			this.created = created;
			this.urls = urls;
		}

		public IEnumerator<Sprite> GetEnumerator()
		{
			return Images.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return GetEnumerator();
		}

		public async Task<List<Sprite>> Fetch()
		{
			images = new List<Sprite>();
			foreach (string data in urls)
			{
				if (data == null)
					continue;
				Sprite image = await Sprite.FromUrl(data);

				images.Add(image);
			}
			return images;
		}

		public static ImageResponse Generate(ImageResponseContent resp)
		{
			if(resp.data == null)
			{
				Debug.LogError($"No returned data.");
			}
			string[] urls = new string[resp.data != null ? resp.data.Count : 0];
			for(int i = 0; i < urls.Length; i++)
			{
				urls[i] = resp.data[i].url;
			}
			return new ImageResponse(resp.created, urls);
		}
	}
}