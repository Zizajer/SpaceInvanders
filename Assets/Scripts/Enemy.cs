using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip OnDeathSound;
    public GameObject Bullet;
    public float ShootForce;
    public float shootProbability;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
        Invoke("Shoot", 1f);
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

    void Shoot()
    {
        float randomNumber = Random.Range(0f,1f);
        if (randomNumber < shootProbability)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0));
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.down * ShootForce * Time.deltaTime);
        }
        Invoke("Shoot", 1f);
    }
}
