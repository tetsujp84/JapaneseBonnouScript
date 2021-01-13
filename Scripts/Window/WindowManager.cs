using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class WindowManager : MonoBehaviour
{
    static WindowManager inst;

    public static WindowManager Inst
    {
        get
        {
            return inst;
        }
    }

    public enum LayerName
    {
        MAIN = 1000,
        OVERLAY = 2000,
    }

    public enum WindowName
    {
        RANKING,
        BONNOU_REGISTER,
    }

    Dictionary<WindowName, string> _windowNameDict = new Dictionary<WindowName, string>()
    {
        { WindowName.RANKING, "RankingWindow" },
        { WindowName.BONNOU_REGISTER, "RegisterBonnouWindow" },
    };

    Dictionary<WindowName, GameObject> _windowObjectCacheDict = new Dictionary<WindowName, GameObject>();

    Dictionary<LayerName, GameObject> _layerObjectDict = new Dictionary<LayerName, GameObject>();

    Hashtable _windowParams = new Hashtable();

    Dictionary<LayerName,List<BaseWindow>> _instantiatedWindowList = new Dictionary<LayerName, List<BaseWindow>>();

    public async UniTaskVoid Open(WindowName windowName,LayerName layerName=LayerName.MAIN)
    {
        if (!IsExistWindow(windowName,layerName))
        {
            var windowObject = await InstantiateWindowPrefab(windowName, layerName);
            BaseWindow baseWindow = windowObject.GetComponent<BaseWindow>();
            _instantiatedWindowList[layerName].Add(baseWindow);
            SortWindow();
            await baseWindow.Open(windowName);
        }

    }

    public async UniTaskVoid Close(WindowName windowName,LayerName layerName = LayerName.MAIN)
    {
        if (IsExistWindow(windowName,layerName))
        {
            BaseWindow baseWindow = _instantiatedWindowList[layerName].Find(v=>v.WindowName == windowName);
            await baseWindow.Close();
            _instantiatedWindowList[layerName].Remove(baseWindow);
            Destroy(baseWindow.gameObject);
        }
    }

    public bool IsExistWindow(WindowName windowName, LayerName layerName)
    {
        var window = _instantiatedWindowList[layerName].Find(v => v.WindowName == windowName);

        return window != null;
    }

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupLayer();
    }

    void SetupLayer()
    {
        foreach(LayerName layerName in Enum.GetValues(typeof(LayerName)))
        {
            var layer = new GameObject(layerName.ToString());
            layer.transform.SetParent(transform);
            _layerObjectDict.Add(layerName, layer);
            _instantiatedWindowList.Add(layerName, new List<BaseWindow>());

        }
    }

    void SortWindow()
    {
        foreach(LayerName layerName in _instantiatedWindowList.Keys)
        {
            for(int windowIndex = 0;windowIndex < _instantiatedWindowList[layerName].Count; windowIndex++)
            {
                _instantiatedWindowList[layerName][windowIndex].SetSortingOrder((int)layerName + windowIndex);
            }
        }
    }

    async UniTask<GameObject> InstantiateWindowPrefab(WindowName windowName, LayerName layerName = LayerName.MAIN)
    {
        if (!_windowObjectCacheDict.ContainsKey(windowName))
        {
            GameObject windowObject = await Resources.LoadAsync<GameObject>("Window/" + _windowNameDict[windowName]) as GameObject;
            _windowObjectCacheDict.Add(windowName, windowObject);
        }

        var windowInstance = Instantiate(_windowObjectCacheDict[windowName], _layerObjectDict[layerName].transform);

        return windowInstance;
    }

    public void SetParam<T>(string valueName,T value= default(T))
    {
        _windowParams[valueName] = value;
    }

    public T GetParam<T>(string valueName)
    {
        return (T)_windowParams[valueName];
    }
}
