using System;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace Main
{
    public class ModelHammer : MonoBehaviour
    {
        [SerializeField] private Animator hammerAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");

        private UniTaskCompletionSource completionSource;
        
        public async UniTask DoAttackAsync()
        {
            completionSource = new UniTaskCompletionSource();
            hammerAnimator.SetBool(Attack, true);
            await completionSource.Task;
            SetWait();
        }

        private void SetWait()
        {
            SEManager.Instance.Play(SEPath.SE_MAOUDAMASHII_CHIME07);
            hammerAnimator.SetBool(Attack, false);
        }

        // Animationから呼ばれる
        public void FinishAttackAnimation()
        {
            completionSource?.TrySetResult();
        }
    }
}