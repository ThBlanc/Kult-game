using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public string sceneToLoad;

    public void OnPlay()
    {
        if (null == sceneToLoad)
            return;

        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
