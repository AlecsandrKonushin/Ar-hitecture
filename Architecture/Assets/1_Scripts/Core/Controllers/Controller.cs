using UnityEngine;

namespace Core
{
    public class Controller : ScriptableObject, IController
    {
        protected bool pause = true;

        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        public void SetPause(bool value)
        {
            pause = value;
        }

        public virtual void Save() { }
    }
}