using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("SceneLoader READY: " + gameObject.scene.name);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
{
    yield return ScreenFader.Instance.FadeOut();

    SceneManager.LoadScene(sceneName);

    yield return null;

   
    while (ScreenFader.Instance == null)
        yield return null;

    yield return ScreenFader.Instance.FadeIn();
}
}