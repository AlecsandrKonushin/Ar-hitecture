using UI;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Gameplay;

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
            BoxControllers.OnInit.RemoveListener(AfterInit);
            UIManager.Instance.OnInitialize();
            BoxControllers.OnInit.AddListener(AfterStartControllers);
            BoxControllers.StartControllers();
        }

        private void AfterStartControllers()
        {
            BoxControllers.OnInit.RemoveListener(AfterStartControllers);

            OtherActions();

            OnInitEvent?.Invoke();
        }

        protected virtual void OtherActions()
        {
        }
    }
}