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
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public ParticleSystem ExplosionEffect;
    private float scoreAmountIncrease;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
        scoreAmountIncrease = 50;
        Invoke("Shoot", 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            ExplosionEffect.startColor = spriteRenderer.color;
            ParticleSystem particleEffect = Instantiate(ExplosionEffect, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(particleEffect.gameObject, particleEffect.duration);
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().IncreaseScore(scoreAmountIncrease,transform.position,spriteRenderer.color);
            GetComponentInParent<EnemyManager>().SpeedUpGame();
            audio.PlayOneShot(OnDeathSound);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Bunker")
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().TakePlayerLife(100);
        }
    }
    

    void Shoot()
    {
        float randomNumber = Random.Range(0f,1f);
        if (randomNumber < shootProbability)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0));
            bullet.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.down * ShootForce * Time.deltaTime);
        }
        Invoke("Shoot", 1f);
    }


}
