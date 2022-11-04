using CosmosEngine.CoreModule;
using CosmosEngine.Modules;
using SpaceWar;

Game.Create<GameWorld>()
	.AddDefault()
	.RemoveModule<EditorGrid>()
	.LaunchApplication();