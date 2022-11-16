using CosmosEngine;

public class MathExample : GameBehaviour
{
	protected override void Start()
	{
		float ceil = Mathf.Ceil(123.45f);		//124
		float floor = Mathf.Floor(123.45f);		//123
		float round = Mathf.Round(123.45f);		//123
		float rounddig = Mathf.Round(123.45f, 1);	//123.4
	}
}