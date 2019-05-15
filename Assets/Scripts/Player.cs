﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 0;
    public float ShootForce = 0;
    public GameObject Bullet;
    public AudioClip shotingSound;
    public AudioClip deathSound;
    private AudioSource audio;
    public int lives;


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
            transform.position += new Vector3(-MovementSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(MovementSpeed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio.PlayOneShot(shotingSound);
            GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0,0,0,0));
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ShootForce * Time.deltaTime);
        }
    }


}
