using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 0;
    public float ShootForce = 0;
    public GameObject Bullet;
    public Transform boundaryLeft;
    public Transform boundaryRight;
    public AudioClip shotingSound;
    public AudioClip deathSound;
    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!(transform.position.x < boundaryLeft.position.x))
            {
                transform.position += new Vector3(-MovementSpeed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!(transform.position.x > boundaryRight.position.x))
            {
                transform.position += new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio.PlayOneShot(shotingSound);
            GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0,0,0,0));
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ShootForce * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            audio.PlayOneShot(deathSound);
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().TakePlayerLife(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().TakePlayerLife(100);
        }
    }


}
