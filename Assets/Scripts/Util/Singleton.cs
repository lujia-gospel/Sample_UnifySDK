using UnityEngine;
using System.Collections;

public class Singleton<T> where T : new()
{
	class SingletonCreator
	{
		internal static readonly T instance = new T();
	}
	
	public static T Get()
	{
		return SingletonCreator.instance;
	}
}

/// <summary>
/// MonoMehaviour组件派生类的单列
/// </summary>
/// <typeparam name="T">MonoBehaviour</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = Object.FindObjectOfType<T>();
                if (m_Instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    m_Instance = go.AddComponent<T>();
                    if(Application.isPlaying)
                        Object.DontDestroyOnLoad(m_Instance);
                }
            }
            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        m_Instance = this as T;
    }
    public static T Get()
    {
        return Instance;
    }



    /// <summary>
    /// 销毁
    /// </summary>
    public static void Destroy()
    {
        if (m_Instance != null)
        {
            Object.Destroy(m_Instance.gameObject);
            m_Instance = null;
        }
    }
}
