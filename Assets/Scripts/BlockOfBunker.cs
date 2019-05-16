using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfBunker : MonoBehaviour
{
    public ParticleSystem ExplosionEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            PlayEffect();
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "EnemyBullet")
        {
            PlayEffect();
        }
    }

    private void PlayEffect()
    {
        ParticleSystem particleEffect = Instantiate(ExplosionEffect, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(particleEffect.gameObject, particleEffect.duration);
    }



}
