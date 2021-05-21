using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCore
{
    public struct DialogStruct
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

        List<DialogStruct> m_dialogList = new List<DialogStruct>();

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
            int[] arrDialogID = npc.dialogID;
            DialogStruct[] dialogList = new DialogStruct[arrDialogID.Length];
            for(int i =0; i < arrDialogID.Length; ++i)
            {
                DialogData data = GameData.instance.GetDialogDataInID(arrDialogID[i]);
                if(data == null)
                {
                    Debug.LogError("NPC::OnStartDialog::Can't Load Dialog Data");
                }
                dialogList[i].Name = npc.characterName;
                dialogList[i].Dialog = data.Dialog;
            }

            OnStartDialog(dialogList);
        }

        void OnStartDialog(DialogStruct[] dialogList)
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

        void SetDialog(DialogStruct data)
        {
            m_nameText.text = data.Name;
            m_dialogText.text = data.Dialog;
        }

        void OnEventNextDialog()
        {
            m_currentDualogIndex++;
  
            if (m_currentDualogIndex >= m_dialogList.Count)
            {
                OnEndDialog();
            }
            else
            {
                SetDialog(m_dialogList[m_currentDualogIndex]);
            }
        }

        IEnumerator CoroutineEndDialog()
        {
            GameEvent.instance.OnEventFadeOut(0.8f);
            yield return new WaitForSeconds(0.8f);
            GameEvent.instance.OnEventFadeIn(0.8f);
            GameEvent.instance.OnEventEndDialog();
            GameEvent.instance.OnEventShowHUD();
            GameManager.instance.gameMode.mainCamera.GetComponent<CameraController>().ReverseEvent(null);
            GameManager.instance.gameMode.mainCamera.GetComponent<CameraController>().RemovePlayerLayerInCullingMask();
        }
    }
}