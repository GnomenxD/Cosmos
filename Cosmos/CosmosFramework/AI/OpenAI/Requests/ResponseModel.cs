using System.Collections.Generic;

namespace Cosmos.AI.Open_AI
{
	public class ResponseModel
	{
		public long created { get; set; }
		public List<Link>? data { get; set; }

	}
}