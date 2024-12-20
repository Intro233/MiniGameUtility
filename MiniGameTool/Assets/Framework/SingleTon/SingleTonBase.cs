using UnityEngine;

namespace Framework
{
    public abstract class SingleTonBase<T> where T : new()
    {
        private static T instance;
        private static readonly object locker = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
                return instance;
            }
            set { instance = value; }
        }
    }

    public abstract class SingleTonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).ToString();
                        DontDestroyOnLoad(obj);
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    }
}