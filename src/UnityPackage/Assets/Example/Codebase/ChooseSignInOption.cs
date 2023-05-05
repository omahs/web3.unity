using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Example
{
  public class ChooseSignInOption : MonoBehaviour
  {
    public async void OnJsonRpcTapped()
    {
      await InitializeWeb3AndProceed(Web3SignerType.JsonRpc);
    }

    public async void OnMetaMaskSdkTapped()
    {
      await InitializeWeb3AndProceed(Web3SignerType.MetamaskSDK);
    }
    
    public async void OnWeb3AuthTapped()
    {
      await InitializeWeb3AndProceed(Web3SignerType.Web3Auth);
    }

    private static async UniTask InitializeWeb3AndProceed(Web3SignerType signerType)
    {
      await Game.Instance.InitializeWeb3(signerType);
      
      var web3 = Game.Instance.Web3;
      await web3.Wallet.Connect();

      // should probably provide error info
      if (!web3.Wallet.Connected)
      {
        Game.Instance.TerminateWeb3();
        return;
      }
      
      await SceneManager.LoadSceneAsync("2 ActualGame");
    }
  }
}