using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSVReader;

namespace KSW
{
    public class GameData : Singleton<GameData>
    {
        List<DialogData> m_dialogDatas;
        Dictionary<int,QuestData> m_questDatas;

        public Dictionary<int, QuestData> questDatas { get { return m_questDatas; } }

        public void Init()
        {
            LoadDialogData();
            LoadQuestData();

            System.GC.Collect();
        }

        void LoadDialogData()
        {
            Table table = TSVReader.Reader.ReadTSVToTable("Data/DialogTable");
            m_dialogDatas = table.TableToList<DialogData>();
        }

        void LoadQuestData()
        {
            Table table = TSVReader.Reader.ReadTSVToTable("Data/QuestTable");
            m_questDatas = table.TableToDictionary<int, QuestData>();
        }

        public DialogData GetDialogDataInID(int id)
        {
            if (m_dialogDatas == null)
                return null;
            if (m_dialogDatas.Count <= id || id < 0)
                return null;

            return m_dialogDatas[id];
        }
    }
}