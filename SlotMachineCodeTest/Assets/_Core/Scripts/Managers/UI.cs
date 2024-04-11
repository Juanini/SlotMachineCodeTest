using Cysharp.Threading.Tasks;
using HannieEcho.UI;

public class UI : Singleton<UI>
{
    public UIManager uiManager;
    public UINavigation uiNavigation;
    
    public async UniTask Init()
    {
        await uiManager.Init();
    }
}
