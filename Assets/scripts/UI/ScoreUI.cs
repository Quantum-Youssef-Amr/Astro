using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private float StageScoreMultiplier = 20f;
    [SerializeField] private TextMeshProUGUI ScoreUiText;
    private int _score;
    private int _new_score;

    void Start() => GameEventHandler.Instance.OnAstroDestroy += stage => StartCoroutine(AddScore(stage));

    private IEnumerator AddScore(int stage)
    {
        _new_score = _score + Mathf.CeilToInt((stage + 1) * StageScoreMultiplier);

        yield return new WaitUntil(() =>
        {
            UpdateScore(Mathf.CeilToInt(Mathf.Lerp(_score, _new_score, Time.deltaTime * 10)));
            return _score >= _new_score;
        });

        // ensure that the _score is the new one
        UpdateScore(_new_score);
        GameEventHandler.Instance.OnScoreChanged?.Invoke(_score);
    }

    private void UpdateScore(int newScore)
    {
        _score = newScore;
        ScoreUiText.text = DecorateScore(_score);
    }

    private string DecorateScore(int score)
    {
        return score < 10 ? $"00{score}" : (score < 100 ? $"0{score}" : $"{score}");
    }
}
