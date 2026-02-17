using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    private float _new_health;
    void Start()
    {
        GameEventHandler.Instance.OnPlayerTakeDamage += newValue => StartCoroutine(UpdateSlider(newValue));
    }

    void OnDestroy()
    {
        GameEventHandler.Instance.OnPlayerTakeDamage -= newValue => StartCoroutine(UpdateSlider(newValue));
    }

    void OnDisable()
    {
        GameEventHandler.Instance.OnPlayerTakeDamage -= newValue => StartCoroutine(UpdateSlider(newValue));
    }

    private IEnumerator UpdateSlider(float newHealth)
    {
        _new_health = newHealth;
        yield return new WaitUntil(() =>
        {
            HealthSlider.value = Mathf.Lerp(HealthSlider.value, _new_health, 10 * Time.deltaTime);
            return Mathf.CeilToInt(HealthSlider.value) == Mathf.CeilToInt(_new_health);
        });
        HealthSlider.value = Mathf.CeilToInt(_new_health);
    }
}
