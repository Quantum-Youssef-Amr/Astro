using UnityEngine;
using System.Collections;

public class Spowner : MonoBehaviour
{
    [SerializeField] private float SpownForce = 10f;
    [SerializeField] private GameObject[] Astroides;
    [SerializeField] private int Rate = 5, HardLimit = 100;

    private Camera _main;
    private int _spowned;

    void Start()
    {

        _main = Camera.main;
        GameEventHandler.OnAstroDestroy += CalcSpowned;
        GameEventHandler.OnForceAstroDestroy += ForceDeleteCalc;
        StartCoroutine(Spown());
    }

    void OnDisable()
    {
        GameEventHandler.OnAstroDestroy -= CalcSpowned;
        GameEventHandler.OnForceAstroDestroy -= ForceDeleteCalc;
    }

    void OnDestroy()
    {
        GameEventHandler.OnAstroDestroy -= CalcSpowned;
        GameEventHandler.OnForceAstroDestroy -= ForceDeleteCalc;
    }

    private void ForceDeleteCalc()
    {
        _spowned--;
    }

    private void CalcSpowned(int Astro_stage)
    {
        switch (Astro_stage)
        {
            case 0:
                _spowned--;
                break;

            case 1:
                _spowned--;
                _spowned += 2;
                break;

            case 2:
                _spowned--;
                _spowned += 2;
                break;
        }
    }

    private IEnumerator Spown()
    {
        if (_spowned < HardLimit)
        {
            Vector2 spownDir = Random.insideUnitCircle;
            spownDir.Normalize();

            GameObject astroide = Instantiate(Astroides[Random.Range(0, Astroides.Length)], (Vector2)PlayerMovement.PlayerInstance.transform.position + spownDir * (_main.orthographicSize * 2 + 5), Quaternion.identity, transform);
            astroide.GetComponent<Rigidbody2D>().AddForce(Random.value * SpownForce * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);
            astroide.GetComponent<Rigidbody2D>().AddTorque((Random.value - 0.5f) * SpownForce, ForceMode2D.Impulse);

            _spowned++;
        }
        yield return new WaitForSeconds(1f / Rate);
        StartCoroutine(Spown());
    }

}
