using System.IO;
using Data;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace EditorTools
{
    public class MenuTools
    {
        [MenuItem("Tools/Delete Save")]
        private static void DeleteSave()
        {
            if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_LOGS))
            {
                File.Delete(Application.persistentDataPath + SCRO_MainData.PATH_LOGS);
            }

            if (File.Exists(Application.persistentDataPath + SCRO_MainData.PATH_SAVE))
            {
                File.Delete(Application.persistentDataPath + SCRO_MainData.PATH_SAVE);
            }
        }
    }
}

#endif