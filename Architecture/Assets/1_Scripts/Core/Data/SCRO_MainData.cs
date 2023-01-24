using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MainData", menuName = "Data/MainData")]
    public class SCRO_MainData : ScriptableObject
    {
        public const string PATH_SAVE = "/SaveGame.json";
        public const string PATH_LOGS = "/Logs.json";
        public const string PATH_LOCALIZATION_FILES = "Localization/";

        public float DEFAULT_MUSIC_VOLUME = 0.5f;
        public float DEFAULT_SOUND_VOLUME = 1f;
    }
}