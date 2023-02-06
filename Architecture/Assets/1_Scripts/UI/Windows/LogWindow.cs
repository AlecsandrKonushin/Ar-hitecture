using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Logs
{
    public class LogWindow : MonoBehaviour
    {
        private const float TIME_SHOW_LOG = 3f;

        [SerializeField] private Text logText;
        [SerializeField] private Text logErrorText;

        public void Log(string text)
        {
            logText.text = text;

            DOTween.Sequence().AppendInterval(TIME_SHOW_LOG).OnComplete(() =>
            {
                logText.text = "";
            });
        }

        public void LogError(string text)
        {
            logErrorText.text = text;

            DOTween.Sequence().AppendInterval(TIME_SHOW_LOG).OnComplete(() =>
            {
                logErrorText.text = "";
            });
        }
    }
}