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

        public static bool IsHaveSave;

        private SaveData saveData;

        public static float MusicVolume
        {
            get => Instance.saveData.MusicVolume;
            set => Instance.saveData.MusicVolume = value;
        }

        public static float SoundsVolume
        {
            get => Instance.saveData.SoundsVolume;
            set => Instance.saveData.SoundsVolume = value;
        }


        protected override void AfterAwaik()
        {
            OnLoad = new UnityEvent();
        }

        public static void LoadData()
        {
            Instance.saveData = null;
            IsHaveSave = false;
            
            try
            {
                if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_SAVE))
                {
                    string strLoadJson = File.ReadAllText(Application.persistentDataPath + SCRO_MainData.PATH_SAVE);
                    Instance.saveData = JsonUtility.FromJson<SaveData>(strLoadJson);

                    IsHaveSave = true;
                }
                else
                {
                    LogManager.LogError($"Not have save!");
                    Instance.saveData = new SaveData();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError($"Error load game - {ex}");
            }

            OnLoad?.Invoke();
        }

        public static void SaveGame()
        {
            IsHaveSave = true;

            BoxControllers.SaveGame();

            string jsonString = JsonUtility.ToJson(Instance.saveData);

            try
            {
                File.WriteAllText(Application.persistentDataPath + SCRO_MainData.PATH_SAVE, jsonString);
            }
            catch (Exception ex)
            {
                LogManager.LogError($"Error save game - {ex}");
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            //SaveGame();            
        }

        public static void DeleteAllSave(bool closeGame = true)
        {
            if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_SAVE))
            {
                File.Delete(Application.persistentDataPath + SCRO_MainData.PATH_SAVE);
            }

            if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_LOGS))
            {
                File.Delete(Application.persistentDataPath + SCRO_MainData.PATH_LOGS);
            }

            if (closeGame)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
            else
            {
                IsHaveSave = false;
                Instance.saveData = new SaveData();
            }

        }

        public static void AddLog(bool error, string message)
        {
            string strLoadJson = "";

            if (error)
            {
                message = "EROOR: " + message;
            }

            if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_LOGS))
            {
                strLoadJson = File.ReadAllText(Application.persistentDataPath + SCRO_MainData.PATH_LOGS);
                strLoadJson += $"\n{message}";
            }

            try
            {
                File.WriteAllText(Application.persistentDataPath + SCRO_MainData.PATH_LOGS, strLoadJson);
            }
            catch (Exception ex)
            {
                LogManager.LogError($"Error save logs - {ex}>");
            }
        }
    }
}