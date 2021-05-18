using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSW
{
    public interface ICharacterCommand
    {
        void OnStart();
        void OnUpdate();
        void OnEnd();
        bool IsEnd();
    }
}

