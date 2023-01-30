using Opgave;
using System;

internal class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		CosmosFramework.CoreModule.Game.Create<GameWorld>()
	.Resolution(CosmosFramework.ScreenResolution.m_900p, false)
	.LaunchApplication();
	}
}