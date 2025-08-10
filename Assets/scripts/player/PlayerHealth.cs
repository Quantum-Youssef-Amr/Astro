using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float Health;
    [SerializeField] private AudioSource DamageAudio, DieAudio;

    public void Damage(float damage)
    {
        DamageAudio.Play();
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
        GameEventHandler.OnPlayerTakeDamage?.Invoke(Health);
    }

    public void Die()
    {
        GameEventHandler.OnGameOver?.Invoke();
        DieAudio.Play();
    }
}
