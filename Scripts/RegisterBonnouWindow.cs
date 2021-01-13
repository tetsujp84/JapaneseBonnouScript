using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UserBonnou;
using TMPro;

public class RegisterBonnouWindow : BaseWindow
{

    [SerializeField]
    InputField _inputField_bonnouTitle;

    [SerializeField]
    InputField _inputField_bonnouDescription;

    [SerializeField]
    Button _btn_register;

    [SerializeField]
    Button _btn_close;

    [SerializeField]
    Text _txt_caution;

    [SerializeField]
    TMP_Text _txt_description_afterRegister;

    public override async UniTask Open(WindowManager.WindowName windowName)
    {
        _canvasGroup.alpha = 0;
        _txt_caution.gameObject.SetActive(false);
        _txt_description_afterRegister.gameObject.SetActive(false);
        _btn_register.onClick.AddListener(() => OnClickButton_Register().Forget());
        _btn_close.onClick.AddListener(() => OnClickButton_Close());
        

        await base.Open(windowName);
    }

    public async UniTaskVoid OnClickButton_Register()
    {
        if (string.IsNullOrEmpty(_inputField_bonnouTitle.text))
        {
            _txt_caution.gameObject.SetActive(true);
        }
        else
        {
            _txt_caution.gameObject.SetActive(false);

            string title = _inputField_bonnouTitle.text;
            string description = _inputField_bonnouDescription.text;

            _btn_register.gameObject.SetActive(false);
            _txt_description_afterRegister.gameObject.SetActive(true);


            await BonnouAPI.RegisterBonnou(title, description);

            await UniTask.Delay(1000);
            WindowManager.Inst.SetParam<bool>("registeredBonnou", true);

            WindowManager.Inst.Close(WindowName).Forget();
        }
        
    }

    public void OnClickButton_Close()
    {
        WindowManager.Inst.Close(WindowName).Forget();
    }

    
}
