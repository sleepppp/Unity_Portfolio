using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public static class TransformExtention
    {
        //부모에서 이름으로 찾기 
        public static Transform FindByParent(this Transform transform, string name)
        {
            Transform current = transform;
            while (true)
            {
                if (current == null)
                    return null;

                if (current.name == name)
                    return current;

                current = current.parent;
            }
        }

        //부모에서 컴포넌트 찾아오기
        public static T FindComponentByParent<T>(this Transform transform) where T : Component
        {
            Transform current = transform;
            T result = null;
            while (true)
            {
                if (current == null)
                    return null;

                result = current.GetComponent<T>();
                if (result)
                    return result;

                current = current.parent;
            }
        }
    }
}