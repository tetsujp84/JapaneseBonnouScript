using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class ClearedBonnouViewController : MonoBehaviour
    {
        [SerializeField] private ClearedBonnouView clearedBonnouView;
        public void Show(BonnouEntity clearedEntity)
        {
            for (int i = 0; i < clearedEntity.Score; i++)
            {
                var instance = Instantiate(clearedBonnouView, clearedBonnouView.transform.parent);
                instance.Show(clearedEntity);   
            }
        }
    }
}
