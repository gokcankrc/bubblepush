using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppBootstrapper 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void CreateManagers()
    {
        Debug.Log("Create Managers");
        var managers = Resources.Load("Managers");
        var mangerInstance = UnityEngine.Object.Instantiate(managers);
        UnityEngine.Object.DontDestroyOnLoad(mangerInstance);
    }
}
