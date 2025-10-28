using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{

    [SerializeField]
    int sceneIndex;

    public Slider loadingBar;

    void Start()
    {
        if (loadingBar)
            LoadMainScene();
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
        if (loadingBar)
            loadingBar.value = 0;
    }
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            if (loadingBar)
                loadingBar.value = asyncLoad.progress;

            yield return new WaitForEndOfFrame();
        }
        
    }
}
