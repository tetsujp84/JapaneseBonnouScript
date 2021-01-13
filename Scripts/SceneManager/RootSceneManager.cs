using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManager
{
    public interface IScene
    {
        UniTask InitializeAsync();
        UniTask ShowAsync();
        UniTask HideAsync();
    }

    public static class RootSceneManager
    {
        private static IScene currentScene;

        public static async UniTask LoadSceneAsync<T>(Func<T, UniTask> initializer = null) where T : class, IScene
        {
            currentScene?.HideAsync();
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(typeof(T).Name.Replace("SceneManager", ""));
            await OnLoadSceneAsync(initializer);
        }

        private static async UniTask OnLoadSceneAsync<T>(Func<T, UniTask> initializer = null) where T : class, IScene
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(0);
            currentScene = scene.GetRootGameObjects().Select(g => g.GetComponent<IScene>()).FirstOrDefault(n => n != null);
            if (currentScene == null)
            {
                Debug.LogError("シーンが読み込めませんでした");
                return;
            }
            await currentScene.InitializeAsync();
            if (initializer != null) await initializer(currentScene as T);
            await currentScene.ShowAsync();
        }

        public abstract class DebugStarterBase<T> : MonoBehaviour where T : UnityEngine.Object, IScene
        {
            protected Func<T, UniTask> initializer;
            /// <summary>
            /// 起動時にシーン読み込み後の処理を呼び出す
            /// </summary>
            private async void Awake()
            {
                if (currentScene != null) return;
                await AwakeAsync();
                await OnLoadSceneAsync(initializer);
            }

            /// <summary>
            /// 起動処理
            /// </summary>
            protected virtual UniTask AwakeAsync() => UniTask.CompletedTask;
        }
    }
}