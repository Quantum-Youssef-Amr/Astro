using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private float StageScoreMuiltplyer = 20f;
    [SerializeField] private TextMeshProUGUI ScoreUiText;
    private int _score;
    private int _new_score;

    void Start()
    {
        GameEventHandler.OnAstroDestroy += stage => StartCoroutine(AddScore(stage));
    }

    private IEnumerator AddScore(int stage)
    {
        _new_score = _score + Mathf.CeilToInt((stage + 1) * StageScoreMuiltplyer);
        yield return new WaitUntil(() =>
        {
            _score = Mathf.CeilToInt(Mathf.Lerp(_score, _new_score, Time.deltaTime * 10));
            ScoreUiText.text = $"{_score}";
            return _score == _new_score;
        });
        GameEventHandler.OnScoreChanged?.Invoke(_score);
    }
}
