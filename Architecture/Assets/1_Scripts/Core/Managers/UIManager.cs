using System;
using System.Collections.Generic;
using Logs;
using UI.Windows;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Dictionary<Type, Window> windows;

        #region INITIALIZE

        public void OnInitialize()
        {
            Window[] getWindows = GetComponentsInChildren<Window>();
            windows = new Dictionary<Type, Window>();

            foreach (var window in getWindows)
            {
                windows.Add(window.GetType(), window);
            }

            foreach (var window in windows)
            {
                window.Value.OnInitialize();
            }

            foreach (var window in windows)
            {
                window.Value.OnStart();
            }
        }

        #endregion INITIALIZE

        #region GET/SHOW/HIDE

        public static T GetWindow<T>() where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                return window as T;
            }
            else
            {
                LogManager.LogError($"Not have window {typeof(T)} !");

                return null;
            }
        }

        public static void ShowWindow<T>() where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                window.Show();
            }
            else
            {
                LogManager.LogError($"Not have window {typeof(T)} for show!");
            }
        }

        public static void HideWindow<T>() where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                window.Hide();
            }
            else
            {
                LogManager.LogError($"Not have window {typeof(T)} for close");
            }
        }

        public static void ChangeLanguage()
        {
            foreach (var window in Instance.windows)
            {
                window.Value.ChangeLanguage();
            }
        }

        #endregion    
    }
}