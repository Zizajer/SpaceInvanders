using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip OnDeathSound;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            GetComponentInParent<EnemyManager>().SpeedUpGame();
            audio.PlayOneShot(OnDeathSound);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
