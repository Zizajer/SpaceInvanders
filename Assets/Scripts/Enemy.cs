using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MovementSpeed;
    public Transform boundaryLeft;
    public Transform boundaryRight;
    public AudioClip OnDeathSound;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x < boundaryLeft.position.x || transform.position.x > boundaryRight.position.x)
        {
            GetComponentInParent<EnemyManager>().SpeedUpGame();
        }
    }

    public void GoDown(float step)
    {
        transform.position += new Vector3(0, step, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            audio.PlayOneShot(OnDeathSound);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
