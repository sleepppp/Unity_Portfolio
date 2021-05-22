using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSVReader;

namespace MyCore
{
    public class GameData : Singleton<GameData>
    {
        List<DialogData> m_dialogData;
        Dictionary<int,QuestData> m_questData;
        Dictionary<int, SkillData> m_skillData;

        public Dictionary<int, QuestData> questData { get { return m_questData; } }
        public Dictionary<int,SkillData> skillData { get { return m_skillData; } }

        public void Init()
        {
            LoadDialogData();
            LoadQuestData();
            LoadSkillData();
        }

        void LoadDialogData()
        {
            Table table = TSVReader.Reader.ReadTSVToTable("Data/DialogTable");
            m_dialogData = table.TableToList<DialogData>();
        }

        void LoadQuestData()
        {
            Table table = TSVReader.Reader.ReadTSVToTable("Data/QuestTable");
            m_questData = table.TableToDictionary<int, QuestData>();
        }

        void LoadSkillData()
        {
            Table table = TSVReader.Reader.ReadTSVToTable("Data/SkillTable");
            m_skillData = table.TableToDictionary<int, SkillData>();
        }

        public DialogData GetDialogDataInID(int id)
        {
            if (m_dialogData == null)
                return null;
            if (m_dialogData.Count <= id || id < 0)
                return null;

            return m_dialogData[id];
        }
    }
}