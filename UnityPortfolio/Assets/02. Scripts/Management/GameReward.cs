using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class GameReward : MonobehaviourSingleton<GameReward>
    {
        public void ApplyReward(int itemID)
        {
            //TODO 임시용. 추후에 데이터 생성되면 수정 요망
            GameObject katanaObject = Resources.Load("Prefabs/Weapon/Katana") as GameObject;

            GameObject newObject = Instantiate(katanaObject);
            CharacterEquipment equip =
                GameManager.instance.gameMode.playerObject.GetComponent<CharacterEquipment>();

            equip.Equipped(equip.backJoint, newObject.transform);
            equip.SetMainWeapon(newObject.transform);

            GameEvent.instance.OnEventNotice("Katana를 손에 넣었다", 2f, false);

        }
    }
}