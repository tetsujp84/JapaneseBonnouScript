using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UserBonnou;

public class BonnouAPITest : MonoBehaviour
{
    // Start is called before the first frame update
    async UniTaskVoid Start()
    {
        //string resultString = await BonnouAPI.RegisterBonnou("5000兆円欲しい！！!", "欲望に正直", 6);

        var requestBonnouList = new RequestBonnouList();
        requestBonnouList.request.Add(new RequestBonnou(1, 1));
        requestBonnouList.request.Add(new RequestBonnou(2, 1));

        UserData.Init();
        await UserData.GetUserBonnouListFromServer(requestBonnouList);
        string resultString = await BonnouAPI.GetBonnou(requestBonnouList);

        Debug.Log(resultString);

        WindowManager.Inst.Open(WindowManager.WindowName.BONNOU_REGISTER).Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
