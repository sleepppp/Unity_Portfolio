using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KSW
{
    //게임에서 발생하는 이벤트들은 해당 클래스를 통해서 전달됩니다. 
    //UI <-> GameEvent <-> InGame
    public class GameEvent : Singleton<GameEvent>
    {
        public event Action<Vector2, float> EventStartStickDrag;
        public event Action<Vector2, float> EventUpdateStickDrag;
        public event Action EventEndStickDrag;

        public event Action<Vector2> EventTouchWorld;

        public event Action<Vector2> EventScreenScroll;

        public event Action<float> EventZoomInOut;

        public void OnEventStartStickDrag(Vector2 dir, float value)
        {
            EventStartStickDrag?.Invoke(dir, value);
        }

        public void OnEventUpdateStickDrag(Vector2 dir, float value)
        {
            EventUpdateStickDrag?.Invoke(dir, value);
        }

        public void OnEventEndStickDrag()
        {
            EventEndStickDrag?.Invoke();
        }

        public void OnEventTouchWorld(Vector2 screenPoint)
        {
            EventTouchWorld?.Invoke(screenPoint);
        }

        public void OnEventScreenScroll(Vector2 movedVector)
        {
            EventScreenScroll?.Invoke(movedVector);
        }

        public void OnEventZoomInOut(float value)
        {
            EventZoomInOut?.Invoke(value);
        }
    }
}