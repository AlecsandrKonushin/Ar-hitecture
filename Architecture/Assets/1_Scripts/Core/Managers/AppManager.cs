using Logs;
using NaughtyAttributes;
using Test;
using UnityEngine;

namespace Core
{
    public class AppManager : Singleton<AppManager>
    {
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isNeedLog;
        [BoxGroup("Parameters app")]
        [SerializeField] private bool analyticIsActive;
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isTestBuild;
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isNeedTutorial;
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isTestAd;

        public static bool IsTutorNow { get; private set; }

        private void Start()
        {
            LogManager.Instance.SetIsNeedLog = isNeedLog;
            TestManager.Instance.Init(isTestBuild);

            LoadSaveManager.AddLog(false, "");
            LoadSaveManager.AddLog(false, "");
            LoadSaveManager.AddLog(false, "NEW START"); ;

            // TODO: Init InApps

            AdManager.Instance.InitEvent.AddListener(AfterInitAd);
            AdManager.Instance.Init(isTestAd);
        }

        private void AfterInitAd()
        {
            AdManager.Instance.InitEvent.RemoveListener(AfterInitAd);

            LoadSaveManager.OnLoad.AddListener(AfterLoadData);
            LoadSaveManager.LoadData();
        }

        private void AfterLoadData()
        {
            LoadSaveManager.OnLoad.RemoveListener(AfterLoadData);

#if !UNITY_EDITOR

            if (analyticIsActive)
            {
                FirebaseManager.Instance.OnInit.AddListener(() =>
                {
                    FirebaseManager.Instance.OnInit.RemoveAllListeners();
                    LoadData();
                });

               FirebaseManager.Instance.Init();
            }
            else
            {
                LoadData();
            }
#else

            LoadData();

#endif
        }

        private void LoadData()
        {
            Debug.Log($"LoadData");

            //if (isNeedTutorial)
            //{
            //    if (LoadSaveManager.StepTutorial != StepTutorial.Finished)
            //    {
            //        LoadSaveManager.DeleteAllSave(false);
            //    }

            //    IsTutorNow = true;
            //}
            //else
            //{
            //    IsTutorNow = false;
            //}

            AudioManager.Instance.ChangeMusicVolume(LoadSaveManager.MusicVolume);
            AudioManager.Instance.ChangeSoundVolume(LoadSaveManager.SoundsVolume);

            SceneControllers.Instance.InitControllers();

            //LoadSceneManager.Instance.LoadGameScene();            
        }
    }
}