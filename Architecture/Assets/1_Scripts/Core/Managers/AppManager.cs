using Logs;
using NaughtyAttributes;
using UI;
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

        private void Start()
        {
            LogManager.Instance.SetIsNeedLog = isNeedLog;

            LoadSaveManager.AddLog(false, "");
            LoadSaveManager.AddLog(false, "");
            LoadSaveManager.AddLog(false, "NEW START"); ;

            // TODO: Init InApps

            // TODO: Init ad

            LoadSaveManager.OnLoad.AddListener(AfterLoadData);
            LoadSaveManager.LoadData();
        }

        private void AfterLoadData()
        {
            LoadSceneManager.Instance.LoadGameScene();            
        }
    }
}