using Core;
using Data;
using Gameplay;
using Logs;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Localization
{
    public class Localizator : Singleton<Localizator>
    {
        [SerializeField] private TypeLanguage language;

        private const string FILE_UI_TEXTS = "UI", FILE_TUTOR_TEXTS = "Tutorial", ZOMBIE_NAMES_TEXTS = "ZombieName";

        private bool isLoad;

        private static Dictionary<string, string[]> uiTexts = new Dictionary<string, string[]>();
        private static Dictionary<string, string[]> tutorTexts = new Dictionary<string, string[]>();
        private static Dictionary<string, string[]> zombieNamesTexts = new Dictionary<string, string[]>();

        public void Init()
        {

#if !UNITY_EDITOR
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            language = TypeLanguage.RU;
        }
        else
        {
            language = TypeLanguage.EN;
        }
#endif

            LoadData();

        }

        public void Init(TypeLanguage language)
        {
            this.language = language;

            LoadData();
        }

        public void ChangeLanguage(TypeLanguage language)
        {
            this.language = language;

            LoadSaveManager.Language = language;

            BoxControllers.GetController<EventsController>().ChangeLocalization();
        }

        #region GET_TEXT

        public static string GetTextUI(string idText)
        {
            return GetText(uiTexts, idText);
        }

        public static string GetTutorialText(string idText)
        {
            return GetText(tutorTexts, idText);
        }

        public static string GetNameZombieText(string idText)
        {
            return GetText(zombieNamesTexts, idText);
        }


        private static string GetText(Dictionary<string, string[]> dictionary, string idText)
        {
            string[] textsById = null;
            string needText = "";

            dictionary.TryGetValue(idText, out textsById);

            if (textsById == null)
            {
                LogManager.LogError($"Not have text in {dictionary} with id '{idText}' !");
            }
            else
            {
                needText = textsById[(int)Instance.language]; // Берём текст текущего языка
            }

            if (needText == "")
            {
                 LogManager.LogError($"Text {dictionary} with id '{idText}', language {0} empty!");
            }

            return needText;
        }

#endregion GET_TEXT

#region LOAD_DATA_TEXTS

        private void LoadData()
        {
            if (isLoad)
            {
                return;
            }

            LoadData(FILE_UI_TEXTS, uiTexts);
            LoadData(FILE_TUTOR_TEXTS, tutorTexts);
            LoadData(ZOMBIE_NAMES_TEXTS, zombieNamesTexts);

            isLoad = true;
        }

        private void LoadData(string fileName, Dictionary<string, string[]> dataDictionary)
        {
            TextAsset textAsset = Resources.Load(SCRO_MainData.PATH_LOCALIZATION_FILES + fileName.ToString()) as TextAsset;

            if (textAsset == null)
            {
                LogManager.LogError($"Not have txt file {fileName} in resources!");
            }
            

            List<string> splitText = new List<string>(textAsset.text.Split('\n'));

            for (int i = 0; i < splitText.Count; i++)
            {
                List<string> addTexts = new List<string>(splitText[i].Split(';'));
                string[] textLanguages = new string[2] { addTexts[1], addTexts[2] };

                dataDictionary.Add(addTexts[0], textLanguages);
            }
        }

#endregion LOAD_DATA_TEXTS
    }

    public enum TypeLanguage
    {
        RU,
        EN
    }
}