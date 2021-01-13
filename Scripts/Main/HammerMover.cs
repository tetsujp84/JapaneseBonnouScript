using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    // 鐘の判定
    public class HammerMover : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public bool IsInSuccessRange => isInSuccessFromAnimation && isPlaying;

        private bool isInSuccessFromAnimation;
        private bool isPlaying;
        private static readonly int Playing = Animator.StringToHash("Playing");
        private float baseSpeed;

        public void Initialize(float oneTime)
        {
            baseSpeed = oneTime;
            animator.speed = baseSpeed;
        }
        
        public void StartMove()
        {
            isPlaying = true;
            UpdateAnimator();
        }

        public void Stop()
        {
            isPlaying = false;
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetBool(Playing, isPlaying);
        }

        public void SetSpeedRate(BonnouEntity bonnouEntity)
        {
            animator.speed = baseSpeed * bonnouEntity.SpeedRate;
        }

        // Animationから呼ばれる
        public void EnableInRange()
        {
            isInSuccessFromAnimation = true;
        }
        
        // Animationからも呼ばれる
        public void DisableInRange()
        {
            isInSuccessFromAnimation = false;
        }
    }
}