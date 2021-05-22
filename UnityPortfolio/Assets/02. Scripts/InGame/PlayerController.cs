using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    //UI를 제외한 InGame Input관련 처리는 이곳에서 받습니다. 
    //UI로부터 조종 관련 InGame 입력 처리는 이곳으로 들어옵니다. 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]protected PlayerCharacter m_controlTarget;

        //Queue<ICharacterCommand> m_commandList = new Queue<ICharacterCommand>();

        public PlayerCharacter controlTarget { get { return m_controlTarget; } }

        protected virtual void OnEnable()
        {
            UnBindInput();  //안정성 처리
            BindInput();
        }

        protected virtual void OnDisable()
        {
            UnBindInput();
        }

        void BindInput()
        {
            GameEvent.instance.EventStartStickDrag += OnEventStartStickDrag;
            GameEvent.instance.EventUpdateStickDrag += OnEventUpdateStickDrag;
            GameEvent.instance.EventEndStickDrag += OnEventEndStickDrag;
            GameEvent.instance.EventTouchWorld += OnEventTouchWorld;
            GameEvent.instance.EventPlayAutoPlay += OnEventPlayAutoPlay;
            GameEvent.instance.EventStopAutoPlay += OnEventStopAutoPlay;
        }

        void UnBindInput()
        {
            GameEvent.instance.EventStartStickDrag -= OnEventStartStickDrag;
            GameEvent.instance.EventUpdateStickDrag -= OnEventUpdateStickDrag;
            GameEvent.instance.EventEndStickDrag -= OnEventEndStickDrag;
            GameEvent.instance.EventTouchWorld -= OnEventTouchWorld;
            GameEvent.instance.EventPlayAutoPlay -= OnEventPlayAutoPlay;
            GameEvent.instance.EventStopAutoPlay -= OnEventStopAutoPlay;
        }

        bool OnEventPlayAutoPlay()
        {
            if (m_controlTarget.IsEquippedWeapon() == false)
            {
                GameEvent.instance.OnEventNotice("무기가 없어 배틀모드로 진입할 수 없습니다", 2f, true);
                return false;
            }

            m_controlTarget.ChangeBattleMode(true);

            return true;
        }

        void OnEventStopAutoPlay()
        {
            m_controlTarget.ChangeBattleMode(false);
        }
        
        void OnEventStartStickDrag(Vector2 dir, float value)
        {

        }

        void OnEventUpdateStickDrag(Vector2 dir, float value)
        {
            if (m_controlTarget == null)
                return;

            if (value <= 0.2f)
                return;

            Vector3 worldDir = ConvertStickInfoByCamera(dir);
            m_controlTarget.Move(worldDir);
        }

        void OnEventEndStickDrag()
        {
            if (m_controlTarget == null)
                return;

            m_controlTarget.MoveEnd();
        }

        void OnEventTouchWorld(Vector2 touchPoint)
        {
            Camera camera = GameManager.instance.gameMode.mainCamera;
            Ray ray = camera.ScreenPointToRay(touchPoint);
            RaycastHit rayResult;

            if (Physics.Raycast(ray,out rayResult))
            {
                if (rayResult.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
                {
                    if (m_controlTarget != null)
                        m_controlTarget.MoveTo(rayResult.point);
                }
            }
        }

        public void SetTarget(PlayerCharacter target)
        {
            m_controlTarget = target;
            m_controlTarget.SetOwner(this);
        }

        Vector3 ConvertStickInfoByCamera(Vector2 stickDir)
        {
            Vector3 worldDir = new Vector3(stickDir.x, 0f, stickDir.y);
            Vector3 finalDir = GameManager.instance.gameMode.mainCamera.transform.TransformDirection(worldDir);
            finalDir.y = 0f;
            finalDir *= 10f;
            finalDir.Normalize();
            //finalDir.Normalize();
            return finalDir;
        }
    }
}
