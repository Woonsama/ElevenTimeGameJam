using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Tang3.Common.Management
{
    public class SingletonMonoBase<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static bool shuttingDown = false;
        private static object @lock = new object();
        private static T instance;

        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                    return null;
                }

                lock (@lock)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                    return instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            shuttingDown = true;
        }


        private void OnDestroy()
        {
            shuttingDown = true;
        }
    }
}


