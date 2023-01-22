using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Slider loadSlider;

    private AsyncOperation operation;

    private TypeScene currentScene = TypeScene.Main;

    public bool IsMainSceneNow { get => currentScene == TypeScene.Main; }
    public bool IsGameSceneNow { get => currentScene == TypeScene.Game; }

    private bool sceneIsLoad = false, sliderIsLoad = false;

    public void LoadMainMenu()
    {
        StartCoroutine(CoLoadScene(0));
        currentScene = TypeScene.Main;
    }

    public void LoadGameScene()
    {
        StartCoroutine(CoLoadScene(1));
        currentScene = TypeScene.Game;
    }

    private IEnumerator CoLoadScene(int number)
    {
        loaderCanvas.SetActive(true);

        loadSlider.transform.DOLocalMove(new Vector3(0, loadSlider.transform.localPosition.y + 400, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        loadSlider.DOValue(1, 0.4f).OnComplete(()=>
        {
            sliderIsLoad = true;

            EndLoadScene();
        });

        operation = SceneManager.LoadSceneAsync(number);

        yield return operation;

        sceneIsLoad = true;
        operation = null;
        loadSlider.value = 1;

       EndLoadScene();
    }

    private void EndLoadScene()
    {
        if (sceneIsLoad && sliderIsLoad)
        {
            sceneIsLoad = false;
            sliderIsLoad = false;

            loadSlider.transform.DOLocalMove(new Vector3(0, loadSlider.transform.localPosition.y - 400, 0), 0.4f).OnComplete(() =>
            {
                loaderCanvas.SetActive(false);

                SceneControllers.Instance.InitControllers();
            });
        }  
    }
}

public enum TypeScene
{
    Main,
    Game
}
