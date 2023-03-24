using System;
using System.Linq;
using Logs;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EventsController", menuName = "Controllers/Game/EventsController")]
    public class EventsController : Controller
    {
        private Action changeLocalization;

        private Action StartGameEvent;

        public void SubscribeOnChangeLocalization(Action sender)
        {
            if (changeLocalization != null && changeLocalization.GetInvocationList().Contains(sender))
            {
                LogManager.LogError($"Try 2 subscribes on ChangeGoldEvent");
            }
            else
            {
                changeLocalization += sender;
            }
        }

        public void UnsubscribeOnChangeLocalization(Action sender)
        {
            changeLocalization -= sender;
        }

        public void ChangeLocalization()
        {
            changeLocalization?.Invoke();
        }
    }
}