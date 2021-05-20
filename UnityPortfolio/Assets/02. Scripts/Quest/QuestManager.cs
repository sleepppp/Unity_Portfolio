using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace KSW
{
    public class QuestManager : MonobehaviourSingleton<QuestManager>
    {
        List<QuestBase> m_executeList = new List<QuestBase>();
        Dictionary<int, QuestBase> m_questList = new Dictionary<int, QuestBase>();

        public void Init()
        {
            Dictionary<int, QuestData> questDatas = GameData.instance.questDatas;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string ksw = "KSW.";
            foreach (KeyValuePair<int,QuestData> item in questDatas)
            {
                QuestBase questBase =
                  assembly.CreateInstance(ksw + item.Value.ClassName) as QuestBase;
                questBase.data = item.Value;

                m_questList.Add(item.Value.ID, questBase);
            }

            //퀘스트 상태 확인. 이곳에서 현재 실행할 퀘스트와 완료된 퀘스트를 구분합니다 
            //CheckQuestState();

            QuestBase testQuest;
            m_questList.TryGetValue(1, out testQuest);
            StartQuest(testQuest);
        }

        private void Update()
        {
            for(int i =0; i < m_executeList.Count; ++i)
            {
                m_executeList[i].Update();

                if(m_executeList[i].isFail)
                {

                }
                else if(m_executeList[i].isSucceeded)
                {
                    m_executeList.RemoveAt(i);
                    --i;
                }
            }
        }

        public void CheckQuestState()
        {

        }

        public void StartQuest(QuestBase data)
        {
            data.Start();
            m_executeList.Add(data);
        }
    }
}