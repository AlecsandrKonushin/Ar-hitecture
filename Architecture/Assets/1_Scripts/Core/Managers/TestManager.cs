using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestManager : Singleton<TestManager>
    {
        [SerializeField] private Text errorText;
        [SerializeField] private GameObject panelTest;
        [SerializeField] private Button openPanelButton, closePanelButton;

        public void Init(bool isTest)
        {
            if (isTest)
            {
                openPanelButton.onClick.AddListener(() =>
                {
                    panelTest.SetActive(true);
                });

                closePanelButton.onClick.AddListener(() =>
                {
                    panelTest.SetActive(false);
                });
            }
        }

        public void ShowError(string error)
        {
            errorText.text = error;
        }

        public void RestartScene()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}