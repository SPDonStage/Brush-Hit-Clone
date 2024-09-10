using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static object _lock = new object(); 
    public static T Instance { get; private set; }
    public void Awake()
    {
        lock (_lock) //prevent multi threads reach to
        {
            if (Instance != null)
            {
                Debug.LogWarning("There should not be more than one singleton of this type");
                Destroy(this);
                return;
            }
            else
            {
                Instance = this as T; 
                DontDestroyOnLoad(this);
            }
        }
    }

}

