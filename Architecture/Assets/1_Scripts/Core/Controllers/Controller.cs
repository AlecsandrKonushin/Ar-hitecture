using UnityEngine;

namespace Core
{
    public class Controller : ScriptableObject, IController
    {
        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        public virtual void Pause(bool pause) { }

        public virtual void Save() { }
    }
}