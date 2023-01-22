using UnityEngine;

namespace Core
{
    public class MonoController : MonoBehaviour, IController
    {
        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        public void Pause(bool pause) { }

        private void Awake()
        {
            BoxControllers.AddMonoController(GetType(), this);
        }
    }
}
