using CosmosEngine.CoreModule;
using CosmosEngine.Modules;
using SpaceShooter;


Game.Create<GameWorld>()
	.AddDefault()
	.AddModule<ProjectileSystem>()
	.RemoveModule<GizmosModule>()
	.LaunchApplication();