using System;
using System.IO;
using Data;
using Logs;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class LoadSaveManager : Singleton<LoadSaveManager>
    {
        [HideInInspector]
        public static UnityEvent OnLoad;

        private static SaveData saveData;

        protected override void AfterAwaik()
        {
            OnLoad = new UnityEvent();
        }

        public static void LoadData()
        {
            saveData = null;

            try
            {
                if (File.Exists(Application.persistentDataPath + MainData.PATH_SAVE))
                {
                    string strLoadJson = File.ReadAllText(Application.persistentDataPath + MainData.PATH_SAVE);
                    saveData = JsonUtility.FromJson<SaveData>(strLoadJson);                    
                }
                else
                {
                    LogManager.LogError($"<color=red>Not have save!</color>");
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError($"<color=red>Error load game</color> - {ex}");
            }

            OnLoad?.Invoke();
        }

        public static void SaveGame()
        {
            saveData = new SaveData();

            string jsonString = JsonUtility.ToJson(saveData);

            try
            {
                File.WriteAllText(Application.persistentDataPath + MainData.PATH_SAVE, jsonString);
            }
            catch (Exception ex)
            {
                Debug.Log($"<color=red>Не удалось сохранить игру - {ex}</color>");
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            SaveGame();            
        }

        public void DeleteAllSave()
        {
            if (File.Exists(Application.persistentDataPath + MainData.PATH_SAVE))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_SAVE);
            }

            if (File.Exists(Application.persistentDataPath + MainData.PATH_LOGS))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_LOGS);
            }


#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }

        public static void AddLog(bool error, string message)
        {
            string strLoadJson = "";

            if (error)
            {
                message = "EROOR: " + message;
            }

            if (File.Exists(Application.persistentDataPath + MainData.PATH_LOGS))
            {
                strLoadJson = File.ReadAllText(Application.persistentDataPath + MainData.PATH_LOGS);
                strLoadJson += $"\n{message}";
            }

            try
            {
                File.WriteAllText(Application.persistentDataPath + MainData.PATH_LOGS, strLoadJson);
            }
            catch (Exception ex)
            {
                Debug.Log($"<color=red>Error save logs - {ex}</color>");
            }
        }
    }
}