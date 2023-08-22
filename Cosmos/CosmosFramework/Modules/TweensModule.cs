using CosmosFramework.Tweening;
using System.Collections.Generic;

namespace CosmosFramework.Modules
{
	internal class TweensModule : GameModule<TweensModule>, IUpdateModule
	{
		private readonly List<TweenerBase> tweeners = new List<TweenerBase>();

		public static void Tween(TweenerBase tweener)
		{
			Singleton.tweeners.Add(tweener);
		}

		void IUpdateModule.Update()
		{
			foreach(TweenerBase tweener in tweeners)
			{
				if (tweener.Done)
					continue;
				tweener.Do();
				Debug.Log($"{tweener}");
			}
		}
	}
}