using UnityEngine;

namespace Extentions
{ 
    public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        // Ara katmanda bırakmak için 
        private static volatile T instance = null;
    
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        GameObject newGO = new GameObject();
                        instance = newGO.AddComponent<T>();
                    }
                }
                
                return instance;
            }
        }
        protected virtual void Awake()
        { 
            instance = this as T;
        }
    }
}
