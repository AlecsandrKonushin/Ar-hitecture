using UI.Buttons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class Window : MonoBehaviour, IInitialize
    {
        [HideInInspector]
        public UnityEvent EndShow, EndChange, EndHide;

        protected CloseButton closeButton;
        protected GameObject background;

        public void OnInitialize()
        {
            closeButton = GetComponentInChildren(typeof(CloseButton), true) as CloseButton;
            background = transform.GetChild(0).gameObject;

            if (closeButton != null)
            {
                closeButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Hide();
                    EndHide?.Invoke();
                });
            }
        }

        public virtual void OnStart() { }

        public virtual void Show() { }
        public virtual void Hide() { }
        public virtual void Change() { }

        public virtual void ChangeLanguage() { }        
    }
}