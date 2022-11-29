using IsometicProject;

GameWorld.Create<GameWorld>()
	.AddModule<IsometicMap>()
	.AddDefault()
	.LaunchApplication();