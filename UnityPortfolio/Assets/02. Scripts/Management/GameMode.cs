using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public enum GameState
    {
        None = -1,
        Normal = 0,
        Battle = 1,
        Dialog = 2
    }

    //게임의 룰 처리는 이곳에서 처리합니다.
    //스테이지 별로 다른 룰이 적용된다 하면 GameMode를 상속 받아서 구현합니다.
    public class GameMode : MonoBehaviour
    {
        [SerializeField] protected PlayerController m_playerController;
        [SerializeField] protected PlayerCharacter m_playerObject;
        [SerializeField] protected Camera m_mainCamera;
        GameState m_gameState;

        List<Enemy> m_enemyList = new List<Enemy>();

        public PlayerController playerContoller { get { return m_playerController; } }
        public PlayerCharacter playerObject { get { return m_playerObject; } }
        public Camera mainCamera { get { return m_mainCamera; } }
        public List<Enemy> enemyList { get { return m_enemyList; } }

        public GameState gameState { get { return m_gameState; } }

        public virtual void Start()
        {
            if (m_playerController == null)
                m_playerController = FindObjectOfType<PlayerController>();
            if (m_mainCamera == null)
                m_mainCamera = Camera.main;

            m_enemyList.AddRange(FindObjectsOfType<Enemy>());

            m_playerController.SetTarget(m_playerObject);

            GameEvent.instance.OnEventFadeIn(3f);
            m_gameState = GameState.Normal;
            GameEvent.instance.EventDeadEnemy += OnEventDeadEnemy;
        }

        public void ChangeGameState(GameState newState)
        {
            if (m_gameState == newState)
                return;

            m_gameState = newState;
        }

        public bool CanAutoBattle()
        {
            if (m_gameState == GameState.Dialog)
                return false;

            return true;
        }

        public void OnEventDeadEnemy(Enemy enemy)
        {
            m_enemyList.Remove(enemy);
        }
    }
}
