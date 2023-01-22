using Data;
using Logs;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class Localizator : Singleton<Localizator>
    {
        [SerializeField] private TypeLanguage language;

        private const string FILE_UI_TEXTS = "UI";

        private bool isLoad;

        private Dictionary<string, string[]> uiTexts = new Dictionary<string, string[]>();

        protected override void AfterAwaik()
        {
            LoadData();
        }

        #region GET_TEXT

        public string GetTextUI(string idText)
        {
            return GetText(uiTexts, idText);
        }

        private string GetText(Dictionary<string, string[]> dictionary, string idText)
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
                needText = textsById[(int)language]; // Берём текст текущего языка
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

            isLoad = true;
        }

        private void LoadData(string fileName, Dictionary<string, string[]> dataDictionary)
        {
            TextAsset textAsset = Resources.Load(MainData.PATH_LOCALIZATION_FILES + fileName.ToString()) as TextAsset;

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