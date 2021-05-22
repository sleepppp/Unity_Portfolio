using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace MyCore
{
    public class SkillSet
    {
        public static void BindSKill(BattleCharacter character,SkillData data)
        {
            SkillBase skill = CreateSkill(data);
            if(skill == null)
            {
                Debug.LogError("Can't Create Skill");
                return;
            }
            skill.Init(character, data);
            character.AddSkill(skill);
        }

        static SkillBase CreateSkill(SkillData data)
        {
            string myCore = "MyCore.";

            System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
            SkillBase skill = assembly.CreateInstance(myCore + data.ClassName) as SkillBase;
            return skill;
        }
    }
}