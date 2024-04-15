using FrameWork.Factories;
using FrameWork.Resource;
using FrameWork.UI;
using FrameWork.Utils;

public class GameLaunch : UnitySingleton<GameLaunch>
{
    protected override void Awake()
    {
        base.Awake();
        this.InitFramework();
        this.InitGameLogic();
    }
    
    /// <summary>
    /// フレームワークを初期化
    /// </summary>
    private void InitFramework()
    {
        ManagerFactory.Instance.CreateManager<ResManager>();
        ManagerFactory.Instance.CreateManager<UIManager>();
    }

    /// <summary>
    /// ゲームロジックに入る
    /// </summary>
    private void InitGameLogic()
    {
        this.gameObject.AddComponent<GameApp>();
        GameApp.Instance.InitGame();
    }
}
