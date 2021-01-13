using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class WindowManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WindowManager.Inst.SetParam<int>("score", 5500);
        _ = WindowManager.Inst.Open(WindowManager.WindowName.RANKING);
    }
}
