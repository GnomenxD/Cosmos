using CosmosFramework.Async;
using System;

namespace CosmosFramework
{
	public static class Wait
	{
		internal class WaitForSecondsPool : YieldInstructionPool<WaitForSeconds>
		{

		}

		internal class WaitForSecondsUnscaledPool : YieldInstructionPool<WaitForSecondsUnscaled>
		{

		}
		
		internal class WaitWhilePool : YieldInstructionPool<WaitWhile>
		{

		}

		internal class WaitUntilPool : YieldInstructionPool<WaitUntil>
		{

		}

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Async.WaitForSeconds"/>
		/// </summary>
		public static WaitForSeconds ForSeconds(float seconds)
		{
			WaitForSeconds t = ScriptableObject.Instance<WaitForSecondsPool>().Request();
			t.seconds = seconds;
			return t;
		}

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Async.WaitForSecondsUnscaled"/>
		/// </summary>
		public static WaitForSecondsUnscaled ForSecondsUnscaled(float seconds)
		{
			WaitForSecondsUnscaled t = ScriptableObject.Instance<WaitForSecondsUnscaledPool>().Request();
			t.seconds = seconds;
			return t;
		}

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Async.WaitWhile"/>
		/// </summary>
		public static WaitWhile While(Func<bool> predicate)
		{
			WaitWhile t = ScriptableObject.Instance<WaitWhilePool>().Request();
			t.predicate = predicate;
			return t;
		}

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Async.WaitUntil"/>
		/// </summary>
		public static WaitUntil Until(Func<bool> predicate)
		{
			WaitUntil t = ScriptableObject.Instance<WaitUntilPool>().Request();
			t.predicate = predicate;
			return t;
		}
	}
}