using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Main
{
    public class ClearTimeline : MonoBehaviour
    {
        [SerializeField] private PlayableDirector clearTimeline;

        public async UniTask PlayAsync()
        {
            clearTimeline.Play();
            await UniTask.WaitUntil(() => clearTimeline.time >= clearTimeline.duration);
        }
    }

}