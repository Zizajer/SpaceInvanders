using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    public float MovementSpeed;
    public ParticleSystem ExplosionEffect;
    public SpriteRenderer spriteRenderer;
    public float ScoreAmountIncrease;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            ExplosionEffect.startColor = spriteRenderer.color;
            ParticleSystem particleEffect = Instantiate(ExplosionEffect, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(particleEffect.gameObject, particleEffect.duration);
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().IncreaseScore(ScoreAmountIncrease);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
