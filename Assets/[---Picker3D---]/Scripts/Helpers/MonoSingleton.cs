using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static volatile T instance = null;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}