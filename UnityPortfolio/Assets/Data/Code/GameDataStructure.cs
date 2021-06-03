using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace MyCore.Data
{
	public class GameDataStructure
    {
		public string DialogDataPath = "Assets/Data/TSV/DialogData.tsv";
		public Dictionary<int,DialogData> DialogData;

		public string QuestDataPath = "Assets/Data/TSV/QuestData.tsv";
		public Dictionary<int,QuestData> QuestData;

		public string SkillDataPath = "Assets/Data/TSV/SkillData.tsv";
		public Dictionary<int,SkillData> SkillData;

		public GameDataStructure()
		{
			DialogData  =  TableStream.LoadTableByTSV(DialogDataPath).TableToDictionary<int,DialogData>();
			QuestData  =  TableStream.LoadTableByTSV(QuestDataPath).TableToDictionary<int,QuestData>();
			SkillData  =  TableStream.LoadTableByTSV(SkillDataPath).TableToDictionary<int,SkillData>();
		}
	}
}
