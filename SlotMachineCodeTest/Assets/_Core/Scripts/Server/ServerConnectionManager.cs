using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Obvious.Soap;
using UnityEngine;

public class ServerConnectionManager : Singleton<ServerConnectionManager>
{
    public ServerEndpoints serverEndpoints;
    
    public ScriptableEventNoParam OnLoginSuccess;
    public ScriptableEventNoParam OnLoginError;
    
    public async void Init()
    {
        await Login();
    }
    
    // Simulate a login flow, here we will 
    private async UniTask Login()
    {
        await UniTask.Delay(500);
        LoginSuccess();
    }

    private void LoginSuccess()
    {
        // Sync User data
        
        
        OnLoginSuccess.Raise();
    }
    
    private void LoginError()
    {
        OnLoginError.Raise();
    }
}
