using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        public void SetTimer(int timerCount)
        {
            timerText.SetText(timerCount.ToString());
        }
    }
}