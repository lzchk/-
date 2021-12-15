using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : Entity
{
    private void Start()
    {
        lives = 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject==Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("у сабаки " + lives + " жизняей");
        }

        if (lives < 1)
            Die();
    }
}
