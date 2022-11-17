using Example;

CosmosEngine.CoreModule.Game
	.Create<Game>()
	.Resolution(CosmosEngine.ScreenResolution.m_720p, false)
	.LaunchApplication();