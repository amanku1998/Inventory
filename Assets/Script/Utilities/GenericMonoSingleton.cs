using UnityEngine;

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    public static T Instance { get { return instance;} }
    private static T instance;

    public void Awake()
    {
        if (instance == null)
        {
            Debug.Log($"Awake called for {typeof(T).Name}");
            instance = (T)this;
            DontDestroyOnLoad(this.gameObject);
            OnAwake();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnAwake() { }
}
