using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSVReader;
using MyCore.Data;

namespace MyCore
{
    public class GameData : Singleton<GameData>
    {
        GameDataStructure m_data;

        public Dictionary<int, QuestData> questData { get { return m_data.QuestData; } }
        public Dictionary<int,SkillData> skillData { get { return m_data.SkillData; } }

        public void Init()
        {
            m_data = new GameDataStructure();
        }

        public DialogData GetDialogDataInID(int id)
        {
            DialogData result = null;
            m_data.DialogData.TryGetValue(id, out result);
            return result;
        }
    }
}