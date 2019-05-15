using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MovementSpeed;
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
    }

    public void GoDown()
    {
        transform.position += new Vector3(0, -0.5f, 0);
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
