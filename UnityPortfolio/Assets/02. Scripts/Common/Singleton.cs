using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    //일반 싱글톤
    public class Singleton<T> where T : class, new()
    {
        static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}