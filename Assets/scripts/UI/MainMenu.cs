using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    public void OnPlay()
    {
        GameEventHandler.OnEnterPlayScene?.Invoke();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
