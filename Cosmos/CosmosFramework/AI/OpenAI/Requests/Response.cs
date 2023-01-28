using Cosmos.AI.Open_AI;
using CosmosFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cosmos.AI
{
	public class Response : IEnumerable<Sprite>
	{
		private readonly long created;
		private readonly List<Sprite> images;

		public long Created => created;
		public Sprite Image => images[0];
		public List<Sprite> Images => images;

		public int Count => images.Count;

		public Response(long created, List<Sprite> images)
		{
			this.created = created;
			this.images = images;
		}

		public IEnumerator<Sprite> GetEnumerator()
		{
			return images.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return GetEnumerator();
		}

		public static async Task<Response> Generate(ResponseModel resp)
		{
			List<Sprite> images = new List<Sprite>();
			foreach(Link data in resp.data)
			{
				if (data == null)
					continue;
				Sprite image = await Sprite.FromUrl(data.url);
				images.Add(image);
			}
			return new Response(resp.created, images);
		}
	}
}