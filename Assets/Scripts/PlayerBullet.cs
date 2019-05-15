﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bunker")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
