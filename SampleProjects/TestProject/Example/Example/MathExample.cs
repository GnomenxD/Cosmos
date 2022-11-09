using CosmosEngine;
using System.Collections.Generic;

internal class MathExample : GameBehaviour
{
	protected override void Start()
	{
		float[] values = new float[] { 10, 6, 2, 8, 13, 5 };
		Debug.Log($"Min: {Mathf.Min(values)}"); //Will output 2.
		Debug.Log($"Max: {Mathf.Max(values)}"); //Will output 13.

		int index = 0;

		foreach (float f in values)
			Debug.Log(f);

		values.ForEach((float f) =>
		{
			Debug.Log($"{index}: {f}");
			index++;
		});

		values.ForEach<float>(Seperate);

		
	}

	private void Seperate(float f)
	{

	}
}