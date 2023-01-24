using System;
using System.Linq;
using Logs;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EventsController", menuName = "Controllers/Game/EventsController")]
    public class EventsController : Controller
    {
        private Action StartGameEvent;
        private Action<int> ChangeGoldEvent;

        public void SubscribeOnChangeGold(Action<int> sender)
        {
            if (ChangeGoldEvent!= null && ChangeGoldEvent.GetInvocationList().Contains(sender))
            {
                LogManager.LogError($"Try 2 subscribes on ChangeGoldEvent");
            }
            else
            {
                ChangeGoldEvent += sender;
            }
        }

        public void UnsubscribeOnChangeGold(Action<int> sender)
        {
            ChangeGoldEvent -= sender;
        }

        public void ChangeGold(int value)
        {
            ChangeGoldEvent?.Invoke(value);
        }
    }
}