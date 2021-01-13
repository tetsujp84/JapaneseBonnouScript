using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonnouScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainBonnouText;
    [SerializeField] private float largeTextSize;
    [SerializeField] private GameObject clearText;
    
    public void SetRemainBonnou(int remain)
    {
        remainBonnouText.SetText(remain.ToString());
        if (remain < 10)
        {
            remainBonnouText.fontSize = largeTextSize;
        }
        if (remain <= 0)
        {
            gameObject.SetActive(false);
            clearText.SetActive(true);
        }
    }
}
