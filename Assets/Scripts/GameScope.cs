using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private ItemsController _itemsController;
    [SerializeField] private Timer _timer;
    [SerializeField] private WinLoseConditions _winLoseConditions;
    [SerializeField] private SceneController _sceneController;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        // in context of this test task, it doesn't make sense to register 
        // object with any other lifetime but Singleton. In a more fleshed out
        // game with multiple levels, their lifetime should be changed to Scoped
        builder.RegisterComponent(_levelSettings);
        builder.RegisterComponent(_itemsController);
        builder.RegisterComponent(_timer);
        builder.RegisterComponent(_winLoseConditions);
        builder.RegisterComponent(_sceneController);
    }
}
