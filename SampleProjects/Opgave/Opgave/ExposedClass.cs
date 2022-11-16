using CosmosEngine;

namespace Opgave
{
	internal class ExposedClass : GameBehaviour
	{
		[ExposedField("Armour Value")]
		private float value;
	}
}