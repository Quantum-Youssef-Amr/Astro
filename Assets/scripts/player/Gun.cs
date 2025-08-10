using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float GunPower, Rate;
    [SerializeField] private AudioSource FireSound;
    private new_inputSystem inputs;
    private Coroutine fire;

    private void Awake()
    {
        inputs = new new_inputSystem();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void Update()
    {
        if (inputs.Player.Attack.IsInProgress())
        {
            if (fire == null)
                fire = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        FireSound.Play();
        GameObject bullet = Instantiate(this.bullet, transform.position, transform.parent.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.parent.rotation * Vector2.up * GunPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1 / Rate);
        fire = null;
    }

}
