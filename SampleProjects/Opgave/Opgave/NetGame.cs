using CosmosFramework;
using CosmosFramework.Netcode;
using System.Threading.Tasks;

namespace Opgave
{
	internal class NetGame : NetcodeBehaviour
	{
		private string changedUrl;

		public void Change(string urls)
		{
			Rpc(nameof(SendChangeClientRpc), null, urls);
		}

		[ClientRPC]
		private async void SendChangeClientRpc(string urls)
		{
			GetComponent<SpriteRenderer>().Sprite = await Sprite.FromUrl(urls);
			//Vector2 position = new Vector2(-3.0f, 0.0f);
			//foreach(string url in urls)
			//{
			//	Debug.Log($"get: {url}");
			//	GameObject go = new GameObject("Picture");
			//	SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
			//	sr.Sprite = await Sprite.FromUrl(url);
			//	go.Transform.Position = position;
			//	position += new Vector2(3.0f, 0.0f);
			//}
		}


	}
}