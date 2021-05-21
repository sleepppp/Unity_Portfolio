using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCore
{
    public class PanelMainControlUI : UIBase
    {
        protected override void Start()
        {
            base.Start();

            GameEvent.instance.EventPlayDialog += OnEventStartDialog;
            GameEvent.instance.EventEndDialog += OnEventEndDialog;
        }

        private void OnDestroy()
        {
            GameEvent.instance.EventPlayDialog -= OnEventStartDialog;
            GameEvent.instance.EventEndDialog -= OnEventEndDialog;
        }

        void OnEventStartDialog(NPC npc)
        {
            gameObject.SetActive(false);
        }

        void OnEventEndDialog()
        {
            gameObject.SetActive(true);
        }
    }
}