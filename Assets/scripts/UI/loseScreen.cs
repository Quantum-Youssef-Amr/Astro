using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private float EaseSpeed = 5f;
    [SerializeField] private TextMeshProUGUI AfterLoseScoreUI;
    private int _score;
    void Start()
    {
        GameEventHandler.OnGameOver += ShowGameOverScreen;
        GameEventHandler.OnScoreChanged += CalcScore;
    }

    void OnDisable()
    {
        GameEventHandler.OnGameOver -= ShowGameOverScreen;
        GameEventHandler.OnScoreChanged -= CalcScore;
    }

    void OnDestroy()
    {
        GameEventHandler.OnGameOver -= ShowGameOverScreen;
        GameEventHandler.OnScoreChanged -= CalcScore;
    }

    private void CalcScore(int newScore)
    {
        _score = newScore;
    }

    private readonly string SaveKey = "Score";
    private void ShowGameOverScreen()
    {
        int m_prev_score;
        if (PlayerPrefs.HasKey(SaveKey))
        {
            m_prev_score = PlayerPrefs.GetInt(SaveKey);
            if (_score > m_prev_score)
            {
                PlayerPrefs.SetInt(SaveKey, _score);
                m_prev_score = _score;
            }
        }
        else
        {
            PlayerPrefs.SetInt(SaveKey, _score);
            m_prev_score = _score;
        }

        StartCoroutine(ShowLoseScreen());
        AfterLoseScoreUI.text = $"Highest score: {m_prev_score}";
    }

    private IEnumerator ShowLoseScreen()
    {
        LoseScreen.gameObject.SetActive(true);
        yield return new WaitUntil(() =>
        {
            LoseScreen.alpha = Mathf.Lerp(LoseScreen.alpha, 1, EaseSpeed * Time.deltaTime);
            return LoseScreen.alpha > 0.95f;
        });
        LoseScreen.alpha = 1f;

        yield return new WaitUntil(() =>
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, EaseSpeed * Time.deltaTime);
            return Time.timeScale < 0.05f;
        });
        Time.timeScale = 0;
    }

    public void GoMainMenu()
    {
        goToScene("MainMenu");
    }

    public void Restart()
    {
        goToScene("Game");
    }

    private void goToScene(string scene)
    {
        if (SceneManager.GetSceneByName(scene) == null)
        {
            print("no scene with this name");
            return;
        }

        Time.timeScale = 1;
        GameEventHandler.CutConnections();
        SceneManager.LoadScene(scene);
    }
}
