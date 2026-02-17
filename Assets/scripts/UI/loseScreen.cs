using System;
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
        GameEventHandler.Instance.OnGameOver += ShowGameOverScreen;
        GameEventHandler.Instance.OnScoreChanged += CalcScore;
    }

    void OnDisable()
    {
        GameEventHandler.Instance.OnGameOver -= ShowGameOverScreen;
        GameEventHandler.Instance.OnScoreChanged -= CalcScore;
    }

    void OnDestroy()
    {
        GameEventHandler.Instance.OnGameOver -= ShowGameOverScreen;
        GameEventHandler.Instance.OnScoreChanged -= CalcScore;
    }

    private void CalcScore(int newScore)
    {
        _score = newScore;
    }

    private void ShowGameOverScreen()
    {
        int m_prev_score = Math.Max(SaveEngine.Instance.Data.HighestScore, _score);

        StartCoroutine(ShowLoseScreen());

        AfterLoseScoreUI.text = $"Highest score: {m_prev_score}";

        SaveEngine.Instance.Data.HighestScore = m_prev_score;
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
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        GameSceneManager.Instance.TransitionWithReplaceScene("Game", "MainMenu", 2f, true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GameSceneManager.Instance.TransitionWithReplaceScene("Game", "Game", 2f, true);
    }
}
