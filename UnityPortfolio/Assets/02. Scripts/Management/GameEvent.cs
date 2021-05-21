using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyCore
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

        public event Action<float> EventFadeOut;
        public event Action<float> EventFadeIn;

        public event Action<Character, Transform> EventCreateHUD;
        public event Action<Character> EventRemoveHUD;

        public event Action<Character, Transform,Action<InteractionHUD>> EventCreateInteractionHUD;
        public event Action<Character> EventRemoveInteractionHUD;

        public event Action<NPC> EventPlayDialog;

        public event Action EventEndDialog;

        public event Action EventHideHUD;
        public event Action EventShowHUD;

        public event Action<GameState> EventChangeGameState;

        public event Action<string, float, bool> EventNotice;

        public event Action<MailInfo> EventArriveMail;
        public event Action<MailInfo> EventRemoveMail;

        public event Action<Enemy> EventDeadEnemy;

        public event Action<SkillData> EventBindSkill;

        public event Func<bool> EventPlayAutoPlay;
        public event Action EventStopAutoPlay;

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

        public void OnEventFadeOut(float time)
        {
            EventFadeOut?.Invoke(time);
        }

        public void OnEventFadeIn(float time)
        {
            EventFadeIn?.Invoke(time);
        }

        public void OnEventCreateHUD(Character character,Transform hudPoint)
        {
            EventCreateHUD?.Invoke(character, hudPoint);
        }

        public void  OnEventRemoveHUD(Character character)
        {
            EventRemoveHUD?.Invoke(character);
        }

        public void OnEventCreateInteractionHUD(Character character, Transform hudPoint, Action<InteractionHUD> notifyEvent)
        {
            EventCreateInteractionHUD?.Invoke(character, hudPoint, notifyEvent);
        }

        public void OnEventRemoveInteractionHUD(Character character)
        {
            EventRemoveInteractionHUD?.Invoke(character);
        }

        public void OnEventPlayDialog(NPC npc)
        {
            EventPlayDialog?.Invoke(npc);
        }

        public void OnEventEndDialog()
        {
            EventEndDialog?.Invoke();
        }

        public void OnEventHideHUD()
        {
            EventHideHUD?.Invoke();
        }

        public void OnEventShowHUD()
        {
            EventShowHUD?.Invoke();
        }
       
        public bool OnEventPlayAutoPlay()
        {
            if (EventPlayAutoPlay != null)
                return EventPlayAutoPlay();
            return false;
        }

        public void OnEventStopAutoPlay()
        {
            EventStopAutoPlay?.Invoke();
        }

        public void OnEventChangeGameState(GameState newState)
        {
            EventChangeGameState?.Invoke(newState);
        }

        public void OnEventNotice(string text, float time, bool ignoreOverlap)
        {
            EventNotice?.Invoke(text, time, ignoreOverlap);
        }

        public void OnEventArriveMail(MailInfo info)
        {
            EventArriveMail?.Invoke(info);
        }

        public void OnEventRemoveMail(MailInfo info)
        {
            EventRemoveMail?.Invoke(info);
        }

        public void OnEventDeadEnemy(Enemy enemy)
        {
            EventDeadEnemy?.Invoke(enemy);
        }

        public void OnEventBindSkill(SkillData data)
        {
            EventBindSkill?.Invoke(data);
        }
    }
}