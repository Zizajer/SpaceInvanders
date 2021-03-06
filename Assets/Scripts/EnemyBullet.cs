﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public ParticleSystem ExplosionEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            PlayEffect();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.tag == "Bunker")
        {
            PlayEffect();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void PlayEffect()
    {
        ExplosionEffect.startColor = GetComponent<SpriteRenderer>().color;
        ParticleSystem particleEffect = Instantiate(ExplosionEffect, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(particleEffect.gameObject, particleEffect.duration);
    }
}
