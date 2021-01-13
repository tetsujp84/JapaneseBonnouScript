using Cysharp.Threading.Tasks;
using Main;
using SceneManager;
using UnityEngine;

public class Boot : MonoBehaviour
{
    private void Start()
    {
        RootSceneManager.LoadSceneAsync<MainSceneManager>().Forget();
    }
}