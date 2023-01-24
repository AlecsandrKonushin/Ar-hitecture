using UnityEngine;

namespace Core
{
    public abstract class MonoController : MonoBehaviour, IController
    {
        protected bool pause = true;

        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        private void Awake()
        {
            BoxControllers.AddMonoController(GetType(), this);
        }

        protected void Reset()
        {
            name = GetType().Name;
        }

        public void SetPause(bool value)
        {
            pause = value;
        }

        public virtual void Save() { }
    }
}
