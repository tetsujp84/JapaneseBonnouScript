using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public class ModelBell : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private Vector3 power;
        [SerializeField] private ModelHammer hammer;

        public async UniTask DoAttackAsync()
        {
            await hammer.DoAttackAsync();
            rigidbody.AddForce(power);
        }
    }
}