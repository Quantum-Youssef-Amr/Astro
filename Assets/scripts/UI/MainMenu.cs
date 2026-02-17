using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 120;
    }

    public void OnPlay()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("MainMenu", "Game", 2f, true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
