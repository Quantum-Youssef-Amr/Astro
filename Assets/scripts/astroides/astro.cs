using UnityEngine;
using System.Collections;

public class astro : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f, ImpulseForce = 5f, selfDamageMultiplier = 0.5f, DistanceToDespawn = 20f;
    [SerializeField] private int numberOfAsteroids = 2;
    [SerializeField] private GameObject[] Smaller_astro;
    [SerializeField] private GameObject ParticleSystem;
    [SerializeField] private int stage;
    [SerializeField] private AudioClip DieAudio;
    private const float TimeToDie = 5f;

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
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                GameObject astro = Instantiate(Smaller_astro[Random.Range(0, Smaller_astro.Length)], transform.position, Quaternion.identity, transform.parent);

                astro.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * ImpulseForce, ForceMode2D.Impulse);
                astro.GetComponent<Rigidbody2D>().AddTorque((Random.value - 0.5f) * ImpulseForce, ForceMode2D.Impulse);
            }
        }
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayerSfx(DieAudio);

        GameEventHandler.Instance.OnAstroDestroy?.Invoke(stage);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().Damage(GetComponent<Rigidbody2D>().mass * selfDamageMultiplier);
        }
    }

    private Coroutine delete;
    private IEnumerator CheckPlayerDist()
    {
        if (Vector2.Distance(PlayerMovement.PlayerInstance.transform.position, transform.position) > DistanceToDespawn)
            delete ??= StartCoroutine(DeleteAfter(TimeToDie));

        else
            if (delete != null) StopCoroutine(delete);

        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckPlayerDist());
    }

    private IEnumerator DeleteAfter(float time)
    {
        yield return new WaitForSeconds(time);
        GameEventHandler.Instance.OnForceAstroDestroy?.Invoke();
        Destroy(gameObject);
    }
}
