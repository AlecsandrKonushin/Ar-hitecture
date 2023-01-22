using Core;
using SaveSystem;
using UI.Windows;
using UnityEngine;

namespace Logs
{
    public class LogManager : Singleton<LogManager>
    {
        private bool isNeedLog = true;

        [SerializeField] private LogWindow logWindow;

        public bool SetIsNeedLog
        {
            set 
            { 
                isNeedLog = value;
            }
        }

        public static void Log(string message)
        {
            if (Instance.isNeedLog)
            {
                Debug.Log(message);

                Instance.logWindow.Log(message);

                LoadSaveManager.AddLog(false, message);
            }
        }

        public static void LogError(string error)
        {
            if (Instance.isNeedLog)
            {
                Debug.Log($"<color=red>{error}</color>");

                Instance.logWindow.LogError(error);

                LoadSaveManager.AddLog(true, error);
            }
        }
    }
}