using Cysharp.Threading.Tasks;
using SceneManager;
using UnityEngine;

namespace Main
{

    public class MainSceneDebugStarter : RootSceneManager.DebugStarterBase<MainSceneManager>
    {
        [SerializeField] private bool userBonnouMode;
        
        /// <summary>
        /// 起動処理
        /// </summary>
        protected override UniTask AwakeAsync()
        {
            UserData.Init();
            initializer = s => s.InitializeAsync(userBonnouMode);
            return UniTask.CompletedTask;
        }
    }
}