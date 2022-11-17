using CosmosEngine;
using CosmosEngine.Modules;
using Opgave;

GameWorld.Create<GameWorld>()
	.Resolution(CosmosEngine.ScreenResolution.m_900p, false)
	.LaunchApplication();