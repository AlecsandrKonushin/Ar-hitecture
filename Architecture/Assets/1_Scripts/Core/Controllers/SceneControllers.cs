using UI;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Core
{
    public class SceneControllers : Singleton<SceneControllers>
    {
        [HideInInspector]
        public UnityEvent OnInitEvent;

        [SerializeField] private Controller[] sceneControllers;

        public void InitControllers()
        {
            DOTween.Sequence().AppendInterval(0.1f).OnComplete(() => // Wait initialize MonoControllers in Awake
            {
                BoxControllers.OnInit.AddListener(AfterInit);
                BoxControllers.InitControllers(sceneControllers);
            });            
        }

        private void AfterInit()
        {
            Debug.Log($"AfterInit");

            BoxControllers.OnInit.RemoveListener(AfterInit);
            UIManager.Instance.OnInitialize();

            OtherActions();

            OnInitEvent?.Invoke();
        }

        protected virtual void OtherActions() { }
    }
}