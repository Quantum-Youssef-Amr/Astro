using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PersestantGameManager : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameEventHandler.OnEnterPlayScene += EnterPlayScene;
        GameEventHandler.OnEnterMainMenu += EnterMainMenu;
    }

    void OnDisable()
    {
        GameEventHandler.OnEnterPlayScene -= EnterPlayScene;
        GameEventHandler.OnEnterMainMenu -= EnterMainMenu;
    }

    void OnDestroy()
    {
        GameEventHandler.OnEnterPlayScene -= EnterPlayScene;
        GameEventHandler.OnEnterMainMenu -= EnterMainMenu;
    }

    private void EnterPlayScene()
    {
        TransationScene("Game");
    }

    private void EnterMainMenu()
    {
        TransationScene("MainMenu");
    }

    public void TransationScene(string ToScene)
    {
        Time.timeScale = 1f;
        GameEventHandler.CutConnections();
        SceneManager.LoadScene(ToScene);
    }
}
