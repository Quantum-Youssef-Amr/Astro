using UnityEngine;
using System.Collections;

public class astro : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f, ImpulseForce = 5f, selfDamageMuiltplyer = 0.5f, DistanceToDespown = 20f;
    [SerializeField] private int numberOfAstroides = 2;
    [SerializeField] private GameObject[] Smallar_astro;
    [SerializeField] private GameObject ParticleSystem;
    [SerializeField] private int stage;
    [SerializeField] private AudioSource DieAudio;

    void Start()
    {
        StartCoroutine(CheckPlayerDist());
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (stage != 0)
        {
            for (int i = 0; i < numberOfAstroides; i++)
            {
                GameObject astro = Instantiate(Smallar_astro[Random.Range(0, Smallar_astro.Length)], transform.position, Quaternion.identity, transform.parent);
                astro.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * ImpulseForce, ForceMode2D.Impulse);
                astro.GetComponent<Rigidbody2D>().AddTorque((Random.value - 0.5f) * ImpulseForce, ForceMode2D.Impulse);
            }
        }
        GameObject pr = Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        pr.GetComponent<AudioSource>().Play();
        GameEventHandler.OnAstroDestroy?.Invoke(stage);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().Damage(GetComponent<Rigidbody2D>().mass * selfDamageMuiltplyer);
        }
    }

    private Coroutine delete;
    private IEnumerator CheckPlayerDist()
    {
        if (Vector2.Distance(PlayerMovement.PlayerInstance.transform.position, transform.position) > DistanceToDespown)
        {
            delete ??= StartCoroutine(DeleteAfter(5f));
        }
        else
        {
            if (delete != null) StopCoroutine(delete);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckPlayerDist());
    }

    private IEnumerator DeleteAfter(float time)
    {
        yield return new WaitForSeconds(time);
        GameEventHandler.OnForceAstroDestroy?.Invoke();
        Destroy(gameObject);
    }
}
