using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    //Monobehaviour용 싱글톤
    public class MonobehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] instances = FindObjectsOfType<T>();

                    if(instances.Length > 1)
                    {
                        throw new System.Exception("Manager is too many");
                    }
                    else if(instances.Length == 1)
                    {
                        _instance = instances[0];
                    }
                    else
                    {
                        GameObject newObject = new GameObject();
                        _instance = newObject.AddComponent<T>();
                        DontDestroyOnLoad(_instance);
                    }
                }
                return _instance;
            }
        }
    }

}