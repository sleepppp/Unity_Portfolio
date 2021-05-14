using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KSW
{
    public struct DialogData
    {
        public string Name;
        public string Dialog;
    }

    public class PanelDialog : UIBase
    {
        GameObject m_boxObject;
        Text m_nameText;
        Text m_dialogText;

        int m_currentDualogIndex = 0;

        List<DialogData> m_dialogList = new List<DialogData>();

        protected override void Start()
        {
            base.Start();

            m_boxObject = transform.Find("Box").gameObject;
            m_nameText = transform.Find("Box/Name").GetComponent<Text>();
            m_dialogText = transform.Find("Box/Text").GetComponent<Text>();

            transform.Find("Box/Background").GetComponent<CustomButton>().EventDown += OnEventNextDialog;

            GameEvent.instance.EventPlayDialog += OnStartDialog;
            //GameEvent.instance.EventEndDialog += OnEndDialog;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventPlayDialog -= OnStartDialog;
            //GameEvent.instance.EventEndDialog -= OnEndDialog;
        }

        void OnStartDialog(NPC npc)
        {
            // Test 용 다이어로그
            DialogData[] list = new DialogData[1];
            list[0].Dialog = "Hello Welcome To UnityWorld";
            list[0].Name = npc.characterName;

            OnStartDialog(list);
        }

        void OnStartDialog(DialogData[] dialogList)
        {
            m_dialogList.AddRange(dialogList);

            m_nameText.text = m_dialogList[0].Name;
            m_dialogText.text = m_dialogList[0].Dialog;
            m_currentDualogIndex = 0;
            m_boxObject.SetActive(true);
        }

        void OnEndDialog()
        {
            m_dialogList.Clear();
            m_boxObject.SetActive(false);

            StartCoroutine(CoroutineEndDialog());
        }

        void OnEventNextDialog()
        {
            m_currentDualogIndex++;
            if(m_currentDualogIndex >= m_dialogList.Count)
            {
                OnEndDialog();
            }
        }

        IEnumerator CoroutineEndDialog()
        {
            GameEvent.instance.OnEventFadeOut(0.8f);
            yield return new WaitForSeconds(0.8f);
            GameEvent.instance.OnEventFadeIn(0.8f);
            GameEvent.instance.OnEventEndDialog();
            GameManager.instance.gameMode.mainCamera.GetComponent<CameraController>().ReverseEvent(null);
        }
    }
}