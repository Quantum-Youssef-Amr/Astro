using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour
{
    [SerializeField] private float Damage = 10f;
    void Start()
    {
        StartCoroutine(DestoryAfter(3f));
    }

    private IEnumerator DestoryAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Astro"))
        {
            collision.gameObject.GetComponent<IDamageable>().Damage(Damage);
            Destroy(gameObject);
        }
    }
}
