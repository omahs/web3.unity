using System;
using ChainSafe.GamingWeb3;
using ChainSafe.GamingWeb3.Build;
using ChainSafe.GamingWeb3.Unity;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Web3Unity.Scripts.Library.Ethers.JsonRpc;
using Web3Unity.Scripts.Library.Ethers.Providers;

namespace Example
{
  public class Game : MonoBehaviour
  {
    public static Game Instance { get; private set; }

    public JsonRpcProviderConfiguration ProviderConfiguration;
    
    public Web3 Web3 { get; private set; }
    
    private async void Awake()
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      
      // load next scene on click
      await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
      await SceneManager.LoadSceneAsync("1 Sign In Options");
    }

    public async UniTask InitializeWeb3(Web3SignerType signerType)
    {
      Web3 = new Web3Builder()
        .Configure(services =>
        {
          services.UseUnityEnvironment();
          services.UseJsonRpcProvider(ProviderConfiguration);
          BindSigner(services, signerType);
        })
        .Build();

      await Web3.Initialize();
    }

    public void TerminateWeb3()
    {
      Web3.Terminate();
      Web3 = null;
    }

    private void BindSigner(IWeb3ServiceCollection services, Web3SignerType signerType)
    {
      switch (signerType)
      {
        case Web3SignerType.JsonRpc:
          services.UseJsonRpcWallet(new JsonRpcSignerConfiguration());
          break;
        case Web3SignerType.MetamaskSDK:
        case Web3SignerType.Web3Auth:
        default:
          throw new InvalidOperationException($"{signerType} is not supported for this demo case.");
      }
    }
  }
}