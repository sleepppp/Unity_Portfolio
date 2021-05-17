using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public class GameManager : MonobehaviourSingleton<GameManager>
    {
        GameMode m_gameMode;

        public GameMode gameMode { get { return m_gameMode; } }

        public void Start()
        {
            GameData.instance.Init();
            QuestManager.instance.Init();

            m_gameMode = FindObjectOfType<GameMode>();
        }
    }
}