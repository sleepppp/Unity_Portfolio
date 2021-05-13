using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    //UI를 제외한 InGame Input관련 처리는 이곳에서 받습니다. 
    //UI로부터 조종 관련 InGame 입력 처리는 이곳으로 들어옵니다. 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]protected MovementObject m_controlTarget;

        public MovementObject controlTarget { get { return m_controlTarget; } }

        protected virtual void OnEnable()
        {
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
        }

        void UnBindInput()
        {
            GameEvent.instance.EventStartStickDrag -= OnEventStartStickDrag;
            GameEvent.instance.EventUpdateStickDrag -= OnEventUpdateStickDrag;
            GameEvent.instance.EventEndStickDrag -= OnEventEndStickDrag;
            GameEvent.instance.EventTouchWorld -= OnEventTouchWorld;
        }
        
        void OnEventStartStickDrag(Vector2 dir, float value)
        {

        }

        void OnEventUpdateStickDrag(Vector2 dir, float value)
        {
            if (m_controlTarget == null)
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

        public void SetTarget(MovementObject target)
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
