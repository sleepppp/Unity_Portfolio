using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace KSW
{
    public class UINavigation : MonobehaviourSingleton<UINavigation>
    {
        Stack<UIView> m_history = new Stack<UIView>();
        Canvas m_mainCanvas;

        private void Start()
        {
            m_mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        }

        public UIView Push(string prefabName)
        {
            GameObject prefab = Resources.Load("Prefabs/UI/" + prefabName) as GameObject;

            GameObject newObject = Instantiate(prefab);
            Canvas canvas = newObject.GetComponent<Canvas>();

            canvas.sortingOrder = m_history.Count + 1;

            UIView view = newObject.GetComponent<UIView>();

            view.Show();

            if(m_history.Count != 0)
            {
                m_history.Peek().GetComponent<GraphicRaycaster>().enabled = false;
                //m_history.Peek().Hide();
            }

            m_history.Push(view);

            m_mainCanvas.GetComponent<GraphicRaycaster>().enabled = false;

            return view;
        }

        public void Pop()
        {
            if (m_history.Count == 0)
                return;

            m_history.Peek().Hide();
            Destroy(m_history.Peek().gameObject);
            m_history.Pop();

            if (m_history.Count == 0)
            {
                m_mainCanvas.GetComponent<GraphicRaycaster>().enabled = true;
            }
            else
            {
                m_history.Peek().GetComponent<GraphicRaycaster>().enabled = true;
            }
        }
    }
}